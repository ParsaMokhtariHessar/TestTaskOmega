using Microsoft.AspNetCore.Mvc;
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
        public IActionResult CreateService([FromBody] string serviceName)
        {
            try
            {
                _servicesRepository.Create(serviceName);
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
        public IActionResult DeleteService(int id)
        {
            try
            {
                _servicesRepository.Delete(id);
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
        public IActionResult GetAllServices()
        {
            try
            {
                var services = _servicesRepository.GetAll();
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}/history")]
        public IActionResult GetAllHistoryForService(int id)
        {
            try
            {
                var history = _servicesRepository.GetAllHistoryByIdSortedByLatest(id);
                return Ok(history);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("by-creation-date/{creationDate}")]
        public IActionResult GetServiceByCreationDate(DateTime creationDate)
        {
            try
            {
                var service = _servicesRepository.GetByCreationDate(creationDate);
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
        public IActionResult GetServiceById(int id)
        {
            try
            {
                var service = _servicesRepository.GetById(id);
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
        public IActionResult GetServiceByName([FromQuery] string serviceName)
        {
            try
            {
                var service = _servicesRepository.GetServiceByName(serviceName);
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
        public IActionResult UpdateService(int id, [FromBody] string newServiceName)
        {
            try
            {
                _servicesRepository.Update(id, newServiceName);
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