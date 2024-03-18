using TestTaskOmega.Application.ApplicationModels;
using TestTaskOmega.Domain;
using TestTaskOmega.Application.RepositoryPattern;
using TestTaskOmega.Domain.Utilities;
using TestTaskOmega.Application.Contracts;
using TestTaskOmega.Application.Exeptions;

namespace TestTaskOmega.Application.ServiceRepository
{
    public class ServicesRepository : IServicesRepository
    {
        private readonly IRepository<Services, string> _serviceRepository;

        public ServicesRepository(IRepository<Services, string> serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<ServiceResponse> CreateAsync(string serviceName)
        {
            if (serviceName == null)
            {
                return new ServiceResponse("Service name cannot be null.");
            }

            var existingServiceResponse = await GetServiceByNameAsync(serviceName);
            if (existingServiceResponse.Success)
            {
                return new ServiceResponse("Service with the same name already exists.");
            }

            var newService = new Services { ServiceName = serviceName };

            try
            {
                await _serviceRepository.CreateAsync(newService);
                return new ServiceResponse();
            }
            catch (Exception ex)
            {
                return new ServiceResponse($"Error creating service: {ex.Message}");
            }
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var serviceResponse = await GetByIdAsync(id);
            if (!serviceResponse.Success)
            {
                return new ServiceResponse($"Service Not Found!");
            }

            try
            {
                await _serviceRepository.DeleteAsync(serviceResponse.Data);
                return new ServiceResponse();
            }
            catch (Exception ex)
            {
                return new ServiceResponse($"Error deleting service: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<IEnumerable<Modifications<string>>>> GetHistory(int id)
        {
            var serviceResponse = await GetByIdAsync(id);
            if (!serviceResponse.Success)
            {
                return new ServiceResponse<IEnumerable<Modifications<string>>>($"Service Not Found!");
            }

            var history = await _serviceRepository.GetHistory(serviceResponse.Data);
            return new ServiceResponse<IEnumerable<Modifications<string>>>(history);
        }

        public async Task<ServiceResponse<IEnumerable<Services>>> GetAllAsync()
        {
            try
            {
                var services = await _serviceRepository.GetAllAsync();
                return new ServiceResponse<IEnumerable<Services>>(services);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<IEnumerable<Services>>($"Error retrieving services: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<Services>> GetByCreationDateAsync(DateTime creationDate)
        {
            try
            {
                var service = await _serviceRepository.GetByCreationDateAsync(creationDate);
                return new ServiceResponse<Services>(service);
            }
            catch (NotFoundException ex)
            {
                return new ServiceResponse<Services>($"Entity with creationDate {creationDate} not found.");
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Services>($"Error retrieving service by creation date: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<Services>> GetByIdAsync(int id)
        {
            try
            {
                var service = await _serviceRepository.GetByIdAsync(id);
                return new ServiceResponse<Services>(service);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Services>($"Error retrieving service by ID: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<Services>> GetServiceByNameAsync(string serviceName)
        {
            try
            {
                var services = await _serviceRepository.GetAllAsync();
                var service = services.FirstOrDefault(s => s.ServiceName == serviceName);

                if (service == null)
                {
                    return new ServiceResponse<Services>($"Service with name '{serviceName}' not found.");
                }

                return new ServiceResponse<Services>(service);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Services>($"Error retrieving service by name: {ex.Message}");
            }
        }

        public async Task<ServiceResponse> UpdateAsync(int id, string newServiceName)
        {
            var existingServiceResponse = await GetByIdAsync(id);
            if (!existingServiceResponse.Success)
            {
                return new ServiceResponse("Service to be updated does not exist.");
            }

            var existingService = existingServiceResponse.Data;
            existingService.ServiceName = newServiceName;

            try
            {
                await _serviceRepository.UpdateAsync(existingService);
                return new ServiceResponse();
            }
            catch (Exception ex)
            {
                return new ServiceResponse($"Error updating service: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<IEnumerable<Services>>> GetAllDeletedAsync()
        {
            try
            {
                var deletedServices = await _serviceRepository.GetAllDeletedAsync();
                return new ServiceResponse<IEnumerable<Services>>(deletedServices);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<IEnumerable<Services>>($"Error retrieving deleted services: {ex.Message}");
            }
        }
    }
}