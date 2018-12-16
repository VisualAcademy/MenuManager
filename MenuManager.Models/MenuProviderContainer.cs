using System.Collections.Generic;

namespace MenuManager.Models
{
    /// <summary>
    /// 컨테이너 클래스
    /// - InMemory, Xml, Sql의 주입해서 각각의 모든 메뉴 리스트 반환
    /// </summary>
    public class MenuProviderContainer
    {
        private IMenuRepository _repository; // _provider, _injector, ...

        public MenuProviderContainer(IMenuRepository provider)
        {
            _repository = provider;
        }

        public List<MenuModel> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
