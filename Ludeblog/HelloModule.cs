using System;

public class HelloModule : Nancy.NancyModule {
	public HelloModule() {
		Get["/{name?World}"] = parameters => String.Format("Hello, {0}!", parameters.name);
	}
}
