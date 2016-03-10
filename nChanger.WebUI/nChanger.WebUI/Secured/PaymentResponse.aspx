<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="PaymentResponse.aspx.cs" Inherits="nChanger.WebUI.Secured.PaymentResponse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-2.2.0.js"></script>
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
                <div class="completed active step">
                    <i class="payment icon small green"></i>
                    <div class="content">
                        <div class="title">Billing</div>
                        <div class="description">Enter billing information.</div>
                    </div>
                </div>
                <div class="disabled step">
                    <i class="help circle icon small green"></i>
                    <div class="content">
                        <div class="title">General Questions</div>
                        <div class="description">Answer General Questions.</div>
                    </div>
                </div>
                <div class="disabled step">
                    <i class="edit icon small green"></i>
                    <div class="content">
                        <div class="title">Form Specific Questions</div>
                        <div class="description">Answer Form Specific Questions.</div>
                    </div>
                </div>
                <div class="disabled step">
                    <div class="content">
                        <div class="title">Review & Download/ Email</div>
                        <div class="description">Review before Download/ Print/ Email.</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="one wide column"></div>
    </div>
    <div class="ui centered" style="padding-bottom: 100px;">
        <div class="ui large form container">
            <div class="ui stacked segment frmPad">
                <div class="ui form">
                    <h3 class="ui dividing header orange">Payment Information</h3>
                      <div class="field">
                          <div id="divStatus" runat="server">
                              
                          </div>
                      </div>
                    <div class="actions">
                        <asp:HyperLink ID="hypProceed" runat="server" NavigateUrl="../Forms/CustomQuestions.aspx"  CssClass="ui button green" TabIndex="0" Visible="False">
                           <i class="arrow right icon"></i> Continue
                        </asp:HyperLink>
                        
                          <asp:HyperLink ID="hypBack" runat="server" NavigateUrl="Billing.aspx"  CssClass="ui button orange" TabIndex="0" Visible="False">
                           <i class="refresh icon"></i> Retry Payment
                        </asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </asp:Content>
