using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mwh.Sample.Common.Models;
using Mwh.Sample.Common.Repositories;
using Supermarket.API.Domain.Services;
using Supermarket.API.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supermarket.API.Controllers
{
    [Route("/api/employees")]
    [Produces("application/json")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly EmployeeMock _emp;

        public EmployeesController(IEmployeeService employeeService, IMapper mapper)
        {
            _emp = new EmployeeMock();
            _employeeService = employeeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lists all employees.
        /// </summary>
        /// <returns>List os employees.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EmployeeModel>), 200)]
        public async Task<IEnumerable<EmployeeModel>> ListAsync()
        {
            IEnumerable<EmployeeModel> myReturn;

            myReturn = _emp.EmployeeCollection().ToArray();

            var employees = await _employeeService.ListAsync().ConfigureAwait(true);
            //var resources = _mapper.Map<IEnumerable<EmployeeModel>, IEnumerable<EmployeeResource>>(employees);

            return myReturn;
        }

        /// <summary>
        /// Saves a new employee.
        /// </summary>
        /// <param name="resource">Employee data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(EmployeeResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveEmployeeResource resource)
        {
            var employee = _mapper.Map<SaveEmployeeResource, EmployeeModel>(resource);
            var result = await _employeeService.SaveAsync(employee);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var employeeResource = _mapper.Map<EmployeeModel, EmployeeResource>(result.Resource);
            return Ok(employeeResource);
        }

        /// <summary>
        /// Updates an existing employee according to an identifier.
        /// </summary>
        /// <param name="id">Employee identifier.</param>
        /// <param name="resource">Updated employee data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EmployeeResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveEmployeeResource resource)
        {
            var employee = _mapper.Map<SaveEmployeeResource, EmployeeModel>(resource);
            var result = await _employeeService.UpdateAsync(id, employee);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var employeeResource = _mapper.Map<EmployeeModel, EmployeeResource>(result.Resource);
            return Ok(employeeResource);
        }

        /// <summary>
        /// Deletes a given employee according to an identifier.
        /// </summary>
        /// <param name="id">Employee identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(EmployeeResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _employeeService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var employeeResource = _mapper.Map<EmployeeModel, EmployeeResource>(result.Resource);
            return Ok(employeeResource);
        }
    }
}