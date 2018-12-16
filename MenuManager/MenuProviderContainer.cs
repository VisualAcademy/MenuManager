using System.Collections.Generic;

namespace MenuManager
{
    /// <summary>
    /// 컨테이너 클래스
    /// - InMemory, XML, SQL의 데이터를 주입해서 각각의 모든 메뉴 리스트 반환
    /// </summary>
    public class MenuProviderContainer
    {
        private MenuBase _repository; // _provider, _injector, ...

        public MenuProviderContainer(MenuBase provider)
        {
            _repository = provider;
        }

        public List<Menu> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
