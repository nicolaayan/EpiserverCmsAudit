@echo off
cls

echo Creating the module zip file..
powershell -command "Compress-Archive -Path ..\N1990.Episerver.Cms.Audit\modules\_protected\CmsAudit\Views, ..\N1990.Episerver.Cms.Audit\modules\_protected\CmsAudit\module.config -DestinationPath ..\N1990.Episerver.Cms.Audit\Modules\_protected\CmsAudit\CmsAudit -CompressionLevel Optimal -Force"
echo.

echo Creating NuGet package..
echo.
nuget pack N1990.Episerver.Cms.Audit.nuspec 
