<%@ Page Title="" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="ManageQuestions.aspx.cs" Inherits="nChanger.WebUI.Admin.ManageQuestions" %>

<%@ Register TagPrefix="uc1" TagName="paging" Src="~/UserControls/Paging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-2.2.0.js"></script>
    <script src="//cdn.tinymce.com/4/tinymce.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui warning message fluid" style="display: none;" id="success-alert">
        <asp:Label ID="lblMsg" Style="font-size: 1.2em; font-weight: bold;" runat="server"></asp:Label>
    </div>

    <%-- Modal Popup --%>
    <div class="ui modal" id="divQuestion">
        <i class="close icon black" onclick="$('.modal').modal('hide');"></i>
        <div class="header">
            <h3 class="ui header green">Add Question</h3>
        </div>
        <div class="content">
            <div class="ui form fluid">
                <div class="field">
                    <div class="field">
                        <label>Question</label>
                        <asp:TextBox runat="server" ID="txtQuestion"  MaxLength="500" ></asp:TextBox>
                    </div>
                </div>

                <h3 class="ui divider"></h3>

                <div class="field">
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="add" ID="reqId" ControlToValidate="ddlQuestionTypeAdd" InitialValue="SEL" ErrorMessage="Please select type of question." Style="color: maroon;"></asp:RequiredFieldValidator>
                    <div class="field fluid">
                        <asp:DropDownList runat="server" ID="ddlQuestionTypeAdd" CssClass="ui normal selection dropdown">
                            <Items>
                                <asp:ListItem Value="SEL" Text="--Select--"></asp:ListItem>
                                <asp:ListItem Value="txt" Text="Input box"></asp:ListItem>
                                <asp:ListItem Value="tar" Text="Text area"></asp:ListItem>
                                <asp:ListItem Value="chk" Text="Check box"></asp:ListItem>
                                <asp:ListItem Value="rdb" Text="Radio button"></asp:ListItem>
                            </Items>
                        </asp:DropDownList>
                    </div>
                </div>

                <h3 class="ui divider"></h3>
                
                <div id="divOptions" style="display: none;">
                     <h3 class="ui header green">Check-box/ Radio button Options</h3> (enter comma separated options like Choice 1, Choice 2...)
                    <div class="field">
                        
                        <div class="field">
                            <asp:TextBox runat="server" ID="txtOptionLabel"  MaxLength="100"></asp:TextBox>
                        </div>
                     </div>
                     <h3 class="ui divider"></h3>
                </div>
                
                <div class="actions">
                    <div class="ui button" onclick="$('.modal').modal('hide');">Cancel</div>
                    <asp:LinkButton runat="server" ID="btnAddQuestion"
                        OnClientClick="javascript:doCustomPost();" UseSubmitBehavior="false"
                        ValidationGroup="add" CausesValidation="True"
                        CssClass="ui green button" OnClick="btnAddQuestion_OnClick">
                            <i class="plus sign icon White"></i>Submit
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="hidQuestionId" />
    <asp:HiddenField runat="server" ID="hidProvinceCategoryId" />
    <asp:HiddenField runat="server" ID="hidQuestionType" />
    <asp:HiddenField runat="server" ID="hidQuestion" />
     <asp:HiddenField runat="server" ID="hidOptions" />
    <%-- Modal Popup End--%>

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
                        Questions
                        <asp:Label ID="lblCurrentProvince" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- Grid --%>
    <div class="ui sixteen wide column fluid">
        <div class="ui grid">
            <div class="one wide column"></div>
            <div class="fourteen wide column">

                
                <uc1:paging  ID="ucPaging" runat="server" Align="right" PageSize="10" 
                    OnNavigator_Click="ImgbtnNavigator_Click" ShowNoOfRecordsDropDown="True"
                    OnNoOfRecords_SelectedIndexChanged="ddlNoOfRecords_IndexChanged"
                    OnPageNo_Changed="txtPageNo_Changed" />
            </div>
            <div class="one wide column"></div>

            <div class="one wide column"></div>
            <div class="fourteen wide column">
                <h3 class="ui header orange">
                    <div class="item">
                        <asp:HyperLink runat="server" ID="hypAddQuestion" CssClass="ui button" Text="Add" NavigateUrl="#" ToolTip="Add Question"><strong><i class="plus icon green large"></i>&nbsp;Add&nbsp;Question</strong></asp:HyperLink>
                        <asp:HyperLink runat="server" ID="hypBack" CssClass="ui button green" Text="Back" NavigateUrl="ManageProvinceCategory.aspx" ToolTip="Back to province categories."><strong><i class="arrow left icon large"></i>&nbsp;Back</strong></asp:HyperLink>
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
                <asp:GridView ID="gvDefineQuestions" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" HorizontalAlign="Right"
                    CssClass="ui compact celled definition table"
                    OnSorting="gvDefineQuestions_Sorting" Width="100%"
                    OnRowDataBound="gvDefineQuestions_OnRowDataBound"
                    OnRowDeleting="gvDefineQuestions_OnRowDeleting"
                    OnSelectedIndexChanged="gvDefineQuestions_OnSelectedIndexChanged">
                    <HeaderStyle CssClass="gridHead" Height="50"></HeaderStyle>
                    <EmptyDataTemplate>
                        <span class="message">No records found.</span>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField>
                            <HeaderStyle Width="20%" />
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkbtnCategory" runat="server" CommandArgument="Category" OnClick="gvDefineQuestions_Sorting"
                                    Text="Category" ForeColor="#000000"></asp:LinkButton>
                                <asp:LinkButton ID="btnSort_Category" runat="server"
                                    CommandArgument="Category"></asp:LinkButton>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("QuestionType").ToString() %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderStyle Width="55%" />
                            <HeaderTemplate>
                                Question
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("Question").ToString() %>
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
            <uc1:paging ID="ucPaging1" Visible="False" runat="server" />
        </div>
    </div>
    <%-- Grid End--%>
    <script type="text/javascript" src="../Scripts/semantic.min.js"></script>
    <script type="text/javascript">
        $(".ui.normal.selection.dropdown.compact").dropdown();
        $(".ui.normal.selection.dropdown").dropdown();

        $(document).ready(function () {
            $('#MainContent_ddlQuestionTypeAdd').on('change', function () {
                if (this.value == 'chk')
                {
                    $("#divOptions").show();
                }
                else {
                    $("#divOptions").hide();
                }
            });
        });


       $(document).on("click", "#MainContent_hypAddQuestion", function() {
           $("#divQuestion").modal({
               detachable: false,
               observeChanges: false,
               onShow: function() {
                   document.getElementById("<%=txtQuestion.ClientID%>").value = "";
                   document.getElementById('<%=hidOptions.ClientID %>').value = "";
               },
               onHide: function() {
                   $("#divOptions").hide();
               }
           }).modal("show").modal("refresh");
       });

       function loadEdit() {
           $("#divQuestion").modal({
               detachable: true,
               observeChanges: true,
               onHide: function() {
                   $("#divOptions").hide();
               }
           }).modal("show").modal("refresh");
       }

       function doCustomPost() {
           var modalQuestion = document.getElementById("<%=txtQuestion.ClientID%>");

           var e = document.getElementById("<%=ddlQuestionTypeAdd.ClientID%>");
           var modalQuestionType = e.options[e.selectedIndex].value;

           
           document.getElementById('<%=hidQuestion.ClientID %>').value = modalQuestion.value;
           document.getElementById('<%=hidQuestionType.ClientID %>').value = modalQuestionType;
           document.getElementById('<%=hidOptions.ClientID %>').value = document.getElementById("<%=txtOptionLabel.ClientID%>").value;

           $(".modal").modal("hide");
       }

        function showAlert() {
            $("#success-alert").show().delay(5000).fadeOut();
        }
    </script>
</asp:Content>
