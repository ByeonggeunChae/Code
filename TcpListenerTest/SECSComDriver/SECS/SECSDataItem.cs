using SECSControl.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SECSControl.SECS
{
    internal class SECSDataItem
    {
        private bool mWaitBit;
        private int mStream;
        private int mFunction;
        private int mErrorCode;
        private int mDeviceID;
        private long mSystemBytes;
        private string mErrorMessage = "";
        private ArrayList mDataValue;

        internal bool WaitBit
        {
            get => mWaitBit;
            set => mWaitBit = value;
        }

        internal int Stream
        {
            get => mStream;
            set => mStream = value;
        }

        internal int Function
        {
            get => mFunction;
            set => mFunction = value;
        }

        internal int ErrorCode
        {
            get => mErrorCode;
            set => mErrorCode = value;
        }

        internal int DeviceID
        {
            get => mDeviceID;
            set => mDeviceID = value;
        }

        internal long SystemBytes
        {
            get => mSystemBytes;
            set => mSystemBytes = value;
        }

        internal string ErrorMessage
        {
            get => mErrorMessage;
            set => mErrorMessage = value;
        }

        internal void AddValue(SECSII_TYPE type, int length, string value)
        {
            SECSIIData data = new SECSIIData();
            data.secsType = type;
            data.length = length;
            data.value = value;

            if (mDataValue == null)
                mDataValue = new ArrayList();
            mDataValue.Add(data);
        }
    }
}
