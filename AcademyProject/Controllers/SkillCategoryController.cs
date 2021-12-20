using Microsoft.AspNetCore.Mvc;
using Academy.Core.Domains.Interfaces;
using Academy.Core.Models;
using System;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;

namespace Academy.Api.Controllers
{
    [Route("api/skillcategories")]
    [ApiController]
    public class SkillCategoryController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICategoryDomain _categoriesDomain;
        public SkillCategoryController(ILogger<CategoryController> logger, ICategoryDomain categoriesDomain)
        {
            _logger = logger;
            _categoriesDomain = categoriesDomain;
        }
        
        /// <summary>
        /// Return the a JSON file with a list of categories and associated skills
        /// Retrieved data: Category, IdCategory, Skill, IdSkill
        /// </summary>
        /// <remarks>GET the categories and associated skills JSON file</remarks>
        /// <response code="200">Succesful Operations. Returns a JSON file with a list of categories and associated skills</response>
        /// <response code="500">If an error occurs returning the JSON file of categories and associated skills</response>    
        [HttpGet]
        public IActionResult Get()
        {
            IActionResult response = null;
            try {
                _logger.LogInformation("Listing all categories and skills");         
                response = File(Encoding.UTF8.GetBytes(_categoriesDomain.GetCategorySkills()), "application/json");
            } catch(Exception defaultException) {

                _logger.LogError(defaultException.Message);     
                response = StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return response;
        }

}   }  