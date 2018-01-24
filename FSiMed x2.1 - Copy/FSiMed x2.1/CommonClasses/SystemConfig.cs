using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using log4net;
using System.Security.Cryptography;
using System.IO;

namespace CommonClasses
{
    public static class SystemConfig
    {
        public static string vSql = string.Empty;
        static connection c = new connection();
        private static byte[] key = { };
        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

        public static string en_r(this string pPassword, int pUserId)
        {
            string vStr = string.Empty;
            try
            {
                vSql = " SELECT EN_R('" + pPassword + "', USER_ID) FROM USER_MASTER WHERE USER_ID = " + pUserId;
                c.cmd.CommandText = vSql;
                c.con_open();
                vStr = c.cmd.ExecuteScalar().ToString();
                c.con_close();

                return vStr;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, vSql);
                return null;
            }
        }

        public static void LogErrorDB(string vId, string vMsg, string vQuery)
        {
            try
            {
                vSql = " INSERT INTO ERROR_LOG(ID, ERR_MSG, QUERY, STATUS, DATE_CREATE) VALUES ('Default','" + vMsg + "','" + vQuery + "','A',Now() ) ";
                c.cmd.CommandText = vSql;
                Logger.Info(vSql);
                c.con_open();
                c.cmd.ExecuteNonQuery();
                c.con_close();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

    }
}
