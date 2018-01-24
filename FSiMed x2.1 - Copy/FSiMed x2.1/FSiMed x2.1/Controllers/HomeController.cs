using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FSiMed_x2._1.Models;
using System.Web.Routing;
using FSiMed_x2._1.App_Start;
using CommonClasses;
using log4net;
using QueryStringEncryption;
using MySql.Data.MySqlClient;
using System.Data.Entity;
using System.Data;
using System.IO;
using System.Text;
using FSiMed_x2._1.BusinessLayer;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace FSiMed_x2._1.Controllers
{

    public class demo1
    {
        public string name { get; set; }

        public string amount { get; set; }
    }
    public class HomeController : Controller
    {
        ILog logger = LogManager.GetLogger(typeof(HomeController));

        public ActionResult Index(string vMsg, string userId)
        {
            //System.Web.HttpContext.Current.Application["Value"] = "1234";
            //string v = " SELECT USER_ID, USER_NAME, CURRENT_PASSWORD FROM USER_MASTER ";
            //connection c = new connection();
            //DataTable dt = new DataTable();
            //c.cmd.CommandText = v;
            //c.da.Fill(dt);

            //dt.TableName = "UserList";
            //CreateJSONFile.WriteJason(dt, Server.MapPath(@"~\JsonFiles\users.json"));

            Session["User_Id"] = null;
            Session["User_Name"] = null;
            ViewBag.Res = vMsg.Decrypt();
            ViewBag.UserID = userId.Decrypt();
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult Login(user_master userMaster)
        {
            try
            {
                fsimed_configdbEntities dbModel = new fsimed_configdbEntities();

                var vRes = dbModel.user_master.Where(x => x.USER_ID == userMaster.USER_ID).Count() + 1;

                string vPassword = vRes == 2 ? userMaster.CURRENT_PASSWORD.en_r(userMaster.USER_ID) : "1";

                vRes = vRes == 2 ? dbModel.user_master.Where(x => x.CURRENT_PASSWORD == vPassword)
                                                      .Where(y => y.USER_ID == userMaster.USER_ID).Count() + 2 : vRes;

                switch (vRes)
                {
                    case 2:
                        return JavaScript("alert('Enter the Correct Password123'); document.getElementById('txtPassword').focus(); EnableButton();");

                    case 1:
                        return JavaScript("alert('Enter the Current User Id'); document.getElementById('txtUserID').focus(); EnableButton();");

                }

                Session["User_Id"] = userMaster.USER_ID;
                Session["User_Name"] = dbModel.user_master.Where(x => x.USER_ID == userMaster.USER_ID).Select(x => x.USER_NAME).SingleOrDefault();

                return RedirectToAction("Login", "Home", new { vMsg = "S".Encrypt(), userId = userMaster.USER_ID.ToString().Encrypt() });
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }

        }

        //[NoDirectAccess]
        public ActionResult Login(string vMsg, string userId)
        {
            Session["User_Id"] = "10001";
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        // public JsonResult Create(string [] vQueryString)//string vQueryString)
        //public JsonResult Create(List<vQueryString> model)//
        public JsonResult Create(CommonBillingInfo model)
        {

            CommonBillingBAL CB = new CommonBillingBAL();

            if (CB.CommonBillingData(model))
            {
                return Json("Record Saved successfully");
            }
            else
            {
                return Json("Some went Wrong");
            }

        }

        [HttpPost]
        [OutputCache(Duration = 20, VaryByParam = "none")]
        public JsonResult getData(string vQueryString)
        {
            var vamt = "0";

            //using (StreamReader file = System.IO.File.OpenText(Server.MapPath(@"~\JsonFiles\Xray.json")))
            //{
            //    using (JsonTextReader reader = new JsonTextReader(file))
            //    {
            //        JArray j2 = (JArray)JToken.ReadFrom((reader));
            //        int cnt = j2.Count;

            //        List<demo1> de = j2.Select(x => new demo1 { name = (string)x["name"], amount = (string)x["amount"] }).ToList();
            //        vamt = de.Where(x => x.name == Name).Select(x => x.amount).First();


            //    }
            //}

            //var json = System.IO.File.ReadAllText(Server.MapPath(@"~\JsonFiles\Xray.json"));

            //var newCompanyMember = "{ \"name\": \"amrednra\", \"amount\": \"800\"}";

            //var jsonObj = JObject.Parse(json);

            //var experienceArrary = jsonObj.GetValue("experiences") as JArray;

            //var newCompany = JObject.Parse(newCompanyMember);

            //experienceArrary.Add(newCompany);

            connection c = new connection();

            c.cmd.CommandText = "select count(1) from product_master ";// where name ='" + vQueryString + "'";
            c.con_open();
            string vAmt = c.cmd.ExecuteScalar().ToString();
            c.con_close();

            return Json(vamt, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Alert()
        {
            return PartialView("alert");
        }

        [HttpGet]
        public ActionResult Test()
        {
            return View();
        }
        [HttpPost, ActionName("CreateTest")]
        public ActionResult Create(NewModel model)
        {
            if (ModelState.IsValid)
            {
                JavaScript("alert incorre");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }


}
