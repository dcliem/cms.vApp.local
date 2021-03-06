﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.Controls.CMSAbstractTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.Controls" Assembly="CMS.Controls" %><%@ Register TagPrefix="cc1" Namespace="CMS.Controls" Assembly="CMS.Controls" %><div style="margin-bottom: 30px;">
        <%-- Search result title --%>
        <div>
            <a style="font-weight: bold" href='<%# SearchResultUrl(true) %>'>
                <%#SearchHighlight(CMS.GlobalHelper.HTMLHelper.HTMLEncode(CMS.ExtendedControls.ControlsHelper.RemoveDynamicControls(DataHelper.GetNotEmpty(Eval("Title"), "/"))), "<span style='font-weight:bold;'>", "</span>")%>
            </a>
        </div>
        <%-- Search result content --%>
        <div style="margin-top: 5px; width: 590px;">
            <%#SearchHighlight(CMS.GlobalHelper.HTMLHelper.HTMLEncode(TextHelper.LimitLength(HttpUtility.HtmlDecode(CMS.GlobalHelper.HTMLHelper.StripTags(CMS.ExtendedControls.ControlsHelper.RemoveDynamicControls(GetSearchedContent(DataHelper.GetNotEmpty(Eval("Content"), ""))), false, " ")), 280, "...")), "<span style='background-color: #FEFF8F'>", "</span>")%><br />
        </div>
        <%-- Relevance, URL, Creattion --%>
        <div style="margin-top: 5px;">
            <%-- Relevance --%>
            <div title="Relevance: <%# Convert.ToInt32(ValidationHelper.GetDouble(Eval("Score"),0.0)*100)%>%"
                style="width: 50px; border: solid 1px #aaaaaa; margin-top: 7px; margin-right: 6px; float: left; color: #0000ff; font-size: 2pt; line-height: 4px; height: 4px;">
                <div style='<%# "background-color:#a7d3a7;width:"+ Convert.ToString(Convert.ToInt32((ValidationHelper.GetDouble(Eval("Score"),0.0)/2)*100))  + "px;height:4px;line-height: 4px;"%>'>
                </div>
            </div>
            <%-- URL --%>
            <span style="color: #008000">
                <%# SearchHighlight(SearchResultUrl(true),"<strong>","</strong>")%>
            </span>
            <%-- Creation --%>
            <span style="color: #888888; font-size: 9pt">
                <%# GetDateTimeString(ValidationHelper.GetDateTime(Eval("Created"), DateTimeHelper.ZERO_TIME), true) %>
            </span>
        </div>
    </div>