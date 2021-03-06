using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Compatibility;

public partial class CMSModules_Licenses_Pages_License_New : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        // Setup page title text and image
        this.CurrentMaster.Title.TitleText = ResHelper.GetString("Licenses_License_New.Title");
        this.CurrentMaster.Title.TitleImage = "~/App_Themes/" + this.Theme + "/Images/CMSSiteManager/Licenses/licenses.gif";

        this.CurrentMaster.Title.HelpTopicName = "new_license";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        if (ddlEdition.Items.Count < 1)
        {
            ddlEdition.Items.Clear();
            ddlEdition.Items.Add("Free");
            ddlEdition.Items.Add("Standard");
            ddlEdition.Items.Add("Base");
            ddlEdition.Items.Add("SmallBusiness");
            ddlEdition.Items.Add("Ecommerce");
            ddlEdition.Items.Add("Professional");
            ddlEdition.Items.Add("Professional3x");
            ddlEdition.Items.Add("Enterprise");
            ddlEdition.Items.Add("Ultimate");
        }

        rfvLicenseKey.ErrorMessage = "Please enter some domain (for example: mydomain.com)";
        btnOK.Text = ResHelper.GetString("general.ok");

        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = ResHelper.GetString("Licenses_License_New.Licenses");
        pageTitleTabs[0, 1] = "~/CMSSiteManager/Licenses/License_List.aspx";
        pageTitleTabs[1, 0] = ResHelper.GetString("licenses_license_new.new");
        pageTitleTabs[1, 1] = "";
        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        lblError.Text = "";

        try
        {
            LicenseKeyInfo lk = new LicenseKeyInfo();
            lk.LoadLicense(this.get_lic(), "");

            if (lk != null)
            {
                switch (lk.ValidationResult)
                {
                    case LicenseValidationEnum.Expired:
                        lblError.Text = ResHelper.GetString("Licenses_License_New.LicenseNotValid.Expired");
                        break;

                    case LicenseValidationEnum.Invalid:
                        lblError.Text = ResHelper.GetString("Licenses_License_New.LicenseNotValid.Invalid");
                        break;

                    case LicenseValidationEnum.NotAvailable:
                        lblError.Text = ResHelper.GetString("Licenses_License_New.LicenseNotValid.NotAvailable");
                        break;

                    case LicenseValidationEnum.WrongFormat:
                        lblError.Text = ResHelper.GetString("Licenses_License_New.LicenseNotValid.WrongFormat");
                        break;

                    case LicenseValidationEnum.Valid:
                        // try to store license into database
                        try
                        {
                            LicenseKeyInfoProvider.SetLicenseKeyInfo(lk);
                            CMS.SiteProvider.UserInfoProvider.ClearLicenseValues();
                            Functions.ClearHashtables();
                            lblInfo.Text = ResHelper.GetString("General.ChangesSaved");
                            lblInfo.Visible = true;
                        }
                        catch (Exception ex)
                        {
                            lblInfo.Visible = false;
                            lblError.Visible = true;
                            if (ex.Message == "[LicenseKeyInfoProvider.SetLicenseKeyInfo]: LicenseDomain already exists.")
                            {
                                lblError.Text = ResHelper.GetString("Licenses_License_New.DomainAlreadyExists").Replace("%%name%%", lk.Domain);
                            }
                        }
                        if (lblInfo.Visible)
                        {
                            UrlHelper.Redirect("License_List.aspx");
                        }
                        break;
                }
            }

            if (lblError.Text != "")
            {
                lblInfo.Visible = false;
                lblError.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblInfo.Visible = false;
            lblError.Visible = true;
            lblError.Text = ex.Message;
        }
    }

    private string get_lic()
    {
        string ret = "";
        if (!txtDomain.Text.StartsWith("http://"))
        {
            txtDomain.Text = "http://" + txtDomain.Text;
        }
        System.Uri udom = new Uri(txtDomain.Text);
        string dom = udom.Host;
        if (dom.StartsWith("www."))
        {
            dom = dom.Substring(4);
        }
        ret += "DOMAIN:" + dom + System.Environment.NewLine;
        ret += "PRODUCT:" + this.get_product() + System.Environment.NewLine;
        ret += "EXPIRATION:00000000" + System.Environment.NewLine;
        ret += "PACKAGES:ADV;ECM;SCN" + System.Environment.NewLine;
        ret += "SERVERS:0" + System.Environment.NewLine;
        ret += "LICENSE:UDTHVIETTRI" + System.Environment.NewLine;
        ret += this.ddlEdition.SelectedIndex.ToString();
        return ret;
    }

    private string get_product()
    {
        string ret = "";
        switch (this.ddlEdition.SelectedIndex)
        {
            case 0:
                ret = "CF06";
                break;
            case 1:
                ret = "CS06";
                break;
            case 2:
                ret = "CB06";
                break;
            case 3:
                ret = "CN06";
                break;
            case 4:
                ret = "CC06";
                break;
            case 5:
                ret = "CP06";
                break;
            case 6:
                ret = "CR06";
                break;
            case 7:
                ret = "CE06";
                break;
            case 8:
                ret = "CU06";
                break;
        }
        return ret;
    }

}
