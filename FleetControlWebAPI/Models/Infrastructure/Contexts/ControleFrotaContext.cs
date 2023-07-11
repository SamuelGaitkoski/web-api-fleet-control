using FleetControlWebAPI.Models.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Essa é a nossa classe de contexto (Context), classe normal mas feita para o contexto.
// o context é quem faz a configuração do banco de dados e quem vai permitir o acesso com o banco de dados.
// Essa classe vai fazer a comunicação com o banco de dados.
// Essa classe herda do DbContext, classe que vem do pacote EntityFrameworkCore.
// A herança é o que usamos para uma classe herdar as características de outra, por meio dessa sintaxe com :
// Criamos o contrutor da classe, que serve para inicializar o objeto.
// Agora vamos trabalhar com o set de dados, cada uma das nossas entidades serão representadas por um DbSet, da seguinte forma:
// public DbSet<Veiculo> Veiculos { get; set; }
// Quando ele não reconhece alguma classe que colocamos, damos um ctrl + . para ele adicionar a referência, que é o using dizendo
// de onde vem aquela classe, etc.
// Veiculos nesse caso é um objeto, é um DbSet de Veiculo.
// Também vamos sobrescrever um método que já existe na classe DbContext, vamos sobrescrever o método por meio da palavra chave override,
// que é o onModelCreating, que serve para nós fazermos algumas configurações no momento de criar o banco de dados. Ele precisa receber
// como parametro um objeto do tipo ModelBuilder na assinatura do método, e dentro dele fazemos nossas configurações.
// essa é a base da nossa classe, que está relacionando as classes que criamos nossas entidades.
// para dizer para nossa classe qual o banco de dados que vamos utilizar e nos conectar, então passamos como parametro no construtor
// o DbContextOptions e vamos passar o parametro options do construtor para a classe filha, por meio do : base, passando o options
// como parametro: base(options). Para dizermos qual a classe especificamos no construtor essas opções de contexto do
// banco (DbContextOptions), fazemos isso na classe Startup, classe de configuração.
// Dessa forma temos aqui informando onde ele vai criar o nosso banco de dados e quais são as classes que ele vai usar para criar 
// essas informações.
// Fizemos agora a configuração do banco.
// O padrão do EF Core quando ele vai criar as tabelas, ele define as chaves primárias, que é o campo único de cada tabela. Por padrão,
// quando ele analisa uma classe ele vai ver o nome da classe seguido pelo id, que é nossa chave primária. Ele não achou o id da classe
// MotoristaVeiculo. Queremos criar uma tabela intermediária com essa classe, que vai ter como chave dois campos, MotoristaId e VeiculoId,
// precisamos configurar isso na mão, na parte de configuração isso para o migration. Podemos testar adicionando uma propriedade
// MotoristaVeiculoId, para ele achar a chave primária da classe MotoristaVeiculo. 
// Incluindo o campo na MotoristaVeiculo, ele da certo e cria para nós uma Migration, logo, quando criarmos uma classe e ela não precisar
// de uma chave composta, incluindo um campo como o nome da classe + Id, ele entende que esse campo é a chave primária da entidade.
// Após o migration dar certo, ele cria uma pasta na nossa aplicação chamada Migrations, coloca a data e o nome que colocamos para nossa
// migration, dentro do migration gerado tem o código SQL que ele vai gerar para nós, são as configurações que ele vai enviar para o BD
// para poder criar a base para nós, criando tabelas para nossas entidades. O migration mantém um histórico de tudo que formos criando,
// alterando, etc, dai podemos saber o que está ocorrendo no banco. No arquivo do migration de Snapshot tem outras configurações de
// relacionamento e etc para nosso banco de dados. 
// Vamo remover o campo MotoristaVeiculoId da classe MotoristaVeiculo, pois vamos criar uma chave composta para fazer o relacionamento,
// por hora, para simplificar, incluimos o campo MotoristaVeiculoId, para não dar erro na migração. 
// Ele não criou o banco de dados no nosso SQL Server, pois o migration não cria o banco, ele cria uma migração para que possamos
// gerar ou atualizar o nosso banco de dados. Para criar o banco de dados, rodamos um "Update-Database", dai ele vai fazer um build,
// como o build da criação do migration, avaliando as configurações e atualizamos que tenhamos feito, baseado nisso ele vai verificar
// se pode criar o banco. Vai executar os comandos CreateDatabase, aplicar os CreateTable, e quando concluir vai exibir "Done", informando
// que ele pode criar o banco de dados sem nenhum problema. Update-Database serve para aplicar a migração no banco de dados, e criar ele.
// Existem outros comandos também.
// Após o comando Update-Database, nosso banco de dados é criado, basta atualizarmos nosso servidor SQL, nossas tabelas também são 
// criadas, com o mesmo nome que colocamos para os nossos DbSet's, na tabela de context, com cada campo que temos nas classes
// transformados em colunas da tabela. Ele cria as colunas com tipos de dados com muitos caracteres, por exemplo VeiculoId sendo um
// nvarchar[450], o que são mais caracteres que precisamos. Vamos ver como ajustar as colunas das tabelas do BD depois.
// Podemos ajustar para não criar no plural o nome das tabelas usando uma configuração do Entity também. 
// quando formos fazer alterações, é comum usarmos o comando "Drop-Database", para ele excluir o banco de dados, nos permitindo aplicar
// alterações na aplicação, no contexto, para atualizarmos nossa estrutura de tabelas e de banco, então podemos criar outra migration
// e criar o banco a partir dessa migration. Fazemos isso quando estamos testando localmente, no nosso ambiente de desenvolvimento,
// em produção não fazemos isso, não excluímos o banco, é importante verificarmos o servidor que estamos para não apagar o que não devemos.
// O Drop-Database apaga nosso banco de dados.
// Dessa forma trabalhamos com o Migration.
// Comandos importantes: Add-Migration MigrationName, Update-Database, Drop-Database.
// Na pasta Migration que ele cria tem todas as informações que estão sendo aplicadas para a criação do banco de dados/tabelas/colunas/etc.
// Vamos ver como melhorar isso, para não precisarmos utilizar uma chave adicional, que é a primária, para nossa classe de relacionamento
// MotoristaVeiculo, e podermos fazer a chave composta com os campos MotoristaId e VeiculoId, e não precisar usar o MotoristaVeiculoId.
// Também veremos formas de configurar o nome das tabelas/campos e tipos de dados dos campos, trabalhando de forma diferente da que
// específicamos nas nossas classes. Também vamos implementar os métodos do nosso repositório. Vamos concluir a configuração do banco,
// utilizando as migrações e alguns recursos do EF Core, vamos desenvolver isso nos métodos da classe ControleFrotaContext,
// onModelCreating, etc.
// No método OnModelCreating vamos fazer as configurações das nossas chaves primárias, definindo nele as configurações do que não é padrão
// do entity.
// Queremos que quando nosso banco for publicado no SQL Server, quando ele for subir de fato, queremos já ter algumas informações pré-carregadas, 
// para ter algumas informações já pré-carregadas, para podermos validar nossa API listando e consultando os dados, pois até o momento não temos
// daods no nosso banco. Vamos ver hoje como fazemos para pré-carregar informações nas nossas tabelas.
// Existem as tabelas de domínio, que são tabelas com valores já definidos. Não temos tabelas de domínio aqui, mas já vamos carregar as informações
// de Motorista e Veiculo nas nossas tabelas.
// Para carregar informações nas nossas tabelas de Veiculo e Motorista, para podermos consultar esses dados, vamos utilizar o objeto modelBuilder,
// no método OnModelCreating, para isso vamos criar um método para não misturar o código e centralizar nele essa parte de pré-carregamento das 
// informações, no método InitializeData().

namespace FleetControlWebAPI.Models.Infrastructure.Contexts
{
    public class ControleFrotaContext : DbContext
    {
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<MotoristaVeiculo> MotoristasVeiculos { get; set; }

        public ControleFrotaContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureEntities(modelBuilder);

            // Chamamos o método InitializeData, passando o modelBuilder como parâmetro.
            // podemos subir essas informações para o banco de dados agora. 
            // Add-Migration InsertDadosTabelas, Update-Database (aplicar última migração criada no nosso banco).
            // Ele gerou nossa migração já com os registros, pegando os id's que ele gerou para nós no nosso método GenerateId da classe BaseEntity.
            // Agora, por meio desse método InitializeData, fizemos o insert de dados nas nossas tabelas do BD, então temos dados na nossa base.
            // Aprendemos como fazemos para subir dados ao inicializar nossa base.
            InitializeData(modelBuilder);

            // precisamos utilizar o método onModelCreating no base, passando nosso modelBuilder como parametro
            base.OnModelCreating(modelBuilder);
        }

        protected void ConfigureEntities(ModelBuilder modelBuilder)
        {
            // Esse onModelCreating vai ir ficando meio grande, conforme vamos alterando/customizando as tabela/campos. Por isso vamos criar um
            // método protegido ConfigureEntities, que é a configuração das nossas entidades. Para essa configuração precisamos no objeto 
            // ModelBuilder, então podemos passar todas essa parte de ajustes para o ConfigureEntities, dai chamamos ele no OnModelCreating,
            // passando o modelBuilder como parâmetro. Poderíamos criar um método para essas configurações de cada tabela, para ficar mais 
            // organizado, ou podemos manter aqui mesmo.
            // dividimos para um método para cada entidade, para alterar nas tabelas do banco.
            // Para as entidades MotoristaVeiculo e Motorista, configuramos utilizando o modelBuilder, para a entidade Veiculo, customizamos
            // os campos no banco utilizando as Annotations, mas o correto é adotarmos um padrão de configurações para as entidades, para facilitar
            // a manutenção do código.
            // Para aplicar essa alterações no nosso banco, damos um Add-Migration CriacaoDoBanco, Update-Database.
            // Se ele estiver dando um erro para o Update-Database que não existe, deletamos a pasta Migrations, criamos uma nova migração 
            // e damos o Update-Database novamente. Ele alterou nossas tabelas e campos corretamente, customizando nossas entidades e campos
            // por meio dessa configuração pelo modelBuilder (Fluent API). Já configuramos tudo agora podemos acessar esses dados.
            ConfigureEntityMotorista(modelBuilder);
            ConfigureEntityMotoristaVeiculo(modelBuilder);
        }

        protected void ConfigureEntityMotorista(ModelBuilder modelBuilder)
        {
            //Alterando o nome da tabela Motorista com o ToTable.
            modelBuilder.Entity<Motorista>()
                .ToTable("tbl_motor");

            // definindo a chave primária da entidade Motorista, usando o HasKey e especificando o campo MotoristaId
            modelBuilder.Entity<Motorista>()
                .HasKey(m => m.MotoristaId);

            // alteramos nosso MotoristaId da tabela MotoristaVeiculo para ser do tipo varchar(50), mas o campo MotoristaId da tabela Motorista
            // está como nvarchar, que é o padrão que o Entity cria uma coluna do tipo string. 
            // vamos configurar agora o nome dos campos. Para essa entidade vamos pegar uma property, que é uma propriedade da nossa entidade
            // Motorista nesse caso.
            // o nome da coluna pode ser diferente mas o tipo não, ele tem que ser o mesmo para não dar problema.
            modelBuilder.Entity<Motorista>()
                .Property(mv => mv.MotoristaId)
                .HasColumnName("motor_id")
                .HasColumnType("varchar(50)");

            // ajustando o nome da coluna do campo Nome da entidade Motorista.
            // para o campo ser obrigatório, que é NOT NULL, usamos o IsRequired(), assim definimos o campo como NOT NULL no banco.
            modelBuilder.Entity<Motorista>()
                .Property(mv => mv.Nome)
                .HasColumnName("nome")
                .HasColumnType("varchar(150)")
                .IsRequired();

            // ajustando o campo CNH da entidade Motorista
            modelBuilder.Entity<Motorista>()
                .Property(mv => mv.CNH)
                .HasColumnName("cnh")
                .HasColumnType("varchar(30)")
                .IsRequired();

            // ajustando o campo ValidadeCNH da entidade Motorista, ele criou esse campo no banco como datetime2, queremos a coluna seja
            // datetime mesmo.
            modelBuilder.Entity<Motorista>()
                .Property(mv => mv.ValidadeCNH)
                .HasColumnName("validadeCNH")
                .HasColumnType("datetime")
                .IsRequired();

            // ajustando o campo Ativo da entidade Motorista.
            // o tipo de dado bit no banco é boolean, que é 0 ou 1, que significa true/false
            modelBuilder.Entity<Motorista>()
                .Property(mv => mv.Ativo)
                .HasColumnName("ativo")
                .HasColumnType("bit")
                .IsRequired();
        }

        // configure da entidade de relacionamento (MotoristaVeiculo)
        protected void ConfigureEntityMotoristaVeiculo(ModelBuilder modelBuilder)
        {
            // configurações
            // configurações para a entidade de relacionamento (MotoristasVeiculos), que só existe para ligar as entidade Veiculos e Motoristas,
            // onde temos uma relação onde um motorista pode dirigir diversos veículos e um veículo pode ser dirigido por vários motorista,
            // uma relação muitos para muitos.
            // O objeto ModelBuilder nos permite fazer essas configurações.
            // Vamos configurar algo nas nossas entidades, por isso botamos o Entity, dai especificamos qual nossa entidade, podemos alterar
            // diversas coisas aqui, pelos métodos que ele nos disponibiliza quando damos o . , podemos definir tipo de dado, nome de campo,
            // nome de tabela, insert de dados iniciais, vamos fazer isso nas próximas aulas, mas aqui vamos focar em como definir uma chave 
            // composta de forma customizada, para isso usamos o HasKey, onde dizemos que nossa entidade tem uma chave, e precisamos configurar
            // isso, vamos usar com a notação do link para poder manipular objetos, vamos dizer que nossa entidade mv => mv. dai ele encontra
            // as propriedades para nós, selecionamos MotoristaId, que seria a chave primária da nossa entidade, mv é o parametro que usamos
            // para passar por cada campo/propriedade da nossa entidade. Configuração para uma tabela com uma só chave primária é:
            // modelBuilder.Entity<MotoristaVeiculo>().HasKey(mv -> mv.MotoristaId);
            // Mas, como nossa tabela tem um campo composto, chave primária definida por 2 campos, dizemos para o entity que queremos criar
            // uma nova chave, usando a palavra reservada new, dai abrimos as chaves e dizemos que nosso mv.VeiculoId, mv.MotoristaId, dessa forma
            // ele consegue configurar nossa chave primária para nós, que será VeiculoId e MotoristaId. Por causa das propriedades de navageção
            // que definimos nas classes que ele sabe que ele tem que fazer o relacionamento, ele sabe que tem um campo que tem o mesmo nome do Id
            // e tem o objeto que relaciona, logo o MotoristaId vai se relacionar com nossa classe Motorista e VeiculoId vai se relacionar com a
            // classe Veiculo, e dentro de cada uma das entidades tem essa chave primária, esse mesmo Id, com mesmo nome, mas também tem a lista
            // IList de objeto, que referencia a classe de relacionamento, e ali estabelecemos a relação, então le por meio da nossa classe de 
            // relação, ai ve que motorista tem a relação por meio da lista de objeto da classe de relacionamento, dai sabe que tem que fazer
            // essa relação entre as 2 classes, o mesmo ocorre com o relacionamento da classe MotoristaVeiculo e Veiculo, o relacionamento é de
            // n para n, por isso usamos uma lista de objetos MotoristaVeiculo, mas se quisessemos fazer um relacionamento de 1 para n, em vez
            // de uma lista usariamos um objeto simples, dai um Veiculo é dirigido por somente um Motorista, ai poderiamos fazer essa relação
            // também. Agora vamos no Package Manager Console, criamos uma nova migração pois alteramos nossa estrutura de banco, que tinha uma
            // chave a mais, a MotoristaVeiculoId, então damos um Add-Migration AtualizacaoNaTabelaRelacionamento, ele não deu nenhum problema,
            // temos nosso migration InitialCreate, agora ele criou o migration novo com o nome que especificamos, onde ele está removendo a
            // coluna MotoristaVeiculoId, que haviamos adicionado antes, agora está alterando a coluna no MotoristaId e adicionando nossa chave
            // primária para a tabela MotoristasVeiculos, usando os 2 campos para essa alteração, que é uma chave composta. Agora executamos o
            // Update-Database, mostrou "Done.", então foi feito. Olhando na nossa tabela, temos só os 2 campos agora, MotoristaId e VeiculoId,
            // que são FK's e PK's também agora, chaves primárias da nossa tabela, temos nossa chave composta agora, dessa forma fazemos 
            // essa configuração, que foi feita na classe de contexto, fazendo uma sobrescrita nesse método OnModelCreating que é do DbContext,
            // dai fazemos essa customização da nossa entidade definido uma chave composta, sendo necessária a utilização do new para a criação
            // da chave composta.
            // Botamos o ToTable para renomear o nome da nossa tabela, que é a mesma coisa que colocar a annotation Table para definir o nome
            // da tabela pelas annotations.
            modelBuilder.Entity<MotoristaVeiculo>()
                .ToTable("tbl_motr_veicl")
                .HasKey(mv => new { mv.VeiculoId, mv.MotoristaId });

            // nossa tabela Veiculo tem o campo VeiculoId, dai mudamos o nome da coluna desse campo, mas a entidade Veiculo se relaciona com
            // a tabela MotoristaVeiculo, pelo VeiculoId, então temos que customizar esse campo VeiculoId para essa tabela MotoristaVeiculo
            // também. Vamos alterar a propriedade VeiculoId, então selecionamos ela no Property, usamos o HasColumnName para alterar o nome da
            // coluna para o mesmo nome definido para coluna VeiculoId na entidade Veiculo, usamos também o HasColumnType, para mudar o tipo
            // de dado da coluna para também trabalhar com o varchar de 50 caracteres.
            modelBuilder.Entity<MotoristaVeiculo>()
                .Property(mv => mv.VeiculoId)
                .HasColumnName("veicl_id")
                .HasColumnType("varchar(50)");

            // Alterando o nome do campo da pk de MotoristaId do MotoristaVeiculo.
            // Para funcionar precisamos adicionar uma migração com as alterações, para ele criar as consultas para alterar o banco dessa forma,
            // então damos um Add-Migration AjusteTabelaMotorista.
            modelBuilder.Entity<MotoristaVeiculo>()
                .Property(mv => mv.MotoristaId)
                .HasColumnName("motor_id")
                .HasColumnType("varchar(50)");
        }

        // método de pré-carregamento de dados no nosso banco de dados, também utiliza o objeto ModelBuilder.
        // nossas entidades Veiculo e Motorista tem um campo de chave (id) que é uma string, que vai gerar um hash/guid para nós, isso é feito 
        // pelo método GenerateId do BaseEntity, vamos usar essa classe para podermos gerar as chaves no nosso método de inicialização dos
        // dados (InitializeData).
        // vamos criar primeiro um Motorista. 
        protected void InitializeData(ModelBuilder modelBuilder)
        {
            // Motorista
            var motoristaId = BaseEntity.GenerateId();

            // para iniciar a tabela de Motorista, usamos o modelBuilder, para trabalhar na parte de entidades, especificamos a entidade Motorista, 
            // temos o método HasData, que é o método que permite inserirmos uma informação em uma tabela, passamos para o método um novo objeto,
            // que é um new Motorista, e inserimos todos os dados do Motorista, inserimos dados para todas as propriedades de um Motorista.
            modelBuilder.Entity<Motorista>().HasData(
                new Motorista
                {
                    MotoristaId = motoristaId, 
                    Nome = "Samuel Almeida", 
                    CNH = "1234567891", 
                    ValidadeCNH = DateTime.Parse("23/02/2024"), 
                    Ativo = true
                }
            );

            // Veiculo
            var veiculoId = BaseEntity.GenerateId();

            modelBuilder.Entity<Veiculo>().HasData(
                new Veiculo
                {
                    VeiculoId = veiculoId,
                    Modelo = "Volvo FH 540",
                    Ano = 2019,
                    Placa = "ABC1234"
                }
            );

            // Agora inserimos na nossa tabela de relacionamento, para isso usamos o modelBuilder, usamos o Entity para alterar nossa entidade
            // MotoristaVeiculo, usamos o HasData para colocar dados no banco, então criamos um novo objeto MotoristaVeiculo, passando as chaves
            // das nossas tabelas, o MotoristaId vai receber a chave de Motorista e VeiculoId o veiculoId.
            modelBuilder.Entity<MotoristaVeiculo>().HasData(
                new MotoristaVeiculo { 
                    MotoristaId = motoristaId,
                    VeiculoId = veiculoId
                }
            );

            // Assim fazemos para fazer insert de um registro em cada uma das nossas tabelas.
            // Vamos gerar mais uma chave e vamos incluir mais um Motorista e mais um Veiculo.
            // vamos reaproveitar as variáveis e vamos gerar novas chaves para passar para os novos registros.
            motoristaId = BaseEntity.GenerateId();
            veiculoId = BaseEntity.GenerateId();

            modelBuilder.Entity<Motorista>().HasData(
                new Motorista
                {
                    MotoristaId = motoristaId,
                    Nome = "João da Silva",
                    CNH = "9876549283",
                    ValidadeCNH = DateTime.Parse("25/07/2025"),
                    Ativo = true
                }
            );

            modelBuilder.Entity<Veiculo>().HasData(
                new Veiculo
                {
                    VeiculoId = veiculoId,
                    Modelo = "Scania R450",
                    Ano = 2018,
                    Placa = "CBA4321"
                }
            );

            // podemos criar diversos registros, podemos montar uma base de dados maior, simulando a criação de uma massa de dados para a base, 
            // por meio de uma classe que cria diversos objetos para nós, passamos ele como parametro no método e fazemos um loop para ele criar
            // cada um dos nossos objetos e termos uma massa de dados maior. Teríamos que adaptar para usar o bloco do modelBuilder com o HasData, 
            // e setar o valor para cada campo. Agora precisamos incluir os dados na tabela de relacionamento para esses novos registros.
            modelBuilder.Entity<MotoristaVeiculo>().HasData(
                new MotoristaVeiculo
                {
                    MotoristaId = motoristaId,
                    VeiculoId = veiculoId
                }
            );
        }
    }
}
