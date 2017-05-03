using TestCorrection.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCorrection.Model;
using TestCorrection.Model.Model;

namespace TestCorrection.Library
{
    public class ImageWorker : IWorker, IDisposable
    {
        private static Entities db = new Entities();

        private CadidateLib candidateLib = new CadidateLib(db);
        private ImageLib imageLib = new ImageLib(db);

        private static int i = 1;
        private static int iMaxCount = 10000;
        private int questionId = 0;
        
        public ImageWorker(int questionId)
        {
            this.questionId = questionId;
        }

        public ImageWorker(int i, CadidateLib candidateLib, ImageLib imageLib)
        {
            this.candidateLib = candidateLib;
            this.imageLib = imageLib;
        }

        public void DoWork()
        {
            while (i < iMaxCount)
            {
                CreateSaveImageCandidate(i);
                i++;
            }
        }

        public void DoThreadWork()
        {
            // The Interlocked.Increment method allows thread-safe modification
            // of variables accessible across multiple threads.
            Interlocked.Increment(ref i);
            while (i < iMaxCount)
            {
                CreateSaveImageCandidate();
                i++;
            }
        }

        public void DoStateWork(Object state)
        {
            // The Interlocked.Increment method allows thread-safe modification
            // of variables accessible across multiple threads.
            Interlocked.Increment(ref i);
            while (i < iMaxCount)
            {
                lock (state)
                {
                    CreateSaveImageCandidate(state);
                }
                i++;
            }
         }

        private void CreateSaveImageCandidate(Object s = null)
        {
            //SomeState state = (SomeState)s;
            //int i = (int)state.Cookie;
            string candidateName = "Candidate " + i;
            Candidate c = new Candidate() { Name = candidateName };
            candidateLib.SaveCandidate(c);
            
            Question q = new Question { Id = questionId, Type = "discursiva", Number = i, Descripton = "Question " + questionId };
            Bitmap image = imageLib.DrawTextInImage("Question " + questionId, candidateName);
            imageLib.Save(q, c, image); //saving ImageCandidate
        }
        
        public void Dispose()
        {
            if (i < iMaxCount)
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
