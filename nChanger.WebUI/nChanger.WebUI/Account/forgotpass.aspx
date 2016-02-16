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
                         <asp:Label ID="lblMsg" runat="server"  CssClass="lblAlert"></asp:Label>
                        <div class="ui large form container row">
                            <div class="ui stacked segment">
                                <label class="ui label">Email or Username</label>
                                <div class="field">
                                    <div class="ui left icon input">
                                        <i class="user icon"></i>
                                         <asp:TextBox runat="server" ID="txtRecover" placeholder="E-mail address/ Username" MaxLength="150"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ForeColor="White" CssClass="ui field error" ValidationGroup="reg" ControlToValidate="txtRecover" ErrorMessage="Please enter email-id" Text="!"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:LinkButton ID="btnSubmit" runat="server" ValidationGroup="reg" 
                                    CausesValidation="True" CssClass="ui fluid large blue animated fade  button" 
                                    TabIndex="0" OnClick="btnSubmit_Click">
                                <i class="lightning icon"></i><div class="visible content">Submit</div>
                                <div class="hidden content"><i class="arrow right icon"></i></div>
                                </asp:LinkButton>
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
