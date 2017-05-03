namespace TestCorrection.ViewModels
{
    using GridMvc.DataAnnotations;
    using System;
    using System.Collections.Generic;

    [GridTable(PagingEnabled = true, PageSize = 10)]
    public partial class QuestionResultVM
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int QuestionId { get; set; }
        public decimal Grade { get; set; }

        public virtual QuestionVM Question { get; set; }
        public virtual CandidateVM Candidate { get; set; }
        public virtual ImageCandidateVM ImageCandidate { get; set; }
    }
}
