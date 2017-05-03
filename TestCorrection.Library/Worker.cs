using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestCorrection.Library
{
    public interface IWorker
    {
        void DoThreadWork();


        void DoStateWork(Object state);
    }

    public class Worker : IWorker
    {
        // This method will be called when the thread is started.
        public void DoThreadWork()
        {
            Console.WriteLine("worker thread: working...");
        }
                
        public void DoStateWork(Object state)
        {
            Console.WriteLine("worker thread: working...");
        }
    }
}
