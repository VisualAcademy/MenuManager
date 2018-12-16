<%@ Control Language="C#" AutoEventWireup="true"
    CodeBehind="MenuManagerUserControl.ascx.cs"
    Inherits="MenuManager.Web.MenuManagerUserControl" %>

<script>
    //[0] 표시 여부 체크박스 선택 시 히든 필드에 선택 값(True) 기록
    function valueChanged() {
        var chkIsVisibles = document.getElementsByName("chkIsVisible");
        var hdnIsVisibles = document.getElementsByName("hdnIsVisible");

        for (var i = 0; i < chkIsVisibles.length; i++) {
            if (chkIsVisibles[i].checked) {
                hdnIsVisibles[i].value = "True"; // 히든 필드를 True로 
            }
            else {
                hdnIsVisibles[i].value = "False";
            }
        }
    }
    function IsBoardValueChanged() {
        var chkIsBoards = document.getElementsByName("chkIsBoard");
        var hdnIsBoards = document.getElementsByName("hdnIsBoard");

        for (var i = 0; i < chkIsBoards.length; i++) {
            if (chkIsBoards[i].checked) {
                hdnIsBoards[i].value = "True"; // 히든 필드를 True로 
            }
            else {
                hdnIsBoards[i].value = "False";
            }
        }
    }

    //[1] 체크박스 전체 선택 및 전체 해제
    var chkFlag = false; // 체크 해제된 상태
    function CheckAll() {
        // 체크박스 배열 가져오기 
        var arr = document.getElementsByName("chkSelect");

        if (chkFlag == false) {
            for (var i = 0; i < arr.length; i++) {
                arr[i].checked = true; // 체크박스 선택
            }
            chkFlag = true;
            return "전체해제";
        }
        else {
            for (var i = 0; i < arr.length; i++) {
                arr[i].checked = false; // 체크박스 해제
            }
            chkFlag = false;
            return "전체선택";
        }
    }

    //[2] 체크 박스 선택 시 해당 행의 배경색 변경
    $(function () {
        $('input[name="chkSelect"]').on('change', function () {
            $(this)
                .closest('tr')
                    .toggleClass('info',
                        $(this).is(':checked'));
        });
        $("#btnCheckAll").on('click', function () {
            $('input[name="chkSelect"]')
                .closest('tr')
                    .toggleClass('info',
                        $('input[name = "chkSelect"]'
                            ).is(':checked'));
        });
    });

    //[3] 체크박스가 하나라도 선택되어 있는지 확인
    function CheckForm() {
        if (CheckboxCheckValidate()) {
            return confirm("정말로 삭제하시겠습니까?");
        }
        return false;
    }
    function CheckboxCheckValidate() {
        var chkSelect = document.getElementsByName("chkSelect");
        for (var i = 0; i < chkSelect.length; i++) {
            if (chkSelect[i].checked) {
                return true;
            }
        }
        alert("체크박스를 선택하세요.");
        return false;
    }
</script>

<div class="container">
    <div class="row">
        <div class="col-md-6">
            <h2>메뉴 관리자</h2>
            <div class="form-group">
                <label class="col-sm-4 control-label" for="ddlCommunity">
                    커뮤니티: </label>
                <div class="col-sm-8">
                    <asp:DropDownList ID="ddlCommunity" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true" 
                        OnSelectedIndexChanged=
                            "ddlCommunity_SelectedIndexChanged">
                        <asp:ListItem Value="0">-선택-</asp:ListItem>
                        <asp:ListItem Value="1">Once</asp:ListItem>
                        <asp:ListItem Value="2">Twice</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <h3>메뉴 등록</h3>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:DropDownList ID="ddlTopOrSub" runat="server"
                CssClass="form-control" AutoPostBack="true"
                OnSelectedIndexChanged="ddlTopOrSub_SelectedIndexChanged">
                <asp:ListItem Value="0">상단메뉴</asp:ListItem>
                <asp:ListItem Value="1">서브메뉴</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlParentMenu" runat="server"
                CssClass="form-control">
                <asp:ListItem Value="0">상단메뉴</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-2 text-right">
            <label class="checkbox">
                <asp:CheckBox ID="chkIsBoard" runat="server" 
                    Checked="false" 
                    AutoPostBack="true"
                    OnCheckedChanged="chkIsBoard_CheckedChanged" />
                게시판
            </label>
        </div>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlBoardList" runat="server"
                Enabled="false"
                AutoPostBack="true"
                OnSelectedIndexChanged="ddlBoardList_SelectedIndexChanged"
                CssClass="form-control">
                <asp:ListItem Value="0">게시판</asp:ListItem>
            </asp:DropDownList>
            <asp:HiddenField ID="hdnBoardAlias" runat="server" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-2 text-right">
            <label>메뉴 이름(타이틀): </label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtMenuName" runat="server"
                CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-2 text-right">
            <label>메뉴 경로(URL): </label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtMenuPath" runat="server"
                CssClass="form-control"></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <div class="col-md-2">
            <asp:DropDownList ID="ddlTarget" runat="server"
                CssClass="form-control">
                <asp:ListItem Selected="True" Value="_self">
                    현재 창
                </asp:ListItem>
                <asp:ListItem Value="_blank">새 창</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-4">
            <asp:RadioButtonList ID="optIsVisible" runat="server"
                Width="100%"
                RepeatDirection="Horizontal"
                RepeatLayout="Table">
                <asp:ListItem Selected="true" Value="1">
                    메뉴 표시</asp:ListItem>
                <asp:ListItem Value="0">메뉴 숨기기</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div class="col-md-2"></div>
        <div class="col-md-4">
            <asp:Button ID="btnAddMenu" runat="server" Text="메뉴 추가"
                CssClass="btn btn-primary form-control"
                OnClick="btnAddMenu_Click" />
        </div>
    </div>
    
    <hr />

    <div class="row">
        <div class="col-md-6">
            <h3>메뉴 리스트</h3>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <style>
                th {
                    text-align:center;
                }
            </style>
            <asp:GridView ID="ctlMenuLists" runat="server"
                ItemType="MenuManager.Models.MenuModel"
                ShowHeaderWhenEmpty="true"
                AutoGenerateColumns="false"
                HeaderStyle-HorizontalAlign="Center"
                CssClass="table table-bordered table-striped table-hover">
                <EmptyDataTemplate>
                    <div class=
                        "alert alert-warning alert-dismissible text-center" 
                        role="alert">
                        등록된 메뉴가 없습니다.
                    </div>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField
                        HeaderStyle-CssClass="text-center"
                        ItemStyle-CssClass="text-center">
                        <HeaderTemplate>
                            <input 
                                type="button" 
                                id="btnCheckAll" 
                                value="전체선택"
                                class="btn btn-sm btn-default"
                                onclick="this.value = CheckAll();"
                            />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input 
                                type="checkbox" 
                                name="chkSelect"
                                value="<%# Eval("MenuId") %>"
                            />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="text-right"
                        HeaderText="메뉴 레벨">
                        <ItemTemplate>
                            <input type="hidden" name="txtMenuId" 
                                value="<%# Eval("MenuId") %>" />
                            <%# FunStep(Eval("ParentId")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="메뉴 이름(타이틀)">
                        <ItemTemplate>
                            <input type="text" 
                                name="txtMenuName" 
                                value="<%# Eval("MenuName") %>"
                                class="form-control" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="메뉴 경로(URL)"
                        ItemStyle-Width="240px">
                        <ItemTemplate>
                            <input type="text" 
                                name="txtMenuPath" 
                                value="<%# Eval("MenuPath") %>"
                                class="form-control" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="새창" 
                        HeaderStyle-Width="100px">
                        <ItemTemplate>
                            <%# FuncTargetCombo(Item.Target) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="표시"
                        HeaderStyle-CssClass="text-nowrap"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input type="hidden" name="hdnIsVisible" 
                                value="<%# Eval("IsVisible") %>" />
                            <%# FuncIsVisible(Eval("IsVisible")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="순서"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input type="hidden" 
                                name="hdnMenuOrder" 
                                value="<%# Eval("MenuOrder") %>"
                                style="width:70px;"
                                class="form-control" />

                            <input type="text" 
                                name="txtMenuOrder" 
                                value="<%# Container.DataItemIndex + 1 %>"
                                style="width:50px;"
                                class="form-control text-center" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="게시판"
                        HeaderStyle-CssClass="text-nowrap"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input type="hidden" name="hdnIsBoard" 
                                value="<%# Eval("IsBoard") %>" />
                            <%# FuncIsBoard(Eval("IsBoard")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="게시판 별칭"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input type="text" 
                                name="txtBoardAlias" 
                                value="<%# Eval("BoardAlias") %>"
                                style="width:80px;"
                                class="form-control" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <asp:Button ID="btnDeleteMenuItem" runat="server" 
                Text="선택 항목 삭제" 
                CssClass="btn btn-danger btn-sm"
                OnClientClick="return CheckForm();"
                OnClick="btnDeleteMenuItem_Click" />
        </div>
        <div class="col-md-6 text-right">
            <asp:Button id="btnUpdateMenu" runat="server" Text="메뉴 업데이트"
                CssClass="btn btn-danger btn-sm"
                OnClick="btnUpdateMenu_Click" />
        </div>
    </div>       
</div>
