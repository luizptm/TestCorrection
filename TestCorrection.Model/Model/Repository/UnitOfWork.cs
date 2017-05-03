using System;

namespace TestCorrection.Model.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IMyContext context;

		public UnitOfWork()
		{
			context = new MyContext();
		}
		public UnitOfWork(IMyContext context)
		{
			this.context = context;
		}
		public int Save()
		{
			return context.SaveChanges();
		}
		public IMyContext Context
		{
			get
			{
				return context;
			}
		}
		public void Dispose()
		{
			if (context != null)
				context.Dispose();
		}
	}
}