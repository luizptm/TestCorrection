using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCorrection.Library;

namespace TestCorrection.Library
{
    public class WorkerThread<T> where T : IWorker
    {
        T workerObject;
        Thread workerThread = null;

        public int sleep = 20000;

        public int maxThreads = 8;
        public int countThreads = 0;

        public WorkerThread(T worker)
        {
            // Create the thread object. This does not start the thread.
            workerObject = worker;
        }

        public WorkerThread(int maxThreads)
        {
            this.maxThreads = maxThreads;
        }

        public void Start()
        {
            if (countThreads > maxThreads)
            {
                return;
            }

            workerThread = new Thread(workerObject.DoThreadWork);
            workerThread.Start();
            countThreads++;

            // Spin for a while waiting for the started thread to become
            // alive:
            while (!workerThread.IsAlive) ;

            Thread.Sleep(sleep);

            return;
        }

        public Thread Create()
        {
            workerThread = new Thread(workerObject.DoThreadWork);

            return workerThread;
        }


        public void Stop()
        {
            // Request that oThread be stopped
            workerThread.Abort();

            // Wait until workerThread finishes. Join also has overloads
            // that take a millisecond interval or a TimeSpan object.
            workerThread.Join();

            Console.WriteLine("main thread: Worker thread has terminated.");
        }

        public void Stop(Thread thread)
        {
            // Request that oThread be stopped
            thread.Abort();

            // Wait until workerThread finishes. Join also has overloads
            // that take a millisecond interval or a TimeSpan object.
            thread.Join();

            Console.WriteLine("main thread: Worker thread has terminated.");
        }

        //https://msdn.microsoft.com/en-us/library/aa645740(v=vs.71).aspx
        public void StartUsingPool()
        {
            bool W2K = false;
            int MaxCount = 8;

            ManualResetEvent eventX = new ManualResetEvent(false);

            try
            {
                Worker w = new Worker();
                //ThreadPool.SetMaxThreads(MaxCount, MaxCount);
                ThreadPool.QueueUserWorkItem(new WaitCallback(w.DoStateWork),
                   new SomeState(0));
                W2K = true;
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("These API's may fail when called on a non-Windows 2000 system.");
                W2K = false;
            }
            if (W2K)  // If running on an OS which supports the ThreadPool methods.
            {
                for (int iItem = 0; iItem < MaxCount; iItem++)
                {
                    //ThreadPool.QueueUserWorkItem(workerObject.DoStateWork);
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(workerObject.DoStateWork));
                    ThreadPool.QueueUserWorkItem(new WaitCallback(workerObject.DoStateWork), new SomeState(iItem));
                    //Thread.Sleep(sleep);
                }
            }
        }
    }
}
