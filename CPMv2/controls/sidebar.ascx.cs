using CPMv2.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CPMv2.controls
{
    public partial class sidebar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["loggeIn"] == null)
                Response.Redirect("~/Account/SignIn.aspx");
            if ((int)HttpContext.Current.Session["loggeIn"] == 0)
            {
                Response.Redirect("~/Account/SignIn.aspx");
            }

          
            Object xx = HttpContext.Current.Session["userType"];
            var cx = JsonConvert.DeserializeObject<UserTypes>(xx.ToString());
            if (cx.name.Equals("agent"))
            {
                divProductsSettings.Visible = false;
                divProductSettings.Visible = false;
                divDealsApprovals.Visible= false;
                divTrainingSettings.Visible = false;
                divVerificationSettings.Visible= false;
                divDashBoard.Visible = false;
                divDealsPayments.Visible=true;
            }

            if (cx.name.Equals("admin"))
            {
                divProductsSettings.Visible = true;
                divProductSettings.Visible = true;
                divDealsApprovals.Visible = true;
                divTrainingSettings.Visible = true;
                divVerificationSettings.Visible = true;
                divDashBoard.Visible = true;
                divDealsPayments.Visible = false;
            }


        }
    }
}