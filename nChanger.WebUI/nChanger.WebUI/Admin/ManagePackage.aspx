<%@ Page Title="" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="ManagePackage.aspx.cs" Inherits="nChanger.WebUI.Admin.ManagePackage" %>
<%@ Register TagPrefix="uc1" TagName="paging" Src="~/UserControls/Paging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-2.2.0.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hidPackageId" />
    <asp:HiddenField runat="server" ID="hidPackageName" />
    <asp:HiddenField runat="server" ID="hidPrice" />
    <asp:HiddenField runat="server" ID="hidFeatures" />
    <asp:HiddenField runat="server" ID="hidActive" />
    <div class="ui modal" id="divPackage">
        <i class="close icon black" onclick="$('.modal').modal('hide');"></i>
        <div class="header">
            <h3 class="ui header green">Add/ Edit Package</h3>
        </div>
        <div class="content">
            <div class="ui form fluid">
                <div class="field">
                    <div class="field">
                        <label>Package Name</label>
                        <asp:TextBox runat="server" ID="txtPackageName" name="name" placeholder="Package Name" MaxLength="50"></asp:TextBox>
                    </div>
                </div>
                <h3 class="ui divider"></h3>
                <div class="field">
                    <div class="field">
                        <div class="ui left labeled input">
                            <div class="ui label">US$</div>
                            <asp:TextBox runat="server" ID="txtPrice" Width="150" placeholder="Amount"></asp:TextBox>
                        </div>
                    </div>
                </div>
                  <h3 class="ui header green">Select Package Features</h3>
                <div class="field fluid">
                     <asp:CheckBoxList runat="server" ID="chkFeatures" RepeatColumns="2"  RepeatDirection="Vertical" CssClass="ui checkbox large fluid" CellPadding="40" CellSpacing="40" />
                </div>
                 <h3 class="ui divider"></h3>
                <div class="field">
                    <div class="field">
                        <div class="ui toggle checkbox green">
                            <asp:CheckBox runat="server" ID="chkIsActive" Checked="True" CssClass="large checkmark icon green" Text="Is Active" />
                        </div>
                    </div>
                </div>
                <h3 class="ui divider"></h3>
                <div class="actions">
                    <div class="ui button" onclick="$('.modal').modal('hide');">Cancel</div>
                    <asp:LinkButton runat="server" ID="btnAddPackage"
                        OnClientClick="javascript:doCustomPost();" UseSubmitBehavior="false"
                        CssClass="ui green button" OnClick="btnAddPackage_OnClick">
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
                    <a href="ManageProvinces.aspx" class="active section">Packages</a>
                </div>

                <h3 class="ui header orange" id="lblCaption" runat="server"></h3>

                <uc1:paging ID="ucPaging" runat="server" Align="right" PageSize="5" OnNavigator_Click="ImgbtnNavigator_Click" ShowNoOfRecordsDropDown="True"
                    OnNoOfRecords_SelectedIndexChanged="ddlNoOfRecords_IndexChanged"
                    OnPageNo_Changed="txtPageNo_Changed" />
            </div>
            <div class="one wide column"></div>

            <div class="one wide column"></div>
            <div class="fourteen wide column">
                <h3 class="ui header orange">
                    <div class="item">
                        <asp:HyperLink runat="server" ID="hypPackage" CssClass="ui button" Text="Add" NavigateUrl="#" ToolTip="Add Category"><strong><i class="plus icon green large"></i>&nbsp;Add&nbsp;Package</strong></asp:HyperLink>
                        <asp:HyperLink runat="server" ID="hypBack" CssClass="ui button green" Text="Back" NavigateUrl="dashboard.aspx" ToolTip="Back to dashboard."><strong><i class="arrow left icon large"></i>&nbsp;Back</strong></asp:HyperLink>
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
                <asp:GridView ID="gvPackage" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" HorizontalAlign="Right"
                    CssClass="ui compact celled definition table"
                    OnSorting="gvPackage_Sorting" Width="100%"
                    OnRowDeleting="gvPackage_OnRowDeleting"
                    OnSelectedIndexChanged="gvPackage_SelectedIndexChanged"
                    OnRowDataBound="gvPackage_OnRowDataBound">
                    <HeaderStyle CssClass="gridHead" Height="50"></HeaderStyle>
                    <EmptyDataTemplate>
                        <span class="message">No records found.</span>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField>
                            <HeaderStyle Width="20%" />
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkbtnPackageName" runat="server" CommandArgument="PackageName" OnClick="gvPackage_Sorting"
                                    Text="Package" ForeColor="#000000"></asp:LinkButton>
                                <asp:LinkButton ID="btnSort_PackageName" runat="server"
                                    CommandArgument="PackageName"></asp:LinkButton>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("PackageName").ToString() %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkbtnPrice" runat="server" CommandArgument="Price" OnClick="gvPackage_Sorting"
                                    Text="Price" ForeColor="#000000"></asp:LinkButton>
                                <asp:LinkButton ID="btnSort_Price" runat="server"
                                    CommandArgument="Price"> </asp:LinkButton>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# string.IsNullOrEmpty(Eval("Price","N"))?"": Convert.ToDecimal(Eval("Price").ToString()).ToString("N") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkbtnIsActive" runat="server" CommandArgument="IsActive" OnClick="gvPackage_Sorting"
                                    Text="Active" ForeColor="#000000"></asp:LinkButton>
                                <asp:LinkButton ID="btnSort_IsActive" runat="server"
                                    CommandArgument="IsActive"> </asp:LinkButton>
                            </HeaderTemplate>
                            <ItemTemplate>
                               <%# Convert.ToBoolean(Eval("IsActive").ToString())? "<i class='ui check icon green large'/>":"" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkbtnEntryDate" runat="server" CommandArgument="EntryDate" OnClick="gvPackage_Sorting"
                                    Text="Entry Date" ForeColor="#000000"></asp:LinkButton>
                                <asp:LinkButton ID="btnSort_EntryDate" runat="server"
                                    CommandArgument="EntryDate"> </asp:LinkButton>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# string.IsNullOrEmpty(Eval("EntryDate","{0:dd/MM/yyyy}"))?"": Convert.ToDateTime(Eval("EntryDate")).ToString("M/dd/yyyy") %>
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
    <script type="text/javascript" src="../Scripts/semantic.min.js"></script>
    <script src="../Scripts/jquery.maskMoney.min.js"></script>
    <script type="text/javascript">
        
        $('.ui.normal.selection.dropdown.compact').dropdown();
        $(document).on('click', "#MainContent_hypPackage", function () {
            $('#divPackage').modal({
                detachable: false,
                observeChanges: false,
                onShow: function () {
                    document.getElementById("<%=txtPackageName.ClientID%>").value = "";
                    document.getElementById("<%=txtPrice.ClientID%>").value = "";
                },
                onHide:function() {
                    clearCheck();
                }
            }).modal('show').modal('refresh');
        });


        $("#MainContent_txtPrice").maskMoney({
            prefix: '', // The symbol to be displayed before the value entered by the user
            allowZero: false, // Prevent users from inputing zero
            allowNegative: true, // Prevent users from inputing negative values
            defaultZero: false, // when the user enters the field, it sets a default mask using zero
            thousands: '.', // The thousands separator
            decimal: '.', // The decimal separator
            precision: 2, // How many decimal places are allowed
            affixesStay: false, // set if the symbol will stay in the field after the user exits the field. 
            symbolPosition: 'left' // use this setting to position the symbol at the left or right side of the value. default 'left'
        });



        function loadEdit() {
            $('#divPackage').modal({
                detachable: true,
                observeChanges: true,
                onHide: function () {
                    clearCheck();
                }
            }).modal('show').modal('refresh');
        }

        function doCustomPost() {
            GetSelectedItem();
            document.getElementById('<%=hidActive.ClientID %>').value = "";
            var modalPackageName = document.getElementById("<%=txtPackageName.ClientID%>");
            var modalPackagePrice = document.getElementById("<%=txtPrice.ClientID%>");

            document.getElementById('<%=hidPackageName.ClientID %>').value = modalPackageName.value;
            document.getElementById('<%=hidPrice.ClientID %>').value = modalPackagePrice.value;

            if (document.getElementById('<%=chkIsActive.ClientID %>').checked)
                document.getElementById('<%=hidActive.ClientID %>').value = "true";
            else
                document.getElementById('<%=hidActive.ClientID %>').value = "false";

            clearCheck();
            $('.modal').modal('hide');

        }

        function clearCheck() {
            var CHK = document.getElementById("<%=chkFeatures.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");
            var label = CHK.getElementsByTagName("label");
            for (var i = 0; i < checkbox.length; i++) {
                checkbox[i].checked = false;
            }
        }


        function GetSelectedItem() {
            document.getElementById("<%=hidFeatures.ClientID%>").value = '';
            var CHK = document.getElementById("<%=chkFeatures.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");
            var label = CHK.getElementsByTagName("label");
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    document.getElementById("<%=hidFeatures.ClientID%>").value += label[i].innerHTML + ',';
                }
            }
        }

        function showAlert() {
            $("#success-alert").show().delay(5000).fadeOut();
        }
    </script>
</asp:Content>
