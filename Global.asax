<%@ Application Language="C#" %>

<script runat="server">
	void Application_BeginRequest(Object sender, EventArgs e)
	{
		if (Request.Url.Scheme != "https" && Request.Url.Host.EndsWith("studienserver.at"))
			Response.Redirect("https://studienserver.at/PSP/");
	}
</script>