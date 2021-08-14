namespace Weekly.Data
{
    public class Result
    {
        public bool Succeeded { get; set; }

        public bool Failed => !Succeeded;

        public string ErrorMessage { get; set; }

        public static Result Success()
        {
            return new Result { Succeeded = true };
        }

        public static Result<TValue> Success<TValue>(TValue value)
        {
            return new Result<TValue> { Succeeded = true, Value = value };
        }

        public static Result Failure(string errorMessage)
        {
            return new Result { ErrorMessage = errorMessage };
        }

        public static Result Failure<TValue>(string errorMessage)
        {
            return new Result<TValue> { ErrorMessage = errorMessage };
        }
    }

    public class Result<TValue> : Result
    {
        public TValue Value { get; set; }
    }
}
