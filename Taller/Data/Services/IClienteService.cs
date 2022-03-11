using System.Collections.Generic;
using System.Threading.Tasks;
using Taller.Data.Model;

namespace Taller.Data.Services
{
    public interface IClienteService
    {
        Task ClienteInsert(Cliente cliente);
        Task ClienteUpdate(Cliente cliente);
        Task<bool> DeleteCliente(int id);
        Task<IEnumerable<Cliente>> Get();
        Task<Cliente> GetId(int Idcliente);
    }
}