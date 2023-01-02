using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskSigningOffer.Models;

namespace TestTaskSigningOffer.Repository
{
    public class ContractRepository : IContractRepository
    {
        private static List<Contract> contractList = new List<Contract>();

        public Contract GetContract(int id)
        {
            return contractList.First(m =>m.Id == id);
        }

        public int NewContract(Contract contract)
        {
            int id = contractList.Count() != 0 ? contractList.Last().Id + 1 : 1;
            contract.Id = id;
            contractList.Add(contract);
            return id;
        }

        public void SigContractClient(int id)
        {
            throw new System.NotImplementedException();
        }

        public void SigContractCompany(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
