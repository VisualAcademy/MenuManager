using MenuManager.Models;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace MenuManager.Web
{
    public partial class MenuSidebarUserControl : UserControl
    {
        // 모델 개체 생성: 1번 커뮤니티에 대한 모든 메뉴 가져오기 
        public List<MenuModel> Model { get; set; } = new List<MenuModel>();

        private int communityId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            // 넘어온 CommunityId 바인딩
            if (Request.QueryString["CommunityId"] != null)
            {
                communityId = 
                    Convert.ToInt32(Request.QueryString["CommunityId"]);
            }

            if (!Page.IsPostBack)
            {
                DisplayData();
            }
        }

        private void DisplayData()
        {
            var repository = new MenuRepository();

            // 커뮤니티별 메뉴 전체 리스트(IsVisible 속성이 true인 것만 출력) 
            Model = repository.GetAllByCommunityId(communityId, true);
        }
    }
}
