using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;

using iTextSharp.text;

using PdfReport;
using PdfReport.Core;
using PdfReport.Templates;
using SUL.Bll;

namespace PdfReportTest
{
    public partial class ReportTest : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTest_OnClick(object sender, EventArgs e)
        {
        //    Company company = new Company().GetParentCompany();
        //    ReportHeader header = new ReportHeader()
        //        {
        //            CompanyName = company.CompanyName,
        //            Address = company.Address,
        //            ReportTitle = "Attendance Report",
        //            LogoPath = ""
        //        };
        //    List<DetailStructure> detailDataList = new List<DetailStructure>()
        //    {
        //        new DetailStructure
        //        {
        //            Slno = 1, FieldName = "AttendanceDate", DataType = "DateTime", Caption = "Attendance Date",
        //            FieldWidth = 100f, Align = Alignments.ALIGN_LEFT, Section = "Date"
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 2, FieldName = "EnglishName", DataType = "String", Caption = "Employee Name",
        //            FieldWidth = 110f, Align = Alignments.ALIGN_LEFT, Section = "Employee"
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 3, FieldName = "Code", DataType = "string", Caption = "Code",
        //            FieldWidth = 100f, Align = Alignments.ALIGN_LEFT, Section = "Employee"
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 4, FieldName = "Department", DataType = "string", Caption = "Department",
        //            FieldWidth = 100f, Align = Alignments.ALIGN_LEFT, Section = "Employee"
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 5, FieldName = "Designation", DataType = "string", Caption = "Designation",
        //            FieldWidth = 100f, Align = Alignments.ALIGN_LEFT, Section = "Employee"
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 6, FieldName = "EntryTime", DataType = "TimeSpan", Caption = "Entry Time",
        //            FieldWidth = 80f, Align = Alignments.ALIGN_LEFT, Section = "Attendance"
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 7, FieldName = "OutTime", DataType = "TimeSpan", Caption = "Out Time",
        //            FieldWidth = 80f, Align = Alignments.ALIGN_LEFT, Section = "Attendance"
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 8, FieldName = "OT", DataType = "TimeSpan", Caption = "OT",
        //            FieldWidth = 80f, Align = Alignments.ALIGN_LEFT, Section = "Attendance"
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 9, FieldName = "LateMinutes", DataType = "TimeSpan", Caption = "Late Minutes",
        //            FieldWidth = 80f, Align = Alignments.ALIGN_LEFT, Section = "Attendance"
        //        }
        //    };


        //    float[] widths = new float[] { 100f, 110f, 100f, 100f, 110f, 80f, 80f, 80f, 80f };

        //    DateTime startDate = DateTime.Parse("Jan 01, 2014");
        //    DateTime endDate = DateTime.Parse("Jan 01, 2016");
        //    DataTable dtAllAttendance = new DataTable();

        //    /dtAllAttendance = new Attendance().AllAttendance(startDate, endDate, 1);

        //    ReportDetail reportDetail = new ReportDetail(detailDataList, dtAllAttendance, 9, "English", widths,
        //        "Verdana", false);

        //    PdfDocument doc = new PdfDocument("Attendance", PageSizes.A4, "Landscape", "English")
        //    {
        //        Header = header,
        //        Details = reportDetail,
        //        ReportTemplate = PdfDocument.ReportType.Basic
        //    };
        //    doc.PageHeader = reportDetail.GetPageHeader(doc.ReportWidth);
        //    doc.ReportTemplate = PdfDocument.ReportType.Basic;
        //    MemoryStream pdfData = doc.WriteToStream();

        //    Session["StreamData"] = pdfData;
        //    Response.Write("<script type='text/javascript'> window.open('ReportViewer.aspx','_blank', 'height=' + screen.height + ',width=' + screen.width + ',resizable=yes,scrollbars=yes,toolbar=yes,menubar=yes,location=yes'); </script>");
        }

        protected void btnSinglePageReport_OnClick(object sender, EventArgs e)
        {
        //    Company company = new Company().GetParentCompany();

        //    List<Sections> sections = new List<Sections>()
        //    {
        //        new Sections("Date"),
        //        new Sections("Employee"),
        //        new Sections("Attendance")
        //    };

        //    ReportHeader header = new ReportHeader()
        //    {
        //        CompanyName = company.CompanyName,
        //        Address = company.Address,
        //        ReportTitle = "Attendance Report",
        //        LogoPath = ""
        //    };
        //    List<DetailStructure> detailDataList = new List<DetailStructure>()
        //    {
        //        new DetailStructure
        //        {
        //            Slno = 1, FieldName = "AttendanceDate", DataType = "DateTime", Caption = "Attendance Date",
        //            FieldWidth = 100f, Align = Alignments.ALIGN_LEFT, Section = "Date"
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 2, FieldName = "EnglishName", DataType = "String", Caption = "Employee Name",
        //            FieldWidth = 110f, Align = Alignments.ALIGN_LEFT, Section = "Employee"
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 3, FieldName = "Code", DataType = "string", Caption = "Code",
        //            FieldWidth = 100f, Align = Alignments.ALIGN_LEFT, Section = "Employee"
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 4, FieldName = "Department", DataType = "string", Caption = "Department",
        //            FieldWidth = 100f, Align = Alignments.ALIGN_LEFT, Section = "Employee"
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 5, FieldName = "Designation", DataType = "string", Caption = "Designation",
        //            FieldWidth = 100f, Align = Alignments.ALIGN_LEFT, Section = "Employee"
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 6, FieldName = "EntryTime", DataType = "TimeSpan", Caption = "Entry Time",
        //            FieldWidth = 80f, Align = Alignments.ALIGN_LEFT, Section = "Attendance"
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 7, FieldName = "OutTime", DataType = "TimeSpan", Caption = "Out Time",
        //            FieldWidth = 80f, Align = Alignments.ALIGN_LEFT, Section = "Attendance"
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 8, FieldName = "OT", DataType = "TimeSpan", Caption = "OT",
        //            FieldWidth = 80f, Align = Alignments.ALIGN_LEFT, Section = "Attendance"
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 9, FieldName = "LateMinutes", DataType = "TimeSpan", Caption = "Late Minutes",
        //            FieldWidth = 80f, Align = Alignments.ALIGN_LEFT, Section = "Attendance"
        //        }
        //    };


        //    float[] widths = new float[] { 100f, 150f, 100f, 150f };

        //    DateTime startDate = DateTime.Parse("Jan 01, 2014");
        //    DateTime endDate = DateTime.Parse("Jan 01, 2016");
        //    DataTable dtAllAttendance = new DataTable();

        //    dtAllAttendance = new Attendance().AllAttendance(startDate, endDate, 1);

        //    ReportDetail reportDetail = new ReportDetail(detailDataList, dtAllAttendance, 9, "English", widths,
        //        "Verdana", false);
        //    reportDetail.DetailSections = sections;

        //    PdfDocument doc = new PdfDocument("Attendance", PageSizes.A4, "Potrait", "English")
        //    {
        //        Header = header,
        //        Details = reportDetail,
        //        ReportTemplate = PdfDocument.ReportType.SinglePage,
        //        ShowPageHeader = false,
        //        PrintPageNo = false
        //    };

        //    MemoryStream pdfData = doc.WriteToStream();

        //    Session["StreamData"] = pdfData;
        //    Response.Write("<script type='text/javascript'> window.open('ReportViewer.aspx','_blank', 'height=' + screen.height + ',width=' + screen.width + ',resizable=yes,scrollbars=yes,toolbar=yes,menubar=yes,location=yes'); </script>");
        }

        protected void btnInvoice_OnClick(object sender, EventArgs e)
        {
        //    Company company = new Company().GetParentCompany();

        //    InvoiceHeader header = new InvoiceHeader()
        //    {
        //        InvoiceName = "Requision",
        //        InvoiceNo = "1",
        //        InvoiceDate = DateTime.Today,
        //        Sender = new Entity() { EntityCaption = "Company Information", Name = company.CompanyName, Address1 = company.Address, Address2 = "", Phone = company.Phone, Email = company.Email, Fax = "" },
        //        Receiver = new Entity() { EntityCaption = "Dealer Information", Name = company.CompanyName, Address1 = company.Address, Address2 = "", Phone = company.Phone, Email = company.Email, Fax = "" },
        //        Transporter = "Sundarban",
        //        TransporterDetails = "",
        //        LogoPath = ""
        //    };

        //    List<DetailStructure> detailDataList = new List<DetailStructure>()
        //    {
        //        new DetailStructure
        //        {
        //            Slno = 1, FieldName = "AttendanceDate", DataType = "DateTime", Caption = "Attendance Date",
        //            FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 2, FieldName = "EnglishName", DataType = "String", Caption = "Employee Name",
        //            FieldWidth = 150f, Align = Alignments.ALIGN_LEFT
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 3, FieldName = "Code", DataType = "string", Caption = "Code",
        //            FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 6, FieldName = "EntryTime", DataType = "TimeSpan", Caption = "Entry Time",
        //            FieldWidth = 80f, Align = Alignments.ALIGN_RIGHT
        //        },
        //        new DetailStructure
        //        {
        //            Slno = 7, FieldName = "OutTime", DataType = "TimeSpan", Caption = "Out Time",
        //            FieldWidth = 80f, Align = Alignments.ALIGN_RIGHT
        //        }
        //    };

        //    float[] widths = new float[] { 100f, 150f, 100f,80f, 80f };

        //    DateTime startDate = DateTime.Parse("Jul 15, 2014");
        //    DateTime endDate = DateTime.Parse("Jul 15, 2014");
        //    DataTable dtAllAttendance = new Attendance().AllAttendance(startDate, endDate, 1);

        //    ReportDetail reportDetail = new ReportDetail(detailDataList, dtAllAttendance, 9, "English", widths,
        //        "Verdana", false) {HeaderBackColor = BaseColor.ORANGE};

        //    InvoiceFooter footer = new InvoiceFooter();

        //    PdfDocument doc = new PdfDocument("Invoice")
        //    {
        //        ReportTemplate = PdfDocument.ReportType.SinglePage,
        //        ShowPageHeader = false,
        //        PrintPageNo = false
        //    };

        //    MemoryStream pdfData = doc.WriteInvoiceStream(header, footer, reportDetail);

        //    Session["StreamData"] = pdfData;
        //    Response.Write("<script type='text/javascript'> window.open('ReportViewer.aspx','_blank', 'height=' + screen.height + ',width=' + screen.width + ',resizable=yes,scrollbars=yes,toolbar=yes,menubar=yes,location=yes'); </script>");
        }
    }
}