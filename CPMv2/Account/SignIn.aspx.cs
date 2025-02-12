using System;
using DevExpress.Web;
using CPMv2.Model;
using DevExpress.Web.Internal.Dialogs;

namespace CPMv2 {
    public partial class SignInModule : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
        }

        protected void SignInButton_Click(object sender, EventArgs e) {
            /* FormLayout.FindItemOrGroupByName("GeneralError").Visible = false;
             if(ASPxEdit.ValidateEditorsInContainer(this)) {
                 // DXCOMMENT: You Authentication logic
                 if(!AuthHelper.SignIn(UserNameTextBox.Text, PasswordButtonEdit.Text)) {
                     GeneralErrorDiv.InnerText = "Invalid login attempt.";
                     FormLayout.FindItemOrGroupByName("GeneralError").Visible = true;
                 }
                 else
                     Response.Redirect("~/");
             }*/

            if (!AuthHelper.SignIn(UserNameTextBox.Text, PasswordButtonEdit.Text))
            {
              //  GeneralErrorDiv.InnerText = "Invalid login attempt.";
              //  FormLayout.FindItemOrGroupByName("GeneralError").Visible = true;
            }
            else
                Response.Redirect("~/Index.aspx");
        }
    }
}