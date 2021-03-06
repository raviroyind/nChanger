﻿<%@ Page Title="" Language="C#" MasterPageFile="../Forms.Master" AutoEventWireup="true" CodeBehind="frmCriminalOffenceInformation.aspx.cs" Inherits="nChanger.WebUI.Forms.frmCriminalOffenceInformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="../Scripts/jquery-2.2.0.js"></script>
<link href="../Style/css/bootstrap.min.css" rel="stylesheet" />
<link href="../Style/css/bootstrap-theme.min.css" rel="stylesheet" />
<script src="../Style/js/bootstrap.min.js"></script>  
    <link href="https://code.jquery.com/ui/1.12.0-beta.1/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<div class="one wide column"></div>
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
                <strong>Information about criminal offences.</strong>
            </div>
            <div class="panel-body"  style="width: 100%;">
                <div id="divMsg" class="alert" style="color: #dc143c;" runat="server"></div>
                <div class="container"  style="width: 100%;">
                    <div class="row">
                        <div class="col-md-6">
                            <label>Are you aware of any outstanding court proceedings against you, other than outstanding criminal charges?</label>
                        </div>
                        <div class="col-md-6 col-md-pull-1">
                            <asp:RadioButtonList ID="rdLstOutstandingCourtProceedings" runat="server" RepeatLayout="flow" RepeatDirection="Horizontal"
                                CellSpacing="10" CellPadding="40"
                                data-toggle="buttons">
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <label>Court file number</label>
                        </div>
                        <div class="col-md-2  col-md-pull-1">
                            <asp:TextBox runat="server" ID="txtCourtFileNumber" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Court name</label>
                        </div>
                        <div class="col-md-2  col-md-pull-1">
                            <asp:TextBox runat="server" ID="txtCourtName" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Address of court</label>
                        </div>
                        <div class="col-md-2 col-md-pull-1">
                            <asp:TextBox runat="server" ID="txtCourtAddress" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <label>Describe proceedings</label>
                        </div>
                        <div class="col-md-4 col-md-pull-1">
                            <asp:TextBox runat="server" ID="txtDescribeProceedings" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label>Are you aware of any outstanding law enforcement orders against you?</label>
                        </div>
                        <div class="col-md-6 col-md-pull-2">
                            <asp:RadioButtonList ID="rdLstOutstandingEnforcementOrders" runat="server" RepeatLayout="flow" RepeatDirection="Horizontal"
                                CellSpacing="10" CellPadding="40"
                                data-toggle="buttons">
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <label>Details of Order or Orders</label>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDetailsOfOutstandingEnforcementOrders" CssClass="form-control text-justify" Rows="5" Columns="60"></asp:TextBox>
                        </div>

                    </div>


                    <div class="row">
                        <div class="col-md-6">
                            <label>Have you ever been convicted of a criminal offence?</label>
                        </div>
                        <div class="col-md-6 col-md-pull-2">
                            <asp:RadioButtonList ID="rdLstEverConvictedOfCriminalOffence" runat="server" RepeatLayout="flow" RepeatDirection="Horizontal"
                                CellSpacing="10" CellPadding="40"
                                data-toggle="buttons">
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-2">
                            <label>Details of Offence or Offences</label>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDetailsOfCriminalOffence" CssClass="form-control text-justify" Rows="5" Columns="60"></asp:TextBox>
                        </div>
                    </div>



                    <div class="row">
                        <div class="col-md-6">
                            <label>Have you ever been found guilty and discharged of a criminal offence?</label>
                        </div>
                        <div class="col-md-6 col-md-pull-2">
                            <asp:RadioButtonList ID="rdLstFoundGuiltyDischarged" runat="server" RepeatLayout="flow" RepeatDirection="Horizontal"
                                CellSpacing="10" CellPadding="40"
                                data-toggle="buttons">
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <label>Details of Offence or Offences</label>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" TextMode="MultiLine" ID="txtFoundGuiltyDetailsOfOffence" CssClass="form-control text-justify" Rows="5" Columns="60"></asp:TextBox>
                        </div>
                    </div>



                    <div class="row">
                        <div class="col-md-8">
                            <label>Have you ever been found guilty and discharged of a criminal offence for which an adult sentence has been imposed under the Youth Criminal Justice Act (Canada)?</label>
                        </div>
                        <div class="col-md-4">
                            <asp:RadioButtonList ID="rdLstAdultSentenceImposed" runat="server" RepeatLayout="flow" RepeatDirection="Horizontal"
                                CellSpacing="10" CellPadding="40"
                                data-toggle="buttons">
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <label>Details of Offence or Offences</label>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDescribeAdultSentence" CssClass="form-control text-justify" Rows="5" Columns="60"></asp:TextBox>
                        </div>
                    </div>



                    <div class="row">
                        <div class="col-md-6">
                            <label>Are you aware of any pending criminal charges against you?</label>
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="rdLstPendingCharges" runat="server" RepeatLayout="flow" RepeatDirection="Horizontal"
                                CellSpacing="10" CellPadding="40"
                                data-toggle="buttons">
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <label>Court file number</label>
                        </div>
                        <div class="col-md-2  col-md-pull-1">
                            <asp:TextBox runat="server" ID="txtPendingChargesCourtNumber" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Court name</label>
                        </div>
                        <div class="col-md-2  col-md-pull-1">
                            <asp:TextBox runat="server" ID="txtPendingChargesCourtName" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Address of court</label>
                        </div>
                        <div class="col-md-2 col-md-pull-1">
                            <asp:TextBox runat="server" ID="txtPendingChargesCourtAddress" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <label>Describe the charges</label>
                        </div>
                        <div class="col-md-4 col-md-pull-1">
                            <asp:TextBox runat="server" ID="txtPendingChargesDescribe" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-5">
                    </div>
                    <div class="col-md-4">
                        <asp:HyperLink runat="server" ID="hypBack" class="btn btn-sm btn-primary" NavigateUrl="../Forms/frmNameChangeInformation.aspx"><i class="glyphicon glyphicon-arrow-left"></i> Back</asp:HyperLink>
                        <asp:LinkButton runat="server" ID="btnSubmit" OnClick="btnSubmit_OnClick" CausesValidation="True" class="btn btn-sm btn-primary"><i class="glyphicon glyphicon-log-in"></i> Save &amp; Continue</asp:LinkButton>
                    </div>
                    <div class="col-md-3">
                    </div>
                </div>
            </div>
        </div>
    </div>
   <%-- <script type="text/javascript" src="../Scripts/semantic.min.js"></script>
    <script type="text/javascript">
        $('.ui.checkbox').checkbox();
    </script>--%>
</asp:Content>
