<%@ Page Title="" Language="C#"  MasterPageFile="../Forms.Master"  AutoEventWireup="true" CodeBehind="frmPersonalInformation.aspx.cs" Inherits="nChanger.WebUI.Forms.FrmOnPersonalInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="../Scripts/jquery-2.2.0.js"></script>
<link href="../Style/css/bootstrap.min.css" rel="stylesheet" />
<link href="../Style/css/bootstrap-theme.min.css" rel="stylesheet" />
<script src="../Style/js/bootstrap.min.js"></script>  
    <link href="https://code.jquery.com/ui/1.12.0-beta.1/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $(".date-input").datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="container fluid" style="width: 100%; margin-top: 3%;">
        <div class="panel panel-info" style="margin-bottom: 5%;">
            <div class="panel-heading">
              <strong>A. Information about you.</strong>
            </div>
             <div class="panel-body"  style="width: 100%;">
              <div id="divMsg" class="alert" style="color: #dc143c;" runat="server"></div>
               <div class="container"  style="width: 100%;">
                    
                    <div class="row">
                        <div class="col-md-1">
                            <label class="control-label">First Name</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtPresentFirstName" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label">Middle Name</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtPresentMiddleName" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label">Last Name</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtPresentLastName" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-1">
                            <label class="control-label">Sex</label>
                        </div>
                        <div class="col-md-3">
                            <asp:RadioButtonList ID="rdListSex" runat="server" RepeatLayout="flow" RepeatDirection="Horizontal"
                                CellSpacing="10" CellPadding="40"
                                data-toggle="buttons">
                                <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label">Street Name/ No.</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtMailAddStreetNo" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label">PO-Box/RR</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtMailAddPOBox" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-1">
                            <label class="control-label">Apt/Unit/Suit No.</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtMailAddAptSuitNo" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label">Buzzer No.</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtMailAddBuzzerNo" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label">City/Town/Village</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtMailAddCityTownVillage" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-1">
                            <label class="control-label">Province</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtMailAddProvience" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label">Postal Code</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtMailAddPostalCode" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label">Home Phone</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtMailAddHomePhoneCode" placeholder="Code" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-1">
                            <label class="control-label">Home Phone</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtMailAddHomePhoneNo" placeholder="Number" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label">Work Phone</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtMailAddWorkPhoneCode" placeholder="Code" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label text-nowrap">Work Phone</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtMailAddWorkPhoneNo" placeholder="Number" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-1">
                            <label class="control-label">How long have you lived in Ontario</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtLivedInOntarioYears" placeholder="Years" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label">Or Months</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtLivedInOntarioMonths" placeholder="Months" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label">
                                Living in Ontario<br>
                                for 12 months?</label>

                        </div>
                        <div class="col-md-3">
                            <asp:RadioButtonList ID="rdListLivedInOntarioPast12Months" runat="server" RepeatLayout="flow" RepeatDirection="Horizontal"
                                CellSpacing="10" CellPadding="10"
                                data-toggle="buttons">
                                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-1">
                            <label class="control-label">Date Of Birth</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtDOB" Width="150" CssClass="form-control date-input"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label">Birth/City/Town/Village </label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtBirthCityTownVillage" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label text-nowrap">Birth Province Or State </label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtBirthProvinceOrState" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-1">
                            <label class="control-label">Birth Country</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtBirthCountry" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-1">
                            <div class="col-md-3">
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-1">
                            <label class="control-label">New First Name</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtNewFirstName" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label">New Middle Name</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtNewMiddleName" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label text-nowrap">New Last Name</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtNewLastName" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-1">
                            <label class="control-label">Are you married?</label>
                        </div>
                        <div class="col-md-3">
                            <asp:RadioButtonList ID="rdListMarried" runat="server" RepeatLayout="flow" RepeatDirection="Horizontal"
                                CellSpacing="10" CellPadding="10"
                                data-toggle="buttons">
                                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="col-md-1">
                        </div>
                        <div class="col-md-3">
                        </div>

                        <div class="col-md-1">
                        </div>
                        <div class="col-md-3">
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-12">
                            <label class="control-label">What was your spouse/partner's name before you got married? (Fill below)</label>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-1">
                            <label class="control-label">First Name</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtPartnerFisrtName" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label">Middle Name</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtPartnerMiddleName" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label text-nowrap">Last Name</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtPartnerLastName" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>



                    <div class="row">
                        <div class="col-md-1">
                            <label class="control-label">When did you get married?</label>
                        </div>
                        <div class="col-md-11">
                            <asp:TextBox runat="server" ID="txtDateMarried" Width="150" CssClass="form-control  date-input"></asp:TextBox>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-12">
                            <label class="control-label">Where did you get married?</label>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-1">
                            <label class="control-label">City/Town/Village</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtCityTownMarried" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label">Province or State</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtStateOrProvinceMarried" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label text-nowrap">Country</label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList runat="server" ID="ddlCountryMarried" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <label class="control-label">Have you ever signed a document called a Joint Declaration of Conjugal Relationship & sent it to the Office of the Registrar General?</label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <asp:RadioButtonList ID="rdListJDeclarationSigned" runat="server" RepeatLayout="flow" RepeatDirection="Horizontal"
                                CellSpacing="10" CellPadding="10"
                                data-toggle="buttons">
                                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <label class="control-label">
                                What is the name of the other person who signed the Joint Declaration of Conjugal Relationship with you?
                            </label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-1">
                            <label class="control-label">First Name</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtJDeclarationPersonFirstName" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label">Middle Name</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtJDeclarationPersonMiddleName" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <label class="control-label text-nowrap">Last Name</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtJDeclarationPersonLastName" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-3">
                            <label class="control-label">
                                When did you send it to the Office of the Registrar General?
                            </label>
                        </div>
                        <div class="col-md-7">
                            <asp:TextBox runat="server" ID="txtSentRegistrarDate" Width="150" CssClass="form-control  date-input"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-3">
                            <label class="control-label">
                                Have you officially cancelled the declaration by submitting Form 4 of the Change of Name Act?
                            </label>
                        </div>
                        <div class="col-md-7">
                            <asp:RadioButtonList ID="rdListSubmittedForm4" runat="server" RepeatLayout="flow" RepeatDirection="Horizontal"
                                CellSpacing="10" CellPadding="10"
                                data-toggle="buttons">
                                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-2">
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-5">
                        </div>
                        <div class="col-md-3">
                            <asp:HyperLink runat="server" ID="hypBack" class="btn btn-sm btn-primary" NavigateUrl="../Secured/Home.aspx"><i class="glyphicon glyphicon-arrow-left"></i> Back</asp:HyperLink>
                            <asp:LinkButton runat="server" ID="btnSubmit" CausesValidation="True" class="btn btn-sm btn-primary" OnClick="btnSubmit_OnClick"><i class="glyphicon glyphicon-log-in"></i> Save &amp; Continue</asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnPreviewPdf" class="btn btn-sm btn-primary" OnClick="btnPreviewPdf_OnClick"><i class="glyphicon glyphicon-print"></i> Preview Pdf</asp:LinkButton>
                        </div>
                        <div class="col-md-4">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
