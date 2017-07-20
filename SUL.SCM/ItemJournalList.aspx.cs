using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PdfReport;
using PdfReport.Core;
using PdfReport.Templates;
using SUL.Bll;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class ItemJournalList : System.Web.UI.Page
    {
        private  UserRoleInfo _role;
        private  Users _user;
        private  Company _company;
        private  DataTable dtItemJournalList;
        //public  int searchBtn;
        //private int _source;

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
        private bool IsValidPageForUser()
        {
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Item Journal") : new AppFunctionality().GetAppFunctionalityId("Item Journal", int.Parse(lblsource.Text));
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId, _user.CompanyId);

            if (!PermissionUser.IsView)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId, _user.CompanyId);
                bool permission = Permission.IsView;
                return permission;
            }
            else
                return true;
        }
        private bool IsValidInsertForUser()
        {
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Item Journal") : new AppFunctionality().GetAppFunctionalityId("Item Journal", int.Parse(lblsource.Text));
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId, _user.CompanyId);

            if (!PermissionUser.IsInsert)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId, _user.CompanyId);
                bool permission = Permission.IsInsert;
                return permission;
            }
            else
                return true;
        }
        private bool IsValidUpdateForUser()
        {
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Item Journal") : new AppFunctionality().GetAppFunctionalityId("Item Journal", int.Parse(lblsource.Text));
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId, _user.CompanyId);

            if (!PermissionUser.IsUpdate)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId, _user.CompanyId);
                bool permission = Permission.IsUpdate;
                return permission;
            }
            else
                return true;
        }
        private bool IsValidDeleteForUser()
        {
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Item Journal") : new AppFunctionality().GetAppFunctionalityId("Item Journal", int.Parse(lblsource.Text));
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId, _user.CompanyId);

            if (!PermissionUser.IsDelete)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId, _user.CompanyId);
                bool permission = Permission.IsDelete;
                return permission;
            }
            else
                return true;
        }
        private void LoadItemJournalList()
        {
            try
            {
                if (lblsearchBtn.Text == "1")
                {
                    RadGridItemJournal.DataSource = dtItemJournalList;
                    RadGridItemJournal.DataBind();
                }
                else
                {
                    dtItemJournalList = new ItemJournal().GetItemJournalFromViewList();

                    RadGridItemJournal.DataSource = dtItemJournalList;
                    RadGridItemJournal.DataBind();
                }
                Session["dtItemJournalList"] = dtItemJournalList;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load Item Journal grid" + ex);
            }

        }
        private void LoadColor()
        {
            try
            {
                List<ListTable> lstListTables = new ListTable().GetAllListTableByType("Color");
                lstListTables.Insert(0, new ListTable());

                rdropColor.DataTextField = "ListValue";
                rdropColor.DataValueField = "ListId";
                rdropColor.DataSource = lstListTables;
                rdropColor.DataBind();

                if (lstListTables.Count == 2)
                    rdropColor.SelectedIndex = 1;
                else
                    rdropColor.SelectedIndex = 0;

            }
            catch (Exception ex)
            {

                Alert.Show(ex.Message);
            }
        }

        private void LoadWareHouse()
        {
            try
            {
                List<WareHouse> lstWareHouses = new WareHouse().GetAllWareHouse(_company.Id);
                lstWareHouses.Insert(0, new WareHouse());

                RadComboBox1.DataTextField = "Name";
                RadComboBox1.DataValueField = "Id";
                RadComboBox1.DataSource = lstWareHouses;
                RadComboBox1.DataBind();

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load warehouse data." + ex);
            }
        }
        private void LoadProduct()
        {
            try
            {
                DataTable dtproduct = new Product().GetProductFromViewList();

                rdropProduct.DataTextField = "ProInfo";
                rdropProduct.DataValueField = "Id";
                rdropProduct.DataSource = dtproduct;
                rdropProduct.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblsource.Text = Request.QueryString["source"] != null ? Request.QueryString["source"] : "0";
            if (!IsValidSession())
            {
                string str = Request.QueryString.ToString();
                if (str == string.Empty)
                    Response.Redirect("UserLogin.aspx?refPage=HomePage.aspx");
                else
                    Response.Redirect("UserLogin.aspx?refPage=HomePage.aspx?" + str);

            }

            if (!IsValidPageForUser())
            {
                Alert.Show("Sorry, You Don't Have permission to access this page.");
                Response.Redirect("UserLogin.aspx?refPage=HomePage.aspx", false);
            }

            if (Session["dtItemJournalList"] != null)
                dtItemJournalList = (DataTable)Session["dtItemJournalList"];
            if (!IsPostBack)
            {
                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                if (!IsValidUpdateForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                lblsearchBtn.Text = "0";
                this.LoadItemJournalList();
                this.LoadProduct();
                this.LoadColor();
                this.LoadWareHouse();
                
            }
        }

        protected void RadGridItemJournal_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadItemJournalList();
        }

        protected void RadGridItemJournal_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadItemJournalList();
        }

        protected void RadGridItemJournal_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            //throw new NotImplementedException();
        }

        protected void btnPrint_OnClick(object sender, EventArgs e)
        {
            try
            {
                Company company = new Company().GetParentCompany();
                ReportHeader header = new ReportHeader()
                {
                    CompanyName = company.CompanyName,
                    Address = company.Address,
                    ReportTitle = "Item Journal List Report",
                    LogoPath = ""
                };
                List<DetailStructure> detailDataList = new List<DetailStructure>()
                {
                    new DetailStructure
                    {
                        Slno = 1, FieldName = "TransactionDate", DataType = "datetime", Caption = "Transaction Date",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 2, FieldName = "TransactionType", DataType = "string", Caption = "Transaction Type",
                        FieldWidth = 110f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 3, FieldName = "Name", DataType = "string", Caption = "WareHouse",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 4, FieldName = "WareHouseFrom", DataType = "string", Caption = "WareHouse From",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 5, FieldName = "ProductName", DataType = "string", Caption = "Product Name",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT 
                    },
                    new DetailStructure
                    {
                        Slno = 6, FieldName = "ListValue", DataType = "string", Caption = "Color",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 7, FieldName = "UnitCode", DataType = "string", Caption = "Unit Code",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                     new DetailStructure
                    {
                        Slno = 8, FieldName = "OpeningBalance", DataType = "string", Caption = "Opening Balance",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT, NeedTotal = true
                    },
                     new DetailStructure
                    {
                        Slno = 9, FieldName = "QuantityIn", DataType = "string", Caption = "Quantity In",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT, NeedTotal = true
                    },
                     new DetailStructure
                    {
                        Slno = 10, FieldName = "QuantityOut", DataType = "double", Caption = "Quantity Out",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT, NeedTotal = true
                    },
                     new DetailStructure
                    {
                        Slno = 11, FieldName = "ClosingBalance", DataType = "string", Caption = "Closing Balance",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT, NeedTotal = true
                    }

                   
                };


                float[] widths = new float[] { 100f, 110f, 100f, 100f, 100f, 100f, 110f, 100f, 100f, 100f,100f };

                //DateTime startDate = DateTime.Parse("Jan 01, 2014");
                //DateTime endDate = DateTime.Parse("Jan 01, 2016");

                //dtRequisitionMaster = new RequisitionMaster().GetRequisitionListFromViewList();

                ReportDetail reportDetail = new ReportDetail(detailDataList, dtItemJournalList, 9, "English", widths,
                    "Verdana", true);

                PdfDocument doc = new PdfDocument("Item Journal List", PageSizes.A4, "Land", "English")
                {
                    Header = header,
                    Details = reportDetail,
                    ReportTemplate = PdfDocument.ReportType.Basic
                };
                doc.PageHeader = reportDetail.GetPageHeader(doc.ReportWidth);
                doc.ReportTemplate = PdfDocument.ReportType.Basic;
                MemoryStream pdfData = doc.WriteToStream();

                Session["StreamData"] = pdfData;
                Response.Write("<script type='text/javascript'> window.open('ReportViewer.aspx','_blank', 'height=' + screen.height + ',width=' + screen.width + ',resizable=yes,scrollbars=yes,toolbar=yes,menubar=yes,location=yes'); </script>");
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }

        }

        
        protected void rdropProduct_OnDataBound(object sender, EventArgs e)
        {
            rdropProduct.Items.Insert(0, new RadComboBoxItem());
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            try
            {
                int warehouseId;
                int productId;
                int colorId;
                if (RadComboBox1.SelectedIndex <= 0 && rdropProduct.SelectedIndex <= 0 && rdropColor.SelectedIndex <= 0 &&
                    dtfromDate.SelectedDate == null)
                {
                    Alert.Show("Please Select one of the option.");
                    return;
                }
                else
                {
                    DateTime startDate = (DateTime)(dtfromDate.SelectedDate ?? DateTime.MinValue);
                    DateTime endDate = (DateTime)(dttoDate.SelectedDate ?? DateTime.MinValue);

                    warehouseId = RadComboBox1.SelectedIndex <= 0 ? 0 : int.Parse(RadComboBox1.SelectedValue);
                    productId = rdropProduct.SelectedIndex <= 0 ? 0 : int.Parse(rdropProduct.SelectedValue);
                    colorId = rdropColor.SelectedIndex <= 0 ? 0 : int.Parse(rdropColor.SelectedValue);

                    dtItemJournalList = new ItemJournal().SearchItemJournal(warehouseId, productId, colorId, startDate,
                        endDate);

                    RadGridItemJournal.DataSource = dtItemJournalList;
                    RadGridItemJournal.DataBind();
                    lblsearchBtn.Text = "1";
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            DataTable dt = dtItemJournalList.Copy();

            ExportToExcel(dt);
        }

        public void ExportToExcel(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {

                    string attachment = "attachment; filename=ItemJournalReport.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/ms-excel";

                    string tab = "";

                    Response.ContentEncoding = System.Text.Encoding.Unicode;
                    Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());


                    StringWriter sw = new StringWriter();
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    DateTime dttime = DateTime.Now;

                    #region ReportHeader
                    Response.Write("Date");
                    tab = "\t";
                    Response.Write(tab + dttime.ToString("MMM dd,yyyy"));
                    Response.Write("\n");
                    #endregion

                    Response.Write("\n");
                    Response.Write("Item Journal List");
                    Response.Write("\n");
                    //foreach (DataColumn dc in dt.Columns)
                    //{
                    Response.Write(sw.ToString());
                    Response.Write("Transaction Date");
                    tab = "\t";
                    Response.Write(tab + "Transaction Type");
                    tab = "\t";
                    Response.Write(tab + "WareHouse");
                    tab = "\t";
                    Response.Write(tab + "WareHouse From");
                    tab = "\t";
                    Response.Write(tab + "Product");
                    tab = "\t";
                    Response.Write(tab + "Color");
                    tab = "\t";
                    Response.Write(tab + "Unit");
                    tab = "\t";
                    Response.Write(tab + "Opening Balance");
                    tab = "\t";
                    Response.Write(tab + "Quantity In");
                    tab = "\t";
                    Response.Write(tab + "Quantity Out");
                    tab = "\t";
                    Response.Write(tab + "Closing Balance");
                    tab = "\t";

                    Response.Write("\n");

                    foreach (DataRow dr in dt.Rows)
                    {
                        tab = "";
                        DateTime date = DateTime.Parse(dr["TransactionDate"].ToString());
                        Response.Write(tab + date.ToString("MMM dd,yyyy"));
                        tab = "\t";
                        Response.Write(tab + dr["TransactionType"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["Name"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["WareHouseFrom"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["ProductName"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["ListValue"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["UnitCode"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["OpeningBalance"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["QuantityIn"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["QuantityOut"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["ClosingBalance"].ToString());
                        tab = "\t";
                        Response.Write("\n");
                    }
                    Response.End();
                    Response.Flush();
                    sw.Close();
                    sw.Dispose();
                }


            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }
    }
}