using System.Collections.Generic;

namespace MenuManager.Models
{
    /// <summary>
    /// 메뉴 저장소에 대한 리파지터리 인터페이스
    /// </summary>
    public interface IMenuRepository
    {
        List<MenuModel> GetAll(int communityId);
        List<MenuModel> GetAllByCommunityId(int communityId);
        List<MenuModel> GetAllByCommunityId(int communityId, bool isVisible);

        void UpdateModel(List<MenuModel> lst);

        /// <summary>
        /// 삭제
        /// </summary>
        void Remove(int menuId);

        MenuModel Add(MenuModel model);

        List<MenuModel> GetMenusByParentId(int parentId);
        List<MenuModel> GetMenusByParentId(int parentId, int communityId);

        int UpdateMenuOrder(int parentId, int communityId);
    }
}
