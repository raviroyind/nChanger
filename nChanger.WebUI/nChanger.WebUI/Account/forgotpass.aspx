<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="forgotpass.aspx.cs" Inherits="nChanger.WebUI.Account.forgotpass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui middlediv">
        <div class="ui centered">
            <div class="ui large form container">
                <div class="ui stacked segment">
                    <div class="ui form">
                        <h4 class="ui dividing header orange"><i class="privacy icon"></i>Password Recovrey</h4>
                        <div class="ui large form container row">
                            <div class="ui stacked segment">
                                <div class="field">
                                    <div class="ui left icon input">
                                        <i class="user icon"></i>
                                        <input type="text" name="email" placeholder="E-mail address">
                                    </div>
                                </div>
                                <div class="field">
                                    <div class="ui left icon input">
                                        <i class="lock icon"></i>
                                        <input type="password" name="password" placeholder="Password">
                                    </div>
                                </div>
                                <div class="ui fluid large blue submit button">Login</div>
                            </div>
                            <div class="ui error message"></div>
                        </div>
                        <div class="striped">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
