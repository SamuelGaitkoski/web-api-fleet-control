using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Projeto de .NET Core usando o Entity Framework Core.
// Criando projeto do tipo ASP.NET Core Web Api - .NET Core 3.1 (versão muito estável) - Configure for HTTPS desmarcado.
// Ferramenta Entity Framework Core (EF Core), mais conhecida como Entity.
// O Entity é um recurso que temos para desenvolver nossas soluções e fazer a parte de acesso aos dados (tabelas do BD).
// Vamos fazer isso sem precisar programar no SQL, o Entity se encarrega de fazer isso para nós.
// Entity é uma ferramenta ORM (Object Relational Mapper - Mapeamento de Objeto Relacional).
// O Entity é quem vai fazer o mapeamento dos nossos objetos, vamos trabalhar com o banco relacional por meio das classes.
// O Entity monta as consultas para nós, trabalhamos com os objetos dai.
// As nossas classes são nossas entidades e vão virar nossas tabelas do SQL, sem precisarmos programarmos em SQL.
// Vamos nos comunicar com nosso BD usando essa api que vamos criar.
// Precisamos do pacotes também, adicionando dependências no nosso projeto, usando o NuGet Package Manager.
// O NuGet é a ferramenta que usamos no .NET, semelhante ao Maven do Java, para fazer a gestão dos pacotes/dependências
// do nosso projeto.
// Vamos fazer uma aplicação (API) para fazer o controle de uma frota de veículos.
// Instalação dos seguintes pacotes do Entity Framework Core no NuGet:
// Pacotes do EF Core:
// Microsoft.EntityFrameworkCore - v3.1.19
// Microsoft.EntityFrameworkCore.Design - v3.1.19
// Microsoft.EntityFrameworkCore.Relational - v3.1.19
// Pacotes do SQL Server para o EF Core:
// Microsoft.EntityFrameworkCore.SqlServer - v3.1.19
// Microsoft.EntityFrameworkCore.SqlServer.Design - v1.1.6
// Pacote do EF Core para as ferramentas para a parte do migration, migração, geração do banco de dados e criação de tabelas:
// Microsoft.EntityFrameworkCore.Tools - v3.1.19
// 2 formas de trabalhar com o Entity: Criar um BD a partir dele ou montar toda a estrutura a partir de um BD já existente.
// Nesse projeto o nosso BD vai ser gerado a partir do Entity. vamos ver 2 formas de fazer isso.
// Botão direito em dependencias -> Manage NuGet Packages.
// Do .NET 3 foi pro 5, não 4. Antes do .NET Core tinha o .NET Framework, que tava na v4 quando o .NET Core foi lançado, 
// para não ter uma confusão entre .NET Core v4 e .NET Framework v4, Microsoft lançou v5, que faz a fusão dos 2 .NET, os 2 na v5.
// Podemos ver as dependencias instaladas em Dependencies, vendo os pacotes, arquivo do projeto .csproj, em PackageReference.
// Nosso contexto é o que vai fazer a ponte com o banco de dados.
// Nosso contexto da aplicação foi configurado para fazer a ponte com o banco de dados através do Entity.
// A classe que vai fazer a ponte com o banco é a ControleFrotaContext, e por meio dela vamos acessar nossas entidades e
// tabelas no SQL Server. Configuramos cada DbSet para fazer a ponte com cada tabela, construtor com o DbCOntextOptions, no
// qual dizemos qual vai ser nosso banco de dados. 
// Nosso arquivo appsettings.json tem que existir no nosso projeto quando ele for compilar, para ele pegar a ConnectionStrings. 
// Pode ocorrer de estarmos debugando e não encontrar esse arquivo na pasta do build, que ele gera. Para termos certeza que esse
// arquivo esta sendo gerado, damos botão direito nele -> Properties -> Copy to Output Directory como Copy Always, para ele sempre
// gerar o arquivo appsettings.json.
// Criamos uma interface com os métodos do CRUD, e implementamos essa interface no VeiculoRepositorioImpl, de implementação, 
// la adicionamos os métodos, que vão trabalhar de forma assíncrona, alterando nos métodos para terem tipos de retorno Task e Task tipado.
// Vamos verificar nosso banco de dados sendo criado, o nosso contexto foi criado para criar o banco para nós. Vamos fazer isso agora. 
// Para isso vamos usar um recurso do Entity Framework Core chamado Migrations, que nos permite criar toda uma documentação das alterações
// que estão sendo geradas no banco e vai montando os scripts, aplicando essa documentação no banco de dados também, nessa parte de
// migração podemos criar o banco, incrementar alterações, excluir o banco, criar ele novamente. 
// Para isso vamos precisar do SQL Server, entramos no servidor que configuramos na connectionStrings. Colocamos um BD que não existe
// até o momento no nosso servidor, o frotadb, vai ser o próprio Entity que vai gerar ele para nós. 
// Para podermos usar o Migration, precisamos do pacote Microsoft.EntityFrameworkCore.Tools, que já instalamos, essa é a biblioteca
// que nos permite trabalhar com o Migration.
// Vamos no menu Tools -> NuGet Package Manager -> Package Manager Console, então vamos executar alguns comandos do migration nesse
// console para fazermos essa configuração: 
// Para criar uma migração digitamos, damos o nome para a migração, nome padrão para migração inicial: "Add-Migration InitialCreate",
// o migration vai validar se tudo que configuramos está certo, se não tem nenhum problema para ele criar para nós o banco de dados,
// se tiver algum problema de configuração da nossa parte ele vai apontar para nós o que tem de errado. Vamos fazer uma série de
// configurações, mas vamos criar com a classe simples, nossas entidades/tabelas.
// Após esse comando ele começa validar o que configuramos no nosso DbSet.
// Estamos trabalhando na parte de acesso a dados utilizando ferramentas ORM (EF Core), que são ferramentas que criam para nós toda a
// configuração do banco e cria e executa nossas consultas sem precisarmos fazer isso no SQL, gerando o BD, etc, sem precisarmos fazer isso
// na mão no SQL.
// Não temos que ajustar nossa aplicação para a ferramenta, temos que adequar a ferramente para nossa necessidades e RN's.
// EF Core é a ferramenta que usamos para trabalhar com o acesso da api ao banco de dados.
// Hoje vamos alterar os campos da tabela Motorista sem mexer na nossa classe, manter a classe como está e customizar as colunas sem utilizar
// as annotations, vamos na classe de contexto, no OnModelCreating, utilizando o modelBuilder, que é o Entity Framework Fluent API.
// Classe context é quem faz a ponte do banco de dados e nosso códigos. Temos nossas classes que são nossas entidades.
// forma de customizar entidades com o modelBuilder é chamado de Fluent API do Entity Framework Core.
// vamos retomar essa parte do repositório depois, pois criamos a interface que tem os métodos para nossas consultas/cadastro de veículo,
// então temos nossa classe repositório que implementa essa interface, vamos implementar isso depois.
// Na próxima aula vamos retomar o repositório e ver como trabalhamos com o Entity Framework Core manipulando nossas informações no repositório.
// Fizemos toda nossa configuração do BD, o Entity é tranquilo para trabalhar, muito prático no dia-a-dia para usarmos nas nossas classes.
// No Entity precisamos do nosso context, que herda do DbContext, que tem suas outras classes base também, que podemos usar para compor nossa 
// aplicação. Temos nossos DbSet que fazem essa ponte entre nossas classes e nossas entidades (tabelas), só isso já bastaria, apontando para nosso
// banco por meio do options, que configuramos na classe de Startup, adicionando o DbContext, onde ele vai pegar do appsetting nossa string de
// contexão, apontando para nosso servidor/BD, isso seria o suficiente para deixar nosso BD configurado. Entramos em um segundo momento que
// precisamos usar os recursos do Migration para aplicar no BD essas alterações e até controlar versão. Se precisarmos fazer algo mais avançado,
// que seria customizar nome de tabelas e de campos, podemos fazer isso de 2 formas, por meio das Annotations e outra utilizando o Fluent API
// do Entity Framework Core, onde usamos a classe ModelBuilder e fazemos todas configurações, customizando nome de tabela e campos. Vimos essa parte
// de inicialização do BD, para ele subir com alguns dados já, por meio do método InitializeData para isso, que recebe a mesma classe ModelBuilder,
// então montamos nossos objetos, fazendo os inserts utilizando o HasData.
// Agora vai ser mais simples para trabalharmos, vamos ter repositório, serviço e controller.
// Antes disso, vamos ver outro assunto. Para aplicar as alterações no BD, utilizamos o comando Update-Database e Drop-Database para excluir
// o BD, criamos migrações pelo Add-Migration NomeDaMigracao, ele cria então a pasta Migrations, onde ficam nossas migrações e alterações de 
// cada migração, e em cada migração ele cria os métodos para criar tudo que precisamos, fazer relacionamentos, criar os campos, insert de dados,
// cria os índices, parte de exclusão, etc. Fizemos isso pois estamos no nosso ambiente de desenvolvimento, em uma empresa, dependendo do ambiente
// de produção/homologação, não podemos aplicar direto nossas alterações no BD, tem toda uma gestão de mudanças. Nesses casos, baseado nas nossas
// migrações, vamos gerar um script SQL Server que vai ter todas as configurações, criação das tabelas, campos, etc. No EF Core ele tem um comando
// que nos permite fazer isso, vamos ver 2 deles agora. Podemos usar os comandos "Script-DbContext" e "Script-Migration" para isso.
// Digitando "Script-DbContext", ele vai avaliar todo modelo que implementamos, vai tentar compilar, se der tudo certo ele cria para nós
// os scripts para nossas tabelas, campos, criando com os tipos de dados corretos para cada campo, gera nossas tabelas, tabela de relacionamento,
// montando as restrições de FK na tabela de relacionamento, monta script para insert iniciais dos dados, monta o índice para a
// tabela MotoristaVeiculo. Ele não salva esse script, só cria o mesmo, então copiamos tudo e podemos executar no nosso SQL Server, para ele gerar
// as consultas para gerar nossas tabelas com os dados que temos. Podemos digitar Script + TAB, dai ele nos apresenta as duas opções que temos,
// Script-DbContext e Script-Migration.
// A outra opção que temos para fazer isso é utilizar o "Script-Migration", que utiliza o Migration, ele funciona da mesma forma, mas podemos incluir
// nele alguns recursos a mais. O Script-Migration também gera a consulta para a criação das nossas tabelas, com os insert de todos dados que temos,
// utilizando aquela tabela de histórico de migração (EFMigraionsHistory). Quando trabalhamos com o Entity, ele cria uma tabela de migração,
// para poder mostrar para nós tudo que fizemos ao longo do histórico e todas as alterações. Ele verifica se essa tabela de histórico de migração
// não existe, se não existe ele cria ela, e no fim ele faz um insert nela. Essa é a diferença dos dois, além do modelo da sua aplicação tem 
// essa parte da Migration em si. O resultado entre as duas formas é bem parecida, mas no Script-Migration tem essa parte a mais. No Script-Migration
// também podemos incluir alguns parâmetros, que é o FROM e o TO. Se tivermos um histórico de migrações, podemos dizer para ele puxar as alterações
// de alguma migração específica até outra migração: Script-Migration -From <<NomeDaMigracao1>> -To <<NomeDaMigracao2>>, podemos especificar as
// versões das nossas migrações e ele gera para nós esse script também. Se tivermos só uma migração ele não tem esse histórico para poder gerar.
// As vezes não queremos pegar a migração final, queremos pegar só de uma parte das alterações para gerar as consultas para o BD, então podemos
// pegar o script entre migrações. 
// Vimos aqui essa parte de geração do script SQL da Migration, para rodar no nosso SQL Server.
// Dando um Update-Database de nossa migração, normalmente, podemos ver que nosso BD vai ter uma tabela _EFMigrationsHistory além das tabelas
// que já teríamos, que registra as migrações que temos, gerando essa parte de versão também, então podemos pegar os Id's das alterações.
// Nossas tabelas já estão sendo populadas, que foi o que configuramos pelo HasData na Fluent API do EF Core.
// Próxima aula, vamos trabalhar com o repositório. Vamos manipular nosso Entity Framework Core dentro das nossas classes.
// Vamos implementar nosso serviço, criando primeiro nossa interface.

namespace FleetControlWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
