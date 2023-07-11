using FleetControlWebAPI.Models.Application.Services;
using FleetControlWebAPI.Models.Domain.Dto;
using FleetControlWebAPI.Models.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetControlWebAPI.Controllers
{
    // Criando nossa controller, indo na pasta Controllers, botão direito, Add -> Controller, adicionando uma Controller Empty.
    // alterando para a classe herdar da interface ControllerBase.
    // Colocando anotações para ele fazer o roteamento.
    // Não vamos trabalhar com actions, vamos trabalhar com nossos métodos da API.
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoController : ControllerBase
    {
        // para funcionar precisamos usar o serviço, então vamos declarar ele, nosso objeto veiculoService.
        private readonly VeiculoService _veiculoService;

        // criando nosso construtor da controller, passando nosso serviço ali, fazendo a injeção de dependência.
        // Relembrando, ja criamos a configuração do serviço no Startup.cs, referenciando qual a interface e qual a classe.
        public VeiculoController(VeiculoService veiculoService)
        {
            _veiculoService = veiculoService;
        }

        // criando a url da nossa api para poder listar os .
        // Anotação para dizer que é uma requisição GET e configuração do roteamento com o Route que vai ser o listar
        // então se o usuário colocar http localhost, a porta da aplicação/api/veiculo/listar ele vai bater aqui na requisição e retorna os dados.
        [HttpGet]
        [Route("listar")]
        public async Task<List<Veiculo>> Listar()
        {
            // estamos criando bem simples, mas poderíamos criar nossas classes customizadas de exception e fazer uma tratativa diferente.
            // Aqui é só para vermos o Entity em ação.
            try
            {
                return await _veiculoService.Listar();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // método cadastrar.
        // vamos retornar só uma string, poderíamos retornar uma resposta 200 da requisição http, mas vamos simplificar retornando uma string,
        // para dizer se foi tudo certo ou que deu algum problema.
        // o método cadastrar do serviço recebe um objeto veículo como parametro, como pegamos então o dado la do browser ou da nossa aplicação para
        // poder tratar ela no nosso projeto, vamos receber no Cadastrar da controller um objeto dto VeiculoDto que criamos, um objeto simples
        // que tem as propriedades, um construtor e converte para nós para entidade depois, precisamos dele para recebermos essas informações.
        // para que o código entenda precisamos anotar o parametro como [FromBody], que pega do corpo da nossa aplicação/requisição para ele entender
        // certinho os valores.
        // Ele ta dando um erro, pois o veiculo é uma classe dto, e o método cadastrar ta esperando receber uma entidade veiculo, então precisamos
        // converter o veiculo para entidade, usando o método que criamos no objeto VeiculoDto.
        // anotando que é uma requisição post, e roteando para cadastrar
        [HttpPost]
        [Route("cadastrar")]
        public async Task<string> Cadastrar([FromBody] VeiculoDto veiculo)
        {
            try
            {
                var entidade = veiculo.ConverterParaEntidade();
                await _veiculoService.Cadastrar(entidade);
                return "Cadastro efetuado com sucesso!";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Listagem mais completa
        [HttpGet]
        [Route("listar2")]
        public async Task<List<Veiculo>> Listar2()
        {
            try
            {
                return await _veiculoService.Listar2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Não criamos todos os métodos, mas a idéia é a mesma, dai podemos criar o atualizar, excluir, os métodos que faltaram.
        // Vamos testar isso na próxima aula utilizando o Postman para isso.
        // O postman nos permite fazer as requisições com url.
        // O listar até podemos ver via chrome, mas para podermos fazer um teste de post precisamos mandar os dados, ai vai ser o postman
        // que vai nos permitir fazer isso.
        // Vamos testar tudo que implementamos aqui.
        // Testando listar (consulta simples) no chrome: localhost:55455/api/veiculo/listar
        // localhost:55455/api/veiculo/listar2 está com erro de referência circular, temos veiculos que possuem motoristas, mas motoristas possuem veiculo,
        // dai da esse erro.
        // cadastrando pela url: localhost:55455/api/veiculo/cadastrar, no postman, em Body -> raw e JSON, passando um objeto.
        // instalando pacote: Microsoft.AspNetCore.Mvc.NewtonsoftJson versão 3.1.19 no projeto.

        // pesquisa, exclusão e atualização dos veículos, se quisermos fazer para a parte de carga, a idéia é a mesma, só vamos alterar
        // o tipo de dado para a carga.

        // atualizar:
        // bem parecido com o cadastrar, mas usamos o HttpPut, para atualizar, roteamento para o atualizar, FromBody para ele entender
        // a entrada dos dados.
        [HttpPut]
        [Route("atualizar")]
        public async Task<string> Atualizar([FromBody] VeiculoDto veiculo)
        {
            try
            {
                var entidade = veiculo.ConverterParaEntidade();
                await _veiculoService.Atualizar(entidade);
                return "Atualização efetuada com sucesso!";
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // pesquisar:
        // vamos passar um id na requisição, dessa forma para que o id passado na requisição seja recebido no método.
        // retorno do método Pesquisar vai ser do tipo Veiculo, estamos usando Task pois o método é assíncrono.
        [HttpGet]
        [Route("pesquisar/{id}")]
        public async Task<Veiculo> Pesquisar(string id)
        {
            try
            {
                return await _veiculoService.Pesquisar(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // excluir:
        // para o método de exclusão vai ser usado o HttpDelete, rota excluir passando o id como parametro, seguindo a mesma idéia
        // do pesquisar, o id do roteamento é o parametro id do método Excluir.
        [HttpDelete]
        [Route("excluir/{id}")]
        public async Task<string> Excluir(string id)
        {
            try
            {
                await _veiculoService.Excluir(id);
                return "Exclusão efetuada com sucesso!";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // só testar tudo agora!!!
        // url exemplo de teste de requisição para o método de pesquisa de veículo:
        // localhost:55455/api/veiculo/pesquisar/8d93e52d-36df-4c6b-9505-239a9ac54966
    }
}
