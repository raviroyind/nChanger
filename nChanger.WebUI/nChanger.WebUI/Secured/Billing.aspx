<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Billing.aspx.cs" Inherits="nChanger.WebUI.Secured.Billing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui grid">
  <div class="four column row">
    <div class="left floated column"></div>
    <div class="right floated column"></div>
  </div>
  <div class="row">
    <div class="three wide column"></div>
    <div class="eight wide column">
        <i class="money icon massive green"></i>
        <h3>Placeholder for Payment Gateway</h3>
        <a id="ancPayment" runat="server" class="ui button massive green" href="GeneralQuestions.aspx">Proceed to Pay $39.00</a>
       <p></p>
         <p>
            <i class="stripe icon extra large orange"></i>
              <i class="paypal icon extra large blue"></i>
             <i class="visa icon extra large green"></i>
             <i class="mastercard icon extra large red"></i>
        </p>
    </div>
    <div class="five wide column"></div>
  </div>
</div>
</asp:Content>
