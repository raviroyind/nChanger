﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="selectPackage.aspx.cs" Inherits="nChanger.WebUI.SelectPackage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <link href="Content/packageStyle.css" rel="stylesheet" />
  <div style="padding-left: 10%;">
        <div class="one wide column"></div>
        <div class="fourteen wide column bordered round-corners">
            <div class="ui ordered steps">
                <div class="active step">
                    <i class="list icon small green"></i>
                    <div class="content">
                        <div class="title">Select Package</div>
                        <div class="description">Choose a suitable package.</div>
                    </div>
                </div>
                <div class="disabled step">
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
    <div class="one wide column"></div>
    <div class="fourteen wide column bordered round-corners">
        <div class="container stackable responsiveContentSlider">
            <div class="row" id="rowPackage" runat="server">
                <div id="divWrapper" runat="server" class='wrapper'>
                    
                </div>
            </div>
        </div>
    </div>

    <div class="one wide column"></div>


</asp:Content>

