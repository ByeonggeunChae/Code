using System;

namespace SECSControl.Common
{
    #region ENUM
    public enum SECS_STATUS
    {
        UNKNOWN = 0,
        DISCONNECT,
        CONNECT,
        SELECT
    }

    public enum SECS_ERROR
    {
        NONE = 0,
        UNKNOWN
    }

    public enum SECSII_TYPE
    {
        L = 0x00,
        B = 0x20,
        BOOLEAN = 0x24,
        A = 0x40,
        I1 = 0x64,
        I2 = 0x68,
        I4 = 0x70,
        I8 = 0x60,
        F4 = 0x90,
        F8 = 0x80,        
        U1 = 0xA4,
        U2 = 0xA8,
        U4 = 0xB0,
        U8 = 0xA0
    }

    public enum TIME_OUT
    {
        T1 = 0, T2, T3, T4, T5, T6, T7, T8
    }
    #endregion

    #region STRUCT
    public struct HSMS_MSG
    {
        public byte[] SessionID;
        public byte StreamNo;
        public byte FunctionNo;
        public byte Ptype;
        public byte Stype;
        public byte[] SystemByte;

        public void SetHeaderMsg(byte[] item)
        {
            SessionID = new byte[2];
            Array.Copy(item, SessionID, 2);
            StreamNo = item[2];
            FunctionNo = item[3];
            Ptype = item[4];
            Stype = item[5];
            SystemByte = new byte[4];
            Array.Copy(item, 6, SystemByte, 0, 4);
        }

        public void SetDataMsg()
        {

        }
    }
    #endregion

    internal class Config
    {
        internal string EquipmentID = "";
        internal string Version = "";
        internal string RemoteIPAddress = "127.0.0.1";
        internal string LocalIPAddress = "";
        internal int RemotePort = 5000;
        internal int LocalPort = 5000;
        internal int T1 = 500;  //  0.5s
        internal int T2 = 1000; //  1s
        internal int T3 = 45000;    //45s
        internal int T4 = 45000;    //45s
        internal int T5 = 10000;    //10s
        internal int T6 = 5000; //5s
        internal int T7 = 10000;    //10s
        internal int T8 = 10000;    //10s

        internal static byte[] Int2Bytes(int value, int size, bool reverse = false)
        {
            byte[] convertBytes = BitConverter.GetBytes(value);
            if ((!reverse && BitConverter.IsLittleEndian) || (reverse && !BitConverter.IsLittleEndian))
                Array.Reverse(convertBytes, 0, size);
            Array.Resize(ref convertBytes, size);
            return convertBytes;
        }

        internal static byte[] Int2Bytes(long value, int size, bool reverse = false)
        {
            byte[] convertBytes = BitConverter.GetBytes(value);
            if ((!reverse && BitConverter.IsLittleEndian) || (reverse && !BitConverter.IsLittleEndian))
                Array.Reverse(convertBytes);

            Array.Resize(ref convertBytes, size);
            return convertBytes;
        }

        internal static int Bytes2Int(byte[] value, bool reverse = false)
        {
            if(value.Length != 4)
            {
                if (reverse)
                    Array.Resize(ref value, 4);
                else
                {
                    byte[] tempArray = new byte[4];
                    Array.Copy(value, 0, tempArray, 4 - value.Length, value.Length);
                    Array.Resize(ref value, 4);
                    Array.Copy(tempArray, value, tempArray.Length);
                }
            }

            if ((!reverse && BitConverter.IsLittleEndian) || (reverse && !BitConverter.IsLittleEndian))
                Array.Reverse(value);

            return BitConverter.ToInt32(value, 0);


            //int convertValue = 0;

            //switch (value.Length)
            //{
            //    case 1:
            //        convertValue = (int)value[0];
            //        break;
            //    case 2:
            //        convertValue = BitConverter.ToInt16(value, 0);
            //        break;
            //    case 4:
            //        convertValue = BitConverter.ToInt32(value, 0);
            //        break;
            //}
            //return convertValue;
        }

        internal static long Bytes2Long(byte[] value, bool reverse = false)
        {
            if ((!reverse && BitConverter.IsLittleEndian) || (reverse && !BitConverter.IsLittleEndian))
                Array.Reverse(value);

            long convertValue = 0;
            switch (value.Length)
            {
                case 4:
                    convertValue = BitConverter.ToInt32(value, 0);
                    break;
                case 8:
                    convertValue = BitConverter.ToInt64(value, 0);
                    break;
            }
            return convertValue;
        }
    }
}
