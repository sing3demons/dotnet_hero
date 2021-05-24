# dotnet_hero

Gen self cert 
$dotnet dev-certs https —trust

Docker => Microsoft SQL Server

$ docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Passw0rd1234!' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-CU10-ubuntu-20.04

ติดตั้งdotnet ef
https://docs.microsoft.com/en-us/ef/core/cli/dotnet

For windown
dotnet ef dbcontext scaffold "Server=localhost,1433;user id=sa; password=Passw0rd1234!; Database=istock;" Microsoft.EntityFrameworkCore.SqlServer -o Entities -c DatabaseContext --context-dir Data

For Mac
dotnet ef dbcontext scaffold ‘Server=localhost,1433;user id=sa; password=Passw0rd1234!; Database=istock;’ Microsoft.EntityFrameworkCore.SqlServer -o Entities -c DatabaseContext --context-dir Data
