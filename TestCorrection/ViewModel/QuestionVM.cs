namespace TestCorrection.ViewModels
{
    using GridMvc.DataAnnotations;
    using System;
    using System.Collections.Generic;

    public partial class QuestionVM
    {
        public QuestionVM()
        {
            this.QuestionGrade = new HashSet<QuestionGradeVM>();
            this.QuestionResult = new HashSet<QuestionResultVM>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public int Number { get; set; }
        public string Descripton { get; set; }

        [NotMappedColumn]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionGradeVM> QuestionGrade { get; set; }

        [NotMappedColumn]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionResultVM> QuestionResult { get; set; }
        public virtual ImageCandidateVM ImageCandidate { get; set; }
    }
}
