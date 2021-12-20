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
    [Route("api/skilllevel")]
    [ApiController]
    public class SkillLevelController : Controller
    {
        private readonly ILogger _logger;
        private readonly ISkillLevelDomain _skillLevelDomain;
        public SkillLevelController(ILogger<SkillLevelController> logger, ISkillLevelDomain skillLevelDomain)
        {
            _logger = logger;
            _skillLevelDomain = skillLevelDomain;
        }
        
        /// <summary>
        /// Return the list of skill levels
        /// Generate a JSON file containing the list of skill levels
        /// Retrieved data: id, skill_level
        /// </summary>
        /// <returns>GET the skill levels's list</returns>
        /// <response code="200">Succesful Operations. Returns the skill level's list</response>
        /// <response code="500">If an error occurs returning the skill level's list</response>    
        [HttpGet]
        public IActionResult Get()
        {
            IActionResult response = null;
            try {
                using (_logger.BeginScope("Message {HoleValue}", DateTime.Now))
                {
                    _logger.LogInformation("Listing all skill levels");
                }
                response = File(Encoding.UTF8.GetBytes(_skillLevelDomain.GetSkillLevels()), "application/json");
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
        /// Get an specific skill level by id.
        /// Retrieved data: id, skill_level
        /// </summary>
        /// <param name="id"></param>
        /// <returns>GET a skill level by param id</returns>
        /// <response code="200">Succesful Operations. Returns the skill level with the param id</response>
        /// <response code="500">If an error occurs listing an skill level</response>  
        [ApiExplorerSettings(IgnoreApi = true)]   
        [HttpGet]
        [Route("{id?}")]
        public ActionResult<SkillLevel> Get(int id)
        {
            ActionResult response = null;
            try {
                _logger.LogInformation("Listing an skill level");

                SkillLevel skillLevel = _skillLevelDomain.Get(id);
                response = Ok(skillLevel);
            } catch (Exception defaultException){
                _logger.LogError(defaultException.Message);

                response = StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return response;
        }
        
    }
}