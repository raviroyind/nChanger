<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="selectPackage.aspx.cs" Inherits="nChanger.WebUI.selectPackage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js" type="text/javascript"></script>
      <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" rel="stylesheet" />
      <link href="assets/package.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
      <div class="container">
          <div class="row">
              <h1 class="ui info-text massive green" ><i class="ui list icon large orange"></i> Select your package </h1>
          </div>
            <div class="row" id="rowPackage" runat="server">
                
            </div>
        </div>
     
     
</asp:Content>
