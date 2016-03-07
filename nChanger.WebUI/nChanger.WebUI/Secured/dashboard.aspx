<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="dashboard.aspx.cs" Inherits="nChanger.WebUI.Secured.Dashboard" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="iTextSharp.text.pdf" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.js"></script>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlProvince" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCategory" EventName="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>
            <div class="ui warning message fluid" style="display: none;" id="success-alert">
                <asp:Label ID="lblMsg" Style="font-size: 1.2em; font-weight: bold;" runat="server" Text="Template deleted successfully!"></asp:Label>
            </div>
            <div class="one wide column">
            </div>
            <div class="fourteen wide column">
                <div class="ui centered">
                    <div class="ui large form container">
                        <div class="ui stacked segment frmPad">
                            <h3 class="ui header orange">Form Selection</h3>
                            <h3 class="ui dividing header"></h3>
                            <div class="content">
                                <div class="field">
                                    <label>Selet Province</label>
                                    <div class="field">
                                        <asp:DropDownList runat="server" CssClass="ui fluid search selection dropdown"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_OnSelectedIndexChanged"
                                            ID="ddlProvince">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="field">
                                    <label>Selet Category</label>
                                    <div class="field">
                                        <asp:DropDownList runat="server" AutoPostBack="True"
                                            CssClass="ui fluid search selection dropdown"
                                            ID="ddlCategory" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <h3 id="lblAviliableForms" runat="server" style="display: none;" class="ui header orange">Aviliable Forms</h3>
                                <div class="ui dividing header green"></div>
                                <div class="field">
                                     
                                    <asp:GridView ID="gvForms" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                        CssClass="ui padded celled table borderless">
                                        <HeaderStyle CssClass="gridHead" Height="50"></HeaderStyle>
                                        <EmptyDataTemplate>
                                            <span class="message">No records found.</span>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="TemplateName"  HeaderText="Form">
                                                <ItemStyle Width="35%"></ItemStyle>
                                                </asp:BoundField>
                                          <%-- <asp:TemplateField>
                                                <HeaderStyle Width="15%" />
                                                <HeaderTemplate>
                                                  Pdf
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                           <asp:HyperLink ID="hypMap" runat="server" CssClass="ui button orange" NavigateUrl='<%# "../Pdf/"+ Eval("PdfFileName")%>'>View Pdf</asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField>
                                                <HeaderStyle Width="50%" />
                                                <HeaderTemplate>
                                                   Comments/ Instruction
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div><%# Convert.ToString(Eval("Comments")) %></div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ItemStyle-Width="120">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnSubmit" CommandName="Proceed" CommandArgument='<%#Eval("Id") %>' CssClass="ui button small green"
                                                        OnClick="btnSubmit_OnClick">Select
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                                <h3 class="ui header green">Completed Forms</h3>
                                <asp:GridView ID="gvTemplate" runat="server" AutoGenerateColumns="False" DataKeyNames="PdfFormTemplateId"
                                    CssClass="ui compact celled definition table" >
                                    <HeaderStyle CssClass="gridHead" Height="50"></HeaderStyle>
                                    <EmptyDataTemplate>
                                        <span class="message">No records found.</span>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField>
                                               <%-- <HeaderStyle Width="15%" />--%>
                                                <HeaderTemplate>
                                                  Pdf
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                           <asp:HyperLink ID="hypMap" runat="server"   Text='<%# Path.GetFileName(Eval("CompletedPdf").ToString())%>'></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkFrm" runat="server" CssClass="ui button green"
                                                    CommandArgument='<%#Eval("PdfFormTemplateId").ToString() %>' CommandName="View"
                                                    OnClick="lnkFrm_OnClick">
                                                      Email/Download
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                             
                                            <ItemTemplate>
                                                 <asp:LinkButton ID="lnkViewForm" runat="server" CssClass="ui button"
                                                    CommandArgument='<%#Eval("Id").ToString() %>' CommandName="View"
                                                    OnClick="lnkViewForm_OnClick">
                                                     <i class="edit icon orange"></i> Input Data
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="one wide column">
            </div>
       
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" src="../Scripts/semantic.min.js"></script>
    <script type="text/javascript">
        $('.ui.fluid.search.selection.dropdown').dropdown();

        function showAlert() {
            $("#success-alert").show().delay(5000).fadeOut();
        }
         
    </script>
</asp:Content>
