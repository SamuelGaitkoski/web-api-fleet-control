using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Projeto de .NET Core usando o Entity Framework Core.
// Criando projeto do tipo ASP.NET Core Web Api - .NET Core 3.1 (vers�o muito est�vel) - Configure for HTTPS desmarcado.
// Ferramenta Entity Framework Core (EF Core), mais conhecida como Entity.
// O Entity � um recurso que temos para desenvolver nossas solu��es e fazer a parte de acesso aos dados (tabelas do BD).
// Vamos fazer isso sem precisar programar no SQL, o Entity se encarrega de fazer isso para n�s.
// Entity � uma ferramenta ORM (Object Relational Mapper - Mapeamento de Objeto Relacional).
// O Entity � quem vai fazer o mapeamento dos nossos objetos, vamos trabalhar com o banco relacional por meio das classes.
// O Entity monta as consultas para n�s, trabalhamos com os objetos dai.
// As nossas classes s�o nossas entidades e v�o virar nossas tabelas do SQL, sem precisarmos programarmos em SQL.
// Vamos nos comunicar com nosso BD usando essa api que vamos criar.
// Precisamos do pacotes tamb�m, adicionando depend�ncias no nosso projeto, usando o NuGet Package Manager.
// O NuGet � a ferramenta que usamos no .NET, semelhante ao Maven do Java, para fazer a gest�o dos pacotes/depend�ncias
// do nosso projeto.
// Vamos fazer uma aplica��o (API) para fazer o controle de uma frota de ve�culos.
// Instala��o dos seguintes pacotes do Entity Framework Core no NuGet:
// Pacotes do EF Core:
// Microsoft.EntityFrameworkCore - v3.1.19
// Microsoft.EntityFrameworkCore.Design - v3.1.19
// Microsoft.EntityFrameworkCore.Relational - v3.1.19
// Pacotes do SQL Server para o EF Core:
// Microsoft.EntityFrameworkCore.SqlServer - v3.1.19
// Microsoft.EntityFrameworkCore.SqlServer.Design - v1.1.6
// Pacote do EF Core para as ferramentas para a parte do migration, migra��o, gera��o do banco de dados e cria��o de tabelas:
// Microsoft.EntityFrameworkCore.Tools - v3.1.19
// 2 formas de trabalhar com o Entity: Criar um BD a partir dele ou montar toda a estrutura a partir de um BD j� existente.
// Nesse projeto o nosso BD vai ser gerado a partir do Entity. vamos ver 2 formas de fazer isso.
// Bot�o direito em dependencias -> Manage NuGet Packages.
// Do .NET 3 foi pro 5, n�o 4. Antes do .NET Core tinha o .NET Framework, que tava na v4 quando o .NET Core foi lan�ado, 
// para n�o ter uma confus�o entre .NET Core v4 e .NET Framework v4, Microsoft lan�ou v5, que faz a fus�o dos 2 .NET, os 2 na v5.
// Podemos ver as dependencias instaladas em Dependencies, vendo os pacotes, arquivo do projeto .csproj, em PackageReference.
// Nosso contexto � o que vai fazer a ponte com o banco de dados.
// Nosso contexto da aplica��o foi configurado para fazer a ponte com o banco de dados atrav�s do Entity.
// A classe que vai fazer a ponte com o banco � a ControleFrotaContext, e por meio dela vamos acessar nossas entidades e
// tabelas no SQL Server. Configuramos cada DbSet para fazer a ponte com cada tabela, construtor com o DbCOntextOptions, no
// qual dizemos qual vai ser nosso banco de dados. 
// Nosso arquivo appsettings.json tem que existir no nosso projeto quando ele for compilar, para ele pegar a ConnectionStrings. 
// Pode ocorrer de estarmos debugando e n�o encontrar esse arquivo na pasta do build, que ele gera. Para termos certeza que esse
// arquivo esta sendo gerado, damos bot�o direito nele -> Properties -> Copy to Output Directory como Copy Always, para ele sempre
// gerar o arquivo appsettings.json.
// Criamos uma interface com os m�todos do CRUD, e implementamos essa interface no VeiculoRepositorioImpl, de implementa��o, 
// la adicionamos os m�todos, que v�o trabalhar de forma ass�ncrona, alterando nos m�todos para terem tipos de retorno Task e Task tipado.
// Vamos verificar nosso banco de dados sendo criado, o nosso contexto foi criado para criar o banco para n�s. Vamos fazer isso agora. 
// Para isso vamos usar um recurso do Entity Framework Core chamado Migrations, que nos permite criar toda uma documenta��o das altera��es
// que est�o sendo geradas no banco e vai montando os scripts, aplicando essa documenta��o no banco de dados tamb�m, nessa parte de
// migra��o podemos criar o banco, incrementar altera��es, excluir o banco, criar ele novamente. 
// Para isso vamos precisar do SQL Server, entramos no servidor que configuramos na connectionStrings. Colocamos um BD que n�o existe
// at� o momento no nosso servidor, o frotadb, vai ser o pr�prio Entity que vai gerar ele para n�s. 
// Para podermos usar o Migration, precisamos do pacote Microsoft.EntityFrameworkCore.Tools, que j� instalamos, essa � a biblioteca
// que nos permite trabalhar com o Migration.
// Vamos no menu Tools -> NuGet Package Manager -> Package Manager Console, ent�o vamos executar alguns comandos do migration nesse
// console para fazermos essa configura��o: 
// Para criar uma migra��o digitamos, damos o nome para a migra��o, nome padr�o para migra��o inicial: "Add-Migration InitialCreate",
// o migration vai validar se tudo que configuramos est� certo, se n�o tem nenhum problema para ele criar para n�s o banco de dados,
// se tiver algum problema de configura��o da nossa parte ele vai apontar para n�s o que tem de errado. Vamos fazer uma s�rie de
// configura��es, mas vamos criar com a classe simples, nossas entidades/tabelas.
// Ap�s esse comando ele come�a validar o que configuramos no nosso DbSet.
// Estamos trabalhando na parte de acesso a dados utilizando ferramentas ORM (EF Core), que s�o ferramentas que criam para n�s toda a
// configura��o do banco e cria e executa nossas consultas sem precisarmos fazer isso no SQL, gerando o BD, etc, sem precisarmos fazer isso
// na m�o no SQL.
// N�o temos que ajustar nossa aplica��o para a ferramenta, temos que adequar a ferramente para nossa necessidades e RN's.
// EF Core � a ferramenta que usamos para trabalhar com o acesso da api ao banco de dados.
// Hoje vamos alterar os campos da tabela Motorista sem mexer na nossa classe, manter a classe como est� e customizar as colunas sem utilizar
// as annotations, vamos na classe de contexto, no OnModelCreating, utilizando o modelBuilder, que � o Entity Framework Fluent API.
// Classe context � quem faz a ponte do banco de dados e nosso c�digos. Temos nossas classes que s�o nossas entidades.
// forma de customizar entidades com o modelBuilder � chamado de Fluent API do Entity Framework Core.
// vamos retomar essa parte do reposit�rio depois, pois criamos a interface que tem os m�todos para nossas consultas/cadastro de ve�culo,
// ent�o temos nossa classe reposit�rio que implementa essa interface, vamos implementar isso depois.
// Na pr�xima aula vamos retomar o reposit�rio e ver como trabalhamos com o Entity Framework Core manipulando nossas informa��es no reposit�rio.
// Fizemos toda nossa configura��o do BD, o Entity � tranquilo para trabalhar, muito pr�tico no dia-a-dia para usarmos nas nossas classes.
// No Entity precisamos do nosso context, que herda do DbContext, que tem suas outras classes base tamb�m, que podemos usar para compor nossa 
// aplica��o. Temos nossos DbSet que fazem essa ponte entre nossas classes e nossas entidades (tabelas), s� isso j� bastaria, apontando para nosso
// banco por meio do options, que configuramos na classe de Startup, adicionando o DbContext, onde ele vai pegar do appsetting nossa string de
// contex�o, apontando para nosso servidor/BD, isso seria o suficiente para deixar nosso BD configurado. Entramos em um segundo momento que
// precisamos usar os recursos do Migration para aplicar no BD essas altera��es e at� controlar vers�o. Se precisarmos fazer algo mais avan�ado,
// que seria customizar nome de tabelas e de campos, podemos fazer isso de 2 formas, por meio das Annotations e outra utilizando o Fluent API
// do Entity Framework Core, onde usamos a classe ModelBuilder e fazemos todas configura��es, customizando nome de tabela e campos. Vimos essa parte
// de inicializa��o do BD, para ele subir com alguns dados j�, por meio do m�todo InitializeData para isso, que recebe a mesma classe ModelBuilder,
// ent�o montamos nossos objetos, fazendo os inserts utilizando o HasData.
// Agora vai ser mais simples para trabalharmos, vamos ter reposit�rio, servi�o e controller.
// Antes disso, vamos ver outro assunto. Para aplicar as altera��es no BD, utilizamos o comando Update-Database e Drop-Database para excluir
// o BD, criamos migra��es pelo Add-Migration NomeDaMigracao, ele cria ent�o a pasta Migrations, onde ficam nossas migra��es e altera��es de 
// cada migra��o, e em cada migra��o ele cria os m�todos para criar tudo que precisamos, fazer relacionamentos, criar os campos, insert de dados,
// cria os �ndices, parte de exclus�o, etc. Fizemos isso pois estamos no nosso ambiente de desenvolvimento, em uma empresa, dependendo do ambiente
// de produ��o/homologa��o, n�o podemos aplicar direto nossas altera��es no BD, tem toda uma gest�o de mudan�as. Nesses casos, baseado nas nossas
// migra��es, vamos gerar um script SQL Server que vai ter todas as configura��es, cria��o das tabelas, campos, etc. No EF Core ele tem um comando
// que nos permite fazer isso, vamos ver 2 deles agora. Podemos usar os comandos "Script-DbContext" e "Script-Migration" para isso.
// Digitando "Script-DbContext", ele vai avaliar todo modelo que implementamos, vai tentar compilar, se der tudo certo ele cria para n�s
// os scripts para nossas tabelas, campos, criando com os tipos de dados corretos para cada campo, gera nossas tabelas, tabela de relacionamento,
// montando as restri��es de FK na tabela de relacionamento, monta script para insert iniciais dos dados, monta o �ndice para a
// tabela MotoristaVeiculo. Ele n�o salva esse script, s� cria o mesmo, ent�o copiamos tudo e podemos executar no nosso SQL Server, para ele gerar
// as consultas para gerar nossas tabelas com os dados que temos. Podemos digitar Script + TAB, dai ele nos apresenta as duas op��es que temos,
// Script-DbContext e Script-Migration.
// A outra op��o que temos para fazer isso � utilizar o "Script-Migration", que utiliza o Migration, ele funciona da mesma forma, mas podemos incluir
// nele alguns recursos a mais. O Script-Migration tamb�m gera a consulta para a cria��o das nossas tabelas, com os insert de todos dados que temos,
// utilizando aquela tabela de hist�rico de migra��o (EFMigraionsHistory). Quando trabalhamos com o Entity, ele cria uma tabela de migra��o,
// para poder mostrar para n�s tudo que fizemos ao longo do hist�rico e todas as altera��es. Ele verifica se essa tabela de hist�rico de migra��o
// n�o existe, se n�o existe ele cria ela, e no fim ele faz um insert nela. Essa � a diferen�a dos dois, al�m do modelo da sua aplica��o tem 
// essa parte da Migration em si. O resultado entre as duas formas � bem parecida, mas no Script-Migration tem essa parte a mais. No Script-Migration
// tamb�m podemos incluir alguns par�metros, que � o FROM e o TO. Se tivermos um hist�rico de migra��es, podemos dizer para ele puxar as altera��es
// de alguma migra��o espec�fica at� outra migra��o: Script-Migration -From <<NomeDaMigracao1>> -To <<NomeDaMigracao2>>, podemos especificar as
// vers�es das nossas migra��es e ele gera para n�s esse script tamb�m. Se tivermos s� uma migra��o ele n�o tem esse hist�rico para poder gerar.
// As vezes n�o queremos pegar a migra��o final, queremos pegar s� de uma parte das altera��es para gerar as consultas para o BD, ent�o podemos
// pegar o script entre migra��es. 
// Vimos aqui essa parte de gera��o do script SQL da Migration, para rodar no nosso SQL Server.
// Dando um Update-Database de nossa migra��o, normalmente, podemos ver que nosso BD vai ter uma tabela _EFMigrationsHistory al�m das tabelas
// que j� ter�amos, que registra as migra��es que temos, gerando essa parte de vers�o tamb�m, ent�o podemos pegar os Id's das altera��es.
// Nossas tabelas j� est�o sendo populadas, que foi o que configuramos pelo HasData na Fluent API do EF Core.
// Pr�xima aula, vamos trabalhar com o reposit�rio. Vamos manipular nosso Entity Framework Core dentro das nossas classes.
// Vamos implementar nosso servi�o, criando primeiro nossa interface.

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
