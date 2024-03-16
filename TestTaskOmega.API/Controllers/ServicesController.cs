using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestTaskOmega.Application.Contracts;
using TestTaskOmega.Application.Exeptions;

namespace TestTaskOmega.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesRepository _servicesRepository;

        public ServicesController(IServicesRepository servicesRepository)
        {
            _servicesRepository = servicesRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateServiceAsync([FromBody] string serviceName)
        {
            try
            {
                await _servicesRepository.CreateAsync(serviceName);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotUniqueException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceAsync(int id)
        {
            try
            {
                await _servicesRepository.DeleteAsync(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServicesAsync()
        {
            try
            {
                var services = await _servicesRepository.GetAllAsync();
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetAllHistoryForServiceAsync(int id)
        {
            try
            {
                var history = await _servicesRepository.GetAllHistoryByIdSortedByLatestAsync(id);
                return Ok(history);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("by-creation-date/{creationDate}")]
        public async Task<IActionResult> GetServiceByCreationDateAsync(DateTime creationDate)
        {
            try
            {
                var service = await _servicesRepository.GetByCreationDateAsync(creationDate);
                return Ok(service);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetServiceByIdAsync(int id)
        {
            try
            {
                var service = await _servicesRepository.GetByIdAsync(id);
                return Ok(service);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetServiceByNameAsync([FromQuery] string serviceName)
        {
            try
            {
                var service = await _servicesRepository.GetServiceByNameAsync(serviceName);
                return Ok(service);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceAsync(int id, [FromBody] string newServiceName)
        {
            try
            {
                await _servicesRepository.UpdateAsync(id, newServiceName);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}