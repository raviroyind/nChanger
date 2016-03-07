<%@ Page Title="" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="DefineEligiblityCriteria.aspx.cs" Inherits="nChanger.WebUI.Admin.DefineEligiblityCriteria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-2.2.0.js"></script>
    <script src="//cdn.tinymce.com/4/tinymce.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui warning message fluid" style="display: none;" id="success-alert">
        <asp:Label ID="lblMsg" Style="font-size: 1.2em; font-weight: bold;" runat="server"></asp:Label>
    </div>


    <div class="ui grid">
        <div class="one wide column"></div>
        <div class="fourteen wide column">
            <div class="ui small breadcrumb">
                <a href="dashboard.aspx" class="section">Home</a>
                <i class="right chevron icon divider"></i>
                <a href="EligiblityCriteria.aspx" class="active section">Define Eligibility Criteria</a>
            </div>
            <h3 class="ui header orange" id="lblCaption" runat="server"></h3>
        </div>
        <div class="one wide column"></div>

        <div class="ui large form container">
            <div class="ui stacked segment frmPad">
                <div class="ui form">
                    <div class="one wide column"></div>
                    <div class="fourteen wide column">
                        <div class="ui dividing header orange">Define Criteria</div>
                        
                        <div class="one wide column"></div>
                    <div class="fourteen wide column">
                        <asp:ValidationSummary runat="server" DisplayMode="BulletList" ShowMessageBox="False" CssClass="frmErrors" ShowSummary="True" />
                    </div>
                    <div class="one wide column"></div>
                        <br/>

                        <div class="field">
                            <label>Criteria Heading</label>
                            <div class="field">
                                <asp:TextBox runat="server" ID="txtCriteriaHeading" MaxLength="200"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ForeColor="White" CssClass="ui field error"
                                    ControlToValidate="txtCriteriaHeading" ErrorMessage="Please enter Criteria Heading." Text="!"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="one wide column"></div>

                    <div class="one wide column"></div>
                    <div class="fourteen wide column">
                        <asp:TextBox runat="server" ID="txtCriteria" CssClass="myTextEditor" MaxLength="10000" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="one wide column"></div>
                    <div class="ui dividing header"></div>
                    <div class="one wide column"></div>
                    <div class="fourteen wide column">
                        <asp:LinkButton runat="server" ID="btnSubmit" OnClick="btnSubmit_OnClick" CssClass="ui right floated button">
                    <i class="plus icon large green"></i>
                    Submit
                        </asp:LinkButton>
                    </div>
                    <br/>
                    <div class="one wide column"></div>

                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="../Scripts/semantic.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            tinymce.init({
                selector: 'textarea',
                theme: 'modern',

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

        function showAlert() {
            document.body.scrollTop = document.documentElement.scrollTop = 0;
            $("#success-alert").show().delay(5000).fadeOut();
            $('.ui.fluid.search.selection.dropdown').dropdown();
        }
    </script>
</asp:Content>
