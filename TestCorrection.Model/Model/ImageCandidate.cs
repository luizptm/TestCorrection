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
    
    public partial class ImageCandidate
    {
        public int QuestiontId { get; set; }
        public int CandidateId { get; set; }
        public byte[] Image { get; set; }
        public string Base64String { get; set; }
        public Nullable<bool> InUse { get; set; }
    
        public virtual Candidate Candidate { get; set; }
        public virtual Question Question { get; set; }
    }
}
