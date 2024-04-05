using Microsoft.AspNetCore.Mvc;
using MediatR;
using conversor_moedas.api.application.Currency.Messaging.Requests;
using conversor_moedas.api.application.Currency.Messaging.Responses;
using conversor_moedas.api.application.Common.Notifier;
using conversor_moedas.domain.Shared;
using conversor_moedas.api.application.Common.Pagination;

namespace conversor_moedas.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        protected readonly ISender _sender;
        protected readonly INotifier _notifier;

        public CurrencyController(ISender sender, INotifier notifier)
        {
            _sender = sender;
            _notifier = notifier;
        }

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(CurrencyResponse), StatusCodes.Status200OK)]
        public async Task<Result<PagedResult<CurrencyResponse>>> GetAll([FromQuery] int? page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        {
            var request = new GetCurrencyRequest(page, pageSize);
            return await _sender.Send(request, cancellationToken);
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<Result> Register([FromBody] RegisterCurrencyRequest request)
        {
            return await _sender.Send(request);
        }

        [HttpGet("convert-currency")]
        [ProducesResponseType(typeof(ConvertCurrencyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<Result<ConvertCurrencyResponse>> ConvertCurrency([FromQuery]string from, [FromQuery]string to, [FromQuery]double amount)
        {
            return await _sender.Send(new ConvertCurrencyRequest(from, to, amount));
        }
    }
}
