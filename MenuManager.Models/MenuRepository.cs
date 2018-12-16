using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MenuManager.Models
{
    public class MenuRepository : IMenuRepository
    {
        private string _connectionString;

        public MenuRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<MenuModel> GetAll()
        {
            List<MenuModel> menus = new List<MenuModel>();


            DataTable dt = new DataTable();
            SqlDataAdapter da =
                new SqlDataAdapter("Select * From Menus", _connectionString);
            da.Fill(dt);

            var q =
                from dr in dt.AsEnumerable()
                select new MenuModel
                {
                    MenuId = dr.Field<int>("MenuId"),
                    MenuOrder = Convert.ToInt32(dr["MenuOrder"]),
                    ParentId =
                        dr["ParentId"] != DBNull.Value
                        ?
                        dr.Field<int>("ParentId")
                        :
                        0,
                    MenuName = dr["MenuName"].ToString(),
                    MenuPath =
                        dr["MenuPath"] != DBNull.Value
                        ?
                        dr.Field<string>("MenuPath")
                        :
                        ""
                };

            if (q != null)
            {
                menus = q.ToList();
            }

            return GetMenuData(menus, 0);

        }

        private List<MenuModel> GetMenuData(List<MenuModel> menus, int parentId)
        {
            List<MenuModel> lst = new List<MenuModel>();

            var q =
                from m in menus
                where m.ParentId == parentId
                orderby m.MenuOrder
                select new MenuModel
                {
                    MenuId = m.MenuId,
                    MenuOrder = m.MenuOrder,
                    ParentId = m.ParentId,
                    MenuName = m.MenuName,
                    MenuPath = m.MenuPath,
                    IsVisible = m.IsVisible,

                    Menus = (m.MenuId != parentId)
                        ? GetMenuData(menus, m.MenuId) : new List<MenuModel>()
                };

            lst = q.ToList();

            return lst;
        }

    }
}
