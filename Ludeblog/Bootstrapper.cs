#if DEBUG

using System;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Ludeblog {
	public class CustomBootstrapper : DefaultNancyBootstrapper {
		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines) {
			base.ApplicationStartup(container, pipelines);
			StaticConfiguration.DisableErrorTraces = false;
			Console.WriteLine("Bootstrapper enabled!!");
		}
	}
}

#endif
