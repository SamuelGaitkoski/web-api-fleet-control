using FleetControlWebAPI.Models.Domain.Entities;
using FleetControlWebAPI.Models.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Essa é nossa classe de servico, classe que vai implementar o servico.
// Essa classe vai herdar os métodos da interface VeiculoService, então vai ter que implementar tudo o que foi definido na interface.
// Fizemos o "Implement interface" para ele implementar os métodos para nós, agora só temos que implementar eles da forma que precisamos mesmo.
// Vamos implementar os métodos da nossa classe de servico, que é simples de implementar pois nosso repositorio já faz tudo para nós, já faz 
// toda a implementação e vai até o context para montar nossas consultas.
// vamos incluir o async na implementação de todos os métodos que temos aqui, pois quando ele implementa os métodos vindo da interface, o async
// não vem nos métodos.
// implementamos nosso servico, que está acessando nosso repositorio e configuramos a dependência do servico na classe Startup.
// Na próxima vamos criar nossa controller, para poder acessar o servico e disponibilizar os dados da nossa API.
// Aqui implementamos a camada de servico da nossa aplicação.

namespace FleetControlWebAPI.Models.Application.Services
{
    public class VeiculoServiceImpl : VeiculoService
    {
        // Para podermos implementar nosso servico, precisamos utilizar os recursos do nosso repositório VeiculoRepositorio, então precisamos injetar
        // ele na nossa classe de servico.
        // Criando como readonly, para ele não alterar depois.
        // Podemos criar o objeto do tipo VeiculoRepositorio assim "veiculoRepositorio" ou com underscore "_veiculoRepositorio".
        private readonly VeiculoRepositorio _veiculoRepositorio;

        // criamos nosso construtor.
        // no nosso construtor incluímos nosso objeto veiculoRepositorio para fazer a injeção de dependência.
        // uma boa parte dos erros que vamos ter no nosso dia-a-dia, é porque esquemos de fazer a configuração da injeção de dependência.
        // Até agora temos nosso repositorio, sua interface e sua classe, e o servico também, sua interface e sua classe, então já vamos implementar
        // a configuração da injeção de dependência.
        // No .NET Core configuramos a injeção de dependência na nossa classe de Startup, no método ConfigureServices.
        public VeiculoServiceImpl(VeiculoRepositorio veiculoRepositorio)
        {
            // adicionamos para nosso objeto nosso objeto veiculoRepositorio.
            _veiculoRepositorio = veiculoRepositorio;
        }

        public async Task Atualizar(Veiculo veiculo)
        {
            // incluímos um try/catch, ou podemos botar usá-lo somente na controller também, para fazer o tratamento de erro.
            try
            {
                // chamando nosso VeiculoRepositorio aqui, chamando o método Atualizar, passando o veiculo recebido como parâmetro na função.
                // incluímos o await, pois o método e chamada é assíncrona.
                await _veiculoRepositorio.Atualizar(veiculo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Cadastrar(Veiculo veiculo)
        {
            // utilizando mesma estrutura com o try/catch, somente chamando o método Cadastrar do repositório.
            try
            {
                await _veiculoRepositorio.Cadastrar(veiculo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Excluir(string veiculoId)
        {
            // utilizando mesma estrutura com o try/catch, somente chamando o método Excluir do repositório.
            try
            {
                await _veiculoRepositorio.Excluir(veiculoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Veiculo>> Listar()
        {
            // como esses métodos Listar, Listar2 e Pesquisar tem um tipo de retorno, nossa implementação muda um pouco, adicionamos o return,
            // para retornarmos nosso objeto.
            try
            {
                return await _veiculoRepositorio.Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Veiculo>> Listar2()
        {
            // adicionamos o return e chamamos o método Listar2 do repositório, que não recebe nenhum parâmetro.
            try
            {
                return await _veiculoRepositorio.Listar2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Veiculo> Pesquisar(string veiculoId)
        {
            // adicionamos o return, pois o método retorna o objeto, então chamamos o método Pesquisar do repositorio, passando o veiculoId,
            // para que ele pesquise o Veiculo pelo veiculoId que recebemos e passamos para ele.
            try
            {
                return await _veiculoRepositorio.Pesquisar(veiculoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
