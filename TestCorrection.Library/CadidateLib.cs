using TestCorrection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCorrection.Model.Model;
using System.Drawing;

namespace TestCorrection.Library
{
    public class CadidateLib
    {
        Entities db = new Entities();

        public CadidateLib() { }

        public CadidateLib(Entities db)
        {
            this.db = db;
        }

        public async Task<Candidate> SaveCandidateAsync(Candidate e)
        {
            if (db.Candidate.Find(e.Id) != null)
            {
                return e;
            }
            db.Candidate.Add(e);
            await db.SaveChangesAsync();

            return e;
        }

        public bool SaveCandidate(Candidate e)
        {
            lock (db)
            {
                if (db.Candidate.Find(e.Id) != null)
                {
                    return false;
                }
                db.Candidate.Add(e);
                db.SaveChanges();
            }
            Console.WriteLine("Candidate '{0}' saved", e.Name);
            return true;
        }

        public bool SaveCandidate(int Id, string name)
        {
            if (db.Candidate.Find(Id) != null)
            {
                return false;
            }
            Candidate e = new Candidate();
            e.Id = Id;
            e.Name = e.Name;
            db.Candidate.Add(e);
            db.SaveChanges();

            return true;
        }

        public async Task<Question> SaveQuestion(Question e)
        {
            db.Question.Add(e);
            await db.SaveChangesAsync();

            return e;
        }

        public bool SaveQuestion(int Id, string Type, int Number, string Description)
        {
            Question e = new Question();
            e.Id = Id;
            e.Type = Type;
            e.Number = Number;
            e.Descripton = Description;
            db.Question.Add(e);
            db.SaveChanges();

            return true;
        }

        public QuestionResult SaveQuestionResult(int Id, Candidate c, Question q)
        {
            QuestionResult e = new QuestionResult();
            e.Id = Id;
            e.CandidateId = c.Id;
            e.QuestionId = q.Id;
            e.Grade = 0;
            db.QuestionResult.Add(e);
            db.SaveChanges();

            return e;
        }

        public async Task<QuestionResult> SaveQuestionResult(int Id, Candidate c, Question q, bool isThread = true)
        {
            QuestionResult e = new QuestionResult();
            e.Id = Id;
            e.CandidateId = c.Id;
            e.QuestionId = q.Id;
            e.Grade = 0;
            db.QuestionResult.Add(e);
            await db.SaveChangesAsync();

            return e;
        }

        public async Task<QuestionResult> SaveQuestionResult(QuestionResult e)
        {
            db.QuestionResult.Add(e);
            await db.SaveChangesAsync();

            return e;
        }
    }
}
