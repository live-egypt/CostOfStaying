namespace CostOfStaying.Core.Services
{
    using System;
    using CostOfStaying;
    using CostOfStaying.Core.Contracts;
    using CostOfStaying.Core.Domain;

    /// <summary>
    /// Adds an explicit payload validation boundary around match command authorization.
    /// The application must supply a validator; there is no permissive default.
    ///
    /// Validation runs before authorization/queue admission so malformed commands do not
    /// consume queue capacity or sequence state. It runs again after dequeue so mutable
    /// reference payloads cannot be changed while queued and bypass validation.
    ///
    /// Validators must be bounded, synchronous, deterministic from the payload,
    /// side-effect-free and free of I/O. They must not mutate match/player/queue state.
    /// Transport adapters must still enforce raw frame-size and serializer quotas before
    /// constructing TCommand; this service receives an already-deserialized typed value.
    /// Single-writer/server-serialized; not thread-safe.
    /// </summary>
    public sealed class MatchCommandPayloadValidationService<TCommand> : IMatchCommandPayloadValidationService<TCommand>
    {
        private readonly IMatchCommandAuthorizationService<TCommand> _authorization;
        private readonly ICommandPayloadValidator<TCommand> _validator;

        internal MatchCommandPayloadValidationService(
            IMatchCommandAuthorizationService<TCommand> authorization,
            ICommandPayloadValidator<TCommand> validator)
        {
            _authorization = authorization ?? throw new ArgumentNullException(nameof(authorization));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            MatchId = _authorization.MatchId;
            if (!MatchId.IsValid)
                throw new ArgumentException("Authorization service must have a valid MatchId.", nameof(authorization));
        }

        public MatchId MatchId { get; }

        /// <summary>
        /// Validates the deserialized payload before session/authorization admission.
        /// Failure leaves queue capacity and sequence/replay state untouched.
        /// </summary>
        public Result TryAdmit(CommandEnvelope<TCommand> envelope)
        {
            if (!envelope.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            Result validation = ValidatePayload(envelope.Command);
            if (validation.IsFailure)
                return validation;

            return _authorization.TryAdmit(envelope);
        }

        /// <summary>
        /// Revalidates the payload after authorization dequeues it and before a caller
        /// may hand the command to its gameplay handler. On failure the command is discarded;
        /// its accepted sequence remains consumed.
        /// </summary>
        public Result<AuthenticatedCommand<TCommand>> TryTakeNext()
        {
            Result<AuthenticatedCommand<TCommand>> admitted = _authorization.TryTakeNext();
            if (admitted.IsFailure)
                return admitted;

            Result validation = ValidatePayload(admitted.Value.Command);
            if (validation.IsFailure)
                return Result<AuthenticatedCommand<TCommand>>.Failure(validation.ErrorCode);

            return admitted;
        }

        private Result ValidatePayload(TCommand command)
        {
            Result result;
            try
            {
                result = _validator.Validate(command);
            }
            catch (Exception)
            {
                return Result.Failure(CoreErrorCode.Unavailable);
            }

            if (result.IsSuccess)
                return Result.Success();

            return result.ErrorCode == CoreErrorCode.Uninitialized
                ? Result.Failure(CoreErrorCode.InvalidState)
                : result;
        }
    }
}
