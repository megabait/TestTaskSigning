using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskSigningOffer.Models.DocumentGeneration;
using TestTaskSigningOffer.Repository;

namespace TestTaskSigningOffer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //services.AddMvc();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IContractRepository, ContractRepository>();
            services.AddTransient<IDocumentGeneration, PdfGeneration>();
            services.AddControllers();
            services.AddSwaggerGen(
                options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "���������� ������� API",
                    Description = "��������� ����������� �������� ����������� ��� � ���������� QR ���� \r\n"+
                    "1. ������� ����� �������� -> �������� id �������� \r\n" +
                    "2. ������� ����� �������� � ��������� ���� idCompany -> �������� ������ �� url �������� (pdf � ������� base64) \r\n"+
                    "3. ����������� ������ \r\n" +
                    "4. ����������� ��������",
                });
            }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();
            //app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
