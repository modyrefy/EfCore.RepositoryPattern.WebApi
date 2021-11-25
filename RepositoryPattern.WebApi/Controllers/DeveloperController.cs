using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeveloperController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult GetPopularDevelopers([FromQuery] int count)
        {
            return Ok(_unitOfWork.Developers.GetPopularDevelopers(count));
        }
        [HttpPost]
        [Route("save")]
        public IActionResult AddDeveloperAndProject()
        {
            try
            {
                var developer = new Developer
                {
                    Followers = 35,
                    Name = "Mukesh Murugan"
                };
                var project = new Project
                {
                    Name = "codewithmukesh"
                };
                _unitOfWork.Developers.Add(developer);
              //  throw new Exception("general error");
                _unitOfWork.Projects.Add(project);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                return BadRequest(ex.Message);
            }
        }

    }
}
