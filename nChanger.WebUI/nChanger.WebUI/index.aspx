<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="nChanger.WebUI.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui middlediv">
        <div class="loginBox">
            <div class="ui centered">
                <div class="ui large form container row">
                    <div class="ui stacked segment">
                        <asp:Label ID="lblMsg" runat="server" Text="" CssClass="lblAlert"></asp:Label>
                        <asp:ValidationSummary runat="server" DisplayMode="BulletList" ShowMessageBox="False" ValidationGroup="reg" CssClass="frmErrors" ShowSummary="True" />
                        <h4 class="ui dividing header orange"><i class="lock open icon"></i>Secured Access</h4>
                        <div class="field">
                            <div class="ui left icon input">
                                <i class="user icon"></i>
                                <asp:TextBox runat="server" ID="txtEmailId"   placeholder="E-mail address/ Username" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ForeColor="White" CssClass="ui field error" ValidationGroup="reg" ControlToValidate="txtEmailId" ErrorMessage="Please enter email-id" Text="!"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="field">
                            <div class="ui left icon input">
                                <i class="lock icon"></i>
                                <asp:TextBox runat="server" ID="txtPassword" placeholder="Password"  TextMode="Password" MaxLength="8"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="reg" ForeColor="#ffffff" ControlToValidate="txtPassword" runat="server" Text="!" ErrorMessage="Password is required.">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <asp:LinkButton ID="btnSubmit" runat="server" ValidationGroup="reg" CausesValidation="True" CssClass="ui fluid large blue animated fade  button" TabIndex="0"
                            OnClientClick="return BtnClick();" OnClick="btnSubmit_Click">
                        <i class="unlock icon"></i><div class="visible content">Login</div>
                        <div class="hidden content"><i class="arrow right icon"></i></div>
                        </asp:LinkButton>
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
        <script type="text/javascript">
            function BtnClick() {

                var val = window.Page_ClientValidate();
                if (!val) {
                    var i = 0;
                    for (; i < window.Page_Validators.length; i++) {
                        if (!window.Page_Validators[i].isvalid) {
                            $("#" + window.Page_Validators[i].controltovalidate)
                                .css("background-color", "#fff6f6").css("color", "#9f3a38").css("border-color", "#e0b4b4");
                        } else {
                            $("#" + window.Page_Validators[i].controltovalidate)
                                .css("background-color", "#ffffff").css("color", "#00000").css("border-color", "");
                        }
                    }
                }
                return val;
            }
        </script>
    </div>
</asp:Content>
