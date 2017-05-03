namespace TestCorrection.ViewModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class RoleVM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RoleVM()
        {
            this.User = new HashSet<UserVM>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime CreationDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserVM> User { get; set; }
    }
}
