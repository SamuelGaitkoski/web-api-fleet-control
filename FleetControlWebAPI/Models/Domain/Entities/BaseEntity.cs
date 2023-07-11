using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// A criação dessa classe é para algumas configurações
// Essa classe BaseEntity vai ser static, não vamos instanciar essa classe, criar um objeto dela.
// Vamos ter o método estático GenerateId que vai gerar os Id's para nós.
// Para isso vamos usar a classe Guid, do pacote System, que vai gerar um número único para nós por meio do método NewGuid.

namespace FleetControlWebAPI.Models.Domain.Entities
{
    public static class BaseEntity
    {
        public static string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
