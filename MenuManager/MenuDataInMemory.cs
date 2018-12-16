using System.Collections.Generic;

namespace MenuManager
{
    public class MenuDataInMemory : MenuBase
    {
        public override List<Menu> GetAll()
        {
            List<Menu> menus = new List<Menu>() {
                new Menu { MenuId = 1, MenuName = "책" },
                new Menu { MenuId = 2, MenuName = "강의" },
                new Menu { MenuId = 3, MenuName = "컴퓨터" }
            };

            // 서브 메뉴 등록
            Menu mnu;

            mnu = new Menu();
            mnu.MenuId = 4;
            mnu.MenuName = "좋은 책";
            mnu.ParentId = 1;
            menus.Find(m => m.MenuId == 1).Menus.Add(mnu);

            mnu = new Menu();
            mnu.MenuId = 5;
            mnu.MenuName = "나쁜 책";
            mnu.ParentId = 1;
            menus.Find(m => m.MenuId == 1).Menus.Add(mnu);

            mnu = new Menu();
            mnu.MenuId = 6;
            mnu.MenuName = "오프라인 강의";
            mnu.ParentId = 2;
            menus.Find(m => m.MenuId == 2).Menus.Add(mnu);

            mnu = new Menu();
            mnu.MenuId = 7;
            mnu.MenuName = "온라인 강의";
            mnu.ParentId = 2;
            menus.Find(m => m.MenuId == 2).Menus.Add(mnu);

            mnu = new Menu();
            mnu.MenuId = 8;
            mnu.MenuName = "데스크톱";
            mnu.ParentId = 3;
            menus.Find(m => m.MenuId == 3).Menus.Add(mnu);

            mnu = new Menu();
            mnu.MenuId = 9;
            mnu.MenuName = "노트북";
            mnu.ParentId = 3;
            menus.Find(m => m.MenuId == 3).Menus.Add(mnu);

            return menus;
        }
    }
}
