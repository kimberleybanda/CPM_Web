using System;
using System.Collections.Generic;
using System.Web;
using CPMv2.DealsCode;
using CPMv2.Model;
using DevExpress.Web;
using Newtonsoft.Json;

namespace CPMv2 {
    public partial class Index : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["loggeIn"] == null)
                Response.Redirect("~/Account/SignIn.aspx");
            if ((int)HttpContext.Current.Session["loggeIn"] == 0)
            {
                Response.Redirect("~/Account/SignIn.aspx");
            }

            if (IsPostBack)
            {
                if (HttpContext.Current.Session["loggerId"] != null)
                {
                    Object xx = HttpContext.Current.Session["userType"];
                    var cx = JsonConvert.DeserializeObject<UserTypes>(xx.ToString());
                    if (cx.name.Equals("admin"))
                    {
                        lblClosedDeals.Text = DealsContextProvider.GetAdminApprovedDeals().ToString();

                        lblOpenDeals.Text = DealsContextProvider.GetAdminNonApprovedDeals().ToString();
                    }
                    else
                    {
                        lblClosedDeals.Text = DealsContextProvider.GetApprovedDeals((int)HttpContext.Current.Session["loggerId"]).ToString();

                        lblOpenDeals.Text = DealsContextProvider.GetNonApprovedDeals((int)HttpContext.Current.Session["loggerId"]).ToString();
                    }


                }

            }
            else
            {
                if (HttpContext.Current.Session["loggerId"] != null)
                {
                    Object xx = HttpContext.Current.Session["userType"];
                    var cx = JsonConvert.DeserializeObject<UserTypes>(xx.ToString());
                    if (cx.name.Equals("admin"))
                    {
                        lblClosedDeals.Text = DealsContextProvider.GetAdminApprovedDeals().ToString();

                        lblOpenDeals.Text = DealsContextProvider.GetAdminNonApprovedDeals().ToString();
                    }
                    else
                    {
                        lblClosedDeals.Text = DealsContextProvider.GetApprovedDeals((int)HttpContext.Current.Session["loggerId"]).ToString();

                        lblOpenDeals.Text = DealsContextProvider.GetNonApprovedDeals((int)HttpContext.Current.Session["loggerId"]).ToString();
                    }


                }

            }
        }

        protected void GridView_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e) {
            e.NewValues["Kind"] = 1;
            e.NewValues["Priority"] = 2;
            e.NewValues["Status"] = 1;
            e.NewValues["IsDraft"] = true;
            e.NewValues["IsArchived"] = false;
            
        }

        protected void GridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
           /* if(e.Parameters == "delete") {
                List<long> selectedIds = GridView.GetSelectedFieldValues("Id").ConvertAll(id => (long)id);
                DataProvider.DeleteIssues(selectedIds);
                GridView.DataBind();
            }*/
        }
    }
}