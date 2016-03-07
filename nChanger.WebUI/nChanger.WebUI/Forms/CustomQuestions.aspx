<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CustomQuestions.aspx.cs" Inherits="nChanger.WebUI.Forms.CustomQuestions" %>
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
                <div class="active step">
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
    <h3 class="ui dividing header"></h3>
    <div class="ui centered" style="padding-bottom: 100px;">
        <div class="ui large form container">
            <div class="ui stacked segment frmPad">
                <div class="ui form">
                    <asp:Label runat="server" ID="lblMsg" Style="color: maroon; font-weight: bold;"></asp:Label>
                    <h4 class="ui dividing header orange"><i class="signup icon"></i>General Questions</h4>

                    <table id="tblFields" class="ui table borderless" runat="server">
                    </table>
                     
                    <div class="actions">
                        <asp:LinkButton ID="btnSubmit" runat="server" ValidationGroup="reg" CausesValidation="True" CssClass="ui right floated button blue" TabIndex="0"
                            OnClick="btnSubmit_OnClick">Save &amp; Continue
                        </asp:LinkButton>
                        <h4 class="ui dividing header"></h4>
                    </div>
                     
                </div>
            </div>

        </div>
    </div>
    <script type="text/javascript" src="../Scripts/semantic.min.js"></script>
    <script type="text/javascript">
        $('.ui.fluid.search.selection.dropdown').dropdown();
    </script>
</asp:Content>
