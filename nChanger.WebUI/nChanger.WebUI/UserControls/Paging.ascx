<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Paging.ascx.cs" Inherits="nChanger.WebUI.Paging" %>
<script type="text/javascript">

    function AllowKeyPress(objEvent, obj, obj1) {

        var bt = document.getElementById('<%= btnPageNo.ClientID %>');

        var count = document.getElementById('<%= lblTotPages.ClientID %>').innerText.replace("of ", "");

        var iKeyCode, strKey;

        var iKeyCode = objEvent.keyCode || objEvent.which;

        if (iKeyCode == 13) {
            if (parseInt(obj.value) > parseInt(count) || obj.value == '' || obj.value < 1) {
                alert('There is no page number \' ' + obj.value + ' \' ');
                return false;
            }
            else {
                if (typeof bt == 'object') {

                    if (typeof obj1 == 'object')
                        obj1.value = obj.value

                    bt.click();
                    return false;
                }
            }
        }
        else if (!(iKeyCode >= 48 && iKeyCode <= 57))
            return false;
    }

</script>
<div class="ui right">
        <table class="ui table stackable" width="100%">
            <tr>
                <td id="tdNav" runat="server" nowrap="nowrap" style="padding: 0px 0px 0px 0px" class="ui align right">
                    <table class="gridHead" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkimgbtnFirst" CausesValidation="false" CommandArgument="First" ToolTip="First" OnClick="imgbtn_Click"
                                    runat="server" ><i aria-hidden="true" class="fast backward icon blue large"></i>
                                </asp:LinkButton>
                                &nbsp;
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkimgbtnPrevious" CausesValidation="false" CommandArgument="Previous" ToolTip="Previous" OnClick="imgbtn_Click"
                                    runat="server" ><i aria-hidden="true" class="backward icon blue large"></i>
                                </asp:LinkButton>
                                &nbsp;
                            </td>
                            <td class="row2" style="padding: 0px 0px 8px 0px">
                                <asp:Label ID="lblPage" runat="server" Text="Page" Font-Bold="true"></asp:Label>&nbsp;
                                <asp:TextBox ID="txtPageNo" Width="40px" CausesValidation="false" CssClass="ui text" Style="padding-top: 5px;font-weight:bold;" MaxLength="4" Height="30" runat="server"></asp:TextBox>&nbsp;<asp:Label
                            ID="lblTotPages" runat="server" Font-Bold="true"></asp:Label>&nbsp;
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkimgbtnNext" CausesValidation="false" CommandArgument="Next" ToolTip="Next" OnClick="imgbtn_Click"
                                    runat="server" CssClass=""><i aria-hidden="true" class="forward icon blue large"></i>
                                </asp:LinkButton>
                                &nbsp;
                            </td>
                            <td>
                                 <asp:LinkButton ID="lnkimgbtnLast" CausesValidation="false" CommandArgument="Last" ToolTip="Last" OnClick="imgbtn_Click"
                                    runat="server" CssClass="btn btn-default"><i aria-hidden="true" class="fast forward icon blue large"></i>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="right"   id="tdNoOfRecords" runat="server">

                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="padding-top: 0px;">
                                <asp:Label ID="lblRecords" runat="server" CssClass="pagerLabel"></asp:Label>
                            </td>
                            <td>
                                <div class="clear" style="width: 15px"></div>
                            </td>
                            <td valign="middle">
                                <asp:Label ID="lblNoOfRecords" runat="server" Text="Show per page&nbsp;"></asp:Label><asp:DropDownList
                                    ID="ddlNoOfRecords" Width="60" runat="server" AutoPostBack="true" Font-Size="11px" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                </td>
                <asp:HiddenField ID="hdnTxtPageNo" EnableViewState="true" runat="server" />
                <asp:Button ID="btnPageNo" runat="server" Text="pageno" OnClick="btnPageNo_Click" CausesValidation="false" />
            </tr>
        </table>
    
</div>
