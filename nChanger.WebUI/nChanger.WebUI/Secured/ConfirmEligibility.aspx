<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ConfirmEligibility.aspx.cs" Inherits="nChanger.WebUI.Secured.ConfirmEligibility" %>
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
                 <div class="field">
                     <asp:Label runat="server" CssClass="ui header blue" ID="lblCriteriaHeading"></asp:Label>
                 </div>
            </div>
            <div class="one wide column"></div>

            <div class="one wide column"></div>
            <div class="fourteen wide column">
               
                <h3 class="ui dividing header blue"></h3> 
                <div id="divCriteria" runat="server" class="ui raised stack">
                    <h3 class="ui header green">No Criteria Defined!</h3>
                </div>
            </div>
            <div class="one wide column"></div>
            
            
            <div class="one wide column"></div>
            <div class="fourteen wide column center aligned">
                 <h3 class="ui dividing header blue"></h3> 
                <input type="checkbox"  value="Accept"  onClick="enableSubmit(this)">
                                        <label>Agree</label>
                <asp:LinkButton runat="server" ID="btnSubmit" OnClick="btnSubmit_OnClick" CssClass="ui center floated button green disabled">
                    <i class="check icon large"></i>
                    Confirm Eligibility
                </asp:LinkButton>
            </div>
           <div class="one wide column"></div>

        </div>
    </div>
    <script type="text/javascript" src="../Scripts/semantic.min.js"></script>
    <script type="text/javascript">
        enableSubmit = function (val) {
            var sbmt = document.getElementById("MainContent_btnSubmit");
           
            if (val.checked == true) {
                sbmt.className = "ui right labeled icon button green";
            }
            else {
                sbmt.className = "ui right labeled icon button green disabled";
            }
        }
        
    </script>
</asp:Content>
