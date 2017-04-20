using System;
using System.Text.RegularExpressions;
using Nancy;

public class BaseModule : NancyModule {
	public BaseModule() {
		Before += ctx => {
			String subdomainPattern = @"https?://(www[.])?(?<subdomain>\w+)[.]ludeman[.]com";
			String url = ctx.Request.Url;

			Match match = Regex.Match(url, subdomainPattern);
			String subdomain = match.Groups["subdomain"].Value;

			if (String.IsNullOrEmpty(subdomain)) {
				Response response = new Response();
				response.StatusCode = Nancy.HttpStatusCode.NotFound;

				return response;
			}

			ctx.Parameters.subdomain = subdomain;
			return null;
		};
	}
}
