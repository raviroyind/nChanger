<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="frmCriminalOffenceInformation.aspx.cs" Inherits="nChanger.WebUI.Forms.frmCriminalOffenceInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-2.2.0.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="one wide column"></div>
    <div class="fourteen wide column">
        <div class="ui small breadcrumb">
            <a href="../Secured/dashboard.aspx" class="active section"></a>
            <%--   <i class="right chevron icon divider"></i>--%>
            <a href="#" class="section">Forms</a>
            <i class="right chevron icon divider"></i>
            <div class="active section">
            </div>
        </div>
    </div>
    <div class="one wide column"></div>

    <div class="ui large form container fluid" style="padding-left: 2%; padding-right: 2%;">
        <div class="ui stacked segment frmPad">
            <div class="ui form">
                <h4 class="ui dividing header">Are you aware of any outstanding court proceedings against you, other than outstanding criminal charges?</h4>
                <div class="field">
                   <%--<label >Are you aware of any outstanding court proceedings against you, other than outstanding criminal charges?</label>--%>
                    <div class="two fields">
                        <div class="field">
                            <div class="inline fields">
                                <div>
                                    <asp:RadioButton ID="rdOutstandingCourtProceedingsYes" CssClass="ui radio checkbox" GroupName="R1" Checked="False" runat="server" Text="Yes" />
                                </div>
                                <div>
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rdOutstandingCourtProceedingsNo" CssClass="ui radio checkbox" GroupName="R1" Checked="True" runat="server" Text="No" />
                                </div>
                            </div>
                        </div>
                        <div class="field">
                               <label>Court file number</label>
                            <div class="field">
                                  <asp:TextBox runat="server" ID="txtCourtFileNumber" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                 <h4 class="ui dividing"></h4>
                <div class="field">
                    <div class="fields">
                        <div class="four wide field">
                            <label>Court name</label>
                            <asp:TextBox runat="server" ID="txtCourtName"></asp:TextBox>
                        </div>
                        <div class="twelve wide field">
                            <label>Address of court</label>
                             <asp:TextBox runat="server" ID="TextBox1"></asp:TextBox>
                        </div>
                    </div>
                </div>
                 <h4 class="ui dividing"></h4>
                <div class="field">
                    <label>Describe proceedings</label>
                       <asp:TextBox runat="server" ID="txtDescribeProceedings" TextMode="MultiLine" Rows="5"></asp:TextBox>
                </div>
                 <h4 class="ui dividing"></h4>
            </div>
           </div>
        
        <div class="ui stacked segment frmPad">
            <div class="ui form">
                <h4 class="ui dividing header">Are you aware of any outstanding court proceedings against you, other than outstanding criminal charges?</h4>
                <div class="field">
                   <%--<label >Are you aware of any outstanding court proceedings against you, other than outstanding criminal charges?</label>--%>
                    <div class="three fields">
                        <div class="field">
                            <div class="two fields">
                                  <div class="field">
                                    <asp:RadioButton ID="RadioButton1"  GroupName="R1" Checked="False" runat="server" Text="Yes" />
                            </div>     
                            <div class="field">      
                                 
                                    <asp:RadioButton style="margin-left: 100px;" ID="RadioButton2" GroupName="R1" Checked="True" runat="server" Text="No" />
                               
                            </div>
                              </div>
                        </div>
                        <div class="field">
                               <label>Court file number</label>
                            <div class="field">
                                  <asp:TextBox runat="server" ID="TextBox2" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                 <h4 class="ui dividing"></h4>
                <div class="field">
                    <div class="fields">
                        <div class="four wide field">
                            <label>Court name</label>
                            <asp:TextBox runat="server" ID="TextBox3"></asp:TextBox>
                        </div>
                        <div class="twelve wide field">
                            <label>Address of court</label>
                             <asp:TextBox runat="server" ID="TextBox4"></asp:TextBox>
                        </div>
                    </div>
                </div>
                 <h4 class="ui dividing"></h4>
                <div class="field">
                    <label>Describe proceedings</label>
                       <asp:TextBox runat="server" ID="TextBox5" TextMode="MultiLine" Rows="5"></asp:TextBox>
                </div>
                 <h4 class="ui dividing"></h4>
            </div>
           </div>
    </div>
    <script type="text/javascript" src="../Scripts/semantic.min.js"></script>
    <script type="text/javascript">
        $('.ui.checkbox').checkbox();
    </script>
</asp:Content>
