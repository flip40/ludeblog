using System;

public class HelloModule : BaseModule {
	public HelloModule() {
		Get["/{name?World}"] = parameters => {
			return String.Format("Hello, {0}! You are on the '{1}' subdomain.\n", parameters.name, parameters.subdomain);
		};
	}
}
