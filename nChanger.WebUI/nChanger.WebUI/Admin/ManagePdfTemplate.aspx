<%@ Page Title="" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="ManagePdfTemplate.aspx.cs" Inherits="nChanger.WebUI.Admin.ManagePdfTemplate" %>
<%@ Register Src="../UserControls/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-2.2.0.min.js"></script>
    <script type="text/javascript" src="http://www.uploadify.com/wp-content/themes/uploadify/js/jquery.min.js"></script>
	<script type="text/javascript" src="http://www.uploadify.com/wp-content/themes/uploadify/js/jquery.uploadify.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="updatePanelPdf"
        runat="server">
        <ProgressTemplate>
            <div id="spinner" class="divspinner">
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
      <div class="ui warning message fluid" style="display: none;" id="success-alert">
        <asp:Label ID="lblMsg" Style="font-size: 1.2em; font-weight: bold;" runat="server" Text="Template deleted successfully!"></asp:Label>
    </div>
<div class="ui sixteen wide column fluid">
    <div class="ui grid">
        <div class="one wide column"></div>
        <div class="fourteen wide column">
            <div class="ui small breadcrumb">
                <a href="dashboard.aspx" class="section">Home</a>
                <i class="right chevron icon divider"></i>
                <a href="ManageProvinces.aspx" class="section">Provinces</a>
                <i class="right chevron icon divider"></i>
                <a href="ManageProvinceCategory.aspx" class="section">Category</a>
                <i class="right chevron icon divider"></i>
                <div class="active section">
                    Pdf Mapping
                </div>
            </div>
        </div>
    </div>
</div>
 
    <div class="one wide column"></div>
    <div class="fourteen wide column">
        <div class="ui centered">
            <div class="ui large form container">
                <div id="divWrapper" runat="server">
                <div class="ui stacked segment frmPad">
                    <h3 class="ui header orange">Upload Pdf</h3>
                    <div class="ui info ignored icon message">
                        <i class="info cloud upload icon"></i>
                        <div class="content">
                            <div class="header">Select Form(s)</div>
                            <div class="ui wide field">
                                <div>
                                    <input class="ui button orange large" type="file" name="uploadify" id="uploadify" />
                                </div>
                                <div id="fileQueue">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                    </div>
            </div>
        </div>
    </div>
    <div class="one wide column"></div>
    
    <asp:UpdatePanel runat="server" ID="updatePanelPdf"  OnLoad = "updatePanelPdf_OnLoad" ChildrenAsTriggers="True">
        <ContentTemplate>
            <div class="ui grid">
                <div class="one wide column"></div>
                <div class="fourteen wide column">
                    <uc1:Paging ID="ucPaging" runat="server" Align="right" PageSize="50" OnNavigator_Click="ImgbtnNavigator_Click" ShowNoOfRecordsDropDown="True"
                        OnNoOfRecords_SelectedIndexChanged="ddlNoOfRecords_IndexChanged"
                        OnPageNo_Changed="txtPageNo_Changed" />
                </div>
                <div class="one wide column"></div>
                
                <div class="one wide column"></div>
                <div class="fourteen wide column">
                    <asp:HyperLink runat="server" ID="hypBack" CssClass="ui button green" Text="Back" onclick="window.history.back();" ToolTip="Back to province categories."><strong><i class="arrow left icon large"></i>&nbsp;Back</strong></asp:HyperLink>
                </div>
                <div class="one wide column"></div>

                <div class="one wide column"></div>
                <div class="fourteen wide column">
                        <asp:GridView ID="gvTemplate" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                            CssClass="ui compact celled definition table" OnSorting="gvTemplate_OnSorting" OnRowDeleting="gvTemplate_RowDeleting" OnRowDataBound="gvTemplate_RowDataBound">
                            <HeaderStyle CssClass="gridHead" Height="50"></HeaderStyle>
                            <EmptyDataTemplate>
                                <span class="message">No records found.</span>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lnkbtnTemplateName" runat="server" CommandArgument="TemplateName"
                                            Text="Template Name" ForeColor="#000000" OnClick="gvTemplate_Sorting"></asp:LinkButton>
                                        <asp:LinkButton ID="btnSort_TemplateName" runat="server"
                                            CommandArgument="TemplateName"> </asp:LinkButton>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <%#Eval("TemplateName").ToString() %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lnkbtnPdfFileName" runat="server" CommandArgument="PdfFileName" OnClick="gvTemplate_Sorting"
                                            Text="PdfFileName" ForeColor="#000000"></asp:LinkButton>
                                        <asp:LinkButton ID="btnSort_PdfFileName" runat="server"
                                            CommandArgument="PdfFileName">  </asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("PdfFileName").ToString() %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lnkbtnIsActive" runat="server" CausesValidation="False" OnClick="gvTemplate_Sorting" CommandArgument="IsActive"
                                            Text="Active" ForeColor="#000000"></asp:LinkButton>
                                        <asp:LinkButton ID="btnSort_IsActive" runat="server"
                                            CommandArgument="IsActive"> </asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("IsActive")  %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lnkbtnEntryDate" runat="server" CommandArgument="EntryDate" OnClick="gvTemplate_Sorting"
                                            Text="Entry Date" ForeColor="#000000"></asp:LinkButton>
                                        <asp:LinkButton ID="btnSort_EntryDate" runat="server"
                                            CommandArgument="EntryDate"> </asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# string.IsNullOrEmpty(Eval("EntryDate","{0:dd/MM/yyyy}"))?"": Convert.ToDateTime(Eval("EntryDate")).ToString("M/dd/yyyy") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" ItemStyle-Width="120">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hypMap" runat="server" CssClass="ui button small green" NavigateUrl='<%# string.Format("FieldMapping.aspx?id={0}&active={1}", Eval("Id"), Eval("IsActive")) %>'>Mapping</asp:HyperLink>
                                        </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:CommandField ButtonType="Image"
                            SelectImageUrl="../images/edit.png"
                            HeaderText="Delete"
                            DeleteImageUrl="../images/delete.png"
                            DeleteText="Delete" SelectText="Edit"
                            ShowSelectButton="False"
                            CausesValidation="False"
                            ShowDeleteButton="true">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:CommandField>
                            </Columns>
                        </asp:GridView>
                        <uc1:Paging ID="ucPaging1" Visible="False" runat="server" />
                     
                </div>
                <div class="one wide column"></div>

                <h4 class="ui dividing header"></h4>
                <h4 class="ui dividing header"></h4>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" src="../Scripts/semantic.min.js"></script>
     <script type="text/javascript">
        $(document).ready(function () {
            $("#uploadify").uploadify({
                uploader: 'ManagePdfTemplate.aspx',
                cancelImg: '../Uplodify/cancel.png',
                swf: '../Uplodify/uploadify.swf',
                folder: '../Pdf',
                'onQueueComplete': function (queueData) {
                     __doPostBack("<%=updatePanelPdf.UniqueID %>", "");
                }
            });
        });
         
         function showAlert() {
             $("#success-alert").show().delay(5000).fadeOut();
         }
    </script>
</asp:Content>
