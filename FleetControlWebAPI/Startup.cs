using FleetControlWebAPI.Models.Application.Services;
using FleetControlWebAPI.Models.Infrastructure.Contexts;
using FleetControlWebAPI.Models.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetControlWebAPI
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
            // dizendo para ele ignorar que nossa consulta est� certa, fazendo isso ele vai resolver o erro da refer�ncia do looping.
            services.AddControllers()
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // aqui no m�todo ConfigureServices incluimos:
            // vamos adicionar um contexto de banco (DbContext), que especifica o nome da nossa classe de context, que � a
            // ControleFrotaContext nesse caso.
            // na cria��o desse objeto do tipo ControleFrotaContext, vamos receber o options, que o objeto filho da classe recebe,
            // ent�o vamos passar o options como parametro, e no options vamos chamar a fun��o UseSqlServer, onde especificamos qual
            // vai ser nosso banco, agora dizemos qual vai ser nossa connectionString, para isso vamos usar o objeto Configuration
            // definido aqui na classe Startup, para pegar essa informa��o do arquivo appsettings, ent�o vamos passar o objeto Configuration,
            // que tem o m�todo GetConnectionString, para o qual vamos passar nossa connectionString, que geralmente � definida como padr�o
            // como DefaultConnection, conex�o padr�o. Para pegar essa conex�o, vamos no SQL Server, pegamos o nome do nosso servidor,
            // o meu no caso � SALMEIDA, para monstarmos a connectionString.
            // nossa connectionString vai ser: Data Source=localhost;Initial Catalog=base;Integrated Security=SSPI;Persist Security Info=False;
            // no appsettings definimos nossa connectionString como DefaultConnection e definimos qual � essa connection padr�o.
            // Com esse DefaultConnection quando inicializar a aplica��o nossa classe Startup vai buscar no DefaultConnection do appsettings
            // a conex�o, vai passar para nosso context (ControleFrotaContext), e nosso context (ControleFrotaContext) recebe nos parametros
            // do contrutor, no  DbContextOptions, no options no caso, dai ele consegue setar para n�s. 
            services.AddDbContext<ControleFrotaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configura��o das depend�ncias da classe que implementa a interface e a interface, do reposit�rio.
            // primeiro � a interface e depois a classe que implementa aquela interface.
            services.AddScoped<VeiculoRepositorio, VeiculoRepositorioImpl>();

            // j� implementamos acima a inje��o de depend�ncia do nosso reposit�rio, falta fazer isso para nosso servico.
            // primeiro adicionamos nossa interface e depois nossa classe de implementa��o da interface.
            services.AddScoped<VeiculoService, VeiculoServiceImpl>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
