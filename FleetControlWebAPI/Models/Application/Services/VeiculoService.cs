using FleetControlWebAPI.Models.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Essa é a interface da classe de servico que teremos.
// Podemos criar como IVeiculoService ou VeiculoService também.
// Nossos métodos vão ser bem parecidos com o nosso repositório.
// Não estamos fazendo noss repositório e servico de forma genérica, como feito em outros projetos, para melhorar um pouco o código, estamos fazendo
// de forma direta.
// Pegamos os métodos da interface do VeiculoRepositorioImpl e colamos aqui.
// Agora vamos implementar nossa interface e os métodos dela.

namespace FleetControlWebAPI.Models.Application.Services
{
    // temos que declarar nossa interface do servico como public.
    public interface VeiculoService
    {
        Task Cadastrar(Veiculo veiculo);
        Task Atualizar(Veiculo veiculo);
        Task<List<Veiculo>> Listar();
        Task<List<Veiculo>> Listar2();
        Task Excluir(string veiculoId);
        Task<Veiculo> Pesquisar(string veiculoId);
    }
}
