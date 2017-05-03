using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using TestCorrection.Library.Helper;
using TestCorrection.Library.Security;
using TestCorrection.Model;
using TestCorrection.Model.Model;
using TestCorrection.ViewModels;

namespace TestCorrection.Library
{
	public class DataAccess
	{
		private Entities db = new Entities();

		public string Sql_Usuarios = "";

        public List<UserVM> Usuarios(int pageNumber, int rowsPerPage)
        {
            List<UserVM> list = new List<UserVM>();

            var sqlEntities = db.Database.SqlQuery<UsuariosCustomQueryVM>("SELECT * FROM User");

            this.Sql_Usuarios = sqlEntities.ToString();

            foreach (var item in sqlEntities)
            {
                UserVM u = new UserVM();
                u.Id = item.Id;
                u.Name = item.Name;
                u.Password = item.Password;
                u.CPF = item.CPF;
                list.Add(u);
            }

            return list;
        }
	}
}