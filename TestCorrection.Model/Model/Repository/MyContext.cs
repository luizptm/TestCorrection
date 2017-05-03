using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace TestCorrection.Model.Repository
{
	public class MyContext : DbContext, IMyContext
	{
		static MyContext()
		{
			Database.SetInitializer<MyContext>(null);
		}
		
		public MyContext()
            : base("name=Entities")
        {
        }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}