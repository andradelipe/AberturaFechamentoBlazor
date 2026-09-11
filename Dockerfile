# ==========================================
# 1. Etapa de Compilacao (SDK)
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# Copia o csproj e restaura as dependencias
COPY ["AberturaFechamentoBlazor.csproj", "./"]
RUN dotnet restore "AberturaFechamentoBlazor.csproj"

# Copia o restante dos arquivos (inclusive alm_hardware.csv e componentes)
COPY . .
RUN dotnet publish "AberturaFechamentoBlazor.csproj" -c Release -o /app/publish

# ==========================================
# 2. Etapa de Execucao (Runtime leve)
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY --from=build /src/alm_hardware.csv .

# O Render escuta por padrao na porta 10000
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "AberturaFechamentoBlazor.dll"]
