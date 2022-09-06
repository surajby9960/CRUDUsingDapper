using CRUDUsingDapper.Model;

namespace CRUDUsingDapper.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        public  Task<IEnumerable<Company>> GetAllComapnies();
        public Task<Company> GetCompanyById(int id);
        public Task InsertCompany(InsertCompany insertCompany);
        public Task UpdateCompany(int id, UpdateCompany updateCompany);
        public Task DeleteCompany(int id);
    }
}
