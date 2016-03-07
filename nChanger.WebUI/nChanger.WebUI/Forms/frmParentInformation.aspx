<%@ Page Title="" Language="C#" MasterPageFile="../Forms.Master" AutoEventWireup="true" CodeBehind="frmParentInformation.aspx.cs" Inherits="nChanger.WebUI.Forms.FrmParentInformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="../Scripts/jquery-2.2.0.js"></script>
<link href="../Style/css/bootstrap.min.css" rel="stylesheet" />
<link href="../Style/css/bootstrap-theme.min.css" rel="stylesheet" />
<script src="../Style/js/bootstrap.min.js"></script>  
    <link href="https://code.jquery.com/ui/1.12.0-beta.1/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="one wide column"></div>
   <%-- <div class="fourteen wide column">
        <div class="ui small breadcrumb">
            <a href="../Secured/dashboard.aspx" class="active section"></a>
            <a href="#" class="section">Forms</a>
            <i class="right chevron icon divider"></i>
            <div class="active section">
            </div>
        </div>
    </div>--%>
    <div class="one wide column"></div>
    <div class="container fluid" style="width: 100%; margin-top: 2%;">
        <div class="panel panel-info" style="margin-bottom: 5%;">
            <div class="panel-heading">
                <strong>B. Information about your Parents.</strong>
            </div>
            <div class="panel-body" style="width: 100%;">

                <div id="divMsg" class="alert" style="color: #dc143c;" runat="server"></div>
                <div class="container" style="width: 100%;">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-default" style="margin-bottom: 5%;">
                                        <div class="panel-heading">
                                            <strong>Details about your Father.</strong>
                                        </div>
                                        <div class="panel-body">

                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <label class="control-label">First Name</label>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:TextBox runat="server" ID="txtFatherFirstName" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <label class="control-label">Middle Name</label>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:TextBox runat="server" ID="txtFatherMiddleName" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <label class="control-label">Last Name</label>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:TextBox runat="server" ID="txtFatherLastName" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <label class="control-label">Last Name (Other)</label>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:TextBox runat="server" ID="txtFatherLastNameOther" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-default" style="margin-bottom: 5%;">
                                        <div class="panel-heading">
                                            <strong>Details about your Mother.</strong>
                                        </div>
                                        <div class="panel-body">

                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <label class="control-label">First Name</label>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:TextBox runat="server" ID="txtMotherFirstName" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <label class="control-label">Middle Name</label>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:TextBox runat="server" ID="txtMotherMiddleName" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <label class="control-label">Last Name (born)</label>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:TextBox runat="server" ID="txtMotherLastNameBorn" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <label class="control-label">Last Name (Present)</label>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:TextBox runat="server" ID="txtMotherLastNamePresent" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <label class="control-label">Last Name (Other)</label>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:TextBox runat="server" ID="txtMotherLastNameOther" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

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
                            <asp:HyperLink runat="server" ID="hypBack" class="btn btn-sm btn-primary" NavigateUrl="../Forms/frmPersonalInformation.aspx"><i class="glyphicon glyphicon-arrow-left"></i> Back</asp:HyperLink>
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
