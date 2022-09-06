using CRUDUsingDapper.Context;
using CRUDUsingDapper.Model;
using CRUDUsingDapper.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace CRUDUsingDapper.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _context;
        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetAllComapnies()
        {
            List<Company> comapnies = new List<Company>();
            var qry = "select * from company";
            using(var connection=_context.CreateConnection())
            {
                var company = await connection.QueryAsync<Company>(qry);
                comapnies=company.ToList();
                foreach(var comapn in comapnies)
                {
                    var res=await connection.QueryAsync<Employee>("Select * from employee where cid=@cid", new {@cid= comapn.CId});
                    comapn.emplist = res.ToList(); 
                }
               return comapnies;
            }
        }
        public async Task<Company> GetCompanyById(int id)
        {
            Company cmp=new Company();
            var qry = "select * from company where cid=@cid";
            using (var connection = _context.CreateConnection())
            {
                cmp=await connection.QuerySingleOrDefaultAsync<Company>(qry, new {@cid=id});
                if(cmp!=null)
                {
                    var empls=await connection.QueryAsync<Employee>("select * from employee where cid=@cid",new {@cid=id});
                    cmp.emplist = empls.ToList();
                }
                return cmp;
            }
        }
        public async Task InsertCompany(InsertCompany insertCompany)
        {
            var qry = "insert into company(cname,caddress) values(@cname,@caddress)";
            var parameter = new DynamicParameters();
            parameter.Add("cname", insertCompany.CName, DbType.String);
            parameter.Add("caddress", insertCompany.CAddress, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                var res= await connection.ExecuteAsync(qry,parameter);
                
            }
        }
        public async Task UpdateCompany(int id,UpdateCompany updateCompany)
        {
            var qry = "update company set cname=@cname,caddress=@caddress where cid=@cid";
            var parameter = new DynamicParameters();
            parameter.Add("cid",id, DbType.Int32);
            parameter.Add("cname",updateCompany.CName, DbType.String);
            parameter.Add("caddress", updateCompany.CAddress, DbType.String);
            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(qry,parameter);
            }
        }
        
        public async Task DeleteCompany(int id)
        {
            var qry = "delete from company where cid=@cid";
            using(var conn = _context.CreateConnection())
            {
                await conn.ExecuteAsync(qry,new { @cid=id });
                await conn.QueryAsync("delete from employee where cid=@cid",new { @cid=id });
            }
        }
    }
   
}
