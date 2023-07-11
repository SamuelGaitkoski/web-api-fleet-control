using FleetControlWebAPI.Models.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Em Repositories criamos uma interface, que é o repositório do Veiculo, repositório apenas do Veiculo.
// Podemos fazer um repositório específico para o Motorista se preferirmos, mas vamos colocar o Motorista aqui junto com o Veiculo.
// Essa interface vai ser publica (public).
// Vai ter o método Cadastrar, que vai receber um veículo.
// Uma interface é onde definimos as regras que nossa classe que vai implementar o repositório precisa implementar, é o nosso contrato.
// quando a classe possui uma interface, ela tem que implementar os métodos/propriedades definidos na interface.
// da forma como o código está configurado aqui, ele está configurado para trabalhar de forma síncrona, que é quando mandamos algo
// e ele executa um passo de cada vez. No .NET também podemos trabalhar de forma assíncrona com nossas requisições, distribuindo
// tarefas em paralelo, algo muito usado ao trabalhar com microserviços, então vamos alterar nossa interface do veículo para trabalhar
// de forma assíncrona. Para isso, tiramos o retorno void do método Cadastrar e trocamos por Task, que vem do pacote de thread do System.
// Nos métodos onde não temos retorno nenhum, que temos void no caso, trocamos por Task. Trocamos isso nos métodos Cadastrar, Atualizar
// e Excluir, que são métodos que não retornam nada. Nos métodos que temos um tipo de retorno, botamos uma Task que retorna alguma coisa,
// essa coisa que é o tipo de dado que os métodos estavam retornando, é uma Task tipada (Task<>), fizemos isso para os métodos Listar,
// Listar2 e Pesquisar, que são métodos que tem retorno de dado. Fizemos isso para os métodos trabalharem de forma assíncrona,
// dessa forma os métodos da nossa interface trabalham de forma assíncrona.
// Nossa interface do repositório irá trabalhar de forma assíncrona por meio da utilização do Task.

namespace FleetControlWebAPI.Models.Infrastructure.Repositories
{
    // temos que declarar nossa interface do repositorio como public, para que possamos criar um objeto do tipo VeiculoRepositorio
    // na classe de implementação do servico (VeiculoServiceImpl). Para que nossa interface do repositório seja acessível e visível.
    public interface VeiculoRepositorio
    {
        Task Cadastrar(Veiculo veiculo);
        Task Atualizar(Veiculo veiculo);
        // forma 1 para listar veículos
        Task<List<Veiculo>> Listar();
        // forma 2 para listar veículos
        Task<List<Veiculo>> Listar2();
        Task Excluir(string veiculoId);
        //passamos o id do veiculo e ele nos retorna o Veiculo
        Task<Veiculo> Pesquisar(string veiculoId);
    }
}
