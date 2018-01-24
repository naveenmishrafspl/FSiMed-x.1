
using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

public class connection 
{
    //public MySqlConnection con = new MySqlConnection(ConfigurationManager.AppSettings["conn"]);

    public static string pEncStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    public static string pkey = "FSPLHIS";
    public MySqlCommand cmd = new MySqlCommand();
    public MySqlCommand cmd1 = new MySqlCommand();
    public MySqlDataAdapter da = new MySqlDataAdapter();
    public MySqlDataAdapter daproc = new MySqlDataAdapter();
    public DataSet ds = new DataSet();
    public DataRow dr;
    public MySqlDataReader mdr;
    public MySqlCommandBuilder cd = new MySqlCommandBuilder();
    public MySqlCommand cmdProc = new MySqlCommand();
    public MySqlCommand cmdProcRights = new MySqlCommand();
    public MySqlConnection con = new MySqlConnection(ConStringDecrypt(pEncStr, pkey));
    //public MySqlConnection con = new MySqlConnection("database=fsimedpro_v1_0_city_gkp;host=localhost;uid=root;pwd=Admin123;persist security info=true;");

    //Method for Decrypting Connection String
    protected static string ConStringDecrypt(string pEncStr, string pkey)
    {
        //Filling key upto 200 character
        pkey = pkey.PadRight(200, ' ');
    
        //Declaring unique key and its value for replacement
        char uniqKey;
        int uniqKeyValue;
       
        //Declaring for comparing with key per character 
        string oldKey;

        //Declaring string for decrypted string
        string originalConString = null;

        //Declaring and Initializing index
        int i = 0;

        try
        {
            //Initializing loop upto the length of key
            while (i < pkey.Length)
            {
                //Declaring variable for inserting ascii value of string and key per character
                int conStrCharValue = (int)pEncStr[i * 2];
                int keyStrCharValue = (int)pEncStr[i * 2 + 1];
                int oldKeyCharValue;
                if (conStrCharValue >= 65)
                {
                    conStrCharValue = conStrCharValue + 10 - 65;
                }
                else
                {
                    conStrCharValue = conStrCharValue - 48;
                }
                if (keyStrCharValue >= 65)
                {
                    keyStrCharValue = keyStrCharValue + 10 - 65;
                }
                else
                {
                    keyStrCharValue = keyStrCharValue - 48;
                }

                //Replacing space with null in key
                oldKey = pkey[i].ToString();
                oldKey = oldKey.Replace(" ", "");

                if (oldKey != "")
                {
                    oldKeyCharValue = (int)pkey[i];
                }
                else
                {
                    oldKeyCharValue = 0;
                }
                int totVal = conStrCharValue * 16 + keyStrCharValue;
                originalConString = (originalConString + (char)(totVal - oldKeyCharValue));

                i = i + 1;
            }

        }
        catch (Exception ex)
        {
            string strMsg = ex.Message;
        }

        //Defining unique key
        uniqKeyValue = 65504;
        //uniqKeyValue = 65555;
        uniqKey = (char)uniqKeyValue;

        //Replacing unique key with space and trim it
        originalConString = originalConString.Replace(uniqKey, ' ');
        originalConString = originalConString.Trim();

        //Returning decrypted connection string
        return originalConString;
    }

    //Creating connection
    public connection()
	{
		cmd.Connection = con;
        cmdProc.Connection = con;
        cmdProcRights.Connection = con;
        cmd1.Connection = con;
        da.SelectCommand = cmd;
        daproc.SelectCommand = cmd;
    }

    //Connection open
    public void con_open()
    {
        if (con.State != ConnectionState.Open)
        {
            con.Open();
        }   
    }

    //Connection close
    public void con_close()
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }   

}