using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;

namespace SUL.SCM
{

    public partial class HomePage : System.Web.UI.Page
    {
        private static UserRoleInfo _role;
        private static Users _user;
        private static Company _company;
        private static bool isNewEntry = true;

        private bool IsValidSession()
        {
            if (Session["user"] == null)
            {
                return false;
            }

            _user = (Users)Session["user"];
            _company = (Company)Session["company"];

            return _user.Id != 0;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsValidSession())
            {
                string str = Request.QueryString.ToString();
                if (str == string.Empty)
                    Response.Redirect("UserLogin.aspx?refPage=homepage.aspx");
                else
                    Response.Redirect("UserLogin.aspx?refPage=homepage.aspx?" + str);

            }
        }
    }
}