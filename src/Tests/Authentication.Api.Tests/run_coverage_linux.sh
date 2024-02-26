#!/bin/bash

# Executa os testes e coleta a cobertura de código
dotnet test --collect:"XPlat Code Coverage"

# Encontra o arquivo de cobertura gerado
coverageFile=$(find . -name 'coverage.cobertura.xml' | head -n 1)

if [ -z "$coverageFile" ]; then
    echo "Arquivo de cobertura não encontrado."
    exit 1
fi

echo "Arquivo de cobertura encontrado: $coverageFile"

# Gera o relatório com o reportgenerator
reportgenerator -reports:"$coverageFile" -targetdir:"coveragereport" -reporttypes:Html

echo "Relatório de cobertura gerado em: coveragereport"