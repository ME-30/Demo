using AutoMapper;
using Demo.Core.DTO;
using Demo.Core.InterFace;
using Demo.EF.DataBase;
using Demo.EF.Entity;
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
    public class DepartmentController : ControllerBase
    {
        private readonly ITIEntity contex;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public DepartmentController(ITIEntity Contex, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.contex = Contex;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetAllDepartment()
        {
            var data = unitOfWork.DptRep.GetAll();
            var result = mapper.Map<IEnumerable<DepartmentDto>>(data);
            return Ok(result);
        }
        [HttpGet("{id:int}", Name = "GetOneDptRoute")]
        public IActionResult GetById(int id)
        {
            var data = unitOfWork.DptRep.GetById(id);
            var result = mapper.Map<DepartmentDto>(data);

            return Ok(result);
        }

        [HttpGet("{Name:alpha}")]
        public IActionResult GetByNameWithDepartment(string Name)
        {
            var data = unitOfWork.DptRep.Find(d => d.Name == Name, new[] { "Employees" } );
            var result = mapper.Map<IEnumerable<DepartmentWithEmpName>>(data);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult PostDepratment(DepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                //
                var data = mapper.Map<Department>(departmentDto);
                var result = unitOfWork.DptRep.Add(data);
                unitOfWork.Complete();

                string url = Url.Link("GetOneDptRoute", new { id = departmentDto.Id });
                return Ok(departmentDto);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id:int}")]
        public IActionResult Update(int id,DepartmentDto dto)
        {


            if (ModelState.IsValid == true)
            {
                var result = mapper.Map<Department>(dto);
                var data = unitOfWork.DptRep.Update(result);

                unitOfWork.Complete();
                return StatusCode(204, dto);

            }
            return BadRequest();
        }


        //    }
        [HttpDelete]
        public IActionResult Rmove(int id)
        {
            //
            try
            {
                unitOfWork.DptRep.Delete(id);
                unitOfWork.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }


            return BadRequest(ModelState);
        }
    }
}

