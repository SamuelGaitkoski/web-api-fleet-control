using FleetControlWebAPI.Models.Domain.Entities;
using FleetControlWebAPI.Models.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// essa é o nosso Repositorio, que é a classe VeiculoRepositorioImpl, de VeiculoRepositorioImplementacao,
// que vai implementar a interface VeiculoRepositorio.
// essa classe vai implementar as regras que definimos na interface VeiculoRepositorio, vai implementar o VeiculoRepositorio.
// Temos que implementar os métodos definidos na interface dai, ele pode implementar a interface, dando um ctrl + . e
// enter em "Implement Interface", dai temos que editá-los.
// incluímos o contrutor dessa classe VeiculoRepositorioImpl.
// Para que aqui possamos manipular, fazer pesquisa, atualização, exclusão, precisamos usar nosso objeto de contexto, 
// então aqui dentro da classe criamos um objeto, que pode ser readonly, do ControleFrotaContext, que é o context,
// nosso contexto de dados. E para podermos acessar esse context, vamos passar no nosso construtor ele como parametro,
// na assinatura do método, dai só precisamos atribuir o valor do context que criamos com o objeto passado na assinatura
// do método VeiculoRepositorioImpl, que é o nosso construtor.
// Agora implementamos os métodos da interface aqui no repositório.
// Nosso repositório implementa nossa interface. Para nosso código entender qual a classe que está implementando a interface,
// precisamos configurar a injeção de dependências da nossa aplicação, para não dar erro na nossa aplicação, dai vamos na classe
// Startup, no método ConfigureServices e pode ser depois da configuração do banco.
// Após alterarmos para os métodos da interface trabalharem de forma assíncrona, implementamos os métodos da interface aqui na
// classe de implementação, dai ele cria cada método utilizando com o Task que definimos para cada método da interface.
// Temos nosso VeiculoRepositorio, que é nossa interface, com todos os métodos, e temos nossa classe VeiculoRepositorioImpl,
// que herda dessa interface e implementa nosso repositório em si.
// Aqui na implementação do repositório precisamos ter nosso objeto de contexto, que é o context, e setamos ele na nossa classe
// por meio da nossa injeção de dependência no construtor da classe, atribuindo o context a ele, usamos o this para dizer que 
// nossa propriedade context do tipo ControleFrotaContext recebe o context passado para o construtor.
// Concluímos nosso repositório de Veiculo.
// Na próxima aula vamos implementar o Serviço, que vamos criar, serviço que vai chamar nosso Repositório.

namespace FleetControlWebAPI.Models.Infrastructure.Repositories
{
    public class VeiculoRepositorioImpl : VeiculoRepositorio
    {
        private readonly ControleFrotaContext context;

        public VeiculoRepositorioImpl(ControleFrotaContext context)
        {
            this.context = context;
        }

        public async Task Atualizar(Veiculo veiculo)
        {
            // vamos buscar um registro primeiro, pesquisando o registro utilizando nosso método Pesquisar, passando o Id
            // do objeto veiculo passado para a função.
            // incluímos o async na criação do método.
            var atualizado = await Pesquisar(veiculo.VeiculoId);
            // fazemos as validações para verificar se ele encontrou o registro pelo id do mesmo.
            // se ele encontrou o registro na tabela, vamos atualizar os campos, pegando os campos do veiculo já no banco
            // e atribuindo os valores para esses campos vindo do Veiculo passado por parâmetro para essa função Atualizar.
            if (veiculo != null && !string.IsNullOrEmpty(veiculo.VeiculoId) && veiculo.VeiculoId.Equals(veiculo.VeiculoId))
            {
                // incluindo as informações atualizadas no nosso objeto já cadastrado na tabela. 
                // o Id do veiculo permanece o mesmo, pois foi pelo mesmo que fizemos a consulta.
                atualizado.Modelo = veiculo.Modelo;
                atualizado.Ano = veiculo.Ano;
                atualizado.Placa = veiculo.Placa;

                // vamos no nosso contexto, acessamos nossa entidade Veiculos e aplicamos um Update() passando o atualizado que
                // é nosso veiculo com as informações atualizadas.
                context.Veiculos.Update(atualizado);
                // confirmar as atualizações/alterações, sem essa linha ele não salva as alterações, botamos await pois
                // o método é assíncrono.
                await context.SaveChangesAsync();
            }
        }

        public async Task Cadastrar(Veiculo veiculo)
        {
            // temos 2 etapas, uma vamos acessar nosso contexto, onde vamos acessar a tabela Veiculos e adicionar o veiculo
            // passado para o método como parâmetro.
            // Em vez de usar só o método Add para adicionar, usamos o método assíncrino AddAsync.
            // esse método Cadastrar não retorna nada, mas temos que fazer a chamada assíncrona dela adicionando o async
            // na criação do método.
            await context.Veiculos.AddAsync(veiculo);
            // adicionamos o Veiculo, agora temos que confirmar no banco que estamos fazendo essa alteração.
            // temos um método dentro do context chamado SaveChangesAsync(), dessa forma confirmamos alterações no nosso banco,
            // podemos fazer várias alterações e confirmar elas depois usando esse método.
            await context.SaveChangesAsync();
        }

        public async Task Excluir(string veiculoId)
        {
            // ele vai passar um id na exclusão e precisamos remover esse objeto da tabela.
            // para poder remover primeiro precisamos identificar se esse objeto existe. Para isso vamos reaproveitar nosso método
            // de pesquisa daqui para verificar se existe um veiculo na tabela com id igual ao que foi passado.
            // incluímos o async no método e o await antes do pesquisar, para forçar nossa chamada assíncrona. Não precisar incluir
            // o Async no Pesquisar pois esse método já é assíncrono.
            var veiculo = await Pesquisar(veiculoId);
            // agora que os dados foram pesquisados acessamos nossa tabela Veiculos pelo nosso contexto e usamos o método Remove
            // que temos, onde passamos um objeto para ser removido, que será um veículo.
            // antes disso vamos validar, se ele encontrou um veiculo pelo id passado e uma validação a mais, se o veiculo achado
            // tiver id igual ao id passado, só para fazer uma validação a mais, que realmente achou esse objeto.
            if (veiculo != null && !string.IsNullOrEmpty(veiculo.VeiculoId) && veiculo.VeiculoId.Equals(veiculoId))
                context.Veiculos.Remove(veiculo);
                // persistindo as alterações, para confirmar as alterações no nosso banco/contexto, refletir as alterações no banco.
                await context.SaveChangesAsync();
        }

        public async Task<List<Veiculo>> Listar()
        {
            // No Entity temos o context, que é nosso objeto de contexto, e por meio dele podemos acessar nossas entidades/tabelas
            // que são representadas por nossos DbSets.
            // Temos o Veiculos, e para listar eles usamos o ToList() do Linq, dai retornamos essa informação. 
            // Como estamos trabalhando com nossos métodos de forma assíncrona, nosso tipo de retorno tem que ser uma Task, por isso
            // tipamos a lista de Veiculo do retorno.
            // incluímos a palavra chave async no nosso método, para ele entender que ele vai trabalhar de forma assíncrona.
            // Para buscar os dados não usamos o ToList(), vamos usar um método extendido do Linq para trabalhar de forma assíncrona,
            // que é o ToListAsync(), extendemos a utilização do Linq importando o pacote necessário, que é o
            // Microsoft.EntityFrameworkCore.
            // para podermos chamar um método assíncrono (com o async), no return usamos a palavra chave await.
            return await context.Veiculos.ToListAsync();
        }

        public async Task<List<Veiculo>> Listar2()
        {
            // o Listar2 vai ser um select um pouco mais completo.
            // Indo na nossa classe Veiculo, vemos que ela tem todas as informações do nosso Veiculo, e além disso temos os 
            // Motoristas, que são todos os Motoristas relacionados ao nosso Veiculo, que é nossa tabela de relacionamento. 
            // Logo, além de trazer as informações de cada veículo, queremos trazer todos os motoristas que tem relacionamento
            // com o nosso veículo pesquisado.
            // Então teremos no Listar2 uma pesquisa mais completa em relação a dados, com mais detalhes.
            // vamos ter um começo igual ao Listar.
            // Como fazemos para incluir informações do Motorista nessa consulta?   
            // antes do ToListAsync(), para listar todos os motoristas e salvar isso em result, vamos colocar um Include(), onde
            // vamos acessar os Motoristas. Para complementar temos um ThenInclude(), então inclua, dai criamos um objeto, 
            // incluindo nome que quisermos para ele, dai relacionamos ele com Veiculo para fechar o vínculo. Os Motoristas
            // vão trazer os Veiculos relacionados com eles.
            // Fazendo isso ele já consegue preencher a tabela de relacionamento para nós.
            // Nossa tabela de relacionamento tem os id's somente, é isso que ele vai preencher.
            var result = await context.Veiculos
                .Include(c => c.Motoristas)
                .ThenInclude(cc => cc.Veiculo)
                .ToListAsync();
            // Queremos também a informação do Motorista. Para fazer isso, depois que pegarmos o resultado da nossa consulta result,
            // vamos manipular nossa lista result, fazendo um loop, onde vamos ter cada item da nossa lista result, para cada
            // veiculo/resultado da nossa lista, vamos pegar a informação dos Motoristas desse item, que é o Veiculo. Dai vamos 
            // pegar esse subitem, que são os Motoristas e vamos popular nosso objeto Motorista através de uma pesquisa, indo no
            // contexto, em Motoristas e pegando o primeiro motorista com mesmo id do id dessa nossa tabela de relacionamento.
            // Quando fazemos o Include ele vai preencher nossa tabela de relacionamento, preenchendo os id's da mesma.
            // Preenchemos então o Motorista da tabela de relacionamento com o subitem.Motorista. O Veiculo não precisamos 
            // preencher pois já temos ele, nosso objeto principal é o Veiculo, só queremos incluir o Motorista para ele.
            // Fazemos esses loops para pegar cada registro da lista, dentro de cada registro pegar a tabela de relacionamento,
            // e pegamos o id do Motorista, se encontrarmos o id e ele for igual adicionamos nosso Mororista na tabela de
            // relacionamento. Ficando mais complexo que a listagem normal (Listar).
            // Isso vai gerar um erro de referência circular, pois ele vai entender que temos um Veiculo que quer acessar as
            // informações do Motorista, só que esse relacionamento é n para n, muitos para muitos, então a leitura que o Entity
            // vai fazer é, Veiculos acessam Motorista, que acessam Veiculos, que acessam Motoristas, então ele fica num loop 
            // e vai gerar um erro na hora de processar essa consulta. Vamos ver esse erro primeiro para resolver ele depois.
            // O código não vai mudar, mas precisamos incluir uma configuração para resolver esse problema de referência circular.
            foreach (var item in result)
            {
                foreach (var subitem in item.Motoristas)
                {
                    subitem.Motorista = context.Motoristas.FirstOrDefault(m => m.MotoristaId.Equals(subitem.MotoristaId));
                }
            }
            //retornando variável criada, com os veículos que temos.
            return result;

        }

        public async Task<Veiculo> Pesquisar(string veiculoId)
        {
            // também vamos usar nosso contexto, que será na entidade Veiculos, onde vamos buscar alguma informação e retornar somente
            // um registro, que é um Veiculo em si.
            // vamos retornar um Veiculo de acordo com o id que ele encontrar, pelo veiculoId passado. 
            // No Link temos o First() ou FirstOrDefault() para buscar, para buscar o primeiro registro da nossa coleção de Veiculo.
            // vamos usar o FirstOrDefault(), o First() também serviria.
            // como estamos fazendo de forma assíncrona, temos que forçar que ele seja assíncrona, usando o FirstOrDefaultAsync()
            // damos um nome para a variavel que vai ser usada para fazer a pesquisa nos objetos.
            // se o veiculoId passado for igual a algum VeiculoId da lista de Veiculo, podemos usar o Equals ou o == para igualar eles.
            // Fazemos os ajustes para a chamada assíncrona, com o async no método e o await no return.
            return await context.Veiculos.FirstOrDefaultAsync(v => v.VeiculoId.Equals(veiculoId));
        }
    }
}
