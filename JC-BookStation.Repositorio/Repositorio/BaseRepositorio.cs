using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using JC_BookStation.Data.Models;
using JC_BookStation.Repositorio.Interface;

namespace JC_BookStation.Repositorio.Repositorio
{
    public class BaseRepositorio<TC, T> : IDisposable, IBaseRepositorio<T>
        where T : class
        where TC : Entities, new()
    {
        private TC _contexto = new TC();
        public TC Context
        {
            get { return _contexto; }
            set { _contexto = value; }
        }
        //

        public void Salvar()
        {
            _contexto.SaveChanges();
        }

        public void Incluir(T entidade)
        {
            _contexto.Set<T>().Add(entidade);
        }

        public void Excluir(T entidade)
        {
            _contexto.Set<T>().Remove(entidade);
        }

        public void Editar(T entidade)
        {
            _contexto.Entry(entidade).State = EntityState.Modified;
        }

        public IQueryable<T> ListarTodos()
        {
            IQueryable<T> query = _contexto.Set<T>();
            return query;
        }

        public IQueryable<T> Buscar(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _contexto.Set<T>().Where(predicate);
            return query;
        }

        public T ListarPorId(string id)
        {
            int idInt;
            Int32.TryParse(id, out idInt);
            return _contexto.Set<T>().Find(idInt);
        }
        public virtual void Dispose()
        {
            _contexto.Dispose();
        }
    }
}