# Lab 11: Monitor services that are deployed to Azure (Monitor and optimize Azure solutions)

## Commands

- `az group create --name "MonitoredAssets" --location "westus"`
- InstrumentationKey: `a17ecf35-b542-4458-99b7-922341ac54cf`
- Connection String: `InstrumentationKey=a17ecf35-b542-4458-99b7-922341ac54cf;IngestionEndpoint=https://northeurope-2.in.applicationinsights.azure.com/;LiveEndpoint=https://northeurope.livediagnostics.monitor.azure.com/`
- URL: `https://smpapipkolosov.azurewebsites.net`
- URL: `https://smpapipkolosov.azurewebsites.net/weatherforecast`
- `dotnet new webapi --output . --name SimpleApi --framework net6.0`
- `dotnet add package Microsoft.ApplicationInsights --version 2.20.0`
- `dotnet add package Microsoft.ApplicationInsights.AspNetCore --version 2.20.0`
- `dotnet add package Microsoft.ApplicationInsights.PerfCounterCollector --version 2.20.0`
- `dotnet add package Microsoft.ApplicationInsights.Profiler.AspNetCore --version 2.4.0`
- `dotnet dev-certs https --trust`
- `Compress-Archive -Path * -DestinationPath api.zip`
- `Connect-AzAccount`
- `Get-AzWebApp -ResourceGroupName MonitoredAssets`
- `Get-AzWebApp -ResourceGroupName MonitoredAssets | Where-Object {$_.Name -like 'smpapi*'}`
- `Get-AzWebApp -ResourceGroupName MonitoredAssets | Where-Object {$_.Name -like 'smpapi*'} | Select-Object -ExpandProperty Name`
- `$webAppName = (Get-AzWebApp -ResourceGroupName MonitoredAssets | Where-Object {$_.Name -like 'smpapi*'})[0] | Select-Object -ExpandProperty Name`
- `Publish-AzWebApp -ResourceGroupName MonitoredAssets -Name $webAppName -ArchivePath "api.zip"`
- `dotnet add package Microsoft.Extensions.Logging.ApplicationInsights --version 2.20.0`
- Get logs: `traces | order by timestamp desc`
- `az group delete --name MonitoredAssets`