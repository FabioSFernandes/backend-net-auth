@echo off
SETLOCAL EnableDelayedExpansion

:: Executa os testes e coleta a cobertura de código

:: testador interno do .net core (não gera relatório de cobertura)
::dotnet test --collect:"Code Coverage" --settings:Custom.runsettings

:: A seguinte linha ativa o uso do Coverlet 
dotnet test --collect:"XPlat Code Coverage" --settings:Custom.runsettings

:: Define o caminho inicial para a busca dos arquivos de cobertura
SET COVERAGE_PATH=TestResults

:: Encontra o arquivo de cobertura gerado, assumindo o formato padrão do nome do arquivo
FOR /R %COVERAGE_PATH% %%F IN (*.cobertura.xml) DO (
    SET COVERAGE_FILE=%%F
)

:: Verifica se o arquivo de cobertura foi encontrado
IF NOT DEFINED COVERAGE_FILE (
    echo Arquivo de cobertura não encontrado.
    GOTO :end
)

echo Arquivo de cobertura encontrado: !COVERAGE_FILE!

:: Gera o relatório com o reportgenerator
reportgenerator "-reports:!COVERAGE_FILE!" "-targetdir:coveragereport" "-reporttypes:Html"

echo Relatório de cobertura gerado em: coveragereport

explorer "coveragereport\index.html"

:end
ENDLOCAL
