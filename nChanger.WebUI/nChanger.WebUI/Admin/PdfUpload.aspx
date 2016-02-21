<%@ Page Title="" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="PdfUpload.aspx.cs" Inherits="nChanger.WebUI.Admin.PdfUpload" %>
<%@ Register Src="../UserControls/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <link rel="stylesheet" type="text/css" href="../Content/jquery.multiselect.css" />
    <link rel="stylesheet" type="text/css" href="../Content/jquery.multiselect.filter.css" />
    <link rel="stylesheet" type="text/css" href="../assets/style.css" />
    <link rel="stylesheet" type="text/css" href="../assets/prettify.css" />
    <link rel="stylesheet" type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/ui-lightness/jquery-ui.css" />
    <script type="text/javascript" src="../Scripts/jquery-2.2.0.min.js"></script>
    
    <script type="text/javascript" src="http://www.uploadify.com/wp-content/themes/uploadify/js/jquery.min.js"></script>
	<script type="text/javascript" src="http://www.uploadify.com/wp-content/themes/uploadify/js/jquery.uploadify.min.js"></script>
    

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.multiselect.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.multiselect.filter.js"></script>
    <script type="text/javascript" src="../assets/prettify.js"></script>
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
            <div class="ui grid padded">
                <div class="sixteen wide column">
                    <div class="ui centered">
                        <div class="ui large form container">
                            <div class="ui small breadcrumb">
                                <a href="dashboard.aspx" class="section">Home</a>
                                <i class="right chevron icon divider"></i>
                                <div class="active section">Templates</div>
                            </div>
                            <div class="ui centered">
                                <div class="ui large form container">
                                    <div class="ui stacked segment frmPad">
                                        <div class="ui form">
                                            <h4 class="ui dividing header orange"><i class="plus icon"></i>Upload Pdf(s)</h4>
                                             <asp:Label runat="server" ID="lblMsg" Style="color: maroon; font-weight: bold;"></asp:Label>
                                            <h4 class="ui dividing header"></h4>
                                            <div class="one field">
                                              
                                                <div class="field">
                                                    <div class="sixteen wide field">
                                                        <div>
                                                     <input class="ui button orange large" type="file" name="uploadify" id="uploadify" />
                                                            </div>
                                                       <%--<a href="javascript:$('#uploadify').uploadifyUpload()">Upload</a>|<a href="javascript:$('#uploadify').uploadifyClearQueue()">
                                                            Cancle</a>--%>
                                                        <div id="fileQueue">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div> 
                                            <h4 class="ui dividing header"></h4>
                                             
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1"  OnLoad = "UpdatePanel1_OnLoad">
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
                    <div class="eight wide column">
                        <asp:GridView ID="gvTemplate" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                            CssClass="ui compact celled definition table" OnSorting="gvTemplate_OnSorting">
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
                                        <asp:HyperLink ID="hypMap" runat="server" CssClass="ui button orange" NavigateUrl='<%# string.Format("TemplateMapping.aspx?id={0}&active={1}", Eval("Id"), Eval("IsActive")) %>'>Mapping</asp:HyperLink>
                                        </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <uc1:Paging ID="ucPaging1" Visible="False" runat="server" />
                    </div>
                </div>
                <div class="one wide column"></div>
                <h4 class="ui dividing header"></h4>
                <h4 class="ui dividing header"></h4>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $("#uploadify").uploadify({
                uploader: 'PdfUpload.aspx',
                cancelImg: '../Uplodify/cancel.png',
                swf: '../Uplodify/uploadify.swf',
                folder: '../Pdf',
                'onQueueComplete': function (queueData) {
                     __doPostBack("<%=UpdatePanel1.UniqueID %>", "");
                }
            });
        });
         
    </script>
</asp:Content>
