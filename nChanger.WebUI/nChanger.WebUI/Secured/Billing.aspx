﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Billing.aspx.cs" Inherits="nChanger.WebUI.Secured.Billing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-2.2.0.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding-left: 10%;">
        <div class="one wide column"></div>
        <div class="fourteen wide column bordered round-corners">
            <div class="ui ordered steps">
                <div class="completed step">
                    <div class="content">
                        <div class="title">Select Package</div>
                        <div class="description">Choose a suitable package.</div>
                    </div>
                </div>
                <div class="active step">
                    <i class="payment icon small green"></i>
                    <div class="content">
                        <div class="title">Billing</div>
                        <div class="description">Enter billing information.</div>
                    </div>
                </div>
                <div class="disabled step">
                    <i class="help circle icon small green"></i>
                    <div class="content">
                        <div class="title">General Questions</div>
                        <div class="description">Answer General Questions.</div>
                    </div>
                </div>
                <div class="disabled step">
                    <i class="edit icon small green"></i>
                    <div class="content">
                        <div class="title">Form Specific Questions</div>
                        <div class="description">Answer Form Specific Questions.</div>
                    </div>
                </div>
                <div class="disabled step">
                    <div class="content">
                        <div class="title">Review & Download/ Email</div>
                        <div class="description">Review before Download/ Print/ Email.</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="one wide column"></div>
    </div>
    <h3 class="ui dividing header"></h3>
    <div class="ui grid">
  <div class="four column row">
    <div class="left floated column"></div>
    <div class="right floated column"></div>
  </div>
  <div class="row">
    <div class="three wide column"></div>
    <div class="eight wide column">
       
        <h3 class="ui dividing header orange">Select a Payment method.</h3>
      
          <div class="ui form">
  <div class="grouped fields">
      
    <div class="field">
      <div class="ui radio checkbox">
        <input type="radio" checked="checked"  name="example2">
        <label><i class="stripe icon massive blue"></i></label>
      </div>
    </div>
   <div class="field">
      <div class="ui radio checkbox disabled">
        <input type="radio" name="example2">
        <label><i class="paypal icon massive yellow disabled"></i></label>
      </div>
    </div>
  </div>
</div>
        

        <a id="ancPayment" runat="server" class="ui button massive green" href="GeneralQuestions.aspx">Proceed to Pay $39.00</a>
       <p></p>
         <p>
            <i class="stripe icon extra large orange"></i>
              <i class="paypal icon extra large blue"></i>
             <i class="visa icon extra large green"></i>
             <i class="mastercard icon extra large red"></i>
        </p>
    </div>
    <div class="five wide column"></div>
  </div>
</div>
</asp:Content>
