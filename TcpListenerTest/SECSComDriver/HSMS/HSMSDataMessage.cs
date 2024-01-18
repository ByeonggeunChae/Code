using SECSControl.Common;
using System;
using System.Collections.Concurrent;

namespace SECSControl.HSMS
{
    internal class HSMSDataMessage : CustomThread
    {
        private bool IsThreadRun = false;
        private ConcurrentQueue<MSGItem> mMessageQueue = new ConcurrentQueue<MSGItem>();
        private HSMSHandler mHandler;

        internal void Initialize(HSMSHandler handler)
        {
            mHandler = handler;
            IsThreadRun = true;
            Start();
        }

        internal void Terminate()
        {
            IsThreadRun = false;
        }

        internal void Enqueue(MSGItem data)
        {
            mMessageQueue.Enqueue(data);
        }

        internal override void Run()
        {
            MSGItem data = null;
            while (IsThreadRun)
            {
                try
                {
                    if (!mMessageQueue.IsEmpty)
                    {
                        mMessageQueue.TryDequeue(out data);
                        ProcessMessage(data);
                    }
                }
                catch (Exception e)
                {

                }
            }
        }

        private void ProcessMessage(MSGItem data)
        {

        }
    }
}
