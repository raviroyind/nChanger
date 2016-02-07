using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using nChanger.Core;

namespace nChanger.WebUI
{
    public class CommonFunctions : Page
    {

        protected void BindBottomPaging(UserControl ucPaging, UserControl ucPaging1)
        {
            (ucPaging1.FindControl("txtPageNo") as TextBox).Text = (ucPaging.FindControl("txtPageNo") as TextBox).Text;
            (ucPaging1.FindControl("lblTotPages") as Label).Text = (ucPaging.FindControl("lblTotPages") as Label).Text;
            (ucPaging1.FindControl("lnkimgbtnFirst") as LinkButton).Enabled = (ucPaging.FindControl("lnkimgbtnFirst") as LinkButton).Enabled;
            (ucPaging1.FindControl("lnkimgbtnPrevious") as LinkButton).Enabled = (ucPaging.FindControl("lnkimgbtnPrevious") as LinkButton).Enabled;
            (ucPaging1.FindControl("lnkimgbtnNext") as LinkButton).Enabled = (ucPaging.FindControl("lnkimgbtnNext") as LinkButton).Enabled;
            (ucPaging1.FindControl("lnkimgbtnLast") as LinkButton).Enabled = (ucPaging.FindControl("lnkimgbtnLast") as LinkButton).Enabled;
        }

        protected void SetSorting(string sSortExp)
        {
            if (Convert.ToString(ViewState["sortColumn"]) == sSortExp)
            {
                if (ViewState["sortDirection"] != null)
                {
                    if ("ASC" == ViewState["sortDirection"].ToString())
                        ViewState["sortDirection"] = "DESC";
                    else
                        ViewState["sortDirection"] = "ASC";
                }
                else
                    ViewState["sortDirection"] = "ASC";
            }
            else
            {
                ViewState["sortColumn"] = sSortExp;
                ViewState["sortDirection"] = "ASC";
            }

        }

        /// <summary>
        /// Used to populate a list control with countries.
        /// </summary>
        /// <returns>Returns a list of Countries.</returns>
        public static List<string> GetCountriesList()
        {
            List<string> list = new List<string>();
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures & CultureTypes.SpecificCultures);
            foreach (CultureInfo info in cultures)
            {
                RegionInfo info2 = new RegionInfo(info.LCID);
                if (!list.Contains(info2.EnglishName))
                {
                    list.Add(info2.EnglishName);
                }
            }

            list.Sort();
            return list;
        }

        public static bool CheckIfUserExists(string email, string userid, out string message)
        {
            var exists = false;
            message = string.Empty;
            try
            {
                using (var dataContext = new nChangerDb())
                {
                    var emailUser = dataContext.Users.FirstOrDefault(u => u.Email.ToLower().Equals(email.ToLower()));
                    if (emailUser != null)
                    {
                        exists = true;
                        message = "Username \"" + userid + "\" already exists.";
                    }

                    var idUser = dataContext.Users.Find(userid);
                    if (idUser != null)
                    {
                        exists = true;
                        message = "Email \"" + email + "\" already exists.";
                    }
                }
            }
            catch (Exception exception)
            { }

            return exists;
        }

        public static bool CheckIfUserExists(string email)
        {
            var exists = false;

            try
            {
                using (var dataContext = new nChangerDb())
                {
                    var emailUser = dataContext.Users.FirstOrDefault(u => u.Email.ToLower().Equals(email.ToLower()));
                    if (emailUser != null)
                        exists = true;
                }
            }
            catch (Exception exception)
            { }

            return exists;
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);


            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public static string GetIpAddress()
        {

            System.Web.HttpContext context = System.Web.HttpContext.Current;
            var ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                var addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        
        public static bool SendMail(string toEmail, string subject, bool isBodyHtml, string body, bool enableSsl)
        {
            var bSuccess = true;
            var fromEmail = Convert.ToString(ConfigurationManager.AppSettings["SMPT_USER"]);
            var fromPassword = Convert.ToString(ConfigurationManager.AppSettings["SMPT_PASS"]);
            var msg = new MailMessage(fromEmail, toEmail, subject, body) { IsBodyHtml = true };
            try
            {
                var client = new SmtpClient(
                    Convert.ToString(ConfigurationManager.AppSettings["SMPT_SERVER"]),
                    Convert.ToInt32(ConfigurationManager.AppSettings["SMPT_PORT"]))
                {
                    Credentials = new NetworkCredential(fromEmail, fromPassword),
                    EnableSsl = true
                };

                client.Send(msg);
            }
            catch (Exception ex)
            {
                bSuccess = false;
            }
            return bSuccess;
        }
 
    }
}