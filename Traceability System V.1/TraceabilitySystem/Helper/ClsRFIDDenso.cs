namespace TraceabilitySystem
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    public class ClsRFIDDenso
    {
        public static int EPC_SIZE = 96;
        private string _RFID_Device = "UR21";
        public string p_RFID_Device =>
           this._RFID_Device;
        private const string CLASS_NAME = "ClsDensoRFIDDenso";
        private const int START_POS = 6;
        public const int LANCOMPORT = 0x3e7;
        public const int RFID_LAN_PORT = 0x4e20;
        private string _Type = "COM";
        private string _IpAddRess = "";
        private byte _ComPort = 0;
        private int _SignalStrength = 50;
        private int _AntenaPort = 1;
        private int _QFactor = 4;
        private int _SESSION = 0;
        private string _ReadWithPC_EPC = "";
        private ushort _DevNo;
        private static string p_RFIDDW_Type;
        private static string p_RFIDDW_IpAddRess;
        public ClsRFIDDenso(int prmPortNo)
        {
            if (prmPortNo == 0x3e7)
            {
                this._ComPort = 0;
            }
            else
            {
                this._ComPort = (byte) prmPortNo;
            }
        }

        public bool fncAbort()
        {
            bool flag;
            string name = MethodBase.GetCurrentMethod().Name;
            try
            {
                int number = (int) UtsAbort(this._ComPort);
                if (number == 0)
                {
                    //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Transfer, "ClsDensoRFIDDenso", name, "バッファクリアOK", "COMポート:" + Conversions.ToString(this._ComPort), "");
                }
                else
                {
                    //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Err, "ClsDensoRFIDDenso", name, "バッファクリアNG", "COMポート:" + Conversions.ToString(this._ComPort) + " エラーコード:0x" + Conversion.Hex(number).PadLeft(4, '0'), "");
                    return false;
                }
                flag = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Err, "ClsDensoRFIDDenso", name, "System Error", exception.Message, "");
                flag = false;
                ProjectData.ClearProjectError();
            }
            return flag;
        }

        public bool fncConnectLanDevice(string prmIPAddress, int prmPort)
        {
            string name = MethodBase.GetCurrentMethod().Name;
            try
            {
                byte[] addressBytes = IPAddress.Parse(prmIPAddress).GetAddressBytes();
                byte[] destinationArray = new byte[9];
                Array.Copy(addressBytes, destinationArray, addressBytes.Length);
                int number = (int) UtsCreateLanDevice((uint) BitConverter.ToInt64(destinationArray.ToArray<byte>(), 0), (ushort) prmPort, ref this._DevNo);
                if (number == 0)
                {
                    //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Transfer, "ClsDensoRFIDDenso", name, "ネットワーク情報登録OK", "【IPアドレス】" + prmIPAddress + " 【ポート番号】" + Conversions.ToString(prmPort), "");
                }
                else
                {
                    //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Transfer, "ClsDensoRFIDDenso", name, "ネットワーク情報登録NG", "【IPアドレス】" + prmIPAddress + " 【ポート番号】" + Conversions.ToString(prmPort) + " 【エラーコード】0x" + Conversion.Hex(number).PadLeft(4, '0'), "");
                    return false;
                }
                number = (int) UtsSetCurrentLanDevice(this._DevNo);
                if (number == 0)
                {
                    //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Transfer, "ClsDensoRFIDDenso", name, "LAN接続準備OK", "【IPアドレス】" + prmIPAddress + " 【ポート番号】" + Conversions.ToString(prmPort), "");
                }
                else
                {
                    //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Transfer, "ClsDensoRFIDDenso", name, "LAN接続準備NG", "【IPアドレス】" + prmIPAddress + " 【ポート番号】" + Conversions.ToString(prmPort) + " 【エラーコード】0x" + Conversion.Hex(number).PadLeft(4, '0'), "");
                    return false;
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Err, "ClsDensoRFIDDenso", name, "System Error", exception.Message, "");
                ProjectData.ClearProjectError();
            }
            return true;
        }

        public bool fncDeleteLanDevice()
        {
            bool flag;
            string name = MethodBase.GetCurrentMethod().Name;
            try
            {
                int number = (int) UtsDeleteLanDevice(this._DevNo);
                if (number == 0)
                {
                    //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Transfer, "ClsDensoRFIDDenso", name, "ネットワーク情報削除OK", "管理番号" + Conversions.ToString((uint) this._DevNo), "");
                }
                else
                {
                    //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Err, "ClsDensoRFIDDenso", name, "ネットワーク情報削除NG", "管理番号:" + Conversions.ToString((uint) this._DevNo) + " エラーコード:0x" + Conversion.Hex(number).PadLeft(4, '0'), "");
                    return false;
                }
                flag = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Err, "ClsDensoRFIDDenso", name, "System Error", exception.Message, "");
                flag = false;
                ProjectData.ClearProjectError();
            }
            return flag;
        }
        public int fncGetData(ref ClsUiiData prmClsUII)
        {
            int num;
            string name = MethodBase.GetCurrentMethod().Name;
            byte[] buffer = new byte[0x44];
            uint bufCount = 1;
            try
            {
                uint num4 = 0;
                Thread.Sleep(500);
                int number = (int) UtsGetContinuousReadResult(this._ComPort, ref buffer[0], bufCount, ref num4);
                if (number == 0)
                {
                    if (num4 > 0)
                    {
                        prmClsUII = new ClsUiiData();
                        prmClsUII.UIIBuff = new byte[(buffer.Length - 1) + 1];
                        buffer.CopyTo(prmClsUII.UIIBuff, 0);

                        byte[] buffer2 = new byte[(buffer[0] - 1) + 1];
                        int num5 = buffer[0] - 1;
                        for (int i = 0; i <= num5; i++)
                        {
                            buffer2[i] = buffer[6 + i];
                        }
                        prmClsUII.EncodeData = BitConverter.ToString(buffer2).Replace("-", "");
                        //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Transfer, "ClsDensoRFIDDenso", name, "データ取得OK", "COMポート:" + Conversions.ToString(this._ComPort), "");
                        return 1;
                    }
                }
                else
                {
                    return -1;
                }
                num = 0;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Err, "ClsDensoRFIDDenso", name, "System Error", exception.Message, "");
                num = -1;
                ProjectData.ClearProjectError();
            }
            return num;
        }

        public bool fncRFID_ConnectON()
        {
            bool flag;
            string name = MethodBase.GetCurrentMethod().Name;
            try
            {
                if ((p_RFIDDW_Type != "LAN") || this.fncConnectLanDevice(p_RFIDDW_IpAddRess, 0x4e20))
                {
                    int number = (int) UtsOpen(this._ComPort);
                    if (number == 0)
                    {
                        //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Evt, "ClsDensoRFIDDenso", name, "RFIDリーダー接続成功", "接続ポート:" + Conversions.ToString(this._ComPort), "");
                        goto Label_00BF;
                    }
                    //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Err, "ClsDensoRFIDDenso", name, "RFIDリーダー接続失敗", "接続ポート:" + Conversions.ToString(this._ComPort) + " エラーコード:0x" + Conversion.Hex(number).PadLeft(4, '0'), "");
                }
                return false;
            Label_00BF:
                flag = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Err, "ClsDensoRFIDDenso", name, "System Error", exception.Message, "");
                flag = false;
                ProjectData.ClearProjectError();
            }
            return flag;
        }

        public bool fncStartRead()
        {
            bool flag;
            string name = MethodBase.GetCurrentMethod().Name;
            try
            {
                int number = (int) UtsStartContinuousReadEx(this._ComPort);
                if (number == 0)
                {
                    //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Transfer, "ClsDensoRFIDDenso", name, "読取開始スタート", "COMポート:" + Conversions.ToString(this._ComPort), "");
                }
                else
                {
                    //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Err, "ClsDensoRFIDDenso", name, "読取開始NG", "COMポート:" + Conversions.ToString(this._ComPort) + " エラーコード:0x" + Conversion.Hex(number).PadLeft(4, '0'), "");
                    return false;
                }
                flag = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Err, "ClsDensoRFIDDenso", name, "System Error", exception.Message, "");
                flag = false;
                ProjectData.ClearProjectError();
            }
            return flag;
        }

        public bool fncStopRead()
        {
            bool flag;
            try
            {
                int number = (int) UtsStopContinuousRead(this._ComPort);
                if (number == 0)
                {
                    //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Transfer, "ClsDensoRFIDDenso", name, "読取停止OK", "COMポート:" + Conversions.ToString(this._ComPort), "");
                }
                else
                {
                    //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Err, "ClsDensoRFIDDenso", name, "読取停止NG", "COMポート:" + Conversions.ToString(this._ComPort) + " エラーコード:0x" + Conversion.Hex(number).PadLeft(4, '0'), "");
                    return false;
                }
                flag = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Err, "ClsDensoRFIDDenso", name, "System Error", exception.Message, "");
                flag = false;
                ProjectData.ClearProjectError();
            }
            return flag;
        }

        public bool fncWriteRFIDData(ref ClsUiiData prmClsUII, int nWriteSizeByte, string uiiValie)
        {

            bool flag =false;
            uint num2 = 0;
            string name = MethodBase.GetCurrentMethod().Name;
            ushort listNum = 1;
            byte[] buffer2 = new byte[340];
            uint num4 = 0;
            try
            {
                Application.DoEvents();
                string[] textArray4;
                string[] textArray3;
                int num8=0;
                byte[] buffer = new byte[0x100];
                int index = 0;
                do
                {
                    buffer[index] = 0;
                    index++;
                }
                while (index <= 0xff);
                buffer[0] = Convert.ToByte(Conversion.Val(WriteParam.Bank));
                buffer[1] = Convert.ToByte(Conversion.Val(WriteParam.Reserved));
                buffer[2] = Convert.ToByte(nWriteSizeByte);
                buffer[3] = 0;
                buffer[4] = Convert.ToByte(WriteParam.Ptr);
                buffer[5] = 0;
                buffer[6] = Convert.ToByte(Conversion.Val(WriteParam.Accesspwd));
                buffer[7] = Convert.ToByte(Conversion.Val(WriteParam.Accesspwd));
                buffer[8] = Convert.ToByte(Conversion.Val(WriteParam.Accesspwd));
                buffer[9] = Convert.ToByte(Conversion.Val(WriteParam.Accesspwd));
                int num6 = nWriteSizeByte - 1;
                for (int i = 0; i <= num6; i++)
                {
                    buffer[10 + i] = Convert.ToByte((int) ((Convert.ToByte(Conversions.ToString(uiiValie[i * 2]), 0x10) * 0x10) + Convert.ToByte(Conversions.ToString(uiiValie[(i * 2) + 1]), 0x10)));
                }
                num4 = UtsStartTagComm(this._ComPort, 3, 0, ref buffer[0], 1, listNum, ref prmClsUII.UIIBuff[0]);
                if ((num4 != 0) && (num4 != 0xa301L))
                {
                    goto Label_0297;
                }
                do
                {
                    uint num3 =0;
                    num4 = UtsGetTagCommResult(this._ComPort, ref buffer2[0], listNum, ref num2, ref num3);
                    if (num4 == 0)
                    {
                        goto Label_018B;
                    }
                }
                while (num4 == 1);
                goto Label_024C;
            Label_018B:
                num8 = buffer2[0] + (buffer2[1] * 0x100);
                if ((num8 == 0) && (num2 > 0L))
                {
                    //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Evt, "ClsDensoRFIDDenso", name, "RFタグ書込み正常完了", "[" + uiiValie + "] => [" + prmClsUII.EncodeData + "]", "");
                    return true;
                }
                //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Err, "ClsDensoRFIDDenso", name, "RFタグ書込み完了エラー(タグ通信エラー)", "[" + uiiValie + "] => [" + prmClsUII.EncodeData + "]ResCode = " + Conversions.ToString(num8), "");
                return false;
            Label_024C:
                textArray3 = new string[] { "[", uiiValie, "] => [", prmClsUII.EncodeData, "]" };
                //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Err, "ClsDensoRFIDDenso", name, "RFタグ書込み完了エラー(異常終了)", string.Concat(textArray3), "");
                return false;
            Label_0297:
                textArray4 = new string[] { "[", uiiValie, "] => [", prmClsUII.EncodeData, "]" };
                //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Err, "ClsDensoRFIDDenso", name, "RFタグ書込み開始エラー", string.Concat(textArray4), "");
                flag = false;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Err, "ClsDensoRFIDDenso", name, "System Error", exception.Message, "");
                ProjectData.ClearProjectError();
            }
            finally
            {
                listNum = 0;
                num2 = 0;
                num4 = 0;
            }
            return flag;
        }

        public void subRFID_ConnectOFF()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            try
            {
                int number = (int) UtsClose(this._ComPort);
                if (number == 0)
                {
                    //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Transfer, "ClsDensoRFIDDenso", name, "RFIDリーダー切断成功", "接続ポート:" + Conversions.ToString(this._ComPort), "");
                    if (p_RFIDDW_Type == "LAN")
                    {
                        this.fncDeleteLanDevice();
                    }
                }
                else
                {
                    //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Err, "ClsDensoRFIDDenso", name, "RFIDリーダー切断失敗", "接続ポート:" + Conversions.ToString(this._ComPort) + " エラーコード:0x" + Conversion.Hex(number).PadLeft(4, '0'), "");
                }
                SVFncClearGarbageCollection();
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                //modSV_LogWrite.SVSubWriteLog(//modSV_LogWrite.EN_LogType.Err, "ClsDensoRFIDDenso", name, "System Error", exception.Message, "");
                ProjectData.ClearProjectError();
            }
        }
        public static bool SVFncClearGarbageCollection()
        {
            try
            {
                GC.Collect(0);
                GC.Collect(1);
                GC.Collect(2);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
                return false;
            }
            return true;
        }
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsAbort(byte Port);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsCheckAlive(byte Port);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsClose(byte Port);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsCreateLanDevice(uint IPaddress, ushort TcpPort, ref ushort DevNo);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsDeleteLanDevice(ushort DevNo);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsGetContinuousReadResult(byte Port, ref byte UIIBUF, uint BufCount, ref uint GetCount);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsGetCurrentLanDevice(ref ushort DevNo);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsGetLanDeviceInfo(ushort DevNo, ref uint IPaddress, ref ushort TcpPort, ref uint Status);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsGetNetworkConfig(byte Port, ref uint IPaddress, ref uint SubnetMask, ref uint Gateway);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsGetParameter(byte Port, ushort Tag, ref byte TLVDATA);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsGetProductNo(byte Port, ref byte MainNo, ref byte RFNo);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsGetTagCommResult(byte Port, ref byte RESULTBUF, uint BufCount, ref uint GetCount, ref uint RestCount);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsGetUii(byte Port, ref byte UIIBUF, uint BufCount, ref uint GetCount, ref uint RestCount);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsGetVersions(byte Port, ref byte OSVer, ref byte MainVer, ref byte RFVer, ref byte ChipVer, ref byte OEMver);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsInitialReset(byte Port);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsListLanDevice(ref ushort DevCount, ref byte DEVICELIST);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsLoadParameter(byte Port, ref byte FilePath);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsOpen(byte Port);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsReadUii(byte Port);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsSetCurrentLanDevice(ushort DevNo);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsSetLanDevice(ushort DevCount, ref byte DEVICELIST);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsSetNetworkConfig(byte Port, uint IPaddress, uint SubnetMask, uint Gateway);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsSetParameter(byte Port, byte TLVDATA);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsSetTagList(byte Port, byte Type, ushort ListNum, ref byte UIIBUF);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsStartContinuousRead(byte Port);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsStartContinuousReadEx(byte Port);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsStartTagComm(byte Port, byte TagCmd, ushort Antenna, ref byte Param, byte ListEnable, ushort ListNum, ref byte UIIBUF);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsStopContinuousRead(byte Port);
        [DllImport("RfidTs.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern uint UtsUpdateDevice(byte Port, ref byte FilePath);

        public string p_Type
        {
            set
            {
                switch (value.ToUpper())
                {
                    case "LAN":
                    case "COM":
                        this._Type = value.ToUpper();
                        break;
                }
            }
        }

        public string p_IpAddRess
        {
            set
            {
                this._IpAddRess = value;
            }
        }

        public int p_SignalStrength
        {
            set
            {
                this._SignalStrength = value;
            }
        }

        public int p_AntenaPort
        {
            set
            {
                this._AntenaPort = value;
            }
        }

        public int p_QFactor
        {
            set
            {
                this._QFactor = value;
            }
        }

        public int p_SESSION =>
            this._SESSION;

        public string p_ReadWithPC_EPC =>
            this._ReadWithPC_EPC;

        public ushort p_DevNo
        {
            set
            {
                this._DevNo = value;
            }
        }

        public class ClsUiiData
        {
            public byte[] UIIBuff;
            public string EncodeData = string.Empty;
            public string ItemCode = string.Empty;
        }

        public class RFID_DATA
        {
            public DateTime ReadDate;
            public string EpcCode;
            public string ItemCode;
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct WriteParam
        {
            public static string Bank;
            public static string Reserved;
            public static short Size;
            public static short Ptr;
            public static string Accesspwd;
            public static byte Writedata;
            static WriteParam()
            {
                Bank = "1";
                Reserved = "0";
                Size = 2;
                Ptr = 4;
                Accesspwd = "00";
            }
        }
    }
}

