using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class BranchInfo : System.Web.UI.Page
    {
        private static int companyId = 1;
        private static bool _isNewEntry;
        private void LoadIncharge()
        {
            try
            {
                List<EmployeeInformation> lstempInfo = new EmployeeInformation().GetAllEmployeeInformation(companyId);

                lstempInfo.Insert(0, new EmployeeInformation());

                rdropInCharge.DataTextField = "FullName";
                rdropInCharge.DataValueField = "Id";
                rdropInCharge.DataSource = lstempInfo;
                rdropInCharge.DataBind();

                if (lstempInfo.Count == 2)
                    rdropInCharge.SelectedIndex = 1;
                else
                    rdropInCharge.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something Is Going Wrong to Load Incharge " + ex);
            }
        }

        private void LoadBranchGrid()
        {
            try
            {
                DataTable dtBranchName = new Branch().GetAllBranchbyCompanyId(companyId);

                if (dtBranchName.Rows.Count == 0)
                    return;
                else
                {
                    RadGridComBranch.DataSource = dtBranchName;
                    RadGridComBranch.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load BranchGrid");
            }


        }

        private void LoadClearData()
        {
            rtxtBranchCode.Text = "";
            rtxtBranchName.Text = "";
            rtxtLocation.Text = "";
            chkIsActive.Checked = false;
            rdropInCharge.SelectedIndex = 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                LoadIncharge();
                LoadBranchGrid();
                _isNewEntry = true;
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            #region validation
            if (rtxtBranchCode.Text == string.Empty)
            {
                Alert.Show("Please Input BranchCode.");
                rtxtBranchCode.Focus();
                return;
            }
            if (rtxtBranchName.Text == string.Empty)
            {
                Alert.Show("Please Input BranchName.");
                rtxtBranchName.Focus();
                return;
            }
            #endregion

            int success;
            try
            {
                Branch objBranch = new Branch();
                objBranch.BranchCode = rtxtBranchCode.Text;
                objBranch.BranchName = rtxtBranchName.Text;
                objBranch.Location = rtxtLocation.Text == string.Empty ? "" : rtxtLocation.Text;
                objBranch.InchargeId = rdropInCharge.SelectedIndex <= 0 ? 0 : int.Parse(rdropInCharge.SelectedValue);
                objBranch.IsActive = chkIsActive.Checked;
                objBranch.CompanyId = companyId;

                if (_isNewEntry == true)
                {
                    success = objBranch.InsertBranch();
                }
                else
                {
                    objBranch.Id = int.Parse(lblId.Text);
                    success = objBranch.UpdateBranch();
                }

                if (success == 0)
                {
                    Alert.Show("Branch Data was not save succesfully!!!");
                    return;
                }
                else
                {
                    this.LoadBranchGrid();
                    this.LoadClearData();
                    _isNewEntry = false;
                }

            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to save data ." + ex);
            }



        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.LoadClearData();
        }
        protected void RadGridComBranch_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            Branch objBranch=new Branch();
            if (e.CommandName == "btnDelete")
            {
                GridDataItem item = (GridDataItem) e.Item;

                int id = int.Parse(item["colId"].Text);
                objBranch.DeleteBranchById(id);
            }
            if (e.CommandName == "btnSelect")
            {
                GridDataItem item = (GridDataItem)e.Item;

                lblId.Text = item["colId"].Text;
                rtxtBranchCode.Text = item["colBranchCode"].Text;
                rtxtBranchName.Text = item["colBranchName"].Text;
                rtxtLocation.Text = item["colLocation"].Text;
                rdropInCharge.SelectedValue = item["colIncharge"].Text;
                chkIsActive.Checked = bool.Parse(item["colIsActive"].Text);

                _isNewEntry = false;
            }
        }
        protected void RadGridComBranch_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadBranchGrid();
        }

        protected void RadGridComBranch_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadBranchGrid();
        }

    }
}