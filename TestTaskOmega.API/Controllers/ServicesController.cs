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

        [HttpDelete("{Entityid}")]
        public async Task<IActionResult> DeleteServiceAsync(int Entityid)
        {
            var response = await _servicesRepository.DeleteAsync(Entityid);
            return HandleResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServicesAsync()
        {
            var response = await _servicesRepository.GetAllAsync();
            return HandleResponse(response);
        }

        [HttpGet("{Entityid}/history")]
        public async Task<IActionResult> GetAllHistoryForServiceAsync(int Entityid)
        {
            var response = await _servicesRepository.GetHistory(Entityid);
            return HandleResponse(response);
        }

        [HttpGet("by-creation-date/{creationDate}")]
        public async Task<IActionResult> GetServiceByCreationDateAsync(DateTime creationDate)
        {
            var response = await _servicesRepository.GetByCreationDateAsync(creationDate);
            return HandleResponse(response);
        }

        [HttpGet("by-Entityid/{Entityid}")]
        public async Task<IActionResult> GetServiceByIdAsync(int Entityid)
        {
            var response = await _servicesRepository.GetByIdAsync(Entityid);
            return HandleResponse(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetServiceByNameAsync([FromQuery] string serviceName)
        {
            var response = await _servicesRepository.GetServiceByNameAsync(serviceName);
            return HandleResponse(response);
        }

        [HttpPut("{Entityid}")]
        public async Task<IActionResult> UpdateServiceAsync(int Entityid, [FromBody] string newServiceName)
        {
            var response = await _servicesRepository.UpdateAsync(Entityid, newServiceName);
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