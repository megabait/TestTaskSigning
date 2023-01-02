using System.Linq;
using System.Threading.Tasks;
using TestTaskSigningOffer.Models;

namespace TestTaskSigningOffer.Repository
{
    public interface IContractRepository
    {
        int NewContract(Contract contract);
        Contract GetContract(int id);
        void SigContractClient(int id);
        void SigContractCompany(int id);
    }
}
