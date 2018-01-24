using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FSiMed_x2._1.Models;
using MySql.Data.MySqlClient;
using System.Text;

namespace FSiMed_x2._1.DataAccessLayer
{
    public class CommonBillingDAL
    {
        public bool CaptureDataCommonBillingInfo(CommonBillingInfo cbi, connection c)
        {

            try
            {

                string vSql = " INSERT INTO COMMON_BILLING_info (PATIENT_ID, VISIT_NO, TOTAL_AMT, DISCOUNT, NET_AMOUNT, GUID) " +
                              " VALUES (" + cbi.PatientId + "," + cbi.VisitNo + "," + cbi.TotalAmt + "," + cbi.Discount + "," + cbi.NetAmount + ",'" + cbi.Guid + "')";
                c.cmd.CommandText = vSql;
                c.cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CaptureDataCommonBillingDetails(CommonBillingInfo cbi, connection c)
        {

            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO COMMON_BILLING_DETAILS (ID, PATIENT_ID, VISIT_NO, TYPE, NAME, DAYS, AMOUNT, GUID) VALUES ");

                foreach (var v in cbi.Details)
                {
                    sb.Append("(" + cbi.Id + "," + cbi.PatientId + "," + cbi.VisitNo + ",'" + v.TypeName + "','" + v.TestName + "'," + v.Days + "," + v.TotalAmt + ",'" + cbi.Guid + "'),");
                }

                string vSql = sb.ToString().TrimEnd(',') + ";";

                c.cmd.CommandText = vSql;
                c.cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
