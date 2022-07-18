namespace TraceabilitySystem
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;

    public class clsSV_DB
    {
        private const string CLASS_NAME = "clsSV_DB";
        private const int RETRY_MAX_COUNT = 1;
        private SQL_S_CONNECTION _ClsConectSql = null;
        private EN_DbKind _conectKind = EN_DbKind.UnKnown;
        private DataTable _tableData = null;
        private string _className = "clsSV_DB";
        private string _procName = string.Empty;
        private string _logSubFolderName = string.Empty;
        private Exception _ClsErrorObj = null;

        public bool SVFncCheckConnectionStatus()
        {
            bool flag;
            try
            {
                if ((this._conectKind == EN_DbKind.SQLServer) && (this._ClsConectSql.ClsOleCn.State == ConnectionState.Open))
                {
                    return true;
                }
                flag = false;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                this._ClsErrorObj = exception;
                modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.Err, this._className, this._procName, "データベース開放エラー", this._ClsErrorObj.Message, this._logSubFolderName);
                flag = false;
                ProjectData.ClearProjectError();
            }
            return flag;
        }

        public int SVFncExecuteNonQuery(string prmSQL, bool prmPrimayLogPass = false)
        {
            int num;
            try
            {
                int num2 = -1;
                if (!prmPrimayLogPass)
                {
                    modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.DbIUD, this._className, this._procName, "ＤＢ登録", prmSQL, this._logSubFolderName);
                }
                if (this._conectKind != EN_DbKind.SQLServer)
                {
                    throw new ArgumentException("データベース未接続");
                }
                SqlCommand command = new SqlCommand(prmSQL, this._ClsConectSql.ClsOleCn);
                if (this._ClsConectSql.ClsTrans != null)
                {
                    command.Transaction = this._ClsConectSql.ClsTrans;
                }
                num2 = command.ExecuteNonQuery();
                if (prmPrimayLogPass)
                {
                    modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.DbIUD, this._className, this._procName, "データベース登録", prmSQL, this._logSubFolderName);
                }
                num = num2;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                this._ClsErrorObj = exception;
                if (prmPrimayLogPass)
                {
                    if (this.p_primaryErr)
                    {
                        num = -1;
                        ProjectData.ClearProjectError();
                        return num;
                    }
                    modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.DbIUD, this._className, this._procName, "データベース登録", prmSQL, this._logSubFolderName);
                }
                modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.Err, this._className, this._procName, "データベース登録エラー", exception.Message, this._logSubFolderName);
                num = -1;
                ProjectData.ClearProjectError();
            }
            return num;
        }

        public bool SVFncFinishTransaction(bool prmCommit)
        {
            bool flag;
            try
            {
                if ((this._conectKind == EN_DbKind.SQLServer) && (this._ClsConectSql.ClsTrans != null))
                {
                    if (prmCommit)
                    {
                        modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.DbIUD, this._className, this._procName, "コミット", this._ClsConectSql.ClsOleCn.ConnectionString, this._logSubFolderName);
                        this._ClsConectSql.ClsTrans.Commit();
                    }
                    else
                    {
                        modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.DbIUD, this._className, this._procName, "ロールバック", this._ClsConectSql.ClsOleCn.ConnectionString, this._logSubFolderName);
                        this._ClsConectSql.ClsTrans.Rollback();
                    }
                    this._ClsConectSql.ClsTrans = null;
                }
                flag = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                this._ClsErrorObj = exception;
                modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.Err, this._className, this._procName, (prmCommit ? "コミット" : "ロールバック") + "エラー", exception.Message, this._logSubFolderName);
                flag = false;
                ProjectData.ClearProjectError();
            }
            return flag;
        }

        public bool SVFncOpenConnection(EN_DbKind prmDtBase, string prmServiceID, string prmUserID, string prmPassWord, string prmDataBaseName)
        {
            bool flag =false;
            string str = string.Empty;
            try
            {
                int num = 0;
                this._conectKind = EN_DbKind.UnKnown;
                if (prmDtBase != EN_DbKind.SQLServer)
                {
                    throw new ArgumentException("データベース種別未定義");
                }
                str = str + "Server          = " + prmServiceID + ";";
                str = str + "User ID         = " + prmUserID + ";";
                str = str + "PassWord        = " + prmPassWord + ";";
                str = str + "Initial Catalog = " + prmDataBaseName;
                this._ClsConectSql = new SQL_S_CONNECTION();
                this._ClsConectSql.ClsOleCn.ConnectionString = str;
                num = 1;
                do
                {
                    try
                    {
                        this._ClsConectSql.ClsOleCn.Open();
                        return true;
                    }
                    catch (Exception exception1)
                    {
                        ProjectData.SetProjectError(exception1);
                        Exception exception = exception1;
                        this._ClsErrorObj = exception;
                        ProjectData.ClearProjectError();
                    }
                    num++;
                }
                while (num <= 1);
                this._ClsConectSql = null;
                modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.Err, this._className, this._procName, "データベース接続エラー", this._ClsErrorObj.Message + "(" + str + ")", this._logSubFolderName);
                flag = false;
            }
            catch (Exception exception3)
            {
                ProjectData.SetProjectError(exception3);
                Exception exception2 = exception3;
                this._ClsErrorObj = exception2;
                modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.Err, this._className, this._procName, "データベース接続エラー", this._ClsErrorObj.Message + "(" + str + ")", this._logSubFolderName);
                flag = false;
                ProjectData.ClearProjectError();
            }
            finally
            {
                if (flag)
                {
                    this._conectKind = prmDtBase;
                }
            }
            return flag;
        }

        public DataTable SVFncSelectDataTable(string prmSQL, bool prmLogHalt = false)
        {
            DataTable table = null;
            try
            {
                if (!prmLogHalt)
                {
                    modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.DbSelect, this._className, this._procName, "データベース検索", prmSQL, this._logSubFolderName);
                }
                this._tableData = new DataTable();
                if (this._conectKind != EN_DbKind.SQLServer)
                {
                    throw new ArgumentException("データベース未接続");
                }
                SqlCommand selectCommand = new SqlCommand(prmSQL, this._ClsConectSql.ClsOleCn);
                if (this._ClsConectSql.ClsTrans != null)
                {
                    selectCommand.Transaction = this._ClsConectSql.ClsTrans;
                }
                this._ClsConectSql.ClsAdapter = new SqlDataAdapter(selectCommand);
                this._ClsConectSql.ClsAdapter.Fill(this._tableData);
                table = this._tableData;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                this._ClsErrorObj = exception;
                if (prmLogHalt)
                {
                    modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.DbSelect, this._className, this._procName, "データベース検索", prmSQL, this._logSubFolderName);
                }
                modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.Err, this._className, this._procName, "データベース検索エラー", exception.Message, this._logSubFolderName);
                ProjectData.ClearProjectError();
            }
            return table;
        }

        public bool SVFncStartTransaction()
        {
            bool flag;
            try
            {
                if (this._conectKind != EN_DbKind.SQLServer)
                {
                    throw new ArgumentException("データベース未接続");
                }
                modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.DbIUD, this._className, this._procName, "トランザクション開始", this._ClsConectSql.ClsOleCn.ConnectionString, this._logSubFolderName);
                this._ClsConectSql.ClsTrans = this._ClsConectSql.ClsOleCn.BeginTransaction();
                flag = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                this._ClsErrorObj = exception;
                modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.Err, this._className, this._procName, "トランザクション開始エラー", exception.Message, this._logSubFolderName);
                flag = false;
                ProjectData.ClearProjectError();
            }
            return flag;
        }

        public void SVSubCloseConnection()
        {
            try
            {
                if (this._conectKind == EN_DbKind.SQLServer)
                {
                    if (this._ClsConectSql.ClsTrans != null)
                    {
                        this.SVFncFinishTransaction(false);
                    }
                    this._ClsConectSql.ClsOleCn.Close();
                    this._ClsConectSql.ClsOleCn.Dispose();
                    this._ClsConectSql = null;
                    this._conectKind = EN_DbKind.UnKnown;
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                this._ClsErrorObj = exception;
                modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.Err, this._className, this._procName, "データベース開放エラー", this._ClsErrorObj.Message, this._logSubFolderName);
                ProjectData.ClearProjectError();
            }
        }

        public string p_logClassName
        {
            set
            {
                if (value.Trim().Length > 0)
                {
                    this._className = value;
                }
                else
                {
                    this._className = "clsSV_DB";
                }
            }
        }

        public string p_logProcName
        {
            set
            {
                this._procName = value;
            }
        }

        public string p_logSubFolderName
        {
            get
            {
                return this._logSubFolderName;
            }
            set
            {
                this._logSubFolderName = value;
            }
        }

        public Exception p_errorObj =>
            this._ClsErrorObj;

        public bool p_primaryErr =>
            (((this._conectKind == EN_DbKind.SQLServer) && (this._ClsErrorObj != null)) && (this._ClsErrorObj.Message.IndexOf("PRIMARY KEY") >= 0));

        public enum EN_DbKind
        {
            UnKnown = -1,
            SQLServer = 3
        }

        private class SQL_S_CONNECTION
        {
            public SqlConnection ClsOleCn = new SqlConnection();
            public SqlTransaction ClsTrans = null;
            public SqlDataAdapter ClsAdapter = null;
        }
    }
}

