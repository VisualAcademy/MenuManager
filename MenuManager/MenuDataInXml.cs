using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MenuManager
{
    public class MenuDataInXml : MenuBase
    {
        private string _connectionString;
        public MenuDataInXml(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override List<Menu> GetAll()
        {
            // App_Data\\Menus.xml 파일 로드
            XElement xml = XElement.Load(_connectionString);

            return GetMenuData(xml, 0);
        }

        private List<Menu> GetMenuData(XElement xml, int parentId)
        {
            List<Menu> menus = new List<Menu>();
            var xmlMenus =
                from node in xml.Elements("Menu")
                where Convert.ToInt32(
                    node.Element("ParentId").Value) == parentId
                select new Menu
                {
                    MenuId = Convert.ToInt32(node.Element("MenuId").Value),
                    MenuName = node.Element("MenuName").Value,
                    // 자식 요소들은 재귀 함수를 사용해서 Menus에 채워 넣음
                    Menus =
                        (parentId !=
                            Convert.ToInt32(node.Element("MenuId").Value))
                        ?
                            GetMenuData(xml, Convert.ToInt32(
                                node.Element("MenuId").Value))
                        :
                            new List<Menu>()
                };
            if (menus != null)
            {
                menus = xmlMenus.ToList();
            }
            return menus;
        }
    }
}
