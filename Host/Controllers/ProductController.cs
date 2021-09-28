using Contract.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Host.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("trendyol-link-converter-api")]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> logger;

        public ProductController(ILogger<ProductController> logger)
        {
            this.logger = logger;
        }

        [Route("deep-link")]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult ConvertDeepLink([FromBody] DeepLinkRequest request)
        {
            logger.LogInformation(request.link);

            return new JsonResult(new { link = @"ty://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=105064" });
        }
    }
}