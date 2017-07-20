using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class BranchDalBase : SqlServerConnection
	{
		public DataTable GetAllBranch(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Branch", "*", " Where Branch.IsActive = 1;", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetBranchById(Hashtable lstData)
		{
			string whereCondition = " where Branch.Id = @Id And Branch.IsActive = 1";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Branch", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertBranch(Hashtable lstData)
		{
			string sqlQuery ="Insert into Branch (BranchCode, BranchName, Location, InchargeId, IsActive, CompanyId) values(@BranchCode, @BranchName, @Location, @InchargeId, @IsActive, @CompanyId);";
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

		public int UpdateBranch(Hashtable lstData)
		{
			string sqlQuery = "Update Branch set BranchCode = @BranchCode, BranchName = @BranchName, Location = @Location, InchargeId = @InchargeId, IsActive = @IsActive, CompanyId = @CompanyId where Branch.Id = @Id;";
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

		public int DeleteBranchById(Hashtable lstData)
		{
			string sqlQuery = "delete from  Branch where Id = @Id;";
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
