using System;
using System.Linq;
using System.Linq.Expressions;

namespace JC_BookStation.Repositorio.Interface
{
    public interface IBaseRepositorio<T> where T : class
    {
        IQueryable<T> ListarTodos();
        IQueryable<T> Buscar(Expression<Func<T, bool>> predicate);
        T ListarPorId(String id);
        void Incluir(T entidade);
        void Excluir(T entidade);
        void Editar(T entidade);
        void Salvar();
        void Dispose();
    }
}
