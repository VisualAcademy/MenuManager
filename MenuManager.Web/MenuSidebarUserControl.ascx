<%@ Control Language="C#" AutoEventWireup="true"
    CodeBehind="MenuSidebarUserControl.ascx.cs"
    Inherits="MenuManager.Web.MenuSidebarUserControl" %>

<div class="container">
    <div class="row">
        <div class="col-md-12">
            <h3>사이드바 테스트</h3>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <div class="panel-group" id="mmuSidebar" role="tablist" 
                aria-multiselectable="true">
                <% foreach (var mi in Model)
                    {
                        if (mi.Menus.Count > 0)
                        {
                %>
                <div class="panel panel-default">
                    <div class="panel-heading" role="tab" 
                        id="mmuSidebar-heading-<%= mi.MenuId %>">
                        <h4 class="panel-title">
                            <a role="button" data-toggle="collapse" 
                                data-parent="#mmuSidebar" 
                                href="#mmuSidebar-<%= mi.MenuId %>" 
                                aria-expanded="true" 
                                aria-controls="mmuSidebar-<%= mi.MenuId %>">
                                <%= mi.MenuName %>
                            </a>
                        </h4>
                    </div>
                    <div id="mmuSidebar-<%= mi.MenuId %>" 
                        class="panel-collapse collapse in" role="tabpanel" 
                        aria-labelledby="mmuSidebar-heading-one">
                        <div class="panel-body">
                            <ul>
                                <% foreach (var smi in mi.Menus)
                                   {
                                %>
                                <li>
                                    <a href="<%= smi.MenuPath %>" 
                                        target="<%= smi.Target %>">
                                        <%= smi.MenuName %>
                                    </a>
                                </li>
                                <%
                                   } 
                                %>
                            </ul>
                        </div>
                    </div>
                </div>
                <%
                        }
                    } 
                %>
            </div>
        </div>
    </div>
</div>
