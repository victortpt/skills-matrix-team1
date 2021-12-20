using Microsoft.AspNetCore.Mvc;
using Academy.Core.Domains.Interfaces;
using Academy.Core.Models;
using System;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Academy.Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICategoryDomain _categoriesDomain;
        public CategoryController(ILogger<CategoryController> logger, ICategoryDomain categoriesDomain)
        {
            _logger = logger;
            _categoriesDomain = categoriesDomain;
        }

        /// <summary>
        /// Create a new category in database.
        /// Input data : new Category
        /// </summary>
        /// <remarks>Create a new Category in dabase</remarks>
        /// <response code="201">Succesful Operation. New category created</response>
        /// <response code="400">Bad request due to invalid input or if category already existed</response>
        /// <response code="500">Internal Server Error. If an error occurs creating the new category</response>    
        [HttpPost("{categoryNameInput}")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Create(string categoryNameInput)
        {
            IActionResult response=null;
            Category CategoryAdd=new Category
                {
                    Category1=categoryNameInput
                }; 
            try
            {
                if (!ValCatName(categoryNameInput) || _categoriesDomain.CheckCagetoryExists(CategoryAdd))
                {
                    _logger.LogInformation("Parameters in the request are not correct.");
                    response=StatusCode((int)HttpStatusCode.BadRequest);
                } else
                    {
                        _categoriesDomain.Add(CategoryAdd); 
                        //Check if db is modified with new value
                        if (_categoriesDomain.CheckCagetoryExists(CategoryAdd))
                            {   
                                _logger.LogInformation("Category created");
                                response= StatusCode((int)HttpStatusCode.Created);
                                Response.Headers.Add("NewCategoryAdded",  _categoriesDomain.RemoveAccentsWithNormalization(CategoryAdd.Category1));
                            }  
                    } 
            }catch(Exception defaultException)  
            {
                _logger.LogError(defaultException.Message);
                response = StatusCode((int)HttpStatusCode.InternalServerError);            
            }
            return response;
        }
        
        /// <summary>
        /// Update existing category in database
        /// Input data : id, category name
        /// </summary>
        /// <remarks> Update existing category in database</remarks>
        /// <response code="201">Succesful Operation. Category updated</response>
        /// <response code="400">Bad request due to invalid input or if category name already exists</response>
        /// <response code="404">Not Found. If no id matches with id parameter</response>
        /// <response code="500">Internal Server Error. If an error occurs updating the category</response>    
        [HttpPut("{categoryId}")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Put(int categoryId,string categoryName)
        {   
            //Validating format of parameters
            bool validParams=ValidateCategoryPutParams(categoryId,categoryName);
            //Creating object to update with param values
            Category CategoryUpdate=new Category
            {
                Id = categoryId,
                Category1 = categoryName
            }; 
            IActionResult response=null;
            try
            {
                   if(!validParams || _categoriesDomain.CheckCagetoryExists(CategoryUpdate))
                   {
                      _logger.LogInformation("Parameters in the request are not correct.");
                      response=StatusCode((int)HttpStatusCode.BadRequest);

                   }else if(_categoriesDomain.Get(categoryId)==null){
                      response=StatusCode((int)HttpStatusCode.NotFound);
                   }else
                   {
                      _categoriesDomain.Update(CategoryUpdate); 
                      _logger.LogInformation("Category updated");
                      response= StatusCode((int)HttpStatusCode.Created);   
                      Response.Headers.Add("NewCategoryIDChangedTo", _categoriesDomain.RemoveAccentsWithNormalization(CategoryUpdate.Category1));
                   }
            }catch(Exception defaultException)
                {   
                    _logger.LogError(defaultException.Message);   
                    response=StatusCode((int)HttpStatusCode.InternalServerError);    
                } 
            return response;                
        }
        /// <summary>
        /// Delete existing category in database
        /// Input data : id
        /// </summary>
        /// <remarks> Delete existing category in database</remarks>
        /// <response code="204">Succesful Operation. Category deleted</response>
        /// <response code="400">Bad request due to invalid input</response>
        /// <response code="404">Not found. If no id matches with id parameter</response>
        /// <response code="409">Conflict. If user tries to delete a category with associated skills</response>
        /// <response code="500">Internal Server Error. If an error occurs deleting the category</response>    
        [HttpDelete("{categoryId}")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int categoryId)
        {
            IActionResult response=null;
        try
        {
            if(categoryId < 1)
            {   
                _logger.LogInformation("Parameters in the request are not correct.");
                response=StatusCode((int)HttpStatusCode.BadRequest);
            }else if(_categoriesDomain.Get(categoryId)==null)
            {   
                response=StatusCode((int)HttpStatusCode.NotFound);
            }else if (_categoriesDomain.CheckIfAssociatedSkills(categoryId))
            {   
                _logger.LogInformation("Dependency conflict");
                response=StatusCode((int)HttpStatusCode.Conflict);
            }else
            {
                _categoriesDomain.Delete(categoryId);
                // Check if element if db id correctly deleted
                 if(_categoriesDomain.Get(categoryId)==null)
                {   
                    _logger.LogInformation("Category deleted");
                    response=StatusCode((int)HttpStatusCode.NoContent);
                    Response.Headers.Add("CategoryIDDeleted",  _categoriesDomain.RemoveAccentsWithNormalization(categoryId.ToString()));
                }
            }
        }catch(Exception defaultException)
        {   
            _logger.LogError(defaultException.Message);   
            response=StatusCode((int)HttpStatusCode.InternalServerError);  
        } 
        return response;
        }

        private bool ValCatName(string categoryNameInput)
        {
          int number;
          if(!String.IsNullOrEmpty(categoryNameInput) && !Int32.TryParse(categoryNameInput, out number) && categoryNameInput.Length<=255)
          {
              return true;
          } else
          {
              return false;
          }
        }
        
        //All validations in this method and the entire method response is true if it is ok and false if is not
        private bool ValidateCategoryPutParams(long id,string name){

             if (ValCatName(name) &&  id > 0)
             {
                 return true;
             }else
             {
                 return false;
             }
        }
    }
}

