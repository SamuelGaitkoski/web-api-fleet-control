using FleetControlWebAPI.Models.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Essa é a classe DTO para nosso veículo.
// Pois vamos fazer configurações para nossas classes e entidades, e vamos precisar utilizar uma classe de veículo simples,
// que tenha só os campos do veículo, independente de configuração de BD.
// construtor da classe é um método que declaramos da seguinte forma: public nomeDaClasse() {}, é um método sem tipo de retorno, 
// somente tem o operador de visibilidade e o nome da nossa classe. O construtor permite que inicializemos nosso objeto,
// podemos inserir propriedades como parametros na assinatura do método ou não, nesse caso precisamos de um construtor sem parametros.
// Nosso método ConverterParaEntidade faz a conversão do objeto DTO para um objeto Veículo da entidade.
// No método construímos um objeto de entidade do tipo Veiculo, atribuindo para ele os valores que vem do DTO (usando o this).
// this nesse caso se refere ao objeto em questão, que é o VeiculoDto nesse caso.

namespace FleetControlWebAPI.Models.Domain.Dto
{
    public class VeiculoDto
    {
        public string VeiculoId { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public int Ano { get; set; }

        public VeiculoDto()
        {

        }

        public Veiculo ConverterParaEntidade()
        {
            var novoVeiculo = new Veiculo();
            // se o Id não for diferente de nulo ou vazio, atribui o valor que tem na dto, se tiver nulo usa o método
            // GenerateId da classe BaseEntity e pede para ele gerar um id para nós, se for um objeto novo.
            // Usando o método IsNullOrEmpty da classe String, do pacote System.
            novoVeiculo.VeiculoId = string.IsNullOrEmpty(this.VeiculoId) ? this.VeiculoId : BaseEntity.GenerateId();
            novoVeiculo.Modelo = this.Modelo;
            this.Placa = this.Placa;
            novoVeiculo.Ano = this.Ano;

            return novoVeiculo;
        } 
    }
}
