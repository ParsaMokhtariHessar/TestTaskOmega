using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTaskOmega.Application.ApplicationModels;
using TestTaskOmega.Application.Contracts;

namespace TestTaskOmega.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
            var response = await _servicesRepository.CreateAsync(serviceName);
            return HandleResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceAsync(int id)
        {
            var response = await _servicesRepository.DeleteAsync(id);
            return HandleResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServicesAsync()
        {
            var response = await _servicesRepository.GetAllAsync();
            return HandleResponse(response);
        }

        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetAllHistoryForServiceAsync(int id)
        {
            var response = await _servicesRepository.GetHistory(id);
            return HandleResponse(response);
        }

        [HttpGet("by-creation-date/{creationDate}")]
        public async Task<IActionResult> GetServiceByCreationDateAsync(DateTime creationDate)
        {
            var response = await _servicesRepository.GetByCreationDateAsync(creationDate);
            return HandleResponse(response);
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetServiceByIdAsync(int id)
        {
            var response = await _servicesRepository.GetByIdAsync(id);
            return HandleResponse(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetServiceByNameAsync([FromQuery] string serviceName)
        {
            var response = await _servicesRepository.GetServiceByNameAsync(serviceName);
            return HandleResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceAsync(int id, [FromBody] string newServiceName)
        {
            var response = await _servicesRepository.UpdateAsync(id, newServiceName);
            return HandleResponse(response);
        }

        [HttpGet("deleted")]
        public async Task<IActionResult> GetAllDeletedServicesAsync()
        {
            var response = await _servicesRepository.GetAllDeletedAsync();
            return HandleResponse(response);
        }

        private IActionResult HandleResponse(ServiceResponse response)
        {
            if (response.Success)
            {
                return Ok();
            }

            if (response.Message == "Service Not Found!" || response.Message == "Service History Not Found!")
            {
                return NotFound(response.Message);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
        }

        private IActionResult HandleResponse<T>(ServiceResponse<T> response)
        {
            if (response.Success)
            {
                return Ok(response.Data);
            }

            if (response.Message == "Service Not Found!" || response.Message == "Service History Not Found!")
            {
                return NotFound(response.Message);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
        }
    }
}