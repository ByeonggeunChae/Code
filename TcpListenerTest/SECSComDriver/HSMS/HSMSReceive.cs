using SECSControl.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;

namespace SECSControl.HSMS
{
    internal class HSMSReceive : CustomThread
    {
        HSMSHandler mHandler;
        bool IsThreadRun = false;
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

        internal override void Run()
        {
            Debug.WriteLine("Initialize HSMSReceiveThread!!");

            int nReadLength = 0;

            while(IsThreadRun)
            {
                try
                {
                    if (mHandler.mReader == null)
                        Thread.Sleep(1000);
                    else
                    {
                        if(!mHandler.mReader.BaseStream.CanRead)
                        {
                            
                        }

                        nReadLength = IPAddress.NetworkToHostOrder(mHandler.mReader.ReadInt32());   //  Read Length
                        mHandler.HSMSReceive(SetHSMSData(ReadSocketStream(nReadLength)));
                    }
                }
                catch(Exception ex)
                {

                }
            }
            Debug.WriteLine("Terminate HSMSReceiveThread!!");
        }

        private MSGItem SetHSMSData(byte[] readByte)
        {
            MSGItem data = new MSGItem()
            {
                Length = readByte.Length,
                Header = new byte[10]
            };
            data.DataItem = new byte[data.Length - 10];
            Array.Copy((Array)readByte, (Array)data.Header, 10);
            Array.Copy((Array)readByte, 10, (Array)data.DataItem, 0, data.DataItem.Length);
            return data;
        }

        private byte[] ReadSocketStream(int ReadLength)
        {
            int index = 0;
            byte[] buffer = new byte[ReadLength];
            //this._timer.Start(eTimeout.T8);
            while (index < ReadLength)
            {
                int num = mHandler.mReader.Read(buffer, index, ReadLength - index);
                //this.mDebugLog.WriteDebugLog(nameof(HSMSReceive), nameof(ReadSocketStream), string.Format("ReadSocketStream length = {0} read = {1} readnow = {2}", (object)ReadLength.ToString(), (object)index.ToString(), (object)num.ToString()), eLevel.Low);
                if (num == 0)
                {
                    Thread.Sleep(500);
                    //this.mDebugLog.WriteDebugLog(nameof(HSMSReceive), nameof(ReadSocketStream), "Abnormal Length is 0", eLevel.Low);
                }
                if (num < 0)
                {
                    //this.mDebugLog.WriteDebugLog(nameof(HSMSReceive), nameof(ReadSocketStream), "Throw EndOfStreamException", eLevel.Low);
                    throw new EndOfStreamException(num.ToString());
                }
                //this._timer.Restart(eTimeout.T8);
                index += num;
                //this._hsms._readLengthT8 = index;
            }
            //this._timer.Stop(eTimeout.T8);
            return buffer;
        }
    }
}
