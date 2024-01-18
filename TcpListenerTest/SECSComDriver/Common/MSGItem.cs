namespace SECSControl.Common
{
    internal class MSGItem
    {
        internal int Length;
        internal byte[] Header;
        internal byte[] DataItem;
        internal bool IsControlMsg;
        internal byte[] CheckSum;
        internal string MessageData = "";
    }
}
