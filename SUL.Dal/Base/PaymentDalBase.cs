using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class PaymentDalBase : SqlServerConnection
	{
		public DataTable GetAllPayment(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Payment", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetPaymentById(Hashtable lstData)
		{
			string whereCondition = " where Payment.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Payment", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertPayment(Hashtable lstData)
		{
            string sqlQuery = "Insert into Payment (MoneyReceiptNo, PaymentDate, DealerId, Address, PaymentType, Amount, PaymentMode, BankNameId, ReferenceNo, RequisitionId, IsVarified, BankCharge, LastBalance, Branch, Status, UpdateBy, CreatedBy, ChequeBank, ChequeBranch, ChequeDate) values(@MoneyReceiptNo, @PaymentDate, @DealerId, @Address, @PaymentType, @Amount, @PaymentMode, @BankNameId, @ReferenceNo, @RequisitionId, @IsVarified, @BankCharge, @LastBalance, @Branch, @Status, @UpdateBy, @CreatedBy, @ChequeBank, @ChequeBranch, @ChequeDate);";
			try
			{
				int success = ExecuteNonQuery(sqlQuery, lstData);
				return success;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
			}
		}

		public int UpdatePayment(Hashtable lstData)
		{
            string sqlQuery = "Update Payment set MoneyReceiptNo = @MoneyReceiptNo, PaymentDate = @PaymentDate, DealerId = @DealerId, Address = @Address, PaymentType = @PaymentType, Amount = @Amount, PaymentMode = @PaymentMode, BankNameId = @BankNameId, ReferenceNo = @ReferenceNo, RequisitionId = @RequisitionId, IsVarified = @IsVarified, BankCharge = @BankCharge, LastBalance = @LastBalance, Branch = @Branch, Status = @Status, UpdateBy = @UpdateBy, CreatedBy = @CreatedBy, ChequeBank = @ChequeBank, ChequeBranch = @ChequeBranch, ChequeDate = @ChequeDate  where Payment.Id = @Id;";
			try
			{
				int success = ExecuteNonQuery(sqlQuery, lstData);
				return success;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
			}
		}

		public int DeletePaymentById(Hashtable lstData)
		{
			string sqlQuery = "delete from  Payment where Id = @Id;";
			try
			{
				int success = ExecuteNonQuery(sqlQuery, lstData);
				return success;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
			}
		}
	}
}
