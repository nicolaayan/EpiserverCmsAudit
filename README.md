# Audit for Optimizely CMS
This module adds visualisation of how Optimizely content types and audiences are used across sites.

![Sites](/Docs/sites.png)

![Page Types](/Docs/page-types.png)

![Block Types](/Docs/block-types.png)

![Audiences](/Docs/audiences.png)

# Installation
Make sure you have the Optimizely [Nuget feed](https://api.nuget.optimizely.com/v3/index.json) configured.

Add the package to your solution
```
install-package N1990.Episerver.Cms.Audit
```
or
```
dotnet add package N1990.Episerver.Cms.Audit
```

Configure services and routing during startup (without this, you will not see module under Add-ons in the UI)
```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        //...
        services.AddAuditServices();
        //...
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //...
        app.UseEndpoints(endpoints =>
        {
            
            // other endpoint configuration
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
        //...
    }
   
}
```
# Security
You will need to be a member of one of these roles to get access to the module:
 * AuditAdmins
 * CmsAdmins

# Sandbox
This repository also have a Sandbox project, based on the Alloy demo site. You should be able to run it directly in your development environment.

In order to log in, you need to create an admin user. Go to: https://localhost:5000/util/register to create your admin user.
