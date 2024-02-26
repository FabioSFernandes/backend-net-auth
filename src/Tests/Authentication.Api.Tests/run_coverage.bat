rem dotnet test --collect:"XPlat Code Coverage"
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura

