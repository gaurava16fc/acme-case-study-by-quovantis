using System;
using ACME.Backend.Core.DTO;
using ACME.Backend.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace ACME.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController
            (
                IRepositoryWrapper repositoryWrapper,
                IMapper mapper,
                ILogger<CustomersController> logger
            )
        {
            this._repo = repositoryWrapper.CustomerRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        
        // GET: api/<CustomersController>
        [HttpGet]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetCustomers()
        {
            this._logger.LogInformation(User.FindFirst(System.Security.Claims.ClaimTypes.UserData).Value);
            var _customers = await _repo.FindAll().ToListAsync();
            var _customersListToReturn = _mapper.Map<IEnumerable<CustomerForDetailedDTO>>(_customers);
            return Ok(_customersListToReturn);
        }
        

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var _customer = await _repo.FindByCondition(e => e.Id == id).FirstOrDefaultAsync();
            if (_customer == null)
            {
                return NotFound(@"Customer with Id = {" + Convert.ToString(id) + "} is not found");
            }
            var _customerToReturn = _mapper.Map<CustomerForDetailedDTO>(_customer);
            return Ok(_customerToReturn);
        }
    }
}
