<%@ Page Title="" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="ManagePackageFeatures.aspx.cs" Inherits="nChanger.WebUI.Admin.ManagePackageFeatures" %>
<%@ Register TagPrefix="uc1" TagName="paging" Src="~/UserControls/Paging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-2.2.0.js"></script>
     <script src="//cdn.tinymce.com/4/tinymce.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hidFeatureId" />
    <asp:HiddenField runat="server" ID="hidFeatureName" />
    <asp:HiddenField runat="server" ID="hidFeatureDesc" />
    <div class="ui modal" id="divFeature">
        <i class="close icon black" onclick="$('.modal').modal('hide');"></i>
        <div class="header">
            <h3 class="ui header green">Add/ Edit Features</h3>
        </div>
        <div class="content">
            <div class="ui form fluid">
                <div class="field">
                    <div class="field">
                        <label>Feature Name</label>
                        <asp:TextBox runat="server" ID="txtFeatureName" placeholder="Feature Name" MaxLength="150"></asp:TextBox>
                    </div>
                </div>
                <h3 class="ui divider"></h3>
                <div class="field">
                    <div class="field">
                        <label>Description</label>
                        <asp:TextBox runat="server" ID="txtDescription" CssClass="myTextEditor" MaxLength="500" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                 
                <h3 class="ui divider"></h3>
                <div class="actions">
                    <div class="ui button" onclick="$('.modal').modal('hide');">Cancel</div>
                    <asp:LinkButton runat="server" ID="btnAddFeature"
                        OnClientClick="javascript:doCustomPost();" UseSubmitBehavior="false"
                        CssClass="ui green button" OnClick="btnAddFeature_OnClick">
                            <i class="plus sign icon White"></i>Submit
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <div class="ui warning message fluid" style="display: none;" id="success-alert">
        <asp:Label ID="lblMsg" Style="font-size: 1.2em; font-weight: bold;" runat="server"></asp:Label>
    </div>
    <div class="ui sixteen wide column fluid">
        <div class="ui grid">
            <div class="one wide column"></div>
            <div class="fourteen wide column">
                <div class="ui small breadcrumb">
                    <a href="dashboard.aspx" class="section">Home</a>
                    <i class="right chevron icon divider"></i>
                    <a href="ManageProvinces.aspx" class="active section">Package Features</a>
                </div>

                <h3 class="ui header orange" id="lblCaption" runat="server"></h3>

                <uc1:paging ID="ucPaging" runat="server" Align="right" PageSize="50" OnNavigator_Click="ImgbtnNavigator_Click" ShowNoOfRecordsDropDown="True"
                    OnNoOfRecords_SelectedIndexChanged="ddlNoOfRecords_IndexChanged"
                    OnPageNo_Changed="txtPageNo_Changed" />
            </div>
            <div class="one wide column"></div>

            <div class="one wide column"></div>
            <div class="fourteen wide column">
                <h3 class="ui header orange">
                    <div class="item">
                        <asp:HyperLink runat="server" ID="hypFeature" CssClass="ui button" Text="Add" NavigateUrl="#" ToolTip="Add Feature"><strong><i class="plus icon green large"></i>&nbsp;Add&nbsp;Feature</strong></asp:HyperLink>
                        <asp:HyperLink runat="server" ID="hypBack" CssClass="ui button green" Text="Back" onclick="window.history.back();" ToolTip="Back to dashboard."><strong><i class="arrow left icon large"></i>&nbsp;Back</strong></asp:HyperLink>
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
                <asp:GridView ID="gvFeature" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" HorizontalAlign="Right"
                    CssClass="ui compact celled definition table"
                    OnSorting="gvFeature_Sorting" Width="100%"
                    OnRowDeleting="gvFeature_OnRowDeleting"
                    OnSelectedIndexChanged="gvFeature_SelectedIndexChanged"
                    OnRowDataBound="gvFeature_OnRowDataBound">
                    <HeaderStyle CssClass="gridHead" Height="50"></HeaderStyle>
                    <EmptyDataTemplate>
                        <span class="message">No records found.</span>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField>
                            <HeaderStyle Width="20%" />
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkbtnFeature" runat="server" CommandArgument="Feature" OnClick="gvFeature_Sorting"
                                    Text="Feature Name" ForeColor="#000000"></asp:LinkButton>
                                <asp:LinkButton ID="btnSort_Feature" runat="server"
                                    CommandArgument="Feature"></asp:LinkButton>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("Feature").ToString() %>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField>
                            <HeaderStyle Width="60%" />
                            <HeaderTemplate>
                                Description
                            </HeaderTemplate>
                            <ItemTemplate>
                              <div> <%# Convert.ToString(Eval("Description")).Length >120?Eval("Description").ToString().Substring(0,120)+"..." :Eval("Description").ToString() %></div>
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
                    </Columns>
                </asp:GridView>
            </div>
            <div class="one wide column"></div>
            <h3 class="ui divider" style="height: 60px;"></h3>
                  
            <uc1:paging ID="ucPaging1" Visible="False" runat="server" />
        </div>
    </div>
    <script type="text/javascript" src="../Scripts/semantic.min.js"></script>
    
    <script type="text/javascript">
        
        $(document).on('click', "#MainContent_hypFeature", function () {
            $('#divFeature').modal({
                detachable: false,
                observeChanges: false,
                onHide: function () {
                    tinyMCE.get('MainContent_txtDescription').setContent('');
                        document.getElementById('<%=txtFeatureName.ClientID%>').value = '';
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
         

        function loadEdit() {
            $('#divFeature').modal({
                detachable: true,
                observeChanges: true,
                onHide: function () {
                    tinyMCE.get('MainContent_txtDescription').setContent('');
                    document.getElementById('<%=txtFeatureName.ClientID%>').value = '';
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
            var ModalTextBox = document.getElementById('<%=txtFeatureName.ClientID%>');
            var HiddenTextBox = document.getElementById('<%=hidFeatureName.ClientID%>');
             
            var modalTextBox1 = document.getElementById('<%=txtDescription.ClientID%>');
            var hidDes = document.getElementById('<%=hidFeatureDesc.ClientID%>');

            var content = tinyMCE.get('MainContent_txtDescription').getContent();
            hidDes.value = content;

            HiddenTextBox.value = ModalTextBox.value;
        }
         

        function showAlert() {
            $("#success-alert").show().delay(5000).fadeOut();
        }

    </script>
</asp:Content>
