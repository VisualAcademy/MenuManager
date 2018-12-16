using System.Collections.Generic;

namespace MenuManager.Models
{
    /// <summary>
    /// 메뉴(Menu) 모델 클래스: MenuModel, MenuViewModel, ...
    /// </summary>
    public class MenuModel
    {
        /// <summary>
        /// 일련번호+고유키
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 보여지는 순서
        /// </summary>
        public int MenuOrder { get; set; }

        /// <summary>
        /// 부모 메뉴 번호: 최상위(0)
        /// </summary>
        public int ParentId { get; set; } = 0;

        /// <summary>
        /// 메뉴 이름
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 선택할 때 이동 경로
        /// </summary>
        public string MenuPath { get; set; }

        /// <summary>
        /// 메뉴 표시 여부
        /// </summary>
        public bool IsVisible { get; set; } = true;
        
        /// <summary>
        /// 커뮤니티 번호(CommunityId, TabId, PortalId, ...)
        /// </summary>
        public int CommunityId { get; set; }

        /// <summary>
        /// 자식 메뉴 리스트
        /// </summary>
        public List<MenuModel> Menus { get; set; } = new List<MenuModel>();

        /// <summary>
        /// ToString 메서드 오버라이드
        /// </summary>
        /// <returns>MenuName</returns>
        public override string ToString()
        {
            return MenuName;
        }
    }
}
