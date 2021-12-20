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
    [Route("api/languageproficiencies")]
    [ApiController]
    public class LanguageLevelController : Controller
    {
        private readonly ILogger _logger;
        private readonly ILanguageLevelDomain _languageLevelDomain;
        public LanguageLevelController(ILogger<LanguageLevelController> logger, ILanguageLevelDomain languageLevelDomain)
        {
            _logger = logger;
            _languageLevelDomain = languageLevelDomain;
        }
        
        /// <summary>
        /// Returns the languages proficiency table data in a JSON format
        /// Retrieved data: proficiency_id, proficiency
        /// </summary>
        /// <returns>GET the language's proficiency list</returns>
        /// <response code="200">Successful Operations. Returns the language's proficiency list</response>
        /// <response code="500">If an error occurs returning the language's proficiency list</response>    
        [HttpGet]
        public IActionResult Get()
        {
            IActionResult response = null;
            try {
                _logger.LogInformation("Listing all languages");
                response = Ok(_languageLevelDomain.GetLanguagesProficiency());
            } catch(Exception defaultException) {

                _logger.LogError(defaultException.Message);
                response = StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return response;
        }

        /// <summary>
        /// Get an specific Language proficiency level by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>GET a language proficiency level by param id</returns>
        /// <response code="200">Successful Operations. Returns the language proficiency level with the param id</response>
        /// <response code="500">If an error occurs listing a language proficiency</response>  
        [ApiExplorerSettings(IgnoreApi = true)]   
        [HttpGet("[action]")]
        public IActionResult GetById(int id)
        {
            IActionResult response = null;
            try {
                _logger.LogInformation("Listing a language");
                LanguageLevel languageLevel=_languageLevelDomain.Get(id);
                if(languageLevel==null){
                    response = StatusCode((int)HttpStatusCode.NotFound);
                }else{
                    response=Ok(languageLevel);
                }

            } catch (Exception defaultException){
                _logger.LogError(defaultException.Message);
                response = StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return response;
        }

        
    }
}