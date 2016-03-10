<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="StripePayment.aspx.cs" Inherits="nChanger.WebUI.Secured.StripePayment" %>
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
                <div class="active step">
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
                    <h3 class="ui dividing header orange">Billing Information</h3>
                    <div class="field">
                        <asp:ValidationSummary runat="server" DisplayMode="BulletList" ShowMessageBox="False"  CssClass="frmErrors" ShowSummary="True" />
                    </div>
                        <div class="fields">
                        <div class="fourteen wide field">
                            <label>Name on Card</label>
                            <asp:TextBox runat="server" ID="txtNameOnCard" placeholder="Name" MaxLength="500"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ForeColor="White" CssClass="ui field error" ValidationGroup="reg"
                                ControlToValidate="txtNameOnCard" ErrorMessage="Please enter a value for Name on Card" Text="!"></asp:RequiredFieldValidator>
                        </div>
                        <div class="two wide field">
                            <label>Amount</label>
                            <asp:Label runat="server" ID="lblAmount"  Style="font-weight: bold;" CssClass="ui header green"></asp:Label>
                        </div>
                    </div>

                    <div class="field">
                        <label>Card Type</label>
                        <asp:DropDownList runat="server" ID="ddlCardType" CssClass="ui selection dropdown">
                            <Items>
                                <asp:ListItem Value="VISA" Text="Visa" />
                                <asp:ListItem Value="MCARD" Text="Mastercard" />
                                <asp:ListItem Value="AMEX" Text="American Express" />
                                <asp:ListItem Value="DISC" Text="Discover" />
                            </Items>
                        </asp:DropDownList>
                    </div>
                    <div class="ui dividing header"></div>
                    <div class="fields">
                        <div class="seven wide field">
                            <label>Card Number</label>
                            <asp:TextBox runat="server" ID="txtCardNumber" MaxLength="16" placeholder="Card #"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ForeColor="White" CssClass="ui field error" 
                                ControlToValidate="txtCardNumber" ErrorMessage="Please enter a value for Card Number." Text="!">
                            </asp:RequiredFieldValidator>
                             
                            <asp:RegularExpressionValidator runat="server" ID="ccVal" 
                                 ValidationExpression="^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\\d{3})\\d{11})$"
                                 ControlToValidate="txtCardNumber" Text="!" ForeColor="White" ErrorMessage="Please enter a valid card number.">
                            </asp:RegularExpressionValidator>
                        </div>
                        <div class="three wide field">
                            <label>CVC</label>
                            <asp:TextBox runat="server" ID="txtCVC" MaxLength="4" placeholder="CVC"></asp:TextBox>
                             <asp:RequiredFieldValidator runat="server" ForeColor="White" CssClass="ui field error" 
                                ControlToValidate="txtCVC" ErrorMessage="Please enter a value for CVC." Text="!">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="six wide field">
                            <label>Expiration</label>
                            <div class="two fields">
                                <div class="field">
                                    <asp:DropDownList runat="server" ID="ddlExpiryMonth" CssClass="ui fluid search dropdown">
                                        <Items>
                                            <asp:ListItem value="Month">Month</asp:ListItem>
                                            <asp:ListItem value="1">January</asp:ListItem>
                                            <asp:ListItem value="2">February</asp:ListItem>
                                            <asp:ListItem value="3">March</asp:ListItem>
                                            <asp:ListItem value="4">April</asp:ListItem>
                                            <asp:ListItem value="5">May</asp:ListItem>
                                            <asp:ListItem value="6">June</asp:ListItem>
                                            <asp:ListItem value="7">July</asp:ListItem>
                                            <asp:ListItem value="8">August</asp:ListItem>
                                            <asp:ListItem value="9">September</asp:ListItem>
                                            <asp:ListItem value="10">October</asp:ListItem>
                                            <asp:ListItem value="11">November</asp:ListItem>
                                            <asp:ListItem value="12">December</asp:ListItem>
                                        </Items>
                                        </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ForeColor="White" CssClass="ui field error" 
                                        ControlToValidate="ddlExpiryMonth" InitialValue="Month" ErrorMessage="Please select Month Expiration." Text="!">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="field">
                                    <asp:TextBox runat="server" ID="txtExpieryYear" MaxLength="4" placeholder="Year"></asp:TextBox>
                                     <asp:RequiredFieldValidator runat="server" ForeColor="White" CssClass="ui field error" 
                                        ControlToValidate="txtExpieryYear" ErrorMessage="Please enter a value Expiery Year." Text="!">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div> 
                    <br/>
                    
                   <div class="ui right aligned">
                        <asp:LinkButton ID="btnSubmit" runat="server"  CausesValidation="True" CssClass="ui right floated button blue" TabIndex="0" OnClick="btnSubmit_Click">
                            Submit
                        </asp:LinkButton>
                    </div>
                    <br/>
                </div>
            </div>
        </div>
    </div>

    <script src="../Scripts/semantic.min.js"></script>
    <script type="text/javascript">
        $('.ui.fluid.search.dropdown').dropdown();
        $('.ui.selection.dropdown').dropdown();
        $('.ui.selection.dropdown.menu').dropdown();
    </script>
</asp:Content>
