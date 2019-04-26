using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace wtflog.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
    	private readonly ILogger _logger;

		public HomeController(ILogger<HomeController> logger)
    	{
        	
			_logger = logger;
    	}   
    
        [HttpGet]
        public string Index()
        {
            //Log.Information("In the controller!");
            _logger.LogInformation("******** Controller LogInformation");
            _logger.LogWarning("IIIIIIIIIIIII Controller LogWarning");
            _logger.LogDebug("*********** Controller   LogDebug");
            //_logger.LogFatal("IIIIIIIIIIIII Controller   LogFatal");
            _logger.LogError("********* Controller   LogError");
            //_logger.LogVerbose("IIIIIIIIIIIII Controller Verbose");

            return "View();";
        }
    }
}