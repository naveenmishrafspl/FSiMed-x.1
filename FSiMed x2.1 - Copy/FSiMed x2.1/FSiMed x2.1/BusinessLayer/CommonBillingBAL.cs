using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using FSiMed_x2._1.Models;
using FSiMed_x2._1.DataAccessLayer;

namespace FSiMed_x2._1.BusinessLayer
{
    public class CommonBillingBAL
    {
        public bool CommonBillingData(CommonBillingInfo cbi)
        {
            connection c = new connection();
            c.con_open();
            MySqlTransaction trans = c.con.BeginTransaction();
            c.cmd.Transaction = trans;

            try
            {
                CommonBillingDAL cb = new CommonBillingDAL();

                cbi.Guid = Guid.NewGuid().ToString();

                bool isTrue = false;

                if (cb.CaptureDataCommonBillingInfo(cbi, c))
                {
                    string vSql = " SELECT ID FROM COMMON_BILLING_INFO WHERE guid = '" + cbi.Guid + "'";
                    c.cmd.CommandText = vSql;
                    cbi.Id = Convert.ToInt32(c.cmd.ExecuteScalar().ToString());

                    isTrue = cb.CaptureDataCommonBillingDetails(cbi, c);
                    
                }

                if (isTrue)
                {
                    trans.Commit();
                    return true;
                }
                else
                {
                    trans.Rollback();
                    return false;
                }
            
            }
            catch(Exception ex )
            {
                trans.Rollback();
                return false;
            }

          
        }
            
    }
}