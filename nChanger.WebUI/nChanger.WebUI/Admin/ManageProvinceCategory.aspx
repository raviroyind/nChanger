<%@ Page Title="" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="ManageProvinceCategory.aspx.cs" Inherits="nChanger.WebUI.Admin.ManageProvinceCategory" %>

<%@ Register TagPrefix="uc1" TagName="paging" Src="~/UserControls/Paging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-2.2.0.js"></script>
    <script src="//cdn.tinymce.com/4/tinymce.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui warning message fluid" style="display: none;" id="success-alert">
        <asp:Label ID="lblMsg" Style="font-size: 1.2em; font-weight: bold;" runat="server"></asp:Label>
    </div>
    <div class="ui modal" id="divCateory">
        <i class="close icon black" onclick="$('.modal').modal('hide');"></i>
        <div class="header">
            <h3 class="ui header green">Add Cateory</h3>
        </div>
        <div class="content">
            <div class="ui form fluid">
                   <div class="field">
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="province" ID="reqId" ControlToValidate="ddlProvinceAdd" InitialValue="SEL" ErrorMessage="Please select province." style="color: maroon; "></asp:RequiredFieldValidator>
                    <div class="field fluid">
                         <asp:DropDownList runat="server" ID="ddlProvinceAdd" CssClass="ui normal selection dropdown"/>
                    </div>
                </div> 
                <h3 class="ui divider"></h3>
                <div class="field">
                    <div class="field fluid">
                        <asp:TextBox runat="server" ID="txtCateory" placeholder="Category Name" MaxLength="200"></asp:TextBox>
                    </div>
                </div>
                <h3 class="ui divider"></h3>
                <div class="field">
                    <div class="field">
                        <label>Description</label>
                        <asp:TextBox runat="server" ID="txtProvinceCateoryDescription" CssClass="myTextEditor" MaxLength="500" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="ui divider"></div>
                <div class="actions">
                    <div class="ui button" onclick="$('.modal').modal('hide');">Cancel</div>
                    <asp:LinkButton runat="server" ID="btnAddProvinceCateory" 
                        OnClientClick="javascript:doCustomPost();" UseSubmitBehavior="false"
                        ValidationGroup="province" CausesValidation="True"
                        CssClass="ui green button" OnClick="btnAddProvinceCateory_OnClick">
                            <i class="plus sign icon White"></i>Submit
                    </asp:LinkButton>
                </div>
            </div>
        </div>

    </div>
    <asp:HiddenField runat="server" ID="hidCategoryName" />
    <asp:HiddenField runat="server" ID="hidCategoryDesc" />
    <asp:HiddenField runat="server" ID="hiddenProvinceId" />
    <asp:HiddenField runat="server" ID="hiddenCategoryId" />

    <div class="ui sixteen wide column fluid">
        <div class="ui grid">
            <div class="one wide column"></div>
            <div class="fourteen wide column">

                <div class="ui small breadcrumb">
                    <a href="dashboard.aspx" class="section">Home</a>
                    <i class="right chevron icon divider"></i>
                    <a href="ManageProvinces.aspx" class="section">Provinces</a>
                    <i class="right chevron icon divider"></i>
                    <div class="active section">
                        <asp:Label ID="lblCurrentProvince" runat="server" />
                    </div>
                </div>

                <h3 class="ui header orange" id="lblCaption" runat="server"></h3>
                <asp:DropDownList runat="server" ID="ddlProvince" CssClass="ui normal selection dropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_OnSelectedIndexChanged"/>
                <uc1:paging ID="ucPaging" runat="server" Align="right" PageSize="5" OnNavigator_Click="ImgbtnNavigator_Click" ShowNoOfRecordsDropDown="True"
                    OnNoOfRecords_SelectedIndexChanged="ddlNoOfRecords_IndexChanged"
                    OnPageNo_Changed="txtPageNo_Changed" />
            </div>
            <div class="one wide column"></div>

            <div class="one wide column"></div>
            <div class="fourteen wide column">
                <h3 class="ui header orange">
                    <div class="item">
                        <asp:HyperLink runat="server" ID="hypAddCat" CssClass="ui button" Text="Add" NavigateUrl="#" ToolTip="Add Category"><strong><i class="plus icon green large"></i>&nbsp;Add&nbsp;Category</strong></asp:HyperLink>
                        <asp:HyperLink runat="server" ID="hypBack" CssClass="ui button green" Text="Back" NavigateUrl="ManageProvinces.aspx" ToolTip="Back to province listing."><strong><i class="arrow left icon large"></i>&nbsp;Back</strong></asp:HyperLink>
                    </div>
                    <div class="item">
                    </div>
                    <div class="pusher"></div>
                    <div class="right item">
                    </div>
                </h3>
            </div>

            <div class="one wide column"></div>
            <div class="one wide column"></div>
            <div class="fourteen wide column">
                <asp:GridView ID="gvProvinceCategory" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" HorizontalAlign="Right"
                    CssClass="ui compact celled definition table"
                    OnSorting="gvProvinceCategory_Sorting" Width="100%"
                    OnRowDataBound="gvProvinceCategory_OnRowDataBound" 
                    OnRowDeleting="gvProvinceCategory_OnRowDeleting" 
                    OnSelectedIndexChanged="gvProvinceCategory_SelectedIndexChanged">
                    <HeaderStyle CssClass="gridHead" Height="50"></HeaderStyle>
                    <EmptyDataTemplate>
                        <span class="message">No records found.</span>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField>
                            <HeaderStyle Width="20%" />
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkbtnCategory" runat="server" CommandArgument="Category" OnClick="gvProvinceCategory_Sorting"
                                    Text="Category" ForeColor="#000000"></asp:LinkButton>
                                <asp:LinkButton ID="btnSort_Category" runat="server"
                                    CommandArgument="Category"></asp:LinkButton>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("Category").ToString() %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderStyle Width="55%" />
                            <HeaderTemplate>
                                Description
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div><%# Convert.ToString(Eval("Description")).Length >120?Eval("Description").ToString().Substring(0,120)+"..." :Eval("Description").ToString() %></div>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:CommandField ButtonType="Image"
                            SelectImageUrl="../images/edit.png"
                            HeaderText="Actions"
                            DeleteImageUrl="../images/delete.png"
                            DeleteText="Delete" SelectText="Edit"
                            ShowSelectButton="true"
                            CausesValidation="False"
                            ShowDeleteButton="true">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:CommandField>
                        <asp:TemplateField>
                            <HeaderStyle Width="12%" />
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <HeaderTemplate>
                                General/ Specific Questions
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="actions center aligned">
                                    <asp:HyperLink ID="hypViewQuestions" runat="server" CssClass="ui button small blue disabled" 
                                         NavigateUrl='<%# string.Format("ManageQuestions.aspx?id={0}", Eval("Id")) %>' 
                                         CommandArgument='<%#Eval("Id").ToString() %>' CommandName="View"
                                        OnClick="lnkViewCategories_OnClick">
                                          View
                                    </asp:HyperLink>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle Width="5%" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <HeaderTemplate>
                                Forms
                            </HeaderTemplate>
                            <ItemTemplate>
                                 <div class="actions center aligned">
                                    <asp:HyperLink ID="hypViewCategories" runat="server" CssClass="ui button small green"
                                        NavigateUrl='<%# string.Format("ManagePdfTemplate.aspx?id={0}", Eval("Id")) %>' 
                                        CommandArgument='<%#Eval("Id").ToString() %>' CommandName="View"
                                        OnClick="lnkViewCategories_OnClick">
                                          View
                                    </asp:HyperLink>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>
            </div>
            <div class="one wide column"></div>
            <uc1:paging ID="ucPaging1" Visible="False" runat="server" />
        </div>
    </div>
    <script type="text/javascript" src="../Scripts/semantic.min.js"></script>
    <script type="text/javascript">
        $('.ui.normal.selection.dropdown').dropdown();
         
        $(document).ready(function () {
            $('#MainContent_hypAddCat').click(function () {
                $('#divCateory').modal({
                    detachable: false,
                    observeChanges: false,
                    onHide: function () {
                        tinyMCE.get('MainContent_txtProvinceCateoryDescription').setContent('');
                        document.getElementById('<%=txtCateory.ClientID%>').value = '';
                         
                    }
                }).modal('show').modal('refresh');
                tinymce.init({
                    selector: 'textarea',
                    theme: 'modern',
                    width: 900,
                    height: 200,
                    plugins: [
                        'advlist autolink lists link image charmap print preview hr anchor pagebreak',
                        'searchreplace wordcount visualblocks visualchars code fullscreen',
                        'insertdatetime media nonbreaking save table contextmenu directionality',
                        'emoticons template paste textcolor colorpicker textpattern imagetools'
                    ],
                    toolbar1: 'insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
                    toolbar2: 'print preview media | forecolor backcolor emoticons',
                    image_advtab: true
                });
            });
        });

        function loadEdit() {
            
            $('#divCateory').modal({
                detachable: true,
                observeChanges: true,
                onHide: function () {
                    tinyMCE.get('MainContent_txtProvinceCateoryDescription').setContent('');
                    document.getElementById('<%=txtCateory.ClientID%>').value = '';
                    
                }
            }).modal('show').modal('refresh');
            tinymce.init({
                selector: 'textarea',
                theme: 'modern',
                width: 900,
                height: 200,
                plugins: [
                    'advlist autolink lists link image charmap print preview hr anchor pagebreak',
                    'searchreplace wordcount visualblocks visualchars code fullscreen',
                    'insertdatetime media nonbreaking save table contextmenu directionality',
                    'emoticons template paste textcolor colorpicker textpattern imagetools'
                ],
                toolbar1: 'insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
                toolbar2: 'print preview media | forecolor backcolor emoticons',
                image_advtab: true
            });

        }

        function doCustomPost() {
            var ModalTextBox = document.getElementById('<%=txtCateory.ClientID%>');
            var hidCategoryName = document.getElementById('<%=hidCategoryName.ClientID%>');
            var hiddenProvinceId = document.getElementById('<%=hiddenProvinceId.ClientID%>');

            var modalTextBox1 = document.getElementById('<%=txtProvinceCateoryDescription.ClientID%>');
            var hidDes = document.getElementById('<%=hidCategoryDesc.ClientID%>');

            var content = tinyMCE.get('MainContent_txtProvinceCateoryDescription').getContent();
            hidDes.value = content;
            hidCategoryName.value = ModalTextBox.value;

            var e = document.getElementById('<%=ddlProvinceAdd.ClientID%>');
            var provId = e.options[e.selectedIndex].value;
             
            hiddenProvinceId.value = provId;
        }

        function showAlert() {
            $("#success-alert").show().delay(5000).fadeOut();
        }

    </script>
</asp:Content>
