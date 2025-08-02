using System;
using System.Threading;

namespace Threads
{
    public class ThreadStructure
    {
        public static Thread SetThread(Action onFinished, Action doThis)
        {
            var thread = new Thread(() =>
            {
                try
                {
                    doThis();
                }
                finally
                {
                    onFinished();
                }

            });
            return thread;
        }

        public static void PlayThread(Action onFinished, Action doThis)
        {
            var thread = new Thread(() =>
            {
                try
                {
                    doThis();
                }
                finally
                {
                    onFinished();
                }

            });
            thread.Start();
        }
    }
}
