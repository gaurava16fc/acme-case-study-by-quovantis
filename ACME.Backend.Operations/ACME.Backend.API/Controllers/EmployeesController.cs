using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ACME.Backend.Core.DTO;
using ACME.Backend.Core.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ACME.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController
            (
                IRepositoryWrapper repositoryWrapper,
                IMapper mapper,
                ILogger<EmployeesController> logger
            )
        {
            this._repo = repositoryWrapper.EmployeeRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        // GET: api/<EmployeesController>
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var _employees = await _repo.FindAll().ToListAsync();            
            var _employeesListToReturn = _mapper.Map<IEnumerable<EmployeeForDetailedDTO>>(_employees) ;
            return Ok(_employeesListToReturn);
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var _employee = await _repo.FindByCondition(e => e.Id == id).FirstOrDefaultAsync();
            if (_employee == null)
            {
                return NotFound(@"Employee with Id = {" + Convert.ToString(id) + "} is not found");
            }
            var _employeeToReturn = _mapper.Map<EmployeeForDetailedDTO>(_employee);
            return Ok(_employeeToReturn);
        }
    }
}
