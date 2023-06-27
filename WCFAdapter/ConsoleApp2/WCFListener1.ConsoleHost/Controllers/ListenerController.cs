using Microsoft.AspNetCore.Mvc;
using SBService;
using System;
using System.Collections.Generic;
using System.Text;

namespace WCFListener1.Controllers
{
    [Route("Listener")]
    [ApiController]
    public class ListenerController: ControllerBase
    {
        [HttpPost("[action]")]
        [ActionName("AcceptMessage")]
        public AcceptMessageResponse AcceptMessage([FromBody] AcceptMessageRequest request)
        {
            Console.WriteLine(request.Body);

            return new AcceptMessageResponse();
        }

        [HttpGet("[action]")]
        [ActionName("GetSourceId")]
        public string GetSourceId()
        {
            return Program.listenerStartup.Configuration["SourceId"];
        }

        [HttpPost("[action]")]
        [ActionName("RiseEvent")]
        public void RiseEvent([FromBody] string ИдТипаСобытия)
        {
            Console.WriteLine(ИдТипаСобытия);
        }
    }
}
