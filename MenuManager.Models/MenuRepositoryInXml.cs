using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MenuManager.Models
{
    public class MenuRepositoryInXml : IMenuRepository
    {
        private string _connectionString;

        public MenuRepositoryInXml(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<MenuModel> GetAll()
        {
            // App_Data\\Menus.xml 파일 로드
            XElement xml = XElement.Load(_connectionString);

            return GetMenuData(xml, 0);
        }

        private List<MenuModel> GetMenuData(XElement xml, int parentId)
        {
            List<MenuModel> menus = new List<MenuModel>();
            var xmlMenus =
                from node in xml.Elements("Menu")
                where Convert.ToInt32(
                    node.Element("ParentId").Value) == parentId
                select new MenuModel
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
                            new List<MenuModel>()
                };
            if (menus != null)
            {
                menus = xmlMenus.ToList();
            }
            return menus;
        }

    }
}
