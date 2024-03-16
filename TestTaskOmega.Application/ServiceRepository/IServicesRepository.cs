using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTaskOmega.Domain;

namespace TestTaskOmega.Application.Contracts
{
    public interface IServicesRepository
    {
        // Query
        Task<Services> GetByIdAsync(int id);
        Task<IEnumerable<Services>> GetAllAsync();
        Task<IEnumerable<ServicesHistory>> GetAllHistoryByIdSortedByLatestAsync(int id);
        Task<Services> GetByCreationDateAsync(DateTime creationDate);
        Task<Services> GetServiceByNameAsync(string serviceName);

        // Command
        Task CreateAsync(string serviceName);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, string newServiceName);
    }
}
