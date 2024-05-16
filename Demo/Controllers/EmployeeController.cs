
using AutoMapper;
using Demo.Core.DTO;
using Demo.Core.InterFace;
using Demo.EF.DataBase;
using Demo.EF.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly ITIEntity contex;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeController(ITIEntity Contex, IUnitOfWork unitOfWork, IMapper mapper)
        {
            contex = Contex;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var data = unitOfWork.EmpRep.GetAll();
            var result = mapper.Map<IEnumerable<EmpDto>>(data);
           
            return Ok(result);
        }
      
        [HttpGet("{id:int}", Name = "GetOneEmpRoute")]
        public IActionResult GetById(int id)
        {
            var data = unitOfWork.EmpRep.GetById(id);
            var result = mapper.Map<EmployeeDataWithDptName>(data);
            return Ok(result);
        }
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var data = unitOfWork.EmpRep.Find(b => b.Name == name , new[] {"Department"});
            var result = mapper.Map<IEnumerable<EmployeeDataWithDptName>>(data);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult PostEmployee(EmpDto dto)
        {
            if (ModelState.IsValid)
            {
                //

                var data = mapper.Map<Emp>(dto);
                unitOfWork.EmpRep.Add(data);
                unitOfWork.Complete();
                string url = Url.Link("GetOneEmpRoute", new { id = dto.Id });
                return Created(url, dto);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, EmpDto dto )
        {
            if (dto != null)
            {
                var data = mapper.Map<Emp>(dto);
                var result = unitOfWork.EmpRep.Update(data);
                unitOfWork.Complete();
             
                return StatusCode(204, dto);
                
            }
        
               return BadRequest(ModelState);


            
        }
        [HttpDelete("{id:int}")]
        public IActionResult Remove(int id)
        {
            try
            {
                unitOfWork.EmpRep.Delete(id);
                unitOfWork.Complete();

                return StatusCode(204, "Date Remove");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return BadRequest(ModelState);

        }
    }

}
