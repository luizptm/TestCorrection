namespace TestCorrection.ViewModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class TeacherVM
    {
        public int IdUser { get; set; }
        public bool IsTeacher { get; set; }
    
        public virtual UserVM User { get; set; }
    }
}
