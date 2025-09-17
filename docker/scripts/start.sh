dotnet tool install --global dotnet-ef
dotnet tool install --global dotnet-aspnet-codegenerator
export PATH="$PATH:/root/.dotnet/tools"


cd /csharp/workspace/ApiCatalogo && \
dotnet add package Microsoft.EntityFrameworkCore.Design && \
dotnet add package Microsoft.EntityFrameworkCore.Sqlite && \
dotnet add package AutoMapper --version 12.0.1 && \
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.0.1

echo "PATH -> $PATH"
echo ""
echo "container started"
tail -f /dev/null