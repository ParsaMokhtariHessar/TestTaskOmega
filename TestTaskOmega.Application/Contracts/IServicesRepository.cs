using TestTaskOmega.Application.RepositoryPattern;
using TestTaskOmega.Domain;

namespace TestTaskOmega.Application.Contracts
{
    public interface IServicesRepository
    {
        //Query
        Services GetById(int id);
        IEnumerable<Services> GetAll();
        IEnumerable<ServicesHistory> GetAllHistoryByIdSortedByLatest(int id);
        Services GetByCreationDate(DateTime creationDate);
        Services GetServiceByName(string ServiceName);
        //Command
        void Create(string serviceName);
        void Delete(int id);       
        
        void Update(int id, string NewserviceName);
    }
}
