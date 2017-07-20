using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class ItemLedger : SUL.Bll.Base.ItemLedgerBase
	{
		private static SUL.Dal.ItemLedgerDal Dal = new SUL.Dal.ItemLedgerDal();
		public ItemLedger() : base()
		{
		}

        public DataTable GetItemLedgerFromViewList()
        {
            Hashtable lstItems = new Hashtable();

            DataTable dt = dal.GetItemLedgerFromViewList(lstItems);
            return dt;
        }


	    public List<ItemLedger> GetAllItemLedgersByItemIdUnitColor(int _itemId,int _unit, int _color)
	    {
            Hashtable lstItems=new Hashtable();

            lstItems.Add("@ItemId",_itemId);
            lstItems.Add("@Unit",_unit);
            lstItems.Add("@Color",_color);

	        DataTable dt = dal.GetAllItemLedgersByItemIdUnitColor(lstItems);
            List<ItemLedger> ItemLedgersList=new List<ItemLedger>();
	        foreach (DataRow dr in dt.Rows)
	        {
	            ItemLedgersList.Add(GetObject(dr));
	        }
	        return ItemLedgersList;
	    }

        public ItemLedger GetItemLedgerByItemIdUnitColor(int _itemId, int _unit, int _color)
        {
            Hashtable lstItems = new Hashtable();

            lstItems.Add("@ItemId", _itemId);
            lstItems.Add("@Unit", _unit);
            lstItems.Add("@Color", _color);

            DataTable dt = dal.GetAllItemLedgersByItemIdUnitColor(lstItems);
            DataRow dr = dt.Rows[0];
            return GetObject(dr);
        }

        public List<ItemLedger> GetAllItemLedgersByItemIdUnit(int _itemId, int _unit)
        {
            Hashtable lstItems = new Hashtable();

            lstItems.Add("@ItemId", _itemId);
            lstItems.Add("@Unit", _unit);

            DataTable dt = dal.GetAllItemLedgersByItemIdUnit(lstItems);
            List<ItemLedger> ItemLedgersList = new List<ItemLedger>();
            foreach (DataRow dr in dt.Rows)
            {
                ItemLedgersList.Add(GetObject(dr));
            }
            return ItemLedgersList;
        }

        public ItemLedger GetItemLedgerByItemIdUnit(int _itemId, int _unit)
        {
            Hashtable lstItems = new Hashtable();

            lstItems.Add("@ItemId", _itemId);
            lstItems.Add("@Unit", _unit);
           

            DataTable dt = dal.GetAllItemLedgersByItemIdUnit(lstItems);
            if(dt.Rows.Count==0)
                return new ItemLedger();
            DataRow dr = dt.Rows[0];
            return GetObject(dr);
        }

        public Int32 UpdateItemLedgerNew()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id.ToString());
           
            lstItems.Add("@TotalIn", TotalIn.ToString());
            lstItems.Add("@TotalOut", TotalOut.ToString());

            return dal.UpdateItemLedgerNew(lstItems);
        }

        public DataTable SearchItemLedger(int warehouseId, int productId, int colorId)
        {
            string conditions = string.Empty;

            Hashtable lstItems = new Hashtable();
            if (warehouseId > 0)
            {
                lstItems.Add("@WareHouseId", warehouseId);
                conditions = " where WareHouseId= @WareHouseId";
            }
            if (productId > 0)
            {
                lstItems.Add("@ItemId", productId);
                if (conditions == string.Empty)
                    conditions = " where ItemId= @ItemId";
                else
                    conditions += " And ItemId= @ItemId";
            }
            if (colorId > 0)
            {
                lstItems.Add("@Color", colorId);
                if (conditions == string.Empty)
                    conditions = "where Color = @Color";
                else
                    conditions += " And Color = @Color";
            }

            return dal.SearchItemLedger(lstItems, conditions);
        }

	}
}
