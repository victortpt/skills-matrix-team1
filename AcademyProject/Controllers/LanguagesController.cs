using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Academy.Core.Domains;
using Academy.Core.Models;
using Academy.Core.Domains.Interfaces;
using Academy.Core.DTO;
using System;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;


namespace Academy.Api.Controllers

{
    [Route("api/languages")]
    [ApiController]
    public class LanguagesController : Controller
    {
        private readonly ILogger _logger;
        private readonly ILanguagesDomain _languagesDomain;
        public LanguagesController(ILogger<LanguagesController> logger, ILanguagesDomain languagesDomain)
        {
            _logger = logger;
            _languagesDomain = languagesDomain;
        }
        
        /// <summary>
        /// Returns the languages table data in a JSON format
        /// Retrieved data: id_language, language, language_code
        /// </summary>
        /// <returns>GET the language's list</returns>
        /// <response code="200">Successful Operations. Returns the language's list</response>
        /// <response code="500">If an error occurs returning the language's list</response>    
        [HttpGet]
        public IActionResult Get()
        {
            IActionResult response = null;
            try {
                using (_logger.BeginScope("Message {HoleValue}", DateTime.Now))
                {
                    _logger.LogInformation("Listing all languages");
                }
           
                  response = File(Encoding.UTF8.GetBytes(_languagesDomain.GetLanguages()), "application/json");
            } catch(Exception defaultException) {
                using (_logger.BeginScope("Error at {HoleValue}", DateTime.Now))
                {
                    _logger.LogError(defaultException.Message);
                }
                response = StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return response;
        }

        /// <summary>
        /// Get an specific Language by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>GET a language by param id</returns>
        /// <response code="200">Successful Operations. Returns the language with the param id</response>
        /// <response code="500">If an error occurs listing an language</response>   
        [ApiExplorerSettings(IgnoreApi = true)]  
        [HttpGet]
        [Route("{id?}")]
        public IActionResult Get(int id)
        {
            try {
                using (_logger.BeginScope("Message {HoleValue}", DateTime.Now))
                {
                    _logger.LogInformation("Listing a language");
                }
                Language language = new Language{ };
                language = _languagesDomain.Get(id);
                return Ok(language);
            } catch (Exception defaultException){
                using (_logger.BeginScope("Error at {HoleValue}", DateTime.Now))
                {
                    _logger.LogError(defaultException.Message);
                }
                return StatusCode(500);
            }
        }

        
    }
}