<%@ Page Title="" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="Payments.aspx.cs" Inherits="nChanger.WebUI.Admin.Payments" %>

<%@ Register TagPrefix="uc1" TagName="paging" Src="~/UserControls/Paging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-2.2.0.js"></script>
    <script src="https://code.jquery.com/ui/1.11.4/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
    <script type="text/javascript">
        $(function () {
            $(".date-input").datepicker();
        });
    </script>
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
        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click"/>
            </Triggers>
        <ContentTemplate>
            <div class="ui warning message fluid" style="display: none;" id="success-alert">
                <asp:Label ID="lblMsg" Style="font-size: 1.2em; font-weight: bold;" runat="server"></asp:Label>
            </div>
            <div class="ui grid padded">
                <div class="sixteen wide column">
                    <div class="ui centered">
                        <div class="ui large form container">
                            <div class="ui small breadcrumb">
                                <a href="dashboard.aspx" class="section">Home</a>
                                <i class="right chevron icon divider"></i>
                                <div class="active section">Payment Listing</div>
                            </div>
                            <div class="ui centered">
                                <div class="ui large form container">
                                    <div class="ui stacked segment frmPad">
                                        <div class="ui form">
                                            <h4 class="ui dividing header orange"><i class="search icon"></i>Search Payents</h4>
                                            <div class="three fields">
                                                <div class="field">
                                                    <label>User Id</label>
                                                    <div class="fields">
                                                        <div class="sixteen wide field">
                                                            <asp:TextBox runat="server" ID="txtUserId" placeholder="UserId" MaxLength="20"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="field">
                                                    <label>Package</label>
                                                    <div class="field">
                                                        <div class="sixteen wide field">
                                                            <asp:DropDownList runat="server" ID="ddlPackage" data-value="af" CssClass="ui search dropdown" CausesValidation="False" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="field">
                                                    <label>Status</label>
                                                    <div class="field">
                                                        <asp:DropDownList runat="server" ID="ddlStatus" data-value="af" CssClass="ui search dropdown" CausesValidation="False">
                                                            <Items>
                                                                <asp:ListItem Text="---SELECT---" Value="SEL"></asp:ListItem>
                                                                <asp:ListItem Text="Successfull" Value="succeeded"></asp:ListItem>
                                                                <asp:ListItem Text="Failed" Value="Failed"></asp:ListItem>
                                                            </Items>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                            </div>

                                            <h4 class="ui dividing header"></h4>

                                            <div class="fields">
                                                <div class="field">
                                                    <label>Payment Date</label>
                                                    <span class="btn btn-default" style="vertical-align: top;">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                    <asp:TextBox runat="server" ID="txtImportDtFrom" placeholder="from" Height="35" Style="font-weight: bold;" CssClass="form-control date-input" Width="120"></asp:TextBox>
                                                </div>

                                                <div class="field">
                                                    <label style="color: white;">.</label>
                                                    <span class="btn btn-default" style="vertical-align: top;">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                    <asp:TextBox runat="server" ID="txtImportDtTo" placeholder="to" Height="35" Style="font-weight: bold;" CssClass="form-control date-input" Width="120"></asp:TextBox>
                                                </div>
                                            </div>

                                            <h4 class="ui dividing header"></h4>
                                            <div class="two fields">
                                                <div class="field">
                                                    <div class="sixteen wide field">
                                                        <asp:LinkButton runat="server" ID="btnSearch" 
                                                            CausesValidation="False"  OnClick="btnSearch_OnClick"
                                                            CssClass="ui button blue animated fade fluid" TabIndex="0">
                                                            <div class="visible content"><i class="search icon"></i> Search</div>
                                                            <div class="hidden content"><i class="search icon"></i> Search</div>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="field">
                                                    <div class="sixteen wide field">
                                                        <asp:LinkButton runat="server" ID="btnReset" CausesValidation="False" CssClass="ui button blue animated fade fluid" TabIndex="0">
                                                            <div class="visible content"><i class="refresh icon"></i> Reset</div>
                                                            <div class="hidden content"><i class="refresh icon"></i> Reset</div>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div class="ui sixteen wide column fluid" style="margin-bottom: 100px;">
                <div class="ui grid">
                    <div class="one wide column"></div>
                    <div class="fourteen wide column">

                        <h3 class="ui header orange">Payment List</h3>

                        <uc1:paging ID="ucPaging" runat="server" Align="right" PageSize="50" OnNavigator_Click="ImgbtnNavigator_Click" ShowNoOfRecordsDropDown="True"
                            OnNoOfRecords_SelectedIndexChanged="ddlNoOfRecords_IndexChanged"
                            OnPageNo_Changed="txtPageNo_Changed" />
                    </div>
                    <div class="one wide column"></div>

                    <div class="one wide column"></div>
                    <div class="fourteen wide column">
                        <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" HorizontalAlign="Right"
                            CssClass="ui compact celled definition table"
                            OnSorting="gvPayment_Sorting" Width="100%"
                            OnRowDataBound="gvPayment_OnRowDataBound" >
                            <HeaderStyle CssClass="gridHead" Height="50"></HeaderStyle>
                            <EmptyDataTemplate>
                                <span class="message">No records found.</span>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderStyle Width="20%" />
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lnkbtnUserId" runat="server" CommandArgument="UserId" OnClick="gvPayment_Sorting"
                                            Text="User" ForeColor="#000000"></asp:LinkButton>
                                        <asp:LinkButton ID="btnSort_UserId" runat="server"
                                            CommandArgument="UserId"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      <asp:HyperLink runat="server" ID="hypView" Target="_self"
                                      NavigateUrl='<%# string.Format("../Admin/UserDetails.aspx?Id={0}",Eval("UserId").ToString())%>' 
                                      Text='<%#Eval("UserId").ToString() %>'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lnkbtnPackageId" runat="server" CommandArgument="PackageId" OnClick="gvPayment_Sorting"
                                            Text="Package" ForeColor="#000000"></asp:LinkButton>
                                        <asp:LinkButton ID="btnSort_PackageId" runat="server"
                                            CommandArgument="PackageId"> </asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblPackageName" Text='<%#Eval("PackageId").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lnkbtnAmount" runat="server" CommandArgument="Amount" OnClick="gvPayment_Sorting"
                                            Text="Amount" ForeColor="#000000"></asp:LinkButton>
                                        <asp:LinkButton ID="btnSort_Amount" runat="server"
                                            CommandArgument="Amount"> </asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# string.IsNullOrEmpty(Eval("Amount","N"))?"": Convert.ToDecimal(Eval("Amount")).ToString("C") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lnkbtnStatus" runat="server" CommandArgument="Status" OnClick="gvPayment_Sorting"
                                            Text="Status" ForeColor="#000000"></asp:LinkButton>
                                        <asp:LinkButton ID="btnSort_Status" runat="server"
                                            CommandArgument="Status"> </asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblStatus" Text='<%#Eval("Status").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lnkbtnPaymentDate" runat="server" CommandArgument="PaymentDate" OnClick="gvPayment_Sorting"
                                            Text="Date" ForeColor="#000000"></asp:LinkButton>
                                        <asp:LinkButton ID="btnSort_PaymentDate" runat="server"
                                            CommandArgument="PaymentDate"> </asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# string.IsNullOrEmpty(Eval("PaymentDate","{0:dd/MM/yyyy}"))?"": Convert.ToDateTime(Eval("PaymentDate")).ToString("M/dd/yyyy") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="one wide column"></div>
                    <uc1:paging ID="ucPaging1" Visible="False" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" src="../Scripts/semantic.min.js"></script>

    <script type="text/javascript">
        $('.ui.search.dropdown').dropdown();
    </script>
</asp:Content>
