<%@ Page Title="" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="FieldMapping.aspx.cs" Inherits="nChanger.WebUI.Admin.PdfFieldMapping" %>
<%@ Register Src="../UserControls/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-2.2.0.js"></script>
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
        <asp:Label ID="lblMsg" Style="font-size: 1.2em; font-weight: bold;" runat="server" Text="Template mapping saved!"></asp:Label>
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
                <div class="ui stacked segment frmPad">
                    <h3 class="ui header orange">Pdf Field Mapping</h3>
                     
                    <div class="one field">
                        <div class="field">
                            <div id="divMsg" runat="server" style="display: none;">
                                <i class="warning sign icon large yellow"></i>
                            </div>
                            <asp:Label runat="server" ID="lblMessage" Style="color: maroon; font-weight: bold;"></asp:Label>
                        </div>
                    </div>
                    <h4 class="ui dividing header"></h4>
                    <div class="one field">
                        <div class="field">
                            <label>Template Name</label>
                            <div class="field">
                                <div class="sixteen wide field">
                                    <asp:TextBox runat="server" ID="txtTemplateName" placeholder="Template Name" MaxLength="250"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <h4 class="ui dividing header"></h4>
                    <div class="one field">
                        <div class="field">
                            <label>Pdf</label>
                            <div class="field">
                                <div class="sixteen wide field">
                                    <asp:HyperLink runat="server" ID="hypPdf" Target="_blank" CssClass="ui active"></asp:HyperLink>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="ui large info message">
                        <div class="ui header">
                             <p>
                                 <i class="info circle icon large black"></i>
                                Use the dropdowns in the right to select corressponding database field for each pdf field. 
                                 After you have set-up all the fields click on submit button to save this template. 
                        </p>
                         
                        </div>
                        
                      </div>
                </div>
            </div>
        </div>
    </div>
    <div class="one wide column"></div>
    
    <asp:UpdatePanel runat="server" ID="updatePanelPdf">
        <ContentTemplate>
              <div class="ui grid">
                <div class="one wide column"></div>
                <div class="fourteen wide column">
                    <uc1:Paging ID="ucPaging" runat="server" Align="right" PageSize="250" OnNavigator_Click="ImgbtnNavigator_Click" ShowNoOfRecordsDropDown="True"
                        OnNoOfRecords_SelectedIndexChanged="ddlNoOfRecords_IndexChanged"
                        OnPageNo_Changed="txtPageNo_Changed" />
                </div>
                <div class="one wide column">
                </div>

                  <div class="one wide column"></div>
                  <div class="fourteen wide column">
                      <div class="row right aligned">
                          <div class="right item">
                              <asp:LinkButton runat="server" ID="btnSubmit" Width="200" CausesValidation="False" CssClass="ui button green large" TabIndex="0" OnClick="btnSubmit_OnClick">
                            <i class="database icon large"></i>Submit
                              </asp:LinkButton>
                                <asp:HyperLink runat="server" ID="hypBack" CssClass="ui button green" Text="Back" NavigateUrl="ManagePdfTemplate.aspx" ToolTip="Back to form listing."><strong><i class="arrow left icon large"></i>&nbsp;Back</strong></asp:HyperLink>
                          </div>
                      </div>
                  </div>
                  <div class="one wide column"></div>
                  <div class="one wide column"></div>
                  <div class="fourteen wide column">

                      <asp:GridView ID="gvFields" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                            CssClass="ui compact celled definition table" GridLines="None" Width="100%" OnSorting="gvFields_Sorting" OnRowDataBound="gvFields_RowDataBound">
                            <HeaderStyle CssClass="gridHead" Height="50"></HeaderStyle>
                            <EmptyDataTemplate>
                                <span class="message">No records found.</span>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField HeaderText="Page #" DataField="PdfPageNumber">
                                    <ItemStyle Wrap="False"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Pdf Field Name" DataField="PdfFieldName">
                                    <ItemStyle Wrap="False"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Field Type" DataField="FieldType">
                                    <ItemStyle Wrap="False"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Bottom" DataField="Bottom" />
                                <asp:BoundField HeaderText="Left" DataField="Left" />
                                <asp:BoundField HeaderText="Top" DataField="Top" />
                                <asp:BoundField HeaderText="Right" DataField="Right" />
                                <asp:TemplateField HeaderText="SQL Field Name">
                                    <ItemTemplate>
                                        <asp:DropDownList runat="server" CssClass="ui fluid search selection dropdown" ID="ddlSQLColumn">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <uc1:Paging ID="ucPaging1" Visible="False" runat="server" />
                     
                </div>
                <div class="one wide column"></div>
                <h4 class="ui dividing header"></h4>
                <h4 class="ui dividing header"></h4>
            </div>
            <div class="one wide column"></div>
            <h4 class="ui dividing header"></h4>
            <h4 class="ui dividing header"></h4>
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
