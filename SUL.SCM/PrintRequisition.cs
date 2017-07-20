using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using iTextSharp.text;
using PdfReport;
using PdfReport.Core;
using PdfReport.Templates;
using SUL.Bll;

namespace SUL.SCM
{
    public class PrintRequisition
    {
        /// <summary>
        /// </summary>
        /// <param name="requisitionId"></param>
        /// <param name="logoPath"></param>
        /// <returns></returns>
        public static MemoryStream Print(int requisitionId, string logoPath)
        {
            try
            {
                var company = new Company().GetParentCompany();

                var logoName = Path.GetFileName(logoPath);

                var master = new RequisitionMaster().GetRequisitionMasterById(requisitionId);
                if (master == null)
                {
                    throw new Exception("No Invoice Found.");
                }

                var Ref = string.Empty;

                if (master.CampaignId != null && master.CampaignId != 0)
                {
                    CampaignMaster objcaCampaignMaster = new CampaignMaster().GetCampaignMasterById(master.CampaignId);
                    if (!(objcaCampaignMaster == null || objcaCampaignMaster.Id == 0))
                        Ref = objcaCampaignMaster.CampaignCode;
                }
                var dealer = new DealerInformation().GetDealerInformationById(master.DealerId);

                //CourierInformation courier = (master.Courier == string.Empty)
                //    ? null
                //    : new CourierInformation().GetCourierInformationById(int.Parse(txtQuantity.Text));
                var transporter = "";
                if (master.Courier != null)
                {
                    transporter = master.Courier;
                }


                #region region header
                var header = new InvoiceHeader
                {
                    RefCaption = "",
                    Referrence = "",
                    DateCaption = "Date",
                    InvoiceDate = master.RequisitionDate,
                    InvoiceName = "Requisition",
                    InvoiceCaption = "Requisition No",
                    InvoiceNo = master.RequisitionCode,
                    Sender =
                        new Entity
                        {
                            EntityCaption = "Company Information",
                            Name = company.CompanyName,
                            Address1 = company.Address,
                            Address2 = company.Address2,
                            Phone = "Phone: " + company.Phone,
                            Email = "Email :" + company.Email,
                            Fax = "Fax: +8802-8811273"
                        },
                    Receiver =
                        new Entity
                        {
                            EntityCaption = "Dealer Information",
                            Name = dealer.DealerName,
                            Address1 = dealer.Address,
                            Address2 = "",
                            Phone = dealer.Phone,
                            Email = dealer.Email,
                            Fax = ""
                        },
                    Transporter = transporter,
                    TransporterDetails = "",
                    LogoPath = logoPath,
                    LogoName = logoName,
                    LogoHeight = 55,
                    LogoWidth = 215,
                    ReferenceNo = Ref
                };


                //set fonts for header
                header.DefaultFont = new ReportFont("verdana.ttf", 10, Fonts.NORMAL, BaseColor.BLACK, "English").GetFont();
                header.HeaderFont = new ReportFont("verdana.ttf", 10, Fonts.NORMAL, BaseColor.BLACK, "English").GetFont();
                header.CaptionFont = new ReportFont("verdana.ttf", 10, Fonts.NORMAL, BaseColor.BLACK, "English").GetFont();
                header.CaptionFontBold = new ReportFont("verdana.ttf", 10, Fonts.BOLD, BaseColor.BLACK, "English").GetFont();
                header.CompanyFont = new ReportFont("verdana.ttf", 10, Fonts.BOLD, BaseColor.BLACK, "English").GetFont();

                #endregion end region header

                #region region report details
                var detailDataList = new List<DetailStructure>
                {
                    new DetailStructure
                    {
                        Slno = 1,
                        FieldName = "CategoryCode",
                        DataType = "String",
                        Caption = "Product",
                        FieldWidth = 100f,
                        Align = Element.ALIGN_LEFT,
                        CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                        BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 2,
                        FieldName = "ProductName",
                        DataType = "string",
                        Caption = "Model No",
                        FieldWidth = 120f,
                        Align = Element.ALIGN_LEFT,
                        CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                        BorderColor = BaseColor.BLACK
                    },
                    
                    new DetailStructure
                    {
                        Slno = 3,
                        FieldName = "ColorName",
                        DataType = "string",
                        Caption = "Color",
                        FieldWidth = 40f,
                        Align = Element.ALIGN_LEFT,
                        CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                        BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 4,
                        FieldName = "Quantity",
                        DataType = "integer",
                        Caption = "Quantity",
                        FieldWidth = 40f,
                        Align = Element.ALIGN_RIGHT,
                        CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                        BorderColor = BaseColor.BLACK,
                        NeedTotal = true
                    },
                    new DetailStructure
                    {
                        Slno = 5,
                        FieldName = "Price",
                        DataType = "decimal",
                        Caption = "Price",
                        FieldWidth = 50f,
                        Align = Element.ALIGN_RIGHT,
                        CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                        BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 6,
                        FieldName = "Total2",
                        DataType = "decimal",
                        Caption = "Total Price",
                        FieldWidth = 50f,
                        Align = Element.ALIGN_RIGHT,
                        CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                        BorderColor = BaseColor.BLACK,
                        NeedTotal = true
                    },
                    new DetailStructure
                    {
                        Slno = 7,
                        FieldName = "Discount",
                        DataType = "decimal",
                        Caption = "Line Discount",
                        FieldWidth = 40f,
                        Align = Element.ALIGN_RIGHT,
                        CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                        BorderColor = BaseColor.BLACK,
                        NeedTotal = true
                    },
                    new DetailStructure
                    {
                        Slno = 8,
                        FieldName = "ItemTotal2",
                        DataType = "decimal",
                        Caption = "Line Total",
                        FieldWidth = 100f,
                        Align = Element.ALIGN_RIGHT,
                        CellBorder = Rectangle.BOTTOM_BORDER,
                        BorderColor = BaseColor.BLACK,
                        NeedTotal = true
                    }
                };

                #endregion region report details

                #region region transaction
                var transactions = new List<DetailStructure>
                {
                    new DetailStructure
                    {
                        Slno = 1,
                        FieldName = "TransactionDate",
                        DataType = "DateTime",
                        Caption = "Date",
                        FieldWidth = 40f,
                        Align = Element.ALIGN_LEFT,
                        CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                        BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 2,
                        FieldName = "TransactionType",
                        DataType = "String",
                        Caption = "Type",
                        FieldWidth = 50f,
                        Align = Element.ALIGN_LEFT,
                        CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                        BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 3,
                        FieldName = "SourceNo",
                        DataType = "string",
                        Caption = "Source No",
                        FieldWidth = 40f,
                        Align = Element.ALIGN_LEFT,
                        CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                        BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 4,
                        FieldName = "OpeningBalance",
                        DataType = "TimeSpan",
                        Caption = "Opening Balance",
                        FieldWidth = 40f,
                        Align = Element.ALIGN_RIGHT,
                        CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                        BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 5,
                        FieldName = "Debit",
                        DataType = "TimeSpan",
                        Caption = "Debit",
                        FieldWidth = 40f,
                        Align = Element.ALIGN_RIGHT,
                        CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                        BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 6,
                        FieldName = "Cradit",
                        DataType = "TimeSpan",
                        Caption = "Credit",
                        FieldWidth = 40f,
                        Align = Element.ALIGN_RIGHT,
                        CellBorder = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                        BorderColor = BaseColor.BLACK
                    },
                    new DetailStructure
                    {
                        Slno = 7,
                        FieldName = "ClosingBalance",
                        DataType = "TimeSpan",
                        Caption = "Closing Balance",
                        FieldWidth = 40f,
                        Align = Element.ALIGN_RIGHT,
                        CellBorder = Rectangle.BOTTOM_BORDER,
                        BorderColor = BaseColor.BLACK
                    }
                };

                #endregion region transaction


                float[] widths = { 80f, 80f, 70f, 50f, 50f, 80f, 50f,80f };

                var dtRequistionDetails = new RequisitionDetails().GetRequistionFromView(master.Id);

                var color = new BaseColor(255, 255, 255);
                var reportDetail = new ReportDetail(detailDataList, dtRequistionDetails, 10, "English", widths,
                    "Verdana", false) { HeaderBackColor = color,NeedTotal = true,NoOfRow = 14};

                reportDetail.DetailsFont = new ReportFont("Verdana", 10, Font.NORMAL, BaseColor.BLACK, "English").GetFont();
                reportDetail.CaptionFont = new ReportFont("Verdana", 10, Font.NORMAL, BaseColor.BLACK, "English").GetFont();

                color = BaseColor.WHITE;
                widths = new[] { 50f, 50f, 40f, 40f, 40f, 40f, 40f };

                var captionFont = new ReportFont("Tahoma", 9, Font.NORMAL, BaseColor.BLACK, "English").GetFont();
                var detailFont = new ReportFont("Tahoma", 9, Font.NORMAL, BaseColor.BLACK, "English").GetFont();

                var date = master.RequisitionDate;
                var dtTransactions = new DealerLedger().GetLast3Transaction(dealer.Id, date);

                var dealerTransactions = new ReportDetail(transactions, dtTransactions, 8, "English", widths,
                    "Tahoma", false) { HeaderBackColor = color, CaptionFont = captionFont, DetailsFont = detailFont, NoOfRow = 3 };


                var signatory = new Hashtable();
                signatory.Add(1, "Created By");
                signatory.Add(2, "HOS");
                signatory.Add(3, "Accounts");
                signatory.Add(4, "CFO");
                signatory.Add(5, "MD");
                signatory.Add(6, "CEO");

                #region region payment
                bool showPayment = false;
                PaymentDetailsInfo payment = null;
                Payment objPayment = new Payment().GetPaymentByRequisitionId(requisitionId);
                if (objPayment.Status == null)
                {
                    payment = new PaymentDetailsInfo()
                    {
                        BankNameCaption = "Bank Name: ",
                        BankName = "",
                        BalanceCaption = "Balance/Due: ",
                        Balance = master.LastBalance,
                        RefCaption = "Ref. No: ",
                        Reference = "",
                        DepositCaption = "Deposit Amount: ",
                        Deposit = 0
                    };
                    showPayment = true;
                }
                else
                {
                    if (objPayment.Status.ToLower() != "cancelled")
                    {
                        if (objPayment.Id > 0)
                        {
                            string bankName = string.Empty;
                            if (objPayment.PaymentMode == 1)
                                bankName = "Cash";
                            else
                            {
                                if (objPayment.BankNameId == 0) bankName = "Cash:";
                                else
                                {
                                    BankInformation bank =
                                        new BankInformation().GetBankInformationById(objPayment.BankNameId);
                                    bankName = bank.BankName + ", #" + bank.AccountNo;
                                }
                            }
                            payment = new PaymentDetailsInfo()
                            {
                                BankNameCaption = "Bank Name: ",
                                BankName = bankName,
                                BalanceCaption = "Balance/Due: ",
                                Balance = objPayment.LastBalance,
                                RefCaption = "Ref. No :",
                                Reference = objPayment.ReferenceNo,
                                DepositCaption = "Deposit Amount: ",
                                Deposit = objPayment.Amount
                            };
                            showPayment = true;
                        }
                        else
                        {
                            payment = new PaymentDetailsInfo()
                            {
                                BankNameCaption = "Bank Name: ",
                                BankName = "",
                                BalanceCaption = "Balance/Due: ",
                                Balance = master.LastBalance,
                                RefCaption = "Ref. No :",
                                Reference = "",
                                DepositCaption = "Deposit Amount: ",
                                Deposit = 0
                            };
                            showPayment = true;
                        }
                    }
                }

                #endregion region payment


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

                var footer = new InvoiceFooter()
                {
                    CompanyName = company.CompanyName,
                    ShowTransaction = true,
                    Signatory = signatory,
                    NoOfSignatoryColumn = 6,
                    ShowPayment = showPayment,
                    PaymentDetailsDetails = payment,
                    Remarks = master.Remarks,
                    SummaryList = summaryList
                };

                footer.FooterFont = new ReportFont("Verdana.ttf", 9, Fonts.NORMAL, BaseColor.BLACK,
                    "English").GetFont();

                footer.PaymentFont = new ReportFont("Verdana.ttf", 9, Fonts.NORMAL, BaseColor.BLACK,
                        "English").GetFont();

                var doc = new PdfDocument("Invoice")
                {
                    ReportTemplate = PdfDocument.ReportType.SinglePage,
                    ShowPageHeader = true,
                    PrintPageNo = true,
                    MarginTop = 20f
                };

                var pdfData = doc.WriteInvoiceStream(header, footer, reportDetail, dealerTransactions);

                return pdfData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}