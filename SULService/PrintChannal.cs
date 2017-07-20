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
    public class PrintChannal
    {
        public static MemoryStream Print(int requisitionId, string logoPath)
        {
            try
            {
                Company company = new Company().GetParentCompany();

                string logoName = Path.GetFileName(logoPath);

                RequisitionMaster master = new RequisitionMaster().GetRequisitionMasterById(requisitionId);
                if (master == null) return null;

                DealerInformation dealer = new DealerInformation().GetDealerInformationById(master.DealerId);

                DeliveryMaster objDeliveryMaster=new DeliveryMaster().GetDeliveryMasterByRequisitionId(requisitionId);

                string transporter = "";
                if (master.Courier == string.Empty)
                {
                    CourierInformation courier = new CourierInformation().GetCourierInformationById(int.Parse(master.Courier));

                    if (courier.Id == 0)
                        transporter = master.Courier;
                    else
                    {
                        transporter = courier.Name;
                    }
                }
                InvoiceHeader header = new InvoiceHeader()
                {
                    InvoiceName = "Delivery",
                    InvoiceCaption = "Challan No",
                    InvoiceNo = objDeliveryMaster.DeliveryNo,
                    RefCaption = "",
                    Referrence = "",
                    DateCaption = "Challan Date",
                    InvoiceDate = objDeliveryMaster.DeliveryDate,
                    Sender = new Entity() { EntityCaption = "Company Information", Name = company.CompanyName, Address1 = company.Address, Address2 = company.Address, Phone = "Phone: " + company.Phone, Email = "Email :" + company.Email, Fax = "Fax: +8802-8811273" },
                    Receiver = new Entity() { EntityCaption = "Dealer Information", Name = dealer.DealerName, Address1 = dealer.Address, Address2 = "", Phone = dealer.Phone, Email = dealer.Email, Fax = "" },

                    Transporter = transporter,
                    TransporterDetails = "",

                    LogoPath = logoPath,
                    LogoName = logoName,
                    LogoHeight = 55,
                    LogoWidth = 215,
                    ReferenceNo = ""
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
                        Slno = 1, FieldName = "ModelNo", DataType = "string", Caption = "Product",
                        FieldWidth = 120f, Align = Alignments.ALIGN_LEFT, CellBorder =  Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER , BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 2, FieldName = "CategoryCode", DataType = "String", Caption = "Model No",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT, CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 3, FieldName = "ColorName", DataType = "string", Caption = "Color",
                        FieldWidth = 60f, Align = Alignments.ALIGN_LEFT, CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 3, FieldName = "Quantity", DataType = "numeric", Caption = "Quantity",
                        FieldWidth = 60f, Align = Alignments.ALIGN_LEFT, CellBorder =  Rectangle.BOTTOM_BORDER, BorderColor = BaseColor.BLACK,NeedTotal = true
                    }
                   
                };


                float[] widths = new float[] { 100f, 100f, 60f, 60f };

                DataTable dtRequistionDetails = new RequisitionDetails().GetRequistionFromView(master.Id);

                BaseColor color = new BaseColor(255, 255, 255);
                ReportDetail reportDetail = new ReportDetail(detailDataList, dtRequistionDetails,9, "English", widths,
                    "Verdana", true) { HeaderBackColor = color,NeedTotal = true};
                
                reportDetail.DetailsFont = new ReportFont("Verdana", 10, Font.NORMAL, BaseColor.BLACK, "English").GetFont();
                reportDetail.CaptionFont = new ReportFont("Verdana", 10, Font.NORMAL, BaseColor.BLACK, "English").GetFont();

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
                    ShowTransaction = false,
                    Signatory = signatory,
                    Remarks = master.Remarks,
                    SummaryList = summaryList
                };

                PdfDocument doc = new PdfDocument("Invoice")
                {
                    ReportTemplate = PdfDocument.ReportType.SinglePage,
                    ShowPageHeader = false,
                    PrintPageNo = false,
                    MarginTop = 100f
                };

                MemoryStream pdfData = doc.WriteInvoiceStream(header, footer, reportDetail, null);

                return pdfData;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}