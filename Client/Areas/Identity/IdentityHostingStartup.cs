﻿using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Client.Areas.Identity.IdentityHostingStartup))]
namespace Client.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}