using Dataverse.Web.API.Models;
using Microsoft.AspNetCore.Mvc;
using Dataverse.Http.Connector.Core.Persistence;
using Dataverse.Http.Connector.Core.Extensions.Utilities;

namespace Dataverse.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IDataverseContext _dataverse;

        public EmployeesController(IDataverseContext dataverse)
            => _dataverse = dataverse;

        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            var employees = await _dataverse.Set<Employees>().ToListAsync();
            return Ok(employees);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedEmployee([FromQuery] int page, [FromQuery] int pageSize)
        {
            var pagedEmployees = await _dataverse.Set<Employees>().ToPagedListAsync(page, pageSize);
            return Ok(pagedEmployees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _dataverse.Set<Employees>()
                .FilterAnd(conditions =>
                {
                    conditions.Equal(x => x.Id, id);
                })
                .FirstOrDefaultAsync();

            if (employee is null)
                return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employees model)
        {
            await _dataverse.Set<Employees>().AddAsync(model);

            if (model.Id == Guid.Empty)
                return BadRequest();

            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, [FromBody] Employees model)
        {
            var employee = await _dataverse.Set<Employees>()
                .FilterAnd(conditions =>
                {
                    conditions.Equal(x => x.Id, id);
                }).FirstOrDefaultAsync();

            if (employee is null)
                return NotFound();

            model.Id = id;

            await _dataverse.Set<Employees>().UpdateAsync(model);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _dataverse.Set<Employees>()
                .FilterAnd(conditions =>
                {
                    conditions.Equal(x => x.Id, id);
                }).FirstOrDefaultAsync();

            if (employee is null)
                return NotFound();

            await _dataverse.Set<Employees>().DeleteAsync(employee);
            return Ok();
        }
    }
}
