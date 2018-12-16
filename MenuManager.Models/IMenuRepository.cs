using System.Collections.Generic;

namespace MenuManager.Models
{
    /// <summary>
    /// 메뉴 저장소에 대한 리파지터리 인터페이스
    /// </summary>
    public interface IMenuRepository
    {
        List<MenuModel> GetAll();
    }
}
