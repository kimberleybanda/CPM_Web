using System;
using System.Collections.Generic;
using DevExpress.Web;
using CPMv2.Model;
using DevExpress.Web.Internal.Dialogs;

namespace CPMv2 {
    public partial class Register2 : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e)
        {
            int x = 9;
            List<UserTypes> userTypes = AuthHelper.getUserTypes();
            drpUserType.DataSource = userTypes;
            drpUserType.DataTextField = "name";
            drpUserType.DataValueField = "id";
            drpUserType.DataBind();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
        }

        protected void drpUserType_OnChanged(object sender, EventArgs e)
        {

        }

        protected void SignInButton_Click(object sender, EventArgs e) {
          
            Login2 login = new Login2();
            login.phone = UserNameTextBox.Text;
            login.password = PasswordButtonEdit.Text;
            login.userType = new UserType2();
            login.userType.id = Convert.ToInt32(drpUserType.SelectedValue);
            login.userType.name = drpUserType.Text;

            bool rootLogin = AuthHelper.register(login);
            if (rootLogin)
            {
                Response.Redirect("~/Account/SignIn.aspx");
            }
            else
            {
               // ASPxPopupControl1.ShowOnPageLoad = true;
            }

        }
    }
}