<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmFinancialInformation.aspx.cs" Inherits="nChanger.WebUI.Forms.FrmFinancialInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="panel panel-info" style="margin-bottom: 5%;">
            <div class="panel-heading">
                <strong>E. Financial Information.</strong>
            </div>
            <div class="panel-body">
                <div id="divMsg" class="alert" style="color: #dc143c;" runat="server"></div>
                <div class="form-horizontal">
                    <div class="row">
                        <div class="col-md-3">
                            <label>Has any court or tribunal ordered you to pay money that you have not yet paid??</label>
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="rdLstCourtOrTribunalOrder" runat="server" RepeatLayout="flow" RepeatDirection="Horizontal"
                                CellSpacing="10" CellPadding="40"
                                data-toggle="buttons">
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-2">
                            <label>Court file number</label>
                        </div>
                        <div class="col-md-2  col-md-pull-1">
                            <asp:TextBox runat="server" ID="txtCourtFileNumber" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Date of Court Order </label>
                        </div>
                        <div class="col-md-1 col-md-pull-1">
                            <asp:TextBox runat="server" ID="txtDateCourtOrder" Width="150" CssClass="form-control date-input"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <label>Court name</label>
                        </div>
                        <div class="col-md-2  col-md-pull-1">
                            <asp:TextBox runat="server" ID="txtNameOfCourt" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Person who sued you</label>
                        </div>
                        <div class="col-md-2  col-md-pull-1">
                            <asp:TextBox runat="server" ID="txtNameOfPersonWhoSuedYou" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Court Address</label>
                        </div>
                        <div class="col-md-2 col-md-pull-1">
                            <asp:TextBox runat="server" ID="txtAddressCourtTribunal" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label>Are you aware if a sheriff has been directed to take your real and personal property to enforce an outstanding judgement?</label>
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="rdLstSheriffDirected" runat="server" RepeatLayout="flow" RepeatDirection="Horizontal"
                                CellSpacing="10" CellPadding="40"
                                data-toggle="buttons">
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-2">
                            <label>Writ Number</label>
                        </div>
                        <div class="col-md-2  col-md-pull-1">
                            <asp:TextBox runat="server" ID="txtWritNumber" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <label>Name Of Sheriff</label>
                        </div>
                        <div class="col-md-2  col-md-pull-1">
                            <asp:TextBox runat="server" ID="txtNameOfSherrif" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Address Of Sheriff</label>
                        </div>
                        <div class="col-md-2  col-md-pull-1">
                            <asp:TextBox runat="server" ID="txtAddressOfSheriff" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2 col-md-pull-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label>Are you aware of any liens or security intrests against your personal property?</label>
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="rdLstLiensOrSecurityInterests" runat="server" RepeatLayout="flow" RepeatDirection="Horizontal"
                                CellSpacing="10" CellPadding="40"
                                data-toggle="buttons">
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-3">
                            <label>Name of the person who has the lien or security interest</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtLiensOrSecurityInterestsNameOfPerson" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <label>How much money do you owe?</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtAmountOfMoneyOwed" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Regitration Number</label>
                        </div>
                        <div class="col-md-2  col-md-pull-1">
                            <asp:TextBox runat="server" ID="txtRegitrationNumber" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2 col-md-pull-1">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label>Are you aware of any financial statements registered under the Personal Property Act that name you as debtor?</label>
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="rdLstFinancialStatementsRegistered" runat="server" RepeatLayout="flow" RepeatDirection="Horizontal"
                                CellSpacing="10" CellPadding="40"
                                data-toggle="buttons">
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-2">
                            <label>Regitration Number</label>
                        </div>
                        <div class="col-md-2 col-md-pull-1">
                            <asp:TextBox runat="server" ID="txtFinancialStatementsRegitrationNumber" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <label>Are you an undischarged bankrupt?</label>
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="rdLstUndischargedBankrupt" runat="server" RepeatLayout="flow" RepeatDirection="Horizontal"
                                CellSpacing="10" CellPadding="40"
                                data-toggle="buttons">
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-2 col-md-pull-1">
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <label>Details of bankruptcy</label>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDetailsOfBankruptcy" CssClass="form-control text-justify" Rows="5" Columns="60"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-5">
                        </div>
                        <div class="col-md-4">
                            <asp:HyperLink runat="server" ID="hypBack" class="btn btn-sm btn-primary" NavigateUrl="../Forms/frmCriminalOffenceInformation.aspx"><i class="glyphicon glyphicon-arrow-left"></i> Back</asp:HyperLink>
                            <asp:LinkButton runat="server" ID="btnSubmit" CausesValidation="True" class="btn btn-sm btn-primary" OnClick="btnSubmit_OnClick"><i class="glyphicon glyphicon-log-in"></i> Save &amp; Continue</asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnPreviewPdf" class="btn btn-sm btn-primary" OnClick="btnPreviewPdf_OnClick"><i class="glyphicon glyphicon-print"></i> Preview Pdf</asp:LinkButton>
                        </div>
                        <div class="col-md-3">
                        </div>
                    </div>
                </div>
        </div>
    </div>
</div>
</asp:Content>

