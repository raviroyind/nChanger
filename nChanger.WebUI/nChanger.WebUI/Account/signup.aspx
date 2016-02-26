<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="nChanger.WebUI.Account.Signup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="UpdatePanel1"
        runat="server">
        <ProgressTemplate>
            <div id="spinner" class="divspinner">
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
      <asp:UpdatePanel runat="server" ID="UpdatePanel1">
          <Triggers>
              <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click"/>
          </Triggers>
        <ContentTemplate>
            <div class="ui centered">
                <div class="ui large form container">
                    <div class="ui stacked segment frmPad">
                        <div class="ui form">
                             
                            <asp:ValidationSummary runat="server" DisplayMode="BulletList" ShowMessageBox="False" ValidationGroup="reg" CssClass="frmErrors" ShowSummary="True" />
                             <asp:Label runat="server" ID="lblMsg" Style="color: maroon; font-weight: bold;"></asp:Label>
                            <h4 class="ui dividing header orange"><i class="signup icon"></i>Signup</h4>
                            <div class="field">
                                <label>Name</label>
                                <div class="three fields">
                                    <div class="field">
                                        <asp:TextBox runat="server" ID="txtFirstName" placeholder="First Name" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ForeColor="White" CssClass="ui field error" ValidationGroup="reg"
                                            ControlToValidate="txtFirstName" ErrorMessage="Please enter first name" Text="!"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtFirstName" ForeColor="White" Text="!" ValidationGroup="reg"
                                            ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{3,50}$" runat="server"
                                            ErrorMessage="First Name shoule be between 3 to 50 characters."></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="field">
                                        <asp:TextBox runat="server" ID="txtMiddleName" placeholder="Middle Name" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="field">
                                        <asp:TextBox runat="server" ID="txtLastName" placeholder="Last Name" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ForeColor="White" CssClass="ui field error" ValidationGroup="reg"
                                            ControlToValidate="txtLastName" ErrorMessage="Please enter last name" Text="!"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator Display="Dynamic" ForeColor="White" ControlToValidate="txtLastName" ValidationGroup="reg"
                                            ID="RegularExpressionValidator1" ValidationExpression="^[\s\S]{3,50}$" Text="!" runat="server"
                                            ErrorMessage="Last Name shoule be between 3 to 50 characters."></asp:RegularExpressionValidator>
                                    </div>
                                </div>

                            </div>
                            <div class="two fields">
                                <div class="field">
                                    <label>Email</label>
                                    <div class="field">
                                        <asp:TextBox runat="server" ID="txtEmailId" placeholder="Email" MaxLength="150"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ForeColor="White" CssClass="ui field error" ValidationGroup="reg" ControlToValidate="txtEmailId" ErrorMessage="Please enter email-id" Text="!"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="regExEmail" runat="server" ForeColor="#ffffff" ValidationGroup="reg" ControlToValidate="txtEmailId" ErrorMessage="Please enter valid email-id." Text="!"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="field">
                                    <label>Username</label>
                                    <div class="field">
                                        <asp:TextBox runat="server" ID="txtUserId" placeholder="Username" MaxLength="20"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ForeColor="White" CssClass="ui field error" ValidationGroup="reg" ControlToValidate="txtUserId" ErrorMessage="Please enter userid." Text="!"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtUserId" Text="!" ValidationGroup="reg"
                                            ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{6,12}$" runat="server"
                                            ErrorMessage="Username shoule be between 6 to 12 characters."></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="two fields">
                                <div class="field">
                                    <label>Password</label>
                                    <div class="field">
                                        <asp:TextBox runat="server" ID="txtPassword" placeholder="Password" TextMode="Password" MaxLength="8"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="reg" ForeColor="#ffffff" ControlToValidate="txtPassword" runat="server" Text="!" ErrorMessage="Password is required.">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtPassword" ValidationGroup="reg"
                                            ID="RegularExpressionValidator4" ValidationExpression="^[\s\S]{6,12}$" runat="server" Text="!"
                                            ErrorMessage="Password shoule be between 6 to 12 characters."></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="field">
                                    <label>Confirm</label>
                                    <div class="field">
                                        <asp:TextBox runat="server" ID="txtConfirm" placeholder="Confirm" TextMode="Password" MaxLength="8"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="reg" ForeColor="#ffffff" ControlToValidate="txtConfirm" runat="server" Text="!" ErrorMessage="Please confirm password.">
                                        </asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="compVal" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirm"
                                            ForeColor="#ffffff" ValidationGroup="reg" ErrorMessage="Password confirmation do not match." Text="!">
                                        </asp:CompareValidator>
                                    </div>
                                </div>
                            </div>
                            <h4 class="ui dividing header">Address</h4>

                            <div class="two fields">
                                <div class="field">
                                    <label>Line 1</label>
                                    <div class="field">
                                        <asp:TextBox runat="server" ID="txtAddressLine1" placeholder="Address" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="reg" ForeColor="#ffffff" ControlToValidate="txtAddressLine1" runat="server" Text="!" ErrorMessage="Address Line 1 is required.">
                                        </asp:RequiredFieldValidator>
                                         
                                    </div>
                                </div>
                                <div class="field">
                                    <label>Line 2</label>
                                    <div class="field">
                                        <asp:TextBox runat="server" ID="txtAddressLine2" placeholder="Address" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="two fields">
                                <div class="field">
                                    <label>City</label>
                                    <div class="field">
                                        <asp:TextBox runat="server" ID="txtCity" placeholder="City" MaxLength="90"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="reg" ForeColor="#ffffff" ControlToValidate="txtCity" runat="server" Text="!" ErrorMessage="City is required.">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="field">
                                    <label>Country</label>
                                    <div class="field">
                                        <asp:DropDownList runat="server" ID="ddlCountry" AutoPostBack="True" CssClass="ui normal selection dropdown"  CausesValidation="False" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="reg" ForeColor="#ffffff" ControlToValidate="ddlCountry" InitialValue="SEL" runat="server" Text="!" ErrorMessage="Please select a country">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="three fields">
                                <div class="field">
                                    <label>State</label>
                                    <div class="field">
                                        <div id="divDropState" runat="server">
                                            <asp:DropDownList runat="server" ID="ddlState" CssClass="ui normal selection dropdown" />
                                            <asp:CustomValidator runat="server" ID="customValStateddl" ValidationGroup="reg" ForeColor="White" Text="!"
                                                ControlToValidate="ddlState" ErrorMessage="Please select State" ClientValidationFunction="valStateDropdown"
                                                ></asp:CustomValidator>
                                        </div>
                                        <div id="divStateText" runat="server" style="display: none;">
                                            <asp:TextBox runat="server" ID="txtState" placeholder="State" MaxLength="20"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="field">
                                    <label>Phone</label>
                                    <div class="field">
                                        <asp:TextBox runat="server" ID="txtPhone" placeholder="Phone" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="field">
                                    <label>Zip</label>
                                    <div class="field">
                                        <asp:TextBox runat="server" ID="txtZipCode" placeholder="ZIP Code" MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rqrdcontactZipCode" ValidationGroup="reg" Display="Dynamic"
                                            ForeColor="White" ControlToValidate="txtZipCode" Text="!" ErrorMessage="Zip Code is required">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator
                                            ID="RequiredFieldValidatorZip"
                                            ControlToValidate="txtZipCode"
                                            ValidationExpression="^(\d{5}|\d{5}\-\d{4})$"
                                            ErrorMessage="Zip code must be numeric nnnnn or nnnnn-nnnn."
                                            Display="dynamic" Text="!" ForeColor="White" ValidationGroup="reg"
                                            runat="server"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="six fields">
                                <div class="field">
                                </div>
                                <div class="field">
                                </div>
                                <div class="field">
                                    <asp:LinkButton runat="server" CausesValidation="False" CssClass="ui button blue animated fade fluid" TabIndex="1"
                                        OnClientClick="document.forms[0].reset(), CleanForm();">Reset
                                    </asp:LinkButton>
                                </div>
                                <div class="field">
                                    <asp:LinkButton ID="btnSubmit" runat="server" ValidationGroup="reg" CausesValidation="True" CssClass="ui button blue animated fade fluid" TabIndex="0"
                                         OnClick="btnSubmit_Click">Register
                                    </asp:LinkButton>
                                </div>
                                <div class="field">
                                </div>

                                <div class="field">
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            </ContentTemplate>
    </asp:UpdatePanel>
        
    <script type="text/javascript" src="../Scripts/semantic.min.js"></script>
    <script type="text/javascript">
        $('.ui.normal.selection.dropdown').dropdown();

        function CleanForm() {
            Page_ClientValidate('');
            var i = 0;
            for (; i < Page_Validators.length; i++) {
                $("#" + Page_Validators[i].controltovalidate)
                    .css("background-color", "#ffffff").css("color", "#000000").css("border-color", "");

            }
            $("select").each(function () { this.selectedIndex = 0 });

            return false;

        }

        function BtnClick() {

            var val = Page_ClientValidate();
            if (!val) {
                var i = 0;
                for (; i < Page_Validators.length; i++) {
                    if (!Page_Validators[i].isvalid) {
                        $("#" + Page_Validators[i].controltovalidate)
                            .css("background-color", "#fff6f6").css("color", "#9f3a38").css("border-color", "#e0b4b4");
                    } else {
                        $("#" + Page_Validators[i].controltovalidate)
                            .css("background-color", "#ffffff").css("color", "#00000").css("border-color", "");
                    }
                }
            }
            return val;
        }

        function valStateDropdown(src, args) {
            var country = document.getElementById('<%= ddlCountry.ClientID %>');
            var selectedCountry = country.options[country.selectedIndex].value;

            if (selectedCountry == "US" || selectedCountry == "CA") {
                var state = document.getElementById('<%= ddlState.ClientID %>');
                var selectedState = state.options[state.selectedIndex].value;

                if (selectedState == "SEL")
                    args.isValid = false;
            }
        }
         
    </script>

</asp:Content>
