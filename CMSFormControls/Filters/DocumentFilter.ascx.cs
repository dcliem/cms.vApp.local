using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.Controls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using CMS.ExtendedControls;

public partial class CMSFormControls_Filters_DocumentFilter : CMSAbstractBaseFilterControl
{
    #region "Variables"

    private bool mAllowSiteAutopostback = true;
    private bool mIncludeSiteCondition = false;
    private bool mLoadSites = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets show button.
    /// </summary>
    public LocalizedButton ShowButton
    {
        get
        {
            return btnShow;
        }
    }


    /// <summary>
    /// Gets sites placeholder.
    /// </summary>
    public PlaceHolder SitesPlaceHolder
    {
        get
        {
            return plcSites;
        }
    }


    /// <summary>
    /// Gets document name path placeholder.
    /// </summary>
    public PlaceHolder PathPlaceHolder
    {
        get
        {
            return plcPath;
        }
    }


    /// <summary>
    /// Gets class placeholder.
    /// </summary>
    public PlaceHolder ClassPlaceHolder
    {
        get
        {
            return plcClass;
        }
    }


    /// <summary>
    /// Determines whether to load site selector.
    /// </summary>
    public bool LoadSites
    {
        get
        {
            return mLoadSites;
        }
        set
        {
            mLoadSites = value;
        }
    }


    /// <summary>
    /// Determines whether to include condition for site to the resulting WHERE condition.
    /// </summary>
    public bool IncludeSiteCondition
    {
        get
        {
            return mIncludeSiteCondition;
        }
        set
        {
            mIncludeSiteCondition = value;
        }

    }


    /// <summary>
    /// Determines whether the site selector DDL has autopostback option on or off.
    /// </summary>
    public bool AllowSiteAutopostback
    {
        get
        {
            return mAllowSiteAutopostback;
        }
        set
        {
            mAllowSiteAutopostback = value;
        }
    }


    /// <summary>
    /// Returns selected site name.
    /// </summary>
    public string SelectedSite
    {
        get
        {
            Control postbackControl = ControlsHelper.GetPostBackControl(Page);
            return DataHelper.GetNotEmpty((postbackControl == btnShow) ? siteSelector.Value : ViewState["SelectedSite"], TreeProvider.ALL_SITES);
        }
        private set
        {
            ViewState["SelectedSite"] = value;
        }
    }


    /// <summary>
    /// Where condition.
    /// </summary>
    public override string WhereCondition
    {
        get
        {
            Control postbackControl = ControlsHelper.GetPostBackControl(Page);
            return DataHelper.GetNotEmpty((postbackControl == btnShow) ? CreateWhereCondition(base.WhereCondition) : ViewState["WhereCondition"], string.Empty);
        }
        set
        {
            base.WhereCondition = value;
            ViewState["WhereCondition"] = value;
        }
    }


    /// <summary>
    /// Determines whether filter is set.
    /// </summary>
    public bool FilterIsSet
    {
        get
        {
            return nameFilter.FilterIsSet || classFilter.FilterIsSet || (SelectedSite != TreeProvider.ALL_SITES);
        }
    }

    #endregion


    #region "Delegates and events"

    public event EventHandler OnSiteSelectionChanged;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        siteSelector.DropDownSingleSelect.Width = new Unit(305);

        if (LoadSites)
        {
            LoadSiteSelector();
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Loads the drop-down list with all the available sites.
    /// </summary>
    private void LoadSiteSelector()
    {
        CurrentUserInfo user = CMSContext.CurrentUser;

        // Get all user sites
        if (user.IsGlobalAdministrator)
        {
            // Set site selector
            siteSelector.DropDownSingleSelect.AutoPostBack = AllowSiteAutopostback;
            if (AllowSiteAutopostback)
            {
                siteSelector.UniSelector.OnSelectionChanged += OnSiteSelectionChanged;
            }

            // All options only if global admin has access to Site manager
            if (!user.UserSiteManagerDisabled)
            {
                siteSelector.AllowAll = false;
                siteSelector.UniSelector.SpecialFields = new string[1, 2] { { ResHelper.GetString("general.selectall"), TreeProvider.ALL_SITES } };
            }

            // Preselect all
            if (!RequestHelper.IsPostBack())
            {
                siteSelector.Value = TreeProvider.ALL_SITES;
            }
        }
        else
        {
            plcSites.Visible = false;
        }
    }


    private string CreateWhereCondition(string originalWhere)
    {
        string where = originalWhere;
        // Add where conditions from filters
        string classCondition = classFilter.WhereCondition;
        if (!string.IsNullOrEmpty(classCondition))
        {
            classCondition = string.Format("NodeClassID IN (SELECT ClassID FROM CMS_Class WHERE {0})", classCondition);
        }
        where = SqlHelperClass.AddWhereCondition(where, classCondition);
        where = SqlHelperClass.AddWhereCondition(where, nameFilter.WhereCondition);

        if (IncludeSiteCondition && !string.IsNullOrEmpty(siteSelector.SiteName) && (siteSelector.SiteName != TreeProvider.ALL_SITES))
        {
            where = SqlHelperClass.AddWhereCondition(where, "SiteName = '" + SqlHelperClass.GetSafeQueryString(siteSelector.SiteName, false) + "'");
        }
        return where;
    }

    #endregion


    #region "Control events"

    protected void btnShow_Click(object sender, EventArgs e)
    {
        WhereCondition = CreateWhereCondition(base.WhereCondition);
        SelectedSite = DataHelper.GetNotEmpty(siteSelector.Value, TreeProvider.ALL_SITES);
    }

    #endregion
}
