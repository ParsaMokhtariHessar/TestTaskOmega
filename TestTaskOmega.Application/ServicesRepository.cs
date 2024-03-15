using TestTaskOmega.Application.Contracts;
using TestTaskOmega.Application.Exeptions;
using TestTaskOmega.Application.RepositoryPattern;
using TestTaskOmega.Domain;

namespace TestTaskOmega.Application
{
    public class ServicesRepository : IServicesRepository
    {
        private readonly IRepository<Services, ServicesHistory> _serviceRepository;

        public ServicesRepository(IRepository<Services, ServicesHistory> serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public void Create(string serviceName)
        {
            if (serviceName == null)
            {
                throw new ArgumentNullException(nameof(serviceName), "Service name cannot be null.");
            }
            var existingService = _serviceRepository.GetAll().FirstOrDefault(s => s.ServiceName == serviceName);
            if (existingService != null)
            {
                throw new EntityNotUniqueException($"Service with name '{serviceName}' already exists.");
            }

            // If service name is unique, create the new service
            var newService = new Services { ServiceName = serviceName };
            var newServiceHistory = new ServicesHistory { ServiceName = serviceName };

            _serviceRepository.Create(newService, newServiceHistory);
        }

        public void Delete(int id)
        {
            if (id == default)
            {
                throw new ArgumentException("Service ID cannot be the default value.", nameof(id));
            }

            var service = _serviceRepository.GetById(id);
            if (service == null)
            {
                throw new NotFoundException($"Service with ID {id} not found.");
            }

            var latestHistory = _serviceRepository.GetAllHistoryByIdSortedByLatest(id).FirstOrDefault();
            if (latestHistory == null)
            {
                throw new NotFoundException($"History record for Service with ID {id} not found.");
            }

            _serviceRepository.Delete(service, latestHistory);
        }

        public IEnumerable<Services> GetAll()
        {
            return _serviceRepository.GetAll();
        }

        public IEnumerable<ServicesHistory> GetAllHistoryByIdSortedByLatest(int id)
        {
            if (id == default)
            {
                throw new ArgumentException("Service ID cannot be the default value.", nameof(id));
            }
            return _serviceRepository.GetAllHistoryByIdSortedByLatest(id);
        }

        public Services GetByCreationDate(DateTime creationDate)
        {
            return _serviceRepository.GetByCreationDate(creationDate);
        }

        public Services GetById(int id)
        {
            if (id == default)
            {
                throw new ArgumentException("Service ID cannot be the default value.", nameof(id));
            }
            return _serviceRepository.GetById(id);
        }

        public Services GetServiceByName(string serviceName)
        {
            if (serviceName == null)
            {
                throw new ArgumentNullException(nameof(serviceName), "Service name cannot be null.");
            }
            var service = _serviceRepository.GetAll().FirstOrDefault(s => s.ServiceName == serviceName);
            if (service == null)
            {
                throw new NotFoundException($"Service with name '{serviceName}' not found.");
            }
            return service;
        }

        public void Update(int id, string newServiceName)
        {
            if (id == default)
            {
                throw new ArgumentException("Service ID cannot be the default value.", nameof(id));
            }
            if (newServiceName == null)
            {
                throw new ArgumentNullException(nameof(newServiceName), "Service name cannot be null.");
            }
            var service = _serviceRepository.GetById(id);
            if (service == null)
            {
                throw new NotFoundException($"Service with ID {id} not found.");
            }

            var latestHistory = _serviceRepository.GetAllHistoryByIdSortedByLatest(id).FirstOrDefault();
            if (latestHistory == null)
            {
                throw new NotFoundException($"History record for Service with ID {id} not found.");
            }

            service.ServiceName = newServiceName;
            _serviceRepository.Update(service,latestHistory);
        }
    }
}
