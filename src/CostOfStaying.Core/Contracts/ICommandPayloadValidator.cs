namespace CostOfStaying.Core.Contracts
{
    using CostOfStaying;

    /// <summary>
    /// Application-supplied, bounded, synchronous and side-effect-free validation
    /// of a deserialized typed command payload. It validates payload shape/ranges only;
    /// dynamic match phase and player permissions belong to authorization policy.
    /// </summary>
    public interface ICommandPayloadValidator<TCommand>
    {
        Result Validate(TCommand command);
    }
}
