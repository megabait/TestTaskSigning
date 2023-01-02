using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TestTaskSigningOffer.Models;
using TestTaskSigningOffer.Repository;

namespace TestTaskSigningOffer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private ICompanyRepository companyRepository;
        public CompanyController(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        [HttpPost]
        public string Post(Company company)
        {
            try
            {
                return "id company: " + companyRepository.NewCompany(company);
            }
            catch (Exception ex)
            { 
                return "Error: " + ex.Message;
            }
        }
    }
}
