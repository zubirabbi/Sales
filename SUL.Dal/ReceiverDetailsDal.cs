using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
    public class ReceiverDetailsDal : SUL.Dal.Base.ReceiverDetailsDalBase
    {
        public ReceiverDetailsDal()
            : base()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetPIDetailsTable(Hashtable lstData)
        {
            string queryString = @"select  isnull(ReceiverDetails.Id,0) as Id, ProductId, PIDetails.ProductName,Product.ProductCode, PIquantity , isnull(sum(ReceiveQuantity),0) as receivedQuantity, isnull(Color,'') as color, isnull(ReceiveQuantity,0) as ReceiveQuantity, ProductUnit  
                                    from PIDetails inner join PIMaster on PIDetails.MasterId = PIMaster.id
                                    Left outer join ReceiverMaster on PIMaster.Id = ReceiverMaster.PIId 
                                    Left outer join ReceiverDetails on ReceiverMaster.id  = ReceiverDetails.MasterId 
                                    Inner join Product on  PIDetails.ProductId=Product.Id
                                    where  PIMaster.PINo = @PINo
                                    group by ReceiverDetails.Id, ProductId, PIquantity, Color, ProductUnit,PIDetails.ProductName,Product.ProductCode,ReceiveQuantity ";

            try
            {
                return GetDataTable(queryString, lstData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
            }
        }

        public DataTable GetReceiverDetailsTable(Hashtable lstData)
        {
            string queryString =@"select rd.Id as Id,MasterId,p.Id as ProductId,p.ProductName as ProductName,rd.ProductCode as ProductCode,LCQuantity as PIquantity,receivedQuantity = (select sum(ReceiveQuantity) from  ReceiverDetails),Color,ReceiveQuantity,Unit as ProductUnit
                    from ReceiverDetails rd inner join Product p on rd.ProductCode=p.ProductCode
                    where rd.masterid=@MasterId
                    group by rd.Id,masterid,p.Id,ProductName,rd.ProductCode,LCQuantity,Color,ReceiveQuantity,Unit";

            try
            {
                return GetDataTable(queryString, lstData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetPIDetailsTableByProductId(Hashtable lstData)
        {
            string queryString = @"select PIDetails.ProductId as ProductId, PIDetails.ProductName,Product.ProductCode, PIquantity , isnull(sum(ReceiveQuantity),0) as receivedQuantity,
                                    isnull(Color,'') as color, isnull(ReceiveQuantity,0) as ReceiveQuantity, ProductUnit  
                                    from PIDetails 
									Inner join Product on  PIDetails.ProductId=Product.Id
									inner join PIMaster on PIDetails.MasterId = PIMaster.id
									Left outer join ReceiverMaster on PIMaster.Id = ReceiverMaster.PIId 
                                    Left outer join ReceiverDetails on ReceiverMaster.id  = ReceiverDetails.MasterId and ReceiverDetails.ProductId = @ProductId
                                    where  PIMaster.PINo = @PINo and PIDetails.ProductId = @ProductId
                                    group by  PIDetails.ProductId, PIquantity, Color, ProductUnit,PIDetails.ProductName,Product.ProductCode,ReceiveQuantity";

            try
            {
                return GetDataTable(queryString, lstData);
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
