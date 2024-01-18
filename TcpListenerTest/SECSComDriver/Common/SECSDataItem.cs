using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SECSControl.Common
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
    }
}
