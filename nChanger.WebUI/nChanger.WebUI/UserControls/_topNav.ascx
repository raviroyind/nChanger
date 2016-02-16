<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="_topNav.ascx.cs" Inherits="nChanger.WebUI.UserControls._topNav" %>
<link href="../Content/login.css" rel="stylesheet" />
 <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.js"></script> 
 <nav>
  <ul>
    <li id="login" runat="server">
      <a id="login-trigger" href="#">
        <span>▼</span>
      </a>
      <div id="login-content">
        <div>
          <asp:HyperLink runat="server" ID="hypLogOut" NavigateUrl="../index.aspx?id=lg"><i class="sign out icon orange large"></i>Logout </asp:HyperLink>
            <hr/>

             <asp:HyperLink runat="server" ID="hypProfile" NavigateUrl="../Secured/Profile.aspx"><i class="setting icon orange large"></i>Profile </asp:HyperLink>
        </div>
      </div>                 
    </li>
    
    <li id="signup">
       <asp:HyperLink runat="server" ID="hypUserInfo"> </asp:HyperLink>
    </li>
  </ul>
</nav>
<script type="text/javascript">
    $(document).ready(function () {
        $('#login-trigger').click(function () {
            $(this).next('#login-content').slideToggle();
            $(this).toggleClass('active');

            if ($(this).hasClass('active')) $(this).find('span').html('&#x25B2;');
            else $(this).find('span').html('&#x25BC;');
        })
    });
</script>
