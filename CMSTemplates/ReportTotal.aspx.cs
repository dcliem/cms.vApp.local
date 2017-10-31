﻿using CMS.UIControls;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CMSTemplates_ReportTotal : TemplatePage {
    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            CbExportMode.Items.AddRange(Enum.GetNames(typeof(GridViewDetailExportMode)));
            CbExportMode.Text = GridViewDetailExportMode.Expanded.ToString();
            DeStart.Date = Convert.ToDateTime("01/01/" + DateTime.Now.Year + " 00:00");
            DeEnd.Date = Convert.ToDateTime("31/12/" + DateTime.Now.Year + " 00:00");
        }
    }
    protected void UpdateExportMode() {
        GvLevelA.SettingsDetail.ExportMode = (GridViewDetailExportMode)Enum.Parse(typeof(GridViewDetailExportMode), CbExportMode.Text);
    }
    protected void btnXlsExport_Click(object sender, EventArgs e) {
        UpdateExportMode();
        GvExport.WriteXlsToResponse(string.Format("ReportTotal {0} - {1}", DeStart.Date.ToShortDateString(), DeEnd.Date.ToShortDateString()), new XlsExportOptionsEx() { ExportType = ExportType.WYSIWYG });
    }
    protected void btnXlsxExport_Click(object sender, EventArgs e) {
        UpdateExportMode();
        GvExport.WriteXlsxToResponse(string.Format("ReportTotal {0} - {1}", DeStart.Date.ToShortDateString(), DeEnd.Date.ToShortDateString()), new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
    } 
    protected void OnCustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e) {
        if (e.Column.Caption == "Num")
            e.DisplayText = string.Format("{0:#,###}", e.VisibleRowIndex + 1);
    }
    protected void GvLevelAA_BeforePerformDataSelect(object sender, EventArgs e) {
        Session["ProjectTaskID"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }
    protected void On_DateChanged(object sender, EventArgs e) {
        GvLevelA.DataBind();
    }
    protected void GvLevelA_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e) {
        if ((int)e.GetValue("ProjectTaskStatusID") == 3)
            e.Cell.ForeColor = ColorTranslator.FromHtml("#F77E0E");
        else if ((int)e.GetValue("ProjectTaskStatusID") == 7)
            e.Cell.ForeColor = ColorTranslator.FromHtml("#0FAA15");
        else if ((int)e.GetValue("ProjectTaskStatusID") == 6)
            e.Cell.ForeColor = ColorTranslator.FromHtml("#FF0000");
    }
}