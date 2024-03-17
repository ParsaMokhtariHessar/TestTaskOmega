using TestTaskOmega.Application.ApplicationModels;
using TestTaskOmega.Application.Contracts;
using TestTaskOmega.Application.RepositoryPattern;
using TestTaskOmega.Domain;

namespace TestTaskOmega.Application.ServiceRepository
{
    public class ServicesRepository : IServicesRepository
    {
        private readonly IRepository<Services, ServicesHistory> _serviceRepository;

        public ServicesRepository(IRepository<Services, ServicesHistory> serviceRepository)
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
            var newServiceHistory = new ServicesHistory { ServiceName = serviceName };

            try
            {
                await _serviceRepository.CreateAsync(newService, newServiceHistory);
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

            var historyResponse = await GetAllHistoryByIdSortedByLatestAsync(id);
            if (!historyResponse.Success)
            {
                return new ServiceResponse($"Service History Not Found!");
            }

            try
            {
                var latestHistory = historyResponse.Data.FirstOrDefault(); // Assuming the history is sorted by latest
                if (latestHistory == null)
                {
                    return new ServiceResponse("Latest service history not found.");
                }

                await _serviceRepository.DeleteAsync(serviceResponse.Data, latestHistory);
                return new ServiceResponse();
            }
            catch (Exception ex)
            {
                return new ServiceResponse($"Error deleting service: {ex.Message}");
            }
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

        public async Task<ServiceResponse<IEnumerable<ServicesHistory>>> GetAllHistoryByIdSortedByLatestAsync(int id)
        {
            try
            {
                var history = await _serviceRepository.GetAllHistoryByIdSortedByLatestAsync(id);
                return new ServiceResponse<IEnumerable<ServicesHistory>>(history);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<IEnumerable<ServicesHistory>>($"Error retrieving service history: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<Services>> GetByCreationDateAsync(DateTime creationDate)
        {
            try
            {
                var service = await _serviceRepository.GetByCreationDateAsync(creationDate);
                return new ServiceResponse<Services>(service);
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
            var existingHistory = (await GetAllHistoryByIdSortedByLatestAsync(id)).Data.FirstOrDefault();

            if (existingHistory == null)
            {
                return new ServiceResponse("Service history not found.");
            }

            existingService.ServiceName = newServiceName;
            existingHistory.ServiceName = newServiceName;

            try
            {
                await _serviceRepository.UpdateAsync(existingService, existingHistory);
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