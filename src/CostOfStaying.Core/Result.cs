namespace CostOfStaying
{
    using System;

    /// <summary>Stable machine-readable failure categories shared by Core application contracts.</summary>
    public enum CoreErrorCode
    {
        None = 0,
        Uninitialized = 1,
        InvalidArgument = 2,
        InvalidState = 3,
        NotFound = 4,
        Conflict = 5,
        CapacityExceeded = 6,
        OutOfRange = 7,
        Unauthorized = 8,
        Unavailable = 9
    }

    /// <summary>Allocation-free success/failure result for operations without a return value.</summary>
    public readonly struct Result
    {
        private readonly byte _state;
        private readonly CoreErrorCode _errorCode;

        private Result(bool isSuccess, CoreErrorCode errorCode)
        {
            _state = isSuccess ? (byte)1 : (byte)0;
            _errorCode = errorCode;
        }

        public bool IsSuccess => _state == 1;
        public bool IsFailure => !IsSuccess;
        public CoreErrorCode ErrorCode =>
            IsSuccess ? CoreErrorCode.None :
            _errorCode == CoreErrorCode.None ? CoreErrorCode.Uninitialized : _errorCode;

        public static Result Success() => new Result(true, CoreErrorCode.None);

        public static Result Failure(CoreErrorCode errorCode)
        {
            if (errorCode == CoreErrorCode.None)
                throw new ArgumentOutOfRangeException(nameof(errorCode), "Failure requires a non-success error code.");

            return new Result(false, errorCode);
        }
    }

    /// <summary>Allocation-free success/failure result carrying a non-null value on success.</summary>
    public readonly struct Result<T>
    {
        private readonly byte _state;
        private readonly T? _value;
        private readonly CoreErrorCode _errorCode;

        private Result(T value)
        {
            _state = 1;
            _value = value;
            _errorCode = CoreErrorCode.None;
        }

        private Result(CoreErrorCode errorCode)
        {
            _state = 0;
            _value = default;
            _errorCode = errorCode;
        }

        public bool IsSuccess => _state == 1;
        public bool IsFailure => !IsSuccess;
        public CoreErrorCode ErrorCode =>
            IsSuccess ? CoreErrorCode.None :
            _errorCode == CoreErrorCode.None ? CoreErrorCode.Uninitialized : _errorCode;

        public T Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException("A failed Core result has no value. Error: " + ErrorCode + ".");

                return _value!;
            }
        }

        public static Result<T> Success(T value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value), "A successful result must carry a non-null value.");

            return new Result<T>(value);
        }

        public static Result<T> Failure(CoreErrorCode errorCode)
        {
            if (errorCode == CoreErrorCode.None)
                throw new ArgumentOutOfRangeException(nameof(errorCode), "Failure requires a non-success error code.");

            return new Result<T>(errorCode);
        }
    }
}
