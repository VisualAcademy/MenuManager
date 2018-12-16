using MenuManager.Models;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MenuManager.Web
{
    public partial class MenuManagerUserControl : UserControl
    {
        //private int communityId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCommunity();
            }

            if (!Page.IsPostBack)
            {
                // 넘어온 CommunityId 바인딩
                if (Request.QueryString["CommunityId"] != null)
                {
                    //communityId =
                    //    Convert.ToInt32(Request.QueryString["CommunityId"]);

                    // 드롭다운리스트 선택
                    ddlCommunity.SelectedValue =
                        Request.QueryString["CommunityId"];
                } 
            }

            if (!Page.IsPostBack)
            {
                DisplayData();
            }

            // 커뮤니티가 선택된 상태에서만 메뉴 등록 버튼 활성화
            if (ddlCommunity.SelectedValue == "0")
            {
                btnAddMenu.Enabled = false;
            }
            else
            {
                btnAddMenu.Enabled = true; 
            }
        }

        /// <summary>
        /// 커뮤니티 드롭다운리스트 바인딩
        /// </summary>
        private void BindCommunity()
        {
            ddlCommunity.Items.Add(new ListItem("GOT7", "3"));
        }

        private void DisplayData()
        {
            var repository = new MenuRepository();

            // 커뮤니티별 메뉴 전체 리스트 
            ctlMenuLists.DataSource = 
                repository.GetAll(Convert.ToInt32(ddlCommunity.SelectedValue));
            ctlMenuLists.DataBind(); 
        }

        protected string FuncIsVisible(object objIsVisible)
        {
            string s = "";
            bool isVisible = Convert.ToBoolean(objIsVisible);

            if (isVisible)
            {
                s = "<input type='checkbox' name='chkIsVisible' value='true' " 
                    + "checked onchange='valueChanged()' />";
            }
            else
            {
                s = "<input type='checkbox' name='chkIsVisible' value='false' " 
                    + " onchange='valueChanged()' />";
            }

            return s;
        }

        protected string FuncIsBoard(object objIsBoard)
        {
            string s = "";
            bool isBoard = Convert.ToBoolean(objIsBoard);

            if (isBoard)
            {
                s = "<input type='checkbox' name='chkIsBoard' value='true' "
                    + "checked onchange='IsBoardValueChanged()' />";
            }
            else
            {
                s = "<input type='checkbox' name='chkIsBoard' value='false' "
                    + " onchange='IsBoardValueChanged()' />";
            }

            return s;
        }


        protected string FunStep(object objParentId)
        {
            string s = "";

            int parentId = Convert.ToInt32(objParentId);

            if (parentId == 0)
            {
                s = "<span class='btn btn-default'>상단메뉴</span>";
            }
            else
            {
                s = "<span class='badge text-right'>서브메뉴</span>";
            }

            return s;
        }

        protected void btnUpdateMenu_Click(object sender, EventArgs e)
        {
            List<MenuModel> lst = new List<MenuModel>();

            string[] txtMenuIds = Request.Form.GetValues("txtMenuId");
            string[] txtMenuNames = Request.Form.GetValues("txtMenuName");
            string[] txtMenuPaths = Request.Form.GetValues("txtMenuPath");
            string[] hdnIsVisibles = Request.Form.GetValues("hdnIsVisible");
            string[] txtMenuOrders = Request.Form.GetValues("txtMenuOrder");

            string[] hdnIsBoards = Request.Form.GetValues("hdnIsBoard");
            string[] txtBoardAlias = Request.Form.GetValues("txtBoardAlias");

            string[] lstTarget = Request.Form.GetValues("lstTarget");


            for (int i = 0; i < txtMenuIds.Length; i++)
            {
                MenuModel mm = new MenuModel();
                mm.MenuId = Convert.ToInt32(txtMenuIds[i]);
                mm.MenuName = txtMenuNames[i];
                mm.MenuPath = txtMenuPaths[i];

                // 표시 여부
                if (hdnIsVisibles != null)
                {
                    if (hdnIsVisibles[i] != null)
                    {
                        mm.IsVisible = Convert.ToBoolean(hdnIsVisibles[i]);
                    }
                }
                // 게시판 여부
                if (hdnIsBoards != null)
                {
                    if (hdnIsBoards[i] != null)
                    {
                        mm.IsBoard = Convert.ToBoolean(hdnIsBoards[i]);
                    }
                }
                // 게시판 별칭
                if (txtBoardAlias != null)
                {
                    if (txtBoardAlias[i] != null)
                    {
                        mm.BoardAlias = txtBoardAlias[i];
                    }
                }
                // 타겟
                if (lstTarget != null)
                {
                    if (lstTarget[i] != null)
                    {
                        mm.Target = lstTarget[i];
                    }
                }

                mm.MenuOrder = Convert.ToInt32(txtMenuOrders[i]);

                lst.Add(mm);
            }

            MenuRepository repository = new MenuRepository();
            repository.UpdateModel(lst);

            // 다시 데이터 표시
            // DisplayData();
            // 현재 페이지 다시 로드
            Response.Redirect(Request.RawUrl);
        }

        /// <summary>
        /// 체크박스로 선택된 메뉴 항목 삭제
        /// </summary>
        protected void btnDeleteMenuItem_Click(object sender, EventArgs e)
        {
            var repository = new MenuRepository();

            // 체크박스로 선택된 MenuId를 배열로 받기
            string[] arrMenuIds = Request.Form.GetValues("chkSelect");

            // 널값 체크
            if (arrMenuIds != null)
            {
                for (int i = 0; i < arrMenuIds.Length; i++)
                {
                    // 삭제 처리
                    repository.Remove(Convert.ToInt32(arrMenuIds[i]));
                }

                // 현재 페이지 다시 로드
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void ddlTopOrSub_SelectedIndexChanged(
            object sender, EventArgs e)
        {            
            if (ddlTopOrSub.SelectedValue == "0")
            {
                // 상단메뉴만 등록
                ddlParentMenu.Items.Clear(); 
                ddlParentMenu.Items.Add(new ListItem("상단메뉴", "0"));
                ddlParentMenu.Enabled = false;
            }
            else
            {
                ddlParentMenu.Enabled = true;

                // DB에서 ParentId가 0인 자료 바인딩
                var repository = new MenuRepository();

                ddlParentMenu.DataTextField = "MenuName";
                ddlParentMenu.DataValueField = "MenuId";
                ddlParentMenu.DataSource = 
                    repository.GetMenusByParentId(0,
                        Convert.ToInt32(ddlCommunity.SelectedValue));
                ddlParentMenu.DataBind();

                if (ddlParentMenu.Items.Count == 0)
                {
                    // 상단메뉴만 등록
                    ddlParentMenu.Items.Add(new ListItem("상단메뉴", "0"));
                    ddlParentMenu.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 게시판 여부 체크박스 체크
        /// </summary>
        protected void chkIsBoard_CheckedChanged(
            object sender, EventArgs e)
        {
            if (chkIsBoard.Checked)
            {
                ddlBoardList.Enabled = true;

                BindBoardList();
            }
            else
            {
                ddlBoardList.Enabled = false;
                txtMenuName.Text = txtMenuPath.Text = "";
            }
        }

        private void BindBoardList()
        {
            // 수작업으로 게시판 정보 바인딩
            ddlBoardList.Items.Add(new ListItem("공지사항", "Notice"));
            ddlBoardList.Items.Add(new ListItem("자유게시판", "Free"));
            ddlBoardList.Items.Add(new ListItem("앨범", "Photo"));
            // 실제로는 DB에서 게시판 정보를 읽어서 바인딩
        }

        /// <summary>
        /// 게시판 리스트 드롭다운리스트 선택시 텍스트박스 2개 채우기
        /// </summary>
        protected void ddlBoardList_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            txtMenuName.Text = ddlBoardList.SelectedItem.Text;
            txtMenuPath.Text = 
                $"/BoardList?BoardName={ddlBoardList.SelectedValue}";

            // 게시판 별칭을 추가
            hdnBoardAlias.Value = ddlBoardList.SelectedValue; 
        }

        protected void btnAddMenu_Click(object sender, EventArgs e)
        {
            var repository = new MenuRepository();

            MenuModel mnu = new MenuModel();
            //mnu.CommunityId = Convert.ToInt32(txtCommunityId.Text);
            mnu.CommunityId = Convert.ToInt32(ddlCommunity.SelectedValue);
            mnu.ParentId    = Convert.ToInt32(ddlParentMenu.SelectedValue);
            mnu.IsBoard     = chkIsBoard.Checked;
            mnu.MenuName    = txtMenuName.Text;
            mnu.MenuPath    = txtMenuPath.Text;
            mnu.Target      = ddlTarget.SelectedValue;
            mnu.IsVisible   = 
                optIsVisible.SelectedValue == "1" ? true : false;

            // ParentId가 0이면 가장 나중에 서브 메뉴이면 해당 메뉴 +1 순서에
            mnu.MenuOrder = 
                repository.UpdateMenuOrder(mnu.ParentId, mnu.CommunityId);

            mnu.BoardAlias = hdnBoardAlias.Value; 

            repository.Add(mnu);

            Response.Redirect(Request.RawUrl); 
        }

        protected void ddlCommunity_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            string strCommunityId = ddlCommunity.SelectedValue;

            if (strCommunityId != "0")
            {
                string url =
                Request.ServerVariables["SCRIPT_NAME"] +
                    "?CommunityId=" + strCommunityId;
                Response.Redirect(url);
            }
            else
            {
                Response.Redirect(Request.ServerVariables["SCRIPT_NAME"]);
            }
        }

        protected string FuncTargetCombo(string target)
        {
            string s = 
                "<select name='lstTarget' class='form-control'>";

            if (target == "_self")
            {
                s += "<option value='_self' selected>현재창</option>";
                s += "<option value='_blank'>새 창</option>";
            }
            else
            {
                s += "<option value='_self'>현재창</option>";
                s += "<option value='_blank' selected>새 창</option>";
            }

            s += "</select>";

            return s; 
        }
    }
}
