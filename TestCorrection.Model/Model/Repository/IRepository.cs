using System;
using System.Linq;
using System.Linq.Expressions;

namespace TestCorrection.Model.Repository
{
	public interface IRepository<T> : IDisposable where T : class
	{
		IQueryable<T> All { get; }

		IQueryable<T> AllEager(params Expression<Func<T, object>>[] includes);

		T Get(int id);

		IQueryable<T> GetAll();

		void Post(T entity);

		void Put(int id, T entity);

		void Delete(int id);
	}
}