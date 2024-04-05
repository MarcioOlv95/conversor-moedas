namespace conversor_moedas.domain.Shared
{
    public class Result
    {
        public Result() { }

        public Result(bool isSuccess, List<string>? erros = null)
        {
            IsSuccess = isSuccess;
            Errors = erros;
        }

        public Result(bool isSuccess, string erro)
        {
            IsSuccess = isSuccess;
            Errors.Add(erro);
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess || Errors.Any();

        public List<string>? Errors { get; set; } = new List<string>();
        public static Result Success() => new(true);
        public static Result Failure(string erro) => new(false, erro);
        public static Result Failure(List<string> erro) => new(false, erro);
        public static Result<TValue> Failure<TValue>(List<string> erro) => new(default, false, erro);
        public static Result<TValue> Failure<TValue>(string erro) => new(default, false, erro);
        protected static Result<TValue> Create<TValue>(TValue? value) => new(value, true);
    }

    public class Result<TValue> : Result 
    { 
        private readonly TValue? _value;

        protected internal Result(TValue? value, bool isSuccess, string? error)
            : base(isSuccess, error) 
            => _value = value;

        protected internal Result(TValue? value, bool isSuccess, List<string>? error)
            : base(isSuccess, error)
            => _value = value;

        protected internal Result(TValue? value, bool isSuccess)
            : base(isSuccess, string.Empty)
            => _value = value;

        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("The value of a failure result can not be accessed.");

        public static implicit operator Result<TValue>(TValue? value) => Create(value);
    }

}
