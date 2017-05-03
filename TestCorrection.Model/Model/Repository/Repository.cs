using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace TestCorrection.Model.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly MyContext context;

		public Repository(IUnitOfWork uow)
		{
			context = uow.Context as MyContext;
		}

		public IQueryable<T> All
		{
			get
			{
				return context.Set<T>();
			}
		}

		public IQueryable<T> AllEager(params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = context.Set<T>();
			foreach (var include in includes)
			{
				query = query.Include(include);
			}
			return query;
		}

		public T Get(int id)
		{
			return context.Set<T>().Find(id);
		}

		public IQueryable<T> GetAll()
		{
			IQueryable<T> query = context.Set<T>();
			return query;
		}

		public void Post(T item) //INSERT
		{
			context.Entry(item).State = System.Data.Entity.EntityState.Added;
		}

		public void Put(int id, T item) //DELETE
		{
			context.Set<T>().Attach(item);
			context.Entry(item).State = System.Data.Entity.EntityState.Modified;
		}

		public void Delete(int id)
		{
			var item = context.Set<T>().Find(id);
			context.Set<T>().Remove(item);
		}

		public void Dispose()
		{
			if (context != null)
				context.Dispose();
		}
	}
}