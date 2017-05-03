namespace TestCorrection.ViewModels
{
    using GridMvc.DataAnnotations;
    using System;
    using System.Collections.Generic;
    using TestCorrection.Model;
    using TestCorrection.Model.Model;

    [GridTable(PagingEnabled = true, PageSize = 10)]
    public partial class UserVM
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordQuestion { get; set; }
        public System.DateTime LastActivityDate { get; set; }
        public System.DateTime LastPasswordChangedDate { get; set; }
        public System.DateTime CreationDate { get; set; }

        public virtual Role Role { get; set; }
        public virtual Teacher Teacher { get; set; }
    }

    public class UsuariosCustomQueryVM
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordQuestion { get; set; }
        public System.DateTime LastActivityDate { get; set; }
        public System.DateTime LastPasswordChangedDate { get; set; }
        public System.DateTime CreationDate { get; set; }
    }
}
