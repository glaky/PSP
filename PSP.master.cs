using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace PSP
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = ConfigurationManager.AppSettings["GeneralTitle"];
            HtmlGenericControl myJs = new HtmlGenericControl();
            myJs.TagName = "script";
            myJs.Attributes.Add("type", "text/javascript");
            myJs.Attributes.Add("language", "javascript"); //don't need it usually but for cross browser.
            myJs.Attributes.Add("src", ResolveUrl("jslib/utils.js"));
            this.Page.Header.Controls.Add(myJs);

        }
    }
}
