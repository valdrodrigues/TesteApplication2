using TestApplication.Domain.Entity;

namespace TestApplication.Domain.IRepository
{
    public interface ITabelaDescontoService
    {
        TabelaDesconto BuscarTabelaPeloAno(int ano);
    }
}
