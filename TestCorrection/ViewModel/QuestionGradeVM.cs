namespace TestCorrection.ViewModels
{
    using GridMvc.DataAnnotations;
    using System;
    using System.Collections.Generic;
    using TestCorrection.Model;
    using TestCorrection.Model.Model;

    [GridTable(PagingEnabled = true, PageSize = 10)]
    public partial class QuestionGradeVM
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int GradeId { get; set; }

        public virtual Question Question { get; set; }
        public virtual Grade Grade1 { get; set; }
    }
}
