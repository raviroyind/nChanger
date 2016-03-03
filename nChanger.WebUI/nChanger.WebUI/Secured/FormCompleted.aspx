<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="FormCompleted.aspx.cs" Inherits="nChanger.WebUI.Secured.FormCompleted" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
       <script src="../Scripts/jquery-2.2.0.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
       <div style="padding-left: 10%;">
        <div class="one wide column"></div>
        <div class="fourteen wide column bordered round-corners">
            <div class="ui ordered steps">
                <div class="completed step">
                    <div class="content">
                        <div class="title">Select Package</div>
                        <div class="description">Choose a suitable package.</div>
                    </div>
                </div>
                <div class="completed step">
                    <div class="content">
                        <div class="title">Billing</div>
                        <div class="description">Enter billing information.</div>
                    </div>
                </div>
                <div class="completed step">
                    
                    <div class="content">
                        <div class="title">General Questions</div>
                        <div class="description">Answer General Questions.</div>
                    </div>
                </div>
                <div class="completed step">
                   
                    <div class="content">
                        <div class="title">Form Specific Questions</div>
                        <div class="description">Answer Form Specific Questions.</div>
                    </div>
                </div>
                <div class="active step">
                    <i class="download icon green"></i>
                    <div class="content">
                        <div class="title">Review & Download/ Email</div>
                        <div class="description">Review before Download/ Print/ Email.</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="one wide column"></div>
    </div>
    <h3 class="ui dividing header"></h3>
     <div class="ui warning message fluid" style="display: none;" id="success-alert">
        <asp:Label ID="lblMsg" Style="font-size: 1.2em; font-weight: bold;" runat="server"></asp:Label>
    </div>
    <div class="ui centered">
        <div class="ui large form container">
            <div class="ui stacked segment frmPad">
                <div class="ui form">
                    
                    <h4 class="ui dividing header orange"><i class="signup icon"></i>Download/ Email</h4>
                </div>
                <table class="ui celled table">
                        <thead>
                            <tr>
                                <th class="gridHead">File</th>
                                <th class="gridHead">E-mail address</th>
                                <th class="gridHead">Send</th>
                                <th class="gridHead">Download</th>
                            </tr>
                        </thead>
                        <tbody>
                        <tr>
                            <td>
                                <asp:HyperLink runat="server" Target="_blank" ID="hypFile" NavigateUrl="#"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtEmailId" placeholder="Email"></asp:TextBox>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" ID="lnkSend" CssClass="ui button small" OnClick="lnkSend_OnClick">
                                   <i class="mail icon orange"></i> Send
                                </asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" ID="lnkDownload"  CssClass="ui button small" OnClick="lnkDownload_OnClick">
                                    <i class="download icon blue"></i> Download
                                </asp:LinkButton>
                            </td>
                        </tr>
                        </tbody>
                    <tfoot>
                    <tr>
                        <td colspan="4">
                            <asp:HyperLink runat="server" ID="hypBack"  CssClass="ui button green" NavigateUrl="dashboard.aspx">
                                    <i class="arrow left icon"></i> Back
                                </asp:HyperLink>
                        </td>
                    </tr>
                    </tfoot>
                    </table>
                <div class="actions">
                     
                </div>
            </div>

        </div>
    </div>
    <script type="text/javascript">
        function showAlert() {
            $("#success-alert").show().delay(5000).fadeOut();
        }
    </script>
</asp:Content>
