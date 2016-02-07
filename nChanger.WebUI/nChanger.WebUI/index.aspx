<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="nChanger.WebUI.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui middlediv">
    <div class="loginBox">
        <div class="ui centered">
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
                <div class="row">
                    <a href="Account/forgotpass.aspx">Forgot Pass?</a>
                </div>
                <div class="ui message">
                    New to us? <a href="Account/signup.aspx">Sign Up</a>
                </div>
                <div class="ui error message"></div>
            </div>
            <div class="striped">
            </div>
        </div>

    </div>
        </div>
</asp:Content>
