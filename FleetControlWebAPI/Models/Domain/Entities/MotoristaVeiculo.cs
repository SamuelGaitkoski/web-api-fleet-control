using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Temos um objeto do tipo Motorista e um objeto do tipo Veiculo para o relacionamento entre as classes Motorista e Veiculo.
// Vamos fazer uma configuração para nossa tabela de relacionamento MotoristaVeiculo, para resolver o problema do Migration de não encontrar
// o id que seria a chave primária para essa tabela, para resolver na hora colocamos esse campo MotoristaVeiculoId, mas queremos que as chaves
// dessa tabela sejam MotoristaId e VeiculoId, queremos também que sejam campos que vão se relacionar com a tabela Motorista e Veiculo.
// Podemos criar e recriar o banco de dados várias vezes pelo Update-Database, usando a migração que criamos.
// Vendo nossa tabela MotoristasVeiculos no banco, selecionando o nome da tabela e digitando alt + F1, ele abre todas as propriedades que temos
// para aquela tabela, ele criou nossa PK como sendo MotoristaVeiculoId, e ja criou o relacionamento entre as outras tabelas, criando restrições,
// criando as FK's de relacionamento, com as constraint. O relacionamento entre as tabelas está funcionando, mas ele está criando essa PK que
// que não precisamos, queremos que nossas próprias chaves estrangeiras sejam nossas chaves primárias.
// Tiramos o campo MotoristaVeiculoId e agora vamos fazer as configurações para definir nossas chaves primárias na nossa classe de context,
// no método OnModelCreating.

namespace FleetControlWebAPI.Models.Domain.Entities
{
    public class MotoristaVeiculo
    {
        public string MotoristaId { get; set; }
        public string VeiculoId { get; set; }
        public Motorista Motorista { get; set; }
        public Veiculo Veiculo { get; set; }
    }
}
