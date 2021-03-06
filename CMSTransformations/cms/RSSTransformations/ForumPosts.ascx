﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.Controls.CMSAbstractTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.Controls" Assembly="CMS.Controls" %><%@ Register TagPrefix="cc1" Namespace="CMS.Controls" Assembly="CMS.Controls" %><item>
  <guid isPermaLink="false"><%# Eval("PostGUID") %></guid>
  <title><%# EvalCDATA("PostSubject") %></title>
  <description><![CDATA[<strong><%# EvalCDATA("PostUserName",false) %></strong><br /><%# RemoveDynamicControls(CMSContext.ResolveDiscussionMacros(ValidationHelper.GetString(EvalCDATA("PostText",false),""), EvalInteger("ForumSiteID"))) %>]]></description>
  <pubDate><%# GetRSSDateTime(Eval("PostTime")) %></pubDate>
  <link><![CDATA[<%# GetAbsoluteUrl(GetForumPostUrlForFeed(Eval("PostIDPath"),EvalInteger("PostForumID")), EvalInteger("ForumSiteID")) %>]]></link>
</item>