using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestCorrection.Library
{
    public class Business
    {

        public void TesteImagem()
        {
            ImageLib imageLib = new ImageLib();
            string name = "Candidate " + 1;
            string question = "Question " + 1;
            Bitmap image = imageLib.DrawTextInImage(question, name);

        }

        public void TesteThread()
        {
            //Worker w = new Worker();
            //WorkerThread<Worker> wt = new WorkerThread<Worker>(w);
            //wt.Start();
            //wt.Stop();
            
            Pool p = new Pool();
            p.Run();
        }

        public void GenerateCandidates()
        {
            //Question 1
            Console.WriteLine("************ Question 1");
            ImageWorker iw = new ImageWorker(1);
            WorkerThread<ImageWorker> wti = new WorkerThread<ImageWorker>(iw);
            wti.StartUsingPool();//com threads
            //iw.DoWork();//caso os thread não funcionem

            //Question 2
            Console.WriteLine("************ Question 2");
            iw = new ImageWorker(2);
            //wti = new WorkerThread<ImageWorker>(iw);
            wti.StartUsingPool();//com threads

            //Question 3
            Console.WriteLine("************ Question 3");
            iw = new ImageWorker(3);
            wti = new WorkerThread<ImageWorker>(iw);
            wti.StartUsingPool();//com threads
        }
    }
}
