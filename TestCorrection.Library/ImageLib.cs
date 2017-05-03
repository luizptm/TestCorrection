using TestCorrection.Model;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Data.Entity;
using System;
using TestCorrection.Model.Model;
using System.Drawing.Imaging;

namespace TestCorrection.Library
{
    public class ImageLib
    {
        private string imageFilePathOrigin = @"FundoProva";
        private string imageFilePathFinal = @"FundoProvaTest";
        private string extension = "png";
        private PointF LocationQuestionText = new PointF(60f, 48f);
        private PointF LocationCandidateText = new PointF(60f, 68f);
        private Font arialFont = new Font("Arial", 18);

        Entities db = new Entities();

        public ImageLib() { }

        public ImageLib(Entities db)
        {
            this.db = db;
        }

        public byte[] imageToByteArray(Bitmap imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            return ms.ToArray();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);

            return returnImage;
        }

        public Bitmap byteArrayToBitmap(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Bitmap returnImage = (Bitmap)Image.FromStream(ms);
            return returnImage;
        }

        public Bitmap DrawTextInImage(string question, string candidate)
        {
            Bitmap bitmapOrigin = (Bitmap)Image.FromFile(imageFilePathOrigin + "." + extension);
            /*
            Bitmap bitmap;
            {
                byte[] byteArray = imageToByteArray(ref bitmapOrigin);
                bitmap = new Bitmap(new MemoryStream(byteArray));
            }
            bitmapOrigin.Dispose();
            */
            
            lock (bitmapOrigin)
            {
                Bitmap bitmap = new Bitmap(bitmapOrigin);
                {
                    Graphics graphics = Graphics.FromImage(bitmap);
                    graphics.DrawString(question, arialFont, Brushes.Blue, LocationQuestionText);
                    graphics.DrawString(candidate, arialFont, Brushes.Blue, LocationCandidateText);
                }

                string idQuestion = question.Replace(" ", "");
                string idCandidate = candidate.Replace(" ", "");
                string fileName = imageFilePathFinal + "_" + idQuestion + "_" + idCandidate + "." + extension;

                //bitmap.Save(fileName);//not
                //bitmap.Dispose();
                Console.WriteLine("Image for '{0}' and '{1}' saved", question, candidate);

                DeleteFile(fileName);
                //bitmapOrigin.Dispose();

                return bitmap;
            }
        }

        public void DeleteFile(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            fi.Delete();
            fi = null;

            Console.WriteLine("Physical image for '{0}' deleted", fileName);
        }

        public bool Save(Question q, Candidate c, Bitmap b)
        {
            lock (db)
            {
                ImageCandidate e = new ImageCandidate();
                e.QuestiontId = q.Id;
                e.CandidateId = c.Id;
                e.Image = imageToByteArray(b);
                e.Base64String = Convert.ToBase64String(e.Image);
                e.InUse = false;

                if (db.ImageCandidate.Count() > 0)
                {
                    IQueryable<ImageCandidate> query = db.ImageCandidate.Where(x => x.CandidateId == c.Id && x.QuestiontId == q.Id);
                    if (query.Count() > 0)
                    {
                        db.Entry(e).State = EntityState.Modified;
                        Console.WriteLine("ImageCandidate updated");
                    }
                    else
                    {
                        db.ImageCandidate.Add(e);
                        Console.WriteLine("ImageCandidate added");
                    }
                }
                else
                {
                    db.ImageCandidate.Add(e);
                    Console.WriteLine("ImageCandidate added");
                }
                
                db.SaveChanges();
            }
            
            return true;
        }
    }
}
