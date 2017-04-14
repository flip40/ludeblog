using System;

public class HelloModule : Nancy.NancyModule {
	public HelloModule() {
		Get["/"] = parameters => "Hello World";
	}
}
