using TestTaskOmega.Application.ApplicationModels;
using TestTaskOmega.Domain;
using TestTaskOmega.Domain.Utilities;

namespace TestTaskOmega.Application.Contracts
{
    public interface IServicesRepository
    {
        Task<ServiceResponse<Services>> GetByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<Services>>> GetAllAsync();
        Task<ServiceResponse<IEnumerable<Modifications<string>>>> GetHistory(int id);
        Task<ServiceResponse<Services>> GetByCreationDateAsync(DateTime creationDate);
        Task<ServiceResponse<Services>> GetServiceByNameAsync(string serviceName);
        Task<ServiceResponse<IEnumerable<Services>>> GetAllDeletedAsync();
        Task<ServiceResponse> CreateAsync(string serviceName);
        Task<ServiceResponse> DeleteAsync(int id);
        Task<ServiceResponse> UpdateAsync(int id, string newServiceName);
    }
}

