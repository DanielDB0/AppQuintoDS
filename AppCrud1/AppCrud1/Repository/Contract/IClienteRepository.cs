using AppCrud1.Models;

namespace AppCrud1.Repository.Contract
{
    public interface IClienteRepository
    {
        IEnumerable<Cliente> ObterTodosClientes();

        void Cadastrar(Cliente cliente);

        void Atualizar(Cliente cliente);

        Cliente ObterCliente(int Id);

        void Excluir(int Id);
    }
}
