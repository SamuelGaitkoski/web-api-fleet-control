using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// nossas classes vão ser nossas entidades.
// para criar as propriedades digitamos "prop" e damos tab 2 vezes.
// Colocamos primeiro o tipo de dado, depois o nome da propriedade.
// Como queremos que nossa propriedade seja acessível botamos o operador de visibilidade public.
// Temos nossos métodos get e set para acessar e setar valor para cada propriedade.
// Dependendo do que estiver fazendo podemos deixar só o set disponível e também criar objeto pelo construtor.
// Em Motorista fazemos a mesma configuração para o relacionamento com Veiculo, mas o Motorista se relaciona com Veiculos.
// Relacionamos as classes Motorista e Veiculo por meio da classe intermediária MotoristaVeiculo, relacionando as classes pelo
// IList tanto em Motorista quanto em Veiculo.

namespace FleetControlWebAPI.Models.Domain.Entities
{
    public class Motorista
    {
        public string MotoristaId { get; set; }
        public string Nome { get; set; }
        public string CNH { get; set; }
        public DateTime ValidadeCNH { get; set; }
        public bool Ativo { get; set; }
        public IList<MotoristaVeiculo> Veiculos { get; set; }
    }
}
