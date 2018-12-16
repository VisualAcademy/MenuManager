using System.Collections.Generic;

namespace MenuManager.Models
{
    public class MenuRepositoryInMemory : IMenuRepository
    {
        public List<MenuModel> GetAll()
        {
            List<MenuModel> menus = new List<MenuModel>() {
                new MenuModel { MenuId = 1, MenuName = "책" },
                new MenuModel { MenuId = 2, MenuName = "강의" },
                new MenuModel { MenuId = 3, MenuName = "컴퓨터" }
            };

            // 서브 메뉴 등록
            MenuModel mnu;

            mnu = new MenuModel();
            mnu.MenuId = 4;
            mnu.MenuName = "좋은책";
            mnu.ParentId = 1;
            menus.Find(m => m.MenuId == 1).Menus.Add(mnu);

            mnu = new MenuModel();
            mnu.MenuId = 5;
            mnu.MenuName = "나쁜책";
            mnu.ParentId = 1;
            menus.Find(m => m.MenuId == 1).Menus.Add(mnu);

            mnu = new MenuModel();
            mnu.MenuId = 6;
            mnu.MenuName = "좋은강의";
            mnu.ParentId = 2;
            menus.Find(m => m.MenuId == 2).Menus.Add(mnu);

            mnu = new MenuModel();
            mnu.MenuId = 7;
            mnu.MenuName = "나쁜강의";
            mnu.ParentId = 2;
            menus.Find(m => m.MenuId == 2).Menus.Add(mnu);

            mnu = new MenuModel();
            mnu.MenuId = 8;
            mnu.MenuName = "데스크톱";
            mnu.ParentId = 3;
            menus.Find(m => m.MenuId == 3).Menus.Add(mnu);

            mnu = new MenuModel();
            mnu.MenuId = 9;
            mnu.MenuName = "노트북";
            mnu.ParentId = 3;
            menus.Find(m => m.MenuId == 3).Menus.Add(mnu);

            return menus;
        }
    }
}
