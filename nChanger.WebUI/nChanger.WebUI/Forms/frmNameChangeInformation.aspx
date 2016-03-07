<%@ Page Title="" Language="C#" MasterPageFile="../Forms.Master"  AutoEventWireup="true" CodeBehind="frmNameChangeInformation.aspx.cs" Inherits="nChanger.WebUI.Forms.FrmNameChangeInformation" %>
 
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
   <%-- <div class="one wide column"></div>
    <div class="fourteen wide column">
        <div class="ui small breadcrumb">
            <a href="../Secured/dashboard.aspx" class="active section"></a>
           
            <a href="#" class="section">Forms</a>
            <i class="right chevron icon divider"></i>
            <div class="active section">
            </div>
        </div>
    </div>
    <div class="one wide column"></div>--%>
     
    <div class="container fluid" style="width: 100%; margin-top: 2%;">
        <div class="panel panel-info" style="margin-bottom: 5%;">
            <div class="panel-heading">
                <strong>C. Information about your name change.</strong>
            </div>
            <div class="panel-body"  style="width: 100%;">
                <div id="divMsg" class="alert" style="color: #dc143c;" runat="server"></div>
                  <div class="container"  style="width: 100%;">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default" style="margin-bottom: 5%;">
                                <div class="panel-heading">
                                    <strong>Why do you want to change your name? Enter all reasons.</strong>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:TextBox runat="server" ID="txtResonForNameChange" TextMode="MultiLine" CssClass="form-control text-justify" Rows="5" Columns="60"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="col-md-3">
                                            <label>Have you ever changed your name before?</label>
                                        </div>

                                        <div class="col-md-2">
                                            <asp:RadioButtonList ID="rdListChangedNamePriviously" runat="server" RepeatLayout="flow" RepeatDirection="Horizontal"
                                                CellSpacing="10" CellPadding="40"
                                                data-toggle="buttons">
                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-md-2">
                                            <label>When did you change your name.</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox runat="server" ID="txtPreviousNameChangeDate" Width="150" CssClass="form-control date-input"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-1">
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>What was your name before you changed it?</label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-1">
                                            <label class="control-label">First Name</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtPreviousFirstName" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-md-1">
                                            <label class="control-label">Middle Name</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtPreviousMiddleName" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-md-1">
                                            <label class="control-label text-nowrap">Last Name</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtPreviousLastName" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>What was your name after you changed it?</label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-1">
                                            <label class="control-label">First Name</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtFirstNameAfterChange" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-md-1">
                                            <label class="control-label">Middle Name</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtMiddleNameAfterChange" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-md-1">
                                            <label class="control-label text-nowrap">Last Name</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtLastNameAfterChange" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12"><label>Where did you change your name?</label></div>
                                     </div>

                                    <div class="row">
                                        <div class="col-md-1">
                                            <label>Province or State</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtPreviousNameChangeProvince"  CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <label>Country</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" ID="ddlCountry" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    
                                     <div class="row">
                                        <div class="col-md-12"><label>Have you ever applied for a name change before and been refused?</label></div>
                                     </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                                <asp:RadioButtonList ID="rdLstAppliedForChangeAndRefused" runat="server" RepeatLayout="flow" RepeatDirection="Horizontal"
                                                CellSpacing="10" CellPadding="40"
                                                data-toggle="buttons">
                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                     
                    <div class="row">
                        <div class="col-md-5">
                        </div>
                        <div class="col-md-4">
                            <asp:HyperLink runat="server" ID="hypBack" class="btn btn-sm btn-primary" NavigateUrl="../Forms/frmParentInformation.aspx"><i class="glyphicon glyphicon-arrow-left"></i> Back</asp:HyperLink>
                            <asp:LinkButton runat="server" ID="btnSubmit" CausesValidation="True" class="btn btn-sm btn-primary" OnClick="btnSubmit_OnClick"><i class="glyphicon glyphicon-log-in"></i> Save &amp; Continue</asp:LinkButton>
                        </div>
                        <div class="col-md-3">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

