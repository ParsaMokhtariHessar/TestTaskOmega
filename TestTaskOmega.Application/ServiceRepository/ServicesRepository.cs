using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskOmega.Application.Contracts;
using TestTaskOmega.Application.Exeptions;
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

        public async Task CreateAsync(string serviceName)
        {
            if (serviceName == null)
            {
                throw new ArgumentNullException(nameof(serviceName), "Service name cannot be null.");
            }

            var existingService = (await _serviceRepository.GetAllAsync()).FirstOrDefault(s => s.ServiceName == serviceName);
            if (existingService != null)
            {
                throw new EntityNotUniqueException($"Service with name '{serviceName}' already exists.");
            }

            // If service name is unique, create the new service
            var newService = new Services { ServiceName = serviceName };
            var newServiceHistory = new ServicesHistory { ServiceName = serviceName };

            _serviceRepository.CreateAsync(newService, newServiceHistory);
        }

        public async Task DeleteAsync(int id)
        {
            if (id == default)
            {
                throw new ArgumentException("Service ID cannot be the default value.", nameof(id));
            }

            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null)
            {
                throw new NotFoundException($"Service with ID {id} not found.");
            }

            var latestHistory = (await _serviceRepository.GetAllHistoryByIdSortedByLatestAsync(id)).FirstOrDefault();
            if (latestHistory == null)
            {
                throw new NotFoundException($"History record for Service with ID {id} not found.");
            }

            _serviceRepository.DeleteAsync(service, latestHistory);
        }

        public async Task<IEnumerable<Services>> GetAllAsync()
        {
            return await _serviceRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ServicesHistory>> GetAllHistoryByIdSortedByLatestAsync(int id)
        {
            if (id == default)
            {
                throw new ArgumentException("Service ID cannot be the default value.", nameof(id));
            }
            return await _serviceRepository.GetAllHistoryByIdSortedByLatestAsync(id);
        }

        public async Task<Services> GetByCreationDateAsync(DateTime creationDate)
        {
            return await _serviceRepository.GetByCreationDateAsync(creationDate);
        }

        public async Task<Services> GetByIdAsync(int id)
        {
            if (id == default)
            {
                throw new ArgumentException("Service ID cannot be the default value.", nameof(id));
            }
            return await _serviceRepository.GetByIdAsync(id);
        }

        public async Task<Services> GetServiceByNameAsync(string serviceName)
        {
            if (serviceName == null)
            {
                throw new ArgumentNullException(nameof(serviceName), "Service name cannot be null.");
            }
            var service = (await _serviceRepository.GetAllAsync()).FirstOrDefault(s => s.ServiceName == serviceName);
            if (service == null)
            {
                throw new NotFoundException($"Service with name '{serviceName}' not found.");
            }
            return service;
        }

        public async Task UpdateAsync(int id, string newServiceName)
        {
            if (id == default)
            {
                throw new ArgumentException("Service ID cannot be the default value.", nameof(id));
            }
            if (newServiceName == null)
            {
                throw new ArgumentNullException(nameof(newServiceName), "Service name cannot be null.");
            }
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null)
            {
                throw new NotFoundException($"Service with ID {id} not found.");
            }

            var latestHistory = (await _serviceRepository.GetAllHistoryByIdSortedByLatestAsync(id)).FirstOrDefault();
            if (latestHistory == null)
            {
                throw new NotFoundException($"History record for Service with ID {id} not found.");
            }

            service.ServiceName = newServiceName;
            _serviceRepository.UpdateAsync(service, latestHistory);
        }
    }
}