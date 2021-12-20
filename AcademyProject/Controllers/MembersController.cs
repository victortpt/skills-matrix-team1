using Microsoft.AspNetCore.Mvc;
using Academy.Core.DTO;
using Academy.Core.Models;
using Academy.Core.Domains.Interfaces;
using System;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Academy.Api.Controllers

{
    [Route("api/members")]
    [ApiController]

    public class MembersController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMembersDomain _membersDomain;
        public MembersController(ILogger<MembersController> logger, IMembersDomain membersDomain)
        {
            _logger = logger;
            _membersDomain = membersDomain;
        }
        
         /// <summary>
        /// Return the member's list in Json format
        /// Retrived data: Id, Name, Surname, Role, Skills, Languages and last update
        /// </summary>
        /// <remarks>GET the member's list</remarks>
        /// <response code="200">Succesful Operations. Returns the member's list</response>
        /// <response code="500">If an error occurs returning the member's list</response>    
        private IActionResult GetAll()
        {
            IActionResult response = null;
            try {
                _logger.LogInformation("Listing all members");
                response = File(Encoding.UTF8.GetBytes(_membersDomain.GetMembersSkills()), "application/json");
            } catch(Exception defaultException) {
                _logger.LogError(defaultException.Message);
                response = StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return response;
        }

        /// <summary>
        /// Get an specific Member by id.
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>A JSON with data from a member by param id.
        ///</remarks>
        /// <response code="200">Returns the member with the param id</response>
        /// <response code="500">If an error occurs listing an member</response>     
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            IActionResult response;
            try
            {
                if ( id < 0 )
                {
                    _logger.LogInformation("Parameters in the request are not correct.");
                    response=StatusCode((int)HttpStatusCode.BadRequest);
                } else if(_membersDomain.GetMember(id)==null)
                {
                    response = StatusCode((int)HttpStatusCode.NotFound);
                }else{
                    _logger.LogInformation("Listing member's skills");
                    response = Ok(_membersDomain.GetMember(id));                   
                }
            } catch (Exception defaultException)
            {
                _logger.LogError(defaultException.Message);
                response = StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return response;
        }

        /// <summary>
        /// Get skills from an specific Member by id.
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>A JSON with skills from a member by param id.
        ///</remarks>
        /// <response code="200">Returns the member's skills</response>
        /// <response code="500">If an error occurs listing an member</response>
        [HttpGet("{id}/Skills")]
        public IActionResult GetSkills(long id)
        {
            IActionResult response;
            try
            {
                if ( id < 0)
                {
                    _logger.LogInformation("Parameters in the request are not correct.");
                    response=StatusCode((int)HttpStatusCode.BadRequest);
                } else if(_membersDomain.GetMemberSkills(id)==null)
                {
                    response = StatusCode((int)HttpStatusCode.NotFound);
                }else{
                    _logger.LogInformation("Listing member's skills");
                    response = Ok(_membersDomain.GetMemberSkills(id));                   
                }
            } catch (Exception defaultException)
            {
                _logger.LogError(defaultException.Message);
                response = StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return response;
        }


        /// <summary>
        /// Get languages from an specific Member by id.
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>A JSON with languages from a member by param id.
        ///</remarks>
        /// <response code="200">Returns the member's languages</response>
        /// <response code="500">If an error occurs listing an member</response>
        [HttpGet("{id}/Languages")]
        public IActionResult GetMemberLanguages(long id)
        {
            IActionResult response;
            try
            {
                if ( id < 0 )
                {
                    _logger.LogInformation("Parameters in the request are not correct.");
                    response=StatusCode((int)HttpStatusCode.BadRequest);
                } else if(_membersDomain.GetMemberLanguages(id)==null)
                {
                    response = StatusCode((int)HttpStatusCode.NotFound);
                }else{
                    _logger.LogInformation("Listing member's languages");
                    response = Ok(_membersDomain.GetMemberLanguages(id));                   
                }
            } catch (Exception defaultException)
            {
                _logger.LogError(defaultException.Message);
                response = StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return response;
        }

        /// <summary>
        /// Return the filtered member's list in Json format
        /// </summary>
        ///<remarks>GET the list of members filtered by the indicated parameters</remarks>
        /// <response code="200">Succesful Operations. Returns the member's list</response>
        /// <response code="200">Succesful Operations. It can return an empty list</response>
        /// <response code="400">Bad Request. Some of the ids aren't valid or don't correspond</response>
        /// <response code="404">Not Found. Some of the ids are not in the DB</response>
        /// <response code="500">Server Error. If an error occurs returning the member's list</response>    
        
        [HttpGet]
        public IActionResult GetMemberFilter(long categoryId,[FromQuery]List<long> skillId,[FromQuery]List<long> skillLevelId)

        {
            IActionResult response = null;

            try {
                
                if(categoryId==0 && skillId.Count==0 && skillLevelId.Count==0 ) {
                    _logger.LogInformation("Listing all members");
                    response = GetAll();
                } else {
                   response = GetMembersFiltered(categoryId, skillId, skillLevelId);
                }
            } catch(Exception defaultException) {        
                _logger.LogError(defaultException.Message);
                response = StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return response;
        }


        private IActionResult GetMembersFiltered(long categoryId,List<long> skillIds,List<long> skillLevelIds) {
            IActionResult response = null;
            List<MemberFilterDto> getMemberList = _membersDomain.Get(categoryId, skillIds, skillLevelIds);
            if ( categoryId<0 || !skillIds.All(x=> x > 0) || !skillLevelIds.All(x=> x > 0) || (categoryId==0 && skillIds.Count == 0) ||  _membersDomain.validateCategoryWithSkills(categoryId, skillIds) )  {
                _logger.LogInformation("Parameters in the request are not correct.");
                response = StatusCode((int)HttpStatusCode.BadRequest);
            } else if( getMemberList==null) {
                response = StatusCode((int)HttpStatusCode.NotFound);
            } else {
                _logger.LogInformation("Listing members that satisfy the filter");
                response = Ok(getMemberList);                   
            }
            return response;
        }

        /// <summary>
        /// Add a new skill and his skill level to the member profile.
        /// </summary>
        /// <remarks>Add skill and skill level to a member</remarks>
        /// <response code="201">Succesful Operation. Skill and skill level added</response>
        /// <response code="400">Bad request due to invalid input or if any, skill or skill level, are already in that member</response>
        /// <response code="500">If an error occurs during the operation</response>    
        [HttpPost("{memberId}/skills")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult AddSkillsIntoMember(long memberId, [FromBody] SkillAndLevelDto skillAndLevelIds)
        {
            IActionResult response;
            long skillId = skillAndLevelIds.skillId;
            long skillLevelId = skillAndLevelIds.skillLevelId;
            if (memberId<1 || skillId<1 || skillLevelId<1) 
            {
                _logger.LogInformation("Parameters in the request are not correct.");
                return response=StatusCode((int)HttpStatusCode.BadRequest);
            } 
            try
            {
                MemberFilterDto member = _membersDomain.InsertSkillIntoMember(memberId, skillId, skillLevelId); 
                
                if (member==null) 
                {
                    _logger.LogInformation("Parameters are correct but does not exist on the database.");
                    response=StatusCode((int)HttpStatusCode.NotFound);
                }
                else if (member.Name.Equals("error")) 
                {
                    _logger.LogInformation("This member already have that skill with that level.");
                    response=StatusCode((int)HttpStatusCode.BadRequest);
                }
                else 
                {
                    _logger.LogInformation("Added skill and skill level");
                    response= StatusCode((int)HttpStatusCode.Created);
                    Response.Headers.Add("NewSkillAdded","Added skill and skill level");
                }
            } 
            catch(Exception defaultException)  
            {
                _logger.LogError(defaultException.Message);
                response = StatusCode((int)HttpStatusCode.InternalServerError);            
            } 
            return response;
            
        }

        /// <summary>
        /// Delete existing skill from a member in database
        /// Input data : id
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="skillId"></param>
        /// <remarks> Delete existing skill from a member in database</remarks>
        /// <response code="204">Succesful Operation. Member's skill deleted</response>
        /// <response code="400">Bad request due to invalid input</response>
        /// <response code="404">If no id matches with id parameter</response>
        /// <response code="500">If an error occurs deleting the skill</response>    
        [HttpDelete("{memberId}/skills/{skillId}")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteSkillsFromMember(long memberId, long skillId)
        {
            IActionResult response = null;
            if (!validateParams(memberId,skillId, 1)) 
            {
                _logger.LogInformation("Parameters in the request are not correct.");
                response=StatusCode((int)HttpStatusCode.BadRequest);
            } else {
                try
                {
                    if(!_membersDomain.DeleteSkillsFromMember(memberId, skillId)){
                        _logger.LogInformation("Error deleting the skill");
                        response = StatusCode((int)HttpStatusCode.NotFound);
                    }
                    else 
                    {
                        _logger.LogInformation("Changed skill and skill level");
                        response= StatusCode((int)HttpStatusCode.NoContent);
                        Response.Headers.Add("MemberSkillDeleted","Deleted the indicated skill with skill level from member");
                    }
                } 
                catch(Exception defaultException)  
                {
                    _logger.LogError(defaultException.Message);
                    response = StatusCode((int)HttpStatusCode.InternalServerError);            
                } 
            }
            return response;
        }

        /// <summary>
        /// Update existing skill in database
        /// Input data : id, skill name
        /// </summary>
        /// <remarks> Update existing skill in database</remarks>
        /// <response code="204">No content. Skill updated</response>
        /// <response code="400">Bad request due to invalid input or if skill name already exists</response>
        /// <response code="500">If an error occurs updating the skill</response>    
        [HttpPut("{memberId}/skills/{skillId}")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult UpdateSkillIntoMember(long memberId, long skillId, [FromBody] SkillLevelsDto skillLevelId)
        {   
            IActionResult response = null;
            if (!validateParams(memberId, skillId, skillLevelId.SkillLevelId)){
                _logger.LogInformation("Parameters in the request are not correct.");
                response = StatusCode((int)HttpStatusCode.BadRequest);
            } else {
                try {
                    MemberFilterDto member = _membersDomain.UpdateSkillIntoMember(memberId, skillId, skillLevelId.SkillLevelId);
                    if(member == null) {
                        _logger.LogInformation("Parameters in the request are not correct.");
                        response = StatusCode((int)HttpStatusCode.NotFound);
                    }
                    else if(member.Name.Equals("error")) 
                    {
                        _logger.LogInformation("Parameters in the request are not correct.");
                        response = StatusCode((int)HttpStatusCode.BadRequest);
                    }                    
                    else 
                    {
                        _logger.LogInformation("Skill updated");
                        response = StatusCode((int)HttpStatusCode.NoContent);
                        Response.Headers.Add("MemberSkillUpdated","Updated the indicated skill with skill level from member");
                    }
                } catch(Exception defaultException) {   
                    _logger.LogError(defaultException.Message);   
                    response=StatusCode((int)HttpStatusCode.InternalServerError);
                }   
            }
            return response;     
        }
        private bool validateParams(long memberId, long skillId, long skillLevelId)
        {
            if(memberId <= 0 || skillId <= 0 || skillLevelId <= 0){
                return false;
            } else {
                return true;
            }
        }
    }
}
