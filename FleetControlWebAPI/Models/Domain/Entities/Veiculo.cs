using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

// Relacionamento entre as classes Motorista e Veiculo.
// Relação de muitos para muitos, veículo para vários motoristas e motoristas que dirigem vários veículos.
// Para isso vamos criar uma classe intermediária que vai reunir as classes Motorista e Veiculo - MotoristaVeiculo.
// Além dos campos, nessa classe precisamos de uma configuração para o relacionamento do Veiculo e Motorista, para isso
// incluimos uma lista, do tipo IList, da classe MotoristaVeiculo, que faz relacionamento com a classe Veiculo e Motorista,
// nessa caso é o Veiculo relacionado com Motoristas.
// Criamos o construtor da classe Veiculo também, que vai ver o VeiculoId, e se ele for vazio vai gerar um novo id pelo
// método GenerateId() da classe EntidadeBase, se não deixa o VeiculoId vazio mesmo.
// Vamos fazer algumas customizações nas nossas tabelas, tabela foi criada com o mesmo nome do nosso DbSet definido na classe Context,
// la botamos os nomes no plural pois as entidade representam uma coleção da nossa classe, temos uma coleção de veículos então chamamos
// o DbSet de Veiculos, e o padrão que ele usa para criar as tabelas é o mesmo nome do DbSet, mas geralmente as tabelas não são criadas no 
// plural, mas no singular, então o certo seria criar a tabela como Veiculo. Para dizermos para o Entity criar a tabela com outro nome que não
// seja o nome definido no DbSet da tabela no context. Vamos ver uma forma de customizar o nome de tabela do banco, os nossos campos, também, que 
// estão como nvarchar do tipo max e queremos campos string como varchar. Uma das formas de fazer isso é virmos na nossa própria classe, e fazermos
// isso por meio de uma anotação. Para especificarmos o nome da nossa tabela, vamos a cima da classe e colocamos [Table("veiculo")], para isso
// importamos o pacote System.ComponentModel.DataAnnotations.Schema. Para atualizarmos na tabela do banco damos um Drop-Database, 
// Add-Migration AlteracaoNomeTabelaVeiculo, criamos uma nova migração, que vai renomear a tabela Veiculos para tbl_veicl, Update-Database.
// Deu certo, continua normal os relacionamentos, o VeiculoId da nossa tabela de relacionamento referenciando o VeiculoId de tbl_veicl.
// Agora vamos fazer algumas customizações, alterar o nome do campo, usando o Column(""), passando o nome da coluna no campo, colocamos também
// o TypeName, passando o tipo de dado da coluna, que vai ser um varchar de 50 caracteres. Fazemos uma anotação (annotation) dentro de [] (chaves).
// Assim alteramos as tabelas e colunas do banco de dados, utilizando as Annotations (anotações). Após isso fazemos Drop-Database,
// Add-Migration AlteracaoCamposTabelaVeiculo, Update-Database, ele renomeou o nome das colunas, mudou o tipo de dado das colunas.
// Vamos criando migrações e vão ficando muitas migrações, com arquivos que tem cada versão do nosso banco de dados, para não poluir tanto,
// vamos pedir para ele criar uma migração com todas essas alterações para nós, então excluimos a pasta Migrations no nosso Solution Explorer,
// vamos recriar ela, também damos um Drop-Database, para não termos muitas migrações ali, então damos um Add-Migration CriacaoBanco, então
// ele mudou o nome da nossa tabela, tipou as colunas, nossa tabela MotoristaVeiculo está com as chaves conforme definimos, é bom acompanharmos
// o histórico das migrações, mas podemos deletar a pasta de migrações e criar do zero também. Podemos também criar/subir o banco em uma versão
// específica de uma migration, dando um Update-Database + especificar o nome da migração, conseguimos subir naquela versão, um exemplo disso
// é: Update-Database CriacaoBanco, então podemos ver que ele alterou o tipo de dado e tamanho das colunas para nós. Temos 2 caminhos, fazer 
// alterações e customizações utilizando o modelBuilder no OnModelCreating do nosso context, ou por meio de Annotations na nossa classe.
// Veiculo tem as Annotations e Motorista vamos customizar por meio do modelBuilder, para vermos as 2 formas de fazer essas customizações.
// Vimos como configurar nossa classe no Entity por meio das Annotations, que vem do pacote DataAnnotations.Schema.


namespace FleetControlWebAPI.Models.Domain.Entities
{
    [Table("tbl_veicl")]
    public class Veiculo
    {
        [Column("veicl_id", TypeName = "varchar(50)")]
        public string VeiculoId { get; set; }
        [Column("modelo", TypeName = "varchar(100)")]
        public string Modelo { get; set; }
        [Column("placa", TypeName = "varchar(20)")]
        public string Placa { get; set; }
        [Column("ano", TypeName = "int")]
        public int Ano { get; set; }
        public IList<MotoristaVeiculo> Motoristas { get; set; }

        public Veiculo()
        {
            VeiculoId = string.IsNullOrEmpty(VeiculoId) ? BaseEntity.GenerateId() : string.Empty;
        }
    }
}
