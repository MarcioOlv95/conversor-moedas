using conversor_moedas.domain.Enums;

namespace conversor_moedas.domain.Errors
{
    public class Error
    {
        public static readonly Error None = new(ErrorType.None, string.Empty);

        public Error(ErrorType code, string message)
        {
            Code = code.ToString();
            Message = message;
        }

        public string Code { get; }
        public string Message { get; }

    }
}
