using System;
using System.Collections.Generic;
using System.IO;

namespace MenuManager
{
    public class MenuManager
    {
        static void Main(string[] args)
        {
            #region [1] 인-메모리 컬렉션 사용
            Console.WriteLine("[1] 인-메모리 컬렉션 사용");
            var subMenu =
                new MenuProviderContainer(new MenuDataInMemory());
            MenuPrint(subMenu.GetAll());
            #endregion

            #region [2] XML 사용
            Console.WriteLine("[2] XML 사용");
            var xmlMenu =
                new MenuProviderContainer(
                    new MenuDataInXml(
                        Path.Combine(Directory.GetCurrentDirectory(),
                            "App_Data\\Menus.xml")
                    ));
            MenuPrint(xmlMenu.GetAll());
            #endregion
            
            #region [3] SQL Server 데이터베이스 사용
            Console.WriteLine("[3] SQL Server 데이터베이스 사용");
            var sqlMenu =
                new MenuProviderContainer(
                    new MenuDataInSql(
                        "server=(localdb)\\mssqllocaldb;database=MenuManager;"
                            + "Integrated Security=True;"));
            MenuPrint(sqlMenu.GetAll());
            #endregion
        }

        private static void MenuPrint(List<Menu> menus)
        {
            foreach (var menu in menus)
            {
                // 부모 요소 출력
                Console.WriteLine($"{menu.MenuId} - {menu.MenuName}");
                // 자식 요소가 있으면 들여쓰기해서 출력
                if (menu.Menus.Count > 0)
                {
                    foreach (var c in menu.Menus)
                    {
                        Console.WriteLine(
                            $"\t{c.MenuId} - {c.MenuName}");
                    }
                }
            }
            Console.WriteLine();
        }
    }
}
