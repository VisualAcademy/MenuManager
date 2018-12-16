using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MenuManager
{
    public class MenuDataInSql : MenuBase
    {
        private readonly string _connectionString;

        public MenuDataInSql(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override List<Menu> GetAll()
        {
            List<Menu> menus = new List<Menu>();


            DataTable dt = new DataTable();
            SqlDataAdapter da =
                new SqlDataAdapter("Select * From Menus", _connectionString);
            da.Fill(dt);

            var q =
                from dr in dt.AsEnumerable()
                select new Menu
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

        private List<Menu> GetMenuData(List<Menu> menus, int parentId)
        {
            List<Menu> lst = new List<Menu>();

            var q =
                from m in menus
                where m.ParentId == parentId
                orderby m.MenuOrder
                select new Menu
                {
                    MenuId = m.MenuId,
                    MenuOrder = m.MenuOrder,
                    ParentId = m.ParentId,
                    MenuName = m.MenuName,
                    MenuPath = m.MenuPath,
                    IsVisible = m.IsVisible,

                    Menus = (m.MenuId != parentId)
                        ? GetMenuData(menus, m.MenuId) : new List<Menu>()
                };

            lst = q.ToList();

            return lst;
        }
    }
}
