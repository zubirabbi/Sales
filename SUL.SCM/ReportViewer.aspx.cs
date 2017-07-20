using System;
using System.IO;
using System.Web;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfReport;

using PdfDocument = PdfReport.PdfDocument;

namespace PdfReportTest
{
    public partial class ReportViewer : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["StreamData"] == null) return;

                    string reportName = (Session["ReportName"] == null) ? "Report" : Session["ReportName"].ToString();

                    MemoryStream pdfData = (MemoryStream)Session["StreamData"];

                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.Charset = string.Empty;
                    Response.Cache.SetCacheability(HttpCacheability.Public);
                    Response.AddHeader("Content-Disposition",
                        "inline; filename=" + reportName.Replace(" ", "").Replace(":", "-") + ".pdf");
                    Response.OutputStream.Write(pdfData.GetBuffer(), 0, pdfData.GetBuffer().Length);
                    Response.OutputStream.Flush();
                    Response.OutputStream.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void Method1()
        {
            if (Session["PdfReport"] == null) return;

            PdfDocument pdfReport = (PdfDocument)Session["PdfReport"];

            Rectangle pageSize = (pdfReport.PageOrientation == "potrait") ? PageSize.A4 : PageSize.A4.Rotate();

            Document pdfDoc = new Document(pageSize, 10, 10, 45, 45);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc,
               HttpContext.Current.Response.OutputStream);
            writer.ViewerPreferences = PdfWriter.PageModeUseOutlines;
            // Our custom Header and Footer is done using Event Handler
            HeaderFooter PageEventHandler = new HeaderFooter();
            writer.PageEvent = PageEventHandler;
            // Define the page header

            pdfDoc.Open();
            PageEventHandler.PageHeader = pdfReport.PageHeader;

            pdfDoc = pdfReport.WriteToDoc(pdfDoc, PageEventHandler);

            pdfDoc.Close();
            Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment; filename= SampleExport.pdf");
            Response.AddHeader("content-disposition", "inline;filename=ID.pdf");
            HttpContext.Current.Response.Write(pdfDoc);
            Page.ClientScript.RegisterStartupScript(typeof(Page), "Close",
                "<script type=\"text/javascript\">closeWindow()</script>");

            Response.Flush();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}


