//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestCorrection.Model.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuestionResult
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int QuestionId { get; set; }
        public decimal Grade { get; set; }
    
        public virtual Candidate Candidate { get; set; }
        public virtual Question Question { get; set; }
    }
}
