using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using TestTaskSigningOffer.Models;

namespace TestTaskSigningOffer.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private static List<Company> ListCompany = new List<Company>();
        public int NewCompany(Company company)
        {
            int id = ListCompany.Count() != 0 ? ListCompany.Last().Id + 1 : 1;
            company.Id = id;
            ListCompany.Add(company);
            return id;
        }
        public Company GetCompanyById(int id)
        {
            return ListCompany.First(m=>m.Id == id);
        }
    }
}
