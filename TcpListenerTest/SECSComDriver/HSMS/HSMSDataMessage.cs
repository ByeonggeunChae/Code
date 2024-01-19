using SECSControl.Common;
using SECSControl.SECS;
using System;
using System.Collections.Concurrent;
using System.Text;

namespace SECSControl.HSMS
{
    internal class HSMSDataMessage : CustomThread
    {
        private bool IsThreadRun = false;
        private ConcurrentQueue<MSGItem> mMessageQueue = new ConcurrentQueue<MSGItem>();
        private HSMSHandler mHandler;
        SECSDataItem mSECSDataItem;
        private byte[] mDataBytes;
        private int mDataPos = 0;

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
            SECSDataItem secs = GetSECSItem(data);
        }

        internal SECSDataItem GetSECSItem(MSGItem data)
        {
            mSECSDataItem = new SECSDataItem();
            mDataPos = 0;

            GetSECSHeader(data.Header);
            GetSECSData(data.DataItem);

            return mSECSDataItem;
        }

        internal void GetSECSHeader(byte[] headerBytes)
        {
            try
            {
                byte[] arrayDeviceID = new byte[2];
                byte[] arraySystembytes = new byte[4];

                Array.Copy(headerBytes, 0, arrayDeviceID, 0, 2);
                Array.Copy(headerBytes, 6, arraySystembytes, 0, 4);

                mSECSDataItem.DeviceID = Config.Bytes2Int(arrayDeviceID);
                mSECSDataItem.SystemBytes = Config.Bytes2Long(arraySystembytes);
                mSECSDataItem.WaitBit = (headerBytes[2] & 0x80) == 0x80 ? true : false;
                mSECSDataItem.Stream = (int)(headerBytes[2] & 0x7F);
                mSECSDataItem.Function = headerBytes[3];
            }
            catch (Exception ex)
            {

            }
        }

        internal void GetSECSData(byte[] dataBytes)
        {
            try
            {
                if (dataBytes == null || dataBytes.Length == 0)
                    return;

                mDataBytes = dataBytes;
                mDataPos = 0;

                while (mDataPos < mDataBytes.Length)
                {
                    byte type = mDataBytes[mDataPos++];

                    switch (type)
                    {
                        //  L
                        case 0x01:
                        case 0x02:
                        case 0x03:
                            SetList(type - (byte)SECSII_TYPE.L);
                            break;
                        //  B
                        case 0x21:
                        case 0x22:
                        case 0x23:
                            SetBinary(type - (byte)SECSII_TYPE.B);
                            break;
                        //  BOOLEAN
                        case 0x25:
                        case 0x26:
                        case 0x27:
                            SetBooean(type - (byte)SECSII_TYPE.BOOLEAN);
                            break;
                        //  A
                        case 0x41:
                        case 0x42:
                        case 0x43:
                            SetASCII(type - (byte)SECSII_TYPE.A);
                            break;
                        //  I1
                        case 0x65:
                        case 0x66:
                        case 0x67:
                            SetInt1(type - (byte)SECSII_TYPE.I1);
                            break;
                        //  I2
                        case 0x69:
                        case 0x6A:
                        case 0x6B:
                            SetInt2(type - (byte)SECSII_TYPE.I2);
                            break;
                        //  I4
                        case 0x71:
                        case 0x72:
                        case 0x73:
                            SetInt4(type - (byte)SECSII_TYPE.I4);
                            break;
                        //  I8
                        case 0x61:
                        case 0x62:
                        case 0x63:
                            SetInt8(type - (byte)SECSII_TYPE.I8);
                            break;
                        //  F4
                        case 0x91:
                        case 0x92:
                        case 0x93:
                            break;
                        //  F8
                        case 0x81:
                        case 0x82:
                        case 0x83:
                            break;
                        //  U1
                        case 0xA5:
                        case 0xA6:
                        case 0xA7:
                            break;
                        //  U2
                        case 0xA9:
                        case 0xAA:
                        case 0xAB:
                            break;
                        //  U4
                        case 0xB1:
                        case 0xB2:
                        case 0xB3:
                            break;
                        //  U8
                        case 0xA1:
                        case 0xA2:
                        case 0xA3:
                            break;
                    }
                }
            }
            catch (Exception e) 
            {
                
            }
        }

        internal int GetDataLength(int length)
        {
            byte[] lengthArray = new byte[length];
            Array.Copy(mDataBytes, mDataPos, lengthArray, 0, length);
            mDataPos += length;
            return Config.Bytes2Int(lengthArray);
        }

        internal void SetList(int length)
        {
            mSECSDataItem.AddValue(SECSII_TYPE.L, GetDataLength(length), "");
        }

        internal void SetBinary(int length)
        {
            int nDataLength = GetDataLength(length);
            StringBuilder sbBinary = new StringBuilder();

            for(int index = mDataPos; index < mDataPos + nDataLength; index++)
                sbBinary.AppendFormat("{0} ", mDataBytes[index]);

            mDataPos += nDataLength;

            mSECSDataItem.AddValue(SECSII_TYPE.B, nDataLength, sbBinary.ToString());
        }

        internal void SetBooean(int length)
        {
            int nDataLength = GetDataLength(length);
            StringBuilder sbBinary = new StringBuilder();

            for (int index = mDataPos; index < mDataPos + nDataLength; index++)
                sbBinary.AppendFormat("{0} ", mDataBytes[index]);

            mDataPos += nDataLength;

            mSECSDataItem.AddValue(SECSII_TYPE.BOOLEAN, nDataLength, sbBinary.ToString());
        }

        internal void SetASCII(int length)
        {
            string strValue = Encoding.Default.GetString(mDataBytes, mDataPos, length);
            mSECSDataItem.AddValue(SECSII_TYPE.A, length, strValue);
        }

        internal void SetInt1(int length)
        {

        }

        internal void SetInt2(int length)
        {

        }

        internal void SetInt4(int length)
        {

        }

        internal void SetInt8(int length)
        {

        }
    }
}
