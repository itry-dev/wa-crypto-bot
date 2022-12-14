using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WA.Core.ErrorCommands;
using WA.Core.Features.OnWAMessageReceived;
using WA.Core.Features.OnWebhookVerification;

namespace WA.Api.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [Route("v1.0/[controller]")]
    public class WAWebHookController : ControllerBase
    {

        private ILogger _logger;
        private readonly IMediator _mediator;

        public WAWebHookController(IMediator mediator, ILoggerFactory loggerFactory)
        {
            _mediator = mediator;
            _logger = loggerFactory.CreateLogger<WAWebHookController>();
        }

        /// <summary>
        /// WhatsApp makes a GET call for webhook verification process
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string WAWebHookVerification()
        {
            var result = _mediator.Send(new OnWebhookVerificationCommand(Request)).GetAwaiter().GetResult();

            return result;
        }

        /// <summary>
        /// WhatsApp makes a POST call for every subscribed event by user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> WAEventFired()
        {
            try
            {
                var inputMessage = await Request.ReadFromJsonAsync<WA.Core.InputModels.WA.Root>();

                await _mediator.Send(new OnWAMessageReceivedCommand(inputMessage));

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"WhatsApp input message not parsed: {e.Message}");

                await _mediator.Send(new InternalServerErrorCommand(e.Message));
            }

            return Ok();
        }

    }

}

