namespace TraceabilitySystem
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    [StandardModule]
    public sealed class modSV_LogWrite
    {
        private const string CLASS_NAME = "modSV_LogWrite";
        private const long RETRY_MAX_COUNT = 3L;
        private const string LOG_FILE_EXTENSION = ".log";
        private const string SPLIT_STRING = ";";
        private static LOG_PARAM ClsLogParam = new LOG_PARAM();

        private static string FncAddFileDate(string prmOrgFileName, DateTime prmProcDate) => 
            (prmOrgFileName + prmProcDate.ToString("yyyyMMdd"));

        private static bool FncCheckPutLog(EN_LogType prmLogType)
        {
            switch (prmLogType)
            {
                case EN_LogType.Evt:
                    return ClsLogParam.EnableEvent;

                case EN_LogType.DbSelect:
                    return ClsLogParam.EnableDBSelect;

                case EN_LogType.DbIUD:
                    return ClsLogParam.EnableDBUpdate;

                case EN_LogType.Ope:
                    return ClsLogParam.EnableOperate;

                case EN_LogType.Transfer:
                    return ClsLogParam.EnableTransfer;
            }
            return true;
        }

        private static bool FncCheckValidLogDir()
        {
            bool flag;
            try
            {
                if (Directory.Exists(ClsLogParam.LogPath))
                {
                    return true;
                }
                Directory.CreateDirectory(ClsLogParam.LogPath);
                flag = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                flag = false;
                ProjectData.ClearProjectError();
            }
            return flag;
        }

        private static string FncGetLogFileHeader(EN_LogType prmLogType)
        {
            switch (prmLogType)
            {
                case EN_LogType.Err:
                    return "Error_";

                case EN_LogType.Evt:
                    return "Event_";

                case EN_LogType.DbSelect:
                    return "DBSelect_";

                case EN_LogType.DbIUD:
                    return "DBIUD_";

                case EN_LogType.Ope:
                    return "Ope_";

                case EN_LogType.Transfer:
                    return "Trans_";

                case EN_LogType.Special:
                    return "Change_";

                case EN_LogType.PassHistory:
                    return "Pass_";
            }
            return "Log_";
        }

        private static string FncGetLogNametoDateDate(string prmLogFile)
        {
            string str;
            try
            {
                str = prmLogFile.Replace(".log", "").Replace(FncGetLogFileHeader(EN_LogType.DbSelect), "").Replace(FncGetLogFileHeader(EN_LogType.DbIUD), "").Replace(FncGetLogFileHeader(EN_LogType.Err), "").Replace(FncGetLogFileHeader(EN_LogType.Evt), "").Replace(FncGetLogFileHeader(EN_LogType.Ope), "").Replace(FncGetLogFileHeader(EN_LogType.Transfer), "").Replace(FncGetLogFileHeader(EN_LogType.Special), "").Replace(FncGetLogFileHeader(EN_LogType.PassHistory), "");
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                throw exception;
            }
            return str;
        }
        public static void SVSubWriteLog(EN_LogType prmLogType, string prmClassName, string prmFunctionName, string prmHeader, string prmDescription, string prmSubFolder = "")
        {
            object clsLogParam = ClsLogParam;
            lock (clsLogParam)
            {
                if (FncCheckPutLog(prmLogType) && FncCheckValidLogDir())
                {
                    long num = 0L;
                    num = 1L;
                    do
                    {
                        StreamWriter writer = null;
                        bool flag2 = false;
                        try
                        {
                            string str = FncAddFileDate(FncGetLogFileHeader(prmLogType), DateAndTime.Now);
                            string path = string.Empty;
                            if (ClsLogParam.AppPathSeparate)
                            {
                                if (prmSubFolder != "")
                                {
                                    path = ClsLogParam.LogPath + prmSubFolder + @"\";
                                }
                                else
                                {
                                    path = ClsLogParam.LogPath;
                                }
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }
                                path = path + str + ".log";
                            }
                            else
                            {
                                path = ClsLogParam.LogPath + str + ".log";
                            }
                            writer = new StreamWriter(path, true, Encoding.Default);
                            writer.BaseStream.Seek(0L, SeekOrigin.End);
                            writer.WriteLine(((((((string.Empty + DateAndTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + ";")  +
                                prmClassName.Trim() + ";") + prmFunctionName.Trim() + ";") + prmHeader.Trim() + ";") +
                                prmDescription).Replace("\x0002", "[STX]").Replace("\x0003", "[ETX]").Replace("\x0006", "[ACK]").Replace("\x0015", "[NAK]").Replace("\r\n",
                                "[CRLF]").Replace("\r", "[CR]").Replace("\n", "[LF]")));
                            flag2 = true;
                        }
                        catch (Exception exception1)
                        {
                            ProjectData.SetProjectError(exception1);
                            Exception exception = exception1;
                            ProjectData.ClearProjectError();
                        }
                        finally
                        {
                            if (writer != null)
                            {
                                writer.Close();
                            }
                        }
                        if (flag2)
                        {
                            break;
                        }
                        num += 1L;
                    }
                    while (num <= 3L);
                }
            }
        }

        public enum EN_LogType
        {
            Err,
            Evt,
            DbSelect,
            DbIUD,
            Ope,
            Transfer,
            Special,
            PassHistory
        }

        private class LOG_PARAM
        {
            public string LogPath = Application.StartupPath;
            public bool EnableEvent = true;
            public bool EnableDBSelect = true;
            public bool EnableDBUpdate = true;
            public bool EnableOperate = true;
            public bool EnableTransfer = true;
            public bool AppPathSeparate = false;
        }
    }
}

