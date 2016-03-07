<%@ Page Title="" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="EligiblityCriteria.aspx.cs" Inherits="nChanger.WebUI.Admin.EligiblityCriteria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-2.2.0.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="ui warning message fluid" style="display: none;" id="success-alert">
        <asp:Label ID="lblMsg" Style="font-size: 1.2em; font-weight: bold;" runat="server"></asp:Label>
    </div>
    <div class="ui sixteen wide column fluid">
        <div class="ui grid">
            <div class="one wide column"></div>
            <div class="fourteen wide column">
                <div class="ui small breadcrumb">
                    <a href="dashboard.aspx" class="section">Home</a>
                    <i class="right chevron icon divider"></i>
                    <a href="EligiblityCriteria.aspx" class="active section">Eligibility Criteria</a>
                </div>
                <h3 class="ui dividing header orange"></h3>
                <h3 class="ui header orange" id="lblCaption" runat="server"></h3>
            </div>
            <div class="one wide column"></div>
            
            <div class="one wide column"></div>
            <div class="fourteen wide column">
                  <asp:HyperLink runat="server" CssClass="ui right floated button disabled" ID="hypEdit" NavigateUrl="#">
                     
                  </asp:HyperLink>
            </div>
            <div class="one wide column"></div>
            
            <div class="one wide column"></div>
            <div class="fourteen wide column">
                 <div class="field">
                     <label>Criteria  Heading</label>
                     <asp:Label runat="server" CssClass="ui header green" ID="lblCriteriaHeading"></asp:Label>
                 </div>
            </div>
            <div class="one wide column"></div>
            

            <div class="one wide column"></div>
            <div class="fourteen wide column">
                <div id="divCriteria" runat="server">
                    <h3 class="ui header green">No Criteria Defined!</h3>
                </div>
            </div>
            <div class="one wide column"></div>
        </div>
    </div>
    <script type="text/javascript" src="../Scripts/semantic.min.js"></script>

</asp:Content>
