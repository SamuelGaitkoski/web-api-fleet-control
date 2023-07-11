using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// WeatherForecastController é a controller padrão criada, para projeção do tempo.
// As Controllers nos dão acesso a nossa API, requisitamos via browser/postman o endereço e vai cair nos nossos métodos aqui.
// Vamos ter os Models também.
// Na classe Startup (Startup.cs) é onde fazemos todas as configurações da nossa aplicação, configuração da dependência, 
// configuração do BD, etc.
// Classe Program (Program.cs) é a principal, ela vai startar nossa aplicação, criando um webhost para nós, dai acessamos
// nossa aplicação por meio dele.
// no arquivo appsettings.json podemos colocar todas as configurações que precisamos para a aplicação, como log, 
// se precisamos de um diretório de arquivos para relatório, gerar alguma infomação, string de conexão do BD, caminho do banco,
// separamos essas configurações do BD nesse arquivo.
// Temos nossas dependências (Dependencies), onde podemos adicionar pacotes usando o NuGet, para incluirmos nossas dependências,
// vamos ter que incluir alguns pacotes para trabalhar com o Entity Framework Core.
// Pasta Properties temos algumas configurações da nossa aplicação, no arquivo launchSettings.json, configurações para acessar
// nossa aplicação, configuração da url que vamos acessar nossa aplicação, onde vamos iniciar nossa aplicação (IIS Express), 
// mudamos o launchUrl, para nossas api's acessarem pelo caminho api/weatherforecast.
// No cabeçalho, nas anotações que temos, que identificamos que é uma API de Web API, mudamos a rota.
// o [controller] pega o nome da nossa controller e tira o sufixo "Controller".
// Podemos startar nossa aplicação dando um "IIS Express", ele roda o método GET da controller, que retorna as informações que ele gera.
// Não vamos ter as páginas (views), vamos acessar via url. Podemos ter um ASP.NET MVC, aplicação usando algum framework front-end, 
// poderíamos conectar por meio da url da nossa api para consumir a API no front.
// Criamos a pasta Models, vamos usar o padrão MVC para a API, em model vai ter todo nosso código, incluindo a parte do EF Core.
// Controller vai fazer a comunicação desse modelo para disponibilizar os dados.
// Nossa view não vai ter uma página para fazer isso, vamos disponibilizar tudo na url, por meio da controller.
// DTO (Data Transfer Object), objeto simples para transferencia de objeto.
// pasta Contexts terá nossos contextos de aplicação do banco.
// pasta domain terá nossos domínios.
// Existem diversas formas de organização de projeto, entre pastas.

namespace FleetControlWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
