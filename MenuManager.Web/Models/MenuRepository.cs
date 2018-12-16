using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MenuManager.Models
{
    public class MenuRepository : IMenuRepository
    {
        private IDbConnection db;
        private string _connectionString;

        public MenuRepository()
        {
            _connectionString = 
                ConfigurationManager.ConnectionStrings[
                    "ConnectionString"].ConnectionString;
            db = new SqlConnection(_connectionString);
        }

        public List<MenuModel> GetAll(int communityId)
        {
            List<MenuModel> menus = new List<MenuModel>();


            DataTable dt = new DataTable();
            SqlDataAdapter da =
                new SqlDataAdapter(
                    "Select * From Menus Where CommunityId = "
                    + communityId.ToString() 
                    + " Order By MenuOrder Asc, MenuId Asc",
                    _connectionString);
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
                        "",

                    IsVisible = Convert.ToBoolean(dr["IsVisible"]),

                    CommunityId =
                        Convert.ToInt32(dr["CommunityId"].ToString()),

                    IsBoard = Convert.ToBoolean(dr["IsBoard"]),

                    Target =
                        dr["Target"] != DBNull.Value
                        ?
                        dr.Field<string>("Target")
                        :
                        "_self",

                    BoardAlias =
                        dr["BoardAlias"] != DBNull.Value
                        ?
                        dr.Field<string>("BoardAlias")
                        :
                        ""
                };

            if (q != null)
            {
                menus = q.ToList();
            }

            return menus;
        }

        public List<MenuModel> GetAllByCommunityId(int communityId)
        {
            List<MenuModel> menus = new List<MenuModel>();


            DataTable dt = new DataTable();
            SqlDataAdapter da =
                new SqlDataAdapter(
                    "Select * From Menus Where CommunityId = " 
                    + communityId.ToString(), 
                    _connectionString);
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
                        "",

                    IsVisible = Convert.ToBoolean(dr["IsVisible"]),

                    CommunityId =
                        Convert.ToInt32(dr["CommunityId"].ToString()),

                    IsBoard = Convert.ToBoolean(dr["IsBoard"]),

                    Target =
                        dr["Target"] != DBNull.Value
                        ?
                        dr.Field<string>("Target")
                        :
                        "_self",

                    BoardAlias =
                        dr["BoardAlias"] != DBNull.Value
                        ?
                        dr.Field<string>("BoardAlias")
                        :
                        ""
                };

            if (q != null)
            {
                menus = q.ToList();
            }

            return GetMenuData(menus, 0, communityId);
        }
        
        /// <summary>
        /// 커뮤니티별 IsVisible 속성이 참(1)인 메뉴만 읽어오기
        /// </summary>
        public List<MenuModel> GetAllByCommunityId(
            int communityId, bool isVisible)
        {
            List<MenuModel> menus = new List<MenuModel>();
            
            DataTable dt = new DataTable();
            SqlDataAdapter da =
                new SqlDataAdapter(@"
                    Select * 
                    From Menus 
                    Where 
                        IsVisible = " + ((isVisible) ? "1" : "0") + @"
                        And  
                        CommunityId = "
                    + communityId.ToString(),
                    _connectionString);
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
                        "",

                    IsVisible = Convert.ToBoolean(dr["IsVisible"]),


                    CommunityId = Convert.ToInt32(dr["CommunityId"].ToString()),

                    IsBoard = Convert.ToBoolean(dr["IsBoard"]),

                    Target =
                        dr["Target"] != DBNull.Value
                        ?
                        dr.Field<string>("Target")
                        :
                        "_self",

                    BoardAlias =
                        dr["BoardAlias"] != DBNull.Value
                        ?
                        dr.Field<string>("BoardAlias")
                        :
                        ""
                };

            if (q != null)
            {
                menus = q.ToList();
            }

            return GetMenuData(menus, 0, communityId);
        }

        public void UpdateModel(List<MenuModel> lst)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                Update(lst[i]);
            } 
        }

        public MenuModel Update(MenuModel model)
        {
            var sql =
                " Update Menus                  " +
                " Set                            " +
                "    MenuName       =       @MenuName,   " +
                "    MenuOrder       =       @MenuOrder,   " +
                "    MenuPath       =       @MenuPath,   " +
                "    IsVisible       =       @IsVisible,   " +
                "    IsBoard       =       @IsBoard,   " +
                "    Target       =       @Target,   " +
                "    BoardAlias       =       @BoardAlias " +
                " Where MenuId = @MenuId                 ";
            db.Execute(sql, model);
            return model;
        }
        
        private List<MenuModel> GetMenuData(
            List<MenuModel> menus, int parentId, int communityId)
        {
            List<MenuModel> lst = new List<MenuModel>();

            var q =
                from m in menus
                where m.ParentId == parentId && m.CommunityId == communityId
                orderby m.MenuOrder
                select new MenuModel
                {
                    MenuId = m.MenuId,
                    MenuOrder = m.MenuOrder,
                    ParentId = m.ParentId,
                    MenuName = m.MenuName,
                    MenuPath = m.MenuPath,
                    IsVisible = m.IsVisible,

                    CommunityId = m.CommunityId,

                    IsBoard = m.IsBoard,
                    Target = m.Target,
                    BoardAlias = m.BoardAlias,

                    Menus = (m.MenuId != parentId)
                        ? GetMenuData(menus, m.MenuId, communityId) : new List<MenuModel>()
                };

            lst = q.ToList();

            return lst;
        }

        /// <summary>
        /// 메뉴 삭제
        /// </summary>
        public void Remove(int menuId)
        {
            string query = "Delete Menus Where MenuId = @MenuId";
            db.Execute(query, new { MenuId = menuId });
        }

        /// <summary>
        /// 메뉴 추가
        /// </summary>
        public MenuModel Add(MenuModel model)
        {
            string sql = @"
                Insert Into Menus
                (
                    [MenuOrder], 
                    [ParentId], 
                    [MenuName], 
                    [MenuPath], 
                    [IsVisible], 
                    [CommunityId], 
                    [IsBoard], 
                    [Target],
                    [BoardAlias]
                )
                Values
                (
                    @MenuOrder, 
                    @ParentId, 
                    @MenuName, 
                    @MenuPath, 
                    @IsVisible, 
                    @CommunityId, 
                    @IsBoard, 
                    @Target,
                    @BoardAlias
                );

                Select Cast(SCOPE_IDENTITY() As Int);
            ";
            var menuId = db.Query<int>(sql, model).Single();
            model.MenuId = menuId;
            return model;
        }

        public List<MenuModel> GetMenusByParentId(int parentId)
        {
            string sql = 
                "Select * From Menus Where ParentId = @ParentId";
            return db.Query<MenuModel>(
                sql, new { ParentId = parentId }).ToList();
        }

        public List<MenuModel> GetMenusByParentId(
            int parentId, int communityId)
        {
            string sql = @"
                Select * From Menus 
                Where 
                    ParentId = @ParentId 
                    And 
                    CommunityId = @CommunityId
            ";
            return db.Query<MenuModel>(
                sql, 
                new { ParentId = parentId,
                    CommunityId = communityId }).ToList();
        }

        public int UpdateMenuOrder(int parentId, int communityId)
        {
            string sql = "Select * From Menus";
            int menuOrder = 0;

            if (parentId == 0)
            {
                // 기본은 상위 메뉴로 가정
                sql = @"
                    Select 
                        IsNull(Max(MenuOrder), 0) As MaxMenuOrder
                    From Menus 
                    Where CommunityId = @CommunityId;
                ";
                menuOrder = db.Query<int>(sql, 
                    new { CommunityId = communityId }).SingleOrDefault();
            }
            else 
            {
                // 서브 메뉴
                sql = @"
                    Declare @MenuOrder Int;
                    Select @MenuOrder = MenuOrder
                    From Menus 
                    Where MenuId = @ParentId;

                    Update Menus
                    Set
                        MenuOrder = MenuOrder + 1
                    Where 
                        CommunityId = @CommunityId
                        And
                        MenuId > @MenuOrder;

                    Select @MenuOrder;
                ";
                menuOrder = db.Query<int>(sql, 
                    new { ParentId = parentId,
                        CommunityId = communityId }).SingleOrDefault();
            }

            return (menuOrder + 1);
        }
    }
}
