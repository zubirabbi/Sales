using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;

namespace SUL.SCM
{
    public partial class Main : System.Web.UI.MasterPage
    {
        private Users _user;
        private string _menu;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsValidSession()
        {
            if (Session["user"] == null)
            {
                return false;
            }

            _user = (Users)Session["user"];

            _menu = Session["Menu"].ToString();

            return _user.Id != 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsValidSession()) return;

            if (!IsPostBack)
            {
                Literal ltrl = (Literal)FindControl("ltrlMenu");
                ltrl.Text = _menu;
            }
        }

    }
}