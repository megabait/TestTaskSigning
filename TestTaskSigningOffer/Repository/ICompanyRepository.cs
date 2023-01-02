using System.Linq;
using System.Threading.Tasks;
using TestTaskSigningOffer.Models;

namespace TestTaskSigningOffer.Repository
{
    public interface ICompanyRepository
    {
        int NewCompany(Company company);
        Company GetCompanyById(int id);
    }
}
