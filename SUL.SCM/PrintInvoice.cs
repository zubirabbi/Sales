using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using iTextSharp.text;
using PdfReport;
using PdfReport.Core;
using PdfReport.Templates;
using SUL.Bll;

namespace SUL.SCM
{
    public class PrintInvoice
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requisitionId"></param>
        /// <param name="logoPath"></param>
        /// <returns></returns>
        public static MemoryStream Print(int requisitionId, string logoPath)
        {
            try
            {
                Company company = new Company().GetParentCompany();

                string logoName = Path.GetFileName(logoPath);

                RequisitionMaster master = new RequisitionMaster().GetRequisitionMasterById(requisitionId);
                if (master == null)
                {
                    throw new Exception("Invalid requisition. Please check your data.");
                }
                

                DealerInformation dealer = new DealerInformation().GetDealerInformationById(master.DealerId);

                InvoiceMaster Imaster=new InvoiceMaster().GetInvoiceMasterByRequisitionId(requisitionId);
                if (Imaster.Id == 0)
                {
                    throw new Exception("No Invoice Found.");
                }
                //CourierInformation courier = (master.Courier == string.Empty)
                //    ? null
                //    : new CourierInformation().GetCourierInformationById(int.Parse(master.Courier));
                string transporter = "";
                if (master.Courier != null)
                {
                    transporter = master.Courier;
                }
                
                var Ref = string.Empty;
                if (master.CampaignId != null && master.CampaignId != 0)
                {
                    CampaignMaster objcaCampaignMaster = new CampaignMaster().GetCampaignMasterById(master.CampaignId);
                    Ref = objcaCampaignMaster.CampaignCode;
                }

                InvoiceHeader header = new InvoiceHeader()
                {
                    InvoiceName = "Invoice",
                    InvoiceCaption = "Invoice No",
                    InvoiceNo = Imaster.InvoiceNo,
                    RefCaption = "Requisition No",
                    Referrence= master.RequisitionCode,
                    DateCaption = "Invoice Date",
                    InvoiceDate = Imaster.InvoiceDate,
                    Sender = new Entity() { EntityCaption = "Company Information", Name = company.CompanyName, Address1 = company.Address, Address2 = company.Address2, Phone = "Phone: "+company.Phone, Email = "Email :"+company.Email, Fax = "Fax: +8802-8811273" },
                    Receiver = new Entity() { EntityCaption = "Dealer Information", Name = dealer.DealerName, Address1 = dealer.Address, Address2 = "", Phone = dealer.Phone, Email = dealer.Email, Fax = "" },

                    Transporter = transporter,
                    TransporterDetails = "",

                    LogoPath = logoPath,
                    LogoName = logoName,
                    LogoHeight = 55,
                    LogoWidth = 215,
                    ReferenceNo = Ref,
                    
                };

                //set fonts for header
                header.DefaultFont = new ReportFont("verdana.ttf", 10, Fonts.NORMAL, BaseColor.BLACK, "English").GetFont();
                header.HeaderFont = new ReportFont("verdana.ttf", 10, Fonts.NORMAL, BaseColor.BLACK, "English").GetFont();
                header.CaptionFont = new ReportFont("verdana.ttf", 10, Fonts.NORMAL, BaseColor.BLACK, "English").GetFont();
                header.CaptionFontBold = new ReportFont("verdana.ttf", 10, Fonts.BOLD, BaseColor.BLACK, "English").GetFont();
                header.CompanyFont = new ReportFont("verdana.ttf", 10, Fonts.BOLD, BaseColor.BLACK, "English").GetFont();

                List<DetailStructure> detailDataList = new List<DetailStructure>()
                {
                    new DetailStructure
                    {
                        Slno = 1, FieldName = "CategoryCode", DataType = "string", Caption = "Product",
                        FieldWidth = 120f, Align = Alignments.ALIGN_LEFT, CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 2, FieldName = "ProductName", DataType = "String", Caption = "Model No",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT, CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 3, FieldName = "ColorName", DataType = "string", Caption = "Color",
                        FieldWidth = 40f, Align = Alignments.ALIGN_LEFT, CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 4, FieldName = "Quantity", DataType = "integer", Caption = "Quantity",
                        FieldWidth = 40f, Align = Alignments.ALIGN_RIGHT, CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderColor = BaseColor.BLACK,NeedTotal = true
                    },
                    new DetailStructure
                    {
                        Slno = 5, FieldName = "Price", DataType = "decimal", Caption = "Price",
                        FieldWidth = 50f, Align = Alignments.ALIGN_RIGHT, CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderColor = BaseColor.BLACK
                    },
                     new DetailStructure
                    {
                        Slno = 5, FieldName = "Total2", DataType = "decimal", Caption = "Total Price",
                        FieldWidth = 50f, Align = Alignments.ALIGN_RIGHT, CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderColor = BaseColor.BLACK,NeedTotal = true
                    },
                    new DetailStructure
                    {
                        Slno = 6, FieldName = "Discount", DataType = "decimal", Caption = "Line Discount",
                        FieldWidth = 40f, Align = Alignments.ALIGN_RIGHT, CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderColor = BaseColor.BLACK,NeedTotal = true
                    },
                    new DetailStructure
                    {
                        Slno = 7, FieldName = "ItemTotal2", DataType = "decimal", Caption = "Line Total",
                        FieldWidth = 100f, Align = Alignments.ALIGN_RIGHT, CellBorder = Rectangle.BOTTOM_BORDER, BorderColor = BaseColor.BLACK,NeedTotal = true
                    }
                };

                List<DetailStructure> transactions = new List<DetailStructure>()
                {
                    new DetailStructure
                    {
                        Slno = 1, FieldName = "TransactionDate", DataType = "DateTime", Caption = "Date",
                        FieldWidth = 40f, Align = Alignments.ALIGN_LEFT, CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 2, FieldName = "TransactionType", DataType = "String", Caption = "Type",
                        FieldWidth = 50f, Align = Alignments.ALIGN_LEFT, CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 3, FieldName = "SourceNo", DataType = "string", Caption = "Source No",
                        FieldWidth = 40f, Align = Alignments.ALIGN_LEFT, CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 4, FieldName = "OpeningBalance", DataType = "TimeSpan", Caption = "Opening Balance",
                        FieldWidth = 40f, Align = Alignments.ALIGN_RIGHT, CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 5, FieldName = "Debit", DataType = "TimeSpan", Caption = "Debit",
                        FieldWidth = 40f, Align = Alignments.ALIGN_RIGHT, CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 6, FieldName = "Cradit", DataType = "TimeSpan", Caption = "Credit",
                        FieldWidth = 40f, Align = Alignments.ALIGN_RIGHT,CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 7, FieldName = "ClosingBalance", DataType = "TimeSpan", Caption = "Closing Balance",
                        FieldWidth = 40f, Align = Alignments.ALIGN_RIGHT, CellBorder = Rectangle.BOTTOM_BORDER,
                        BorderColor = BaseColor.BLACK
                    }
                };

                float[] widths = { 80f, 80f, 70f, 50f, 50f, 80f, 50f, 80f };

                DataTable dtRequistionDetails = new RequisitionDetails().GetRequistionFromView(master.Id);

                BaseColor color = new BaseColor(255, 255,255);
                ReportDetail reportDetail = new ReportDetail(detailDataList, dtRequistionDetails, 9, "English", widths,
                    "Verdana", true) { HeaderBackColor = color,NeedTotal = true,NoOfRow = 15};
                
                reportDetail.DetailsFont = new ReportFont("Verdana", 10, Font.NORMAL, BaseColor.BLACK, "English").GetFont();
                reportDetail.CaptionFont = new ReportFont("Verdana", 10, Font.NORMAL, BaseColor.BLACK, "English").GetFont();

                color = BaseColor.WHITE;
                widths = new float[] { 50f, 50f, 40f, 40f, 40f, 40f, 40f };

                var captionFont = new ReportFont("Tahoma", 9, Font.NORMAL, BaseColor.BLACK, "English").GetFont();
                var detailFont = new ReportFont("Tahoma", 9, Font.NORMAL, BaseColor.BLACK, "English").GetFont();

                DateTime date =DateTime.Now;

                DataTable dtTransactions = new DealerLedger().GetLast3TransactionForInvoice(dealer.Id, Imaster.Id, date);

                ReportDetail dealerTransactions = new ReportDetail(transactions, dtTransactions, 7, "English", widths,
                    "Tahoma",false) { HeaderBackColor = color, CaptionFont = captionFont, DetailsFont = detailFont };
                Hashtable signatory = new Hashtable();
                signatory.Add(1, "Logistics Head");

                var summaryList = new List<InvoiceSummary>
                {
                    new InvoiceSummary
                    {
                        Caption = "Item Total: ",
                        Value = master.ItemTotal.ToString("##,##0.00"),
                    },
                    new InvoiceSummary
                    {
                        Caption = "Discount: ",
                        Value = master.Discount.ToString("##,##0.00"),
                    },
                    new InvoiceSummary
                    {
                        Caption = "Requisition Total: ",
                        Value = master.RequistionTotal.ToString("##,##0.00"),
                        ValueFont = new ReportFont("Verdana.ttf", 10, Fonts.BOLD, BaseColor.BLACK,
                            "English").GetFont(),
                        CaptionFont = new ReportFont("Verdana.ttf", 10, Fonts.BOLD, BaseColor.BLACK,
                            "English").GetFont(),
                    },
                };

                InvoiceFooter footer = new InvoiceFooter()
                {
                    
                    CompanyName = company.CompanyName,
                    ShowTransaction = true,
                    Signatory = signatory,
                    Remarks = "",
                    SummaryList = summaryList
                };

                footer.FooterFont = new ReportFont("Verdana.ttf", 9, Fonts.NORMAL, BaseColor.BLACK,
                   "English").GetFont();

                footer.PaymentFont = new ReportFont("Verdana.ttf", 9, Fonts.NORMAL, BaseColor.BLACK,
                        "English").GetFont();

                PdfDocument doc = new PdfDocument("Invoice")
                {
                    ReportTemplate = PdfDocument.ReportType.SinglePage,
                    ShowPageHeader = false,
                    PrintPageNo = false,
                    MarginTop = 20f
                };

                MemoryStream pdfData = doc.WriteInvoiceStream(header, footer, reportDetail, dealerTransactions);

                return pdfData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}