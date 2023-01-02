using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using TestTaskSigningOffer.Models.DocumentGeneration;
using TestTaskSigningOffer.Repository;

namespace TestTaskSigningOffer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractController : ControllerBase
    {
        private IContractRepository contractRepository;
        private ICompanyRepository companyRepository;
        private IHttpContextAccessor httpContextAccessor;

        public ContractController(IContractRepository contractRepository, IHttpContextAccessor httpContextAccessor, ICompanyRepository companyRepository)
        {
            this.contractRepository = contractRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.companyRepository = companyRepository;
        }
        [HttpPost]
        public string Post(Models.Contract contract)
        {
            try {
                contract.DateCreate = DateTime.Now;
                contract.SignSmsClient = false;
                contract.SignSmsCompany = false;

                int id = contractRepository.NewContract(contract);
                var request = httpContextAccessor.HttpContext.Request;
                var domain = $"{request.Scheme}://{request.Host}";
                return domain + "/api/Contract/" + id;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [HttpGet("{id}")]
        public string GetContract(int id)
        {
            try
            {
                var contract = contractRepository.GetContract(id);
                var company = companyRepository.GetCompanyById(id);
                IDocumentGeneration documentGeneration = new PdfGeneration();
                return documentGeneration.CreateDocument(contract, company); ;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        [HttpGet("{id}/{sms}/SignClient")]
        public string GetSignClient(int id, string sms)
        {
            try
            {
                var contract = contractRepository.GetContract(id);
                contract.Sms = sms;
                contract.SignSmsClient = true;
                var company = companyRepository.GetCompanyById(id);
                IDocumentGeneration documentGeneration = new PdfGeneration();
                return documentGeneration.CreateDocument(contract, company);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [HttpGet("{id}/SignCompany")]
        public string GetSignCompany(int id)
        {
            try
            {
                var contract = contractRepository.GetContract(id);
                if (contract.SignSmsClient == false)
                {
                    throw new Exception("Сначала должен подписать клиент");
                }
                contract.SignSmsCompany = true;
                var company = companyRepository.GetCompanyById(id);
                IDocumentGeneration documentGeneration = new PdfGeneration();
                return documentGeneration.CreateDocument(contract, company);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}