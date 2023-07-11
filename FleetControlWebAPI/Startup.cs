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
            // dizendo para ele ignorar que nossa consulta está certa, fazendo isso ele vai resolver o erro da referência do looping.
            services.AddControllers()
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // aqui no método ConfigureServices incluimos:
            // vamos adicionar um contexto de banco (DbContext), que especifica o nome da nossa classe de context, que é a
            // ControleFrotaContext nesse caso.
            // na criação desse objeto do tipo ControleFrotaContext, vamos receber o options, que o objeto filho da classe recebe,
            // então vamos passar o options como parametro, e no options vamos chamar a função UseSqlServer, onde especificamos qual
            // vai ser nosso banco, agora dizemos qual vai ser nossa connectionString, para isso vamos usar o objeto Configuration
            // definido aqui na classe Startup, para pegar essa informação do arquivo appsettings, então vamos passar o objeto Configuration,
            // que tem o método GetConnectionString, para o qual vamos passar nossa connectionString, que geralmente é definida como padrão
            // como DefaultConnection, conexão padrão. Para pegar essa conexão, vamos no SQL Server, pegamos o nome do nosso servidor,
            // o meu no caso é SALMEIDA, para monstarmos a connectionString.
            // nossa connectionString vai ser: Data Source=localhost;Initial Catalog=base;Integrated Security=SSPI;Persist Security Info=False;
            // no appsettings definimos nossa connectionString como DefaultConnection e definimos qual é essa connection padrão.
            // Com esse DefaultConnection quando inicializar a aplicação nossa classe Startup vai buscar no DefaultConnection do appsettings
            // a conexão, vai passar para nosso context (ControleFrotaContext), e nosso context (ControleFrotaContext) recebe nos parametros
            // do contrutor, no  DbContextOptions, no options no caso, dai ele consegue setar para nós. 
            services.AddDbContext<ControleFrotaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configuração das dependências da classe que implementa a interface e a interface, do repositório.
            // primeiro é a interface e depois a classe que implementa aquela interface.
            services.AddScoped<VeiculoRepositorio, VeiculoRepositorioImpl>();

            // já implementamos acima a injeção de dependência do nosso repositório, falta fazer isso para nosso servico.
            // primeiro adicionamos nossa interface e depois nossa classe de implementação da interface.
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
