using System;

namespace TestCorrection.Model.Repository
{
	public interface IMyContext : IDisposable
	{
		int SaveChanges();
	}
	public interface IUnitOfWork : IDisposable
	{
		int Save();
		IMyContext Context { get; }
	}
}