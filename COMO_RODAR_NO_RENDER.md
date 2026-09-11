# Guia de Publicação no Render.com (Blazor .NET)

Este documento contém o passo a passo completo para publicar e rodar este projeto Blazor no **Render.com** de forma automática a partir do seu repositório no GitHub.

---

## 📌 Diferença do Python para o .NET no Render
- No Python, o Render possui interpretador nativo a partir do `requirements.txt`.
- No **.NET (C# / Blazor)**, a forma padrão e recomendada pelo Render é utilizando **Docker**. O Docker garante que o .NET SDK compile a aplicação e a execute em um contêiner leve e seguro, sem custos adicionais.

---

## 🚀 Passo a Passo para Publicação

### 1. Criar o arquivo `Dockerfile` na raiz do projeto
Crie um arquivo chamado exatamente `Dockerfile` (sem extensão) na raiz do projeto (`AberturaFechamentoBlazor/`):

```dockerfile
# ==========================================
# 1. Etapa de Compilação (SDK)
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# Copia o csproj e restaura as dependências
COPY ["AberturaFechamentoBlazor.csproj", "./"]
RUN dotnet restore "AberturaFechamentoBlazor.csproj"

# Copia o restante dos arquivos (inclusive alm_hardware.csv e componentes)
COPY . .
RUN dotnet publish "AberturaFechamentoBlazor.csproj" -c Release -o /app/publish

# ==========================================
# 2. Etapa de Execução (Runtime leve)
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS final
WORKDIR /app
COPY --from=build /app/publish .

# O Render escuta por padrão na porta 10000
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "AberturaFechamentoBlazor.dll"]
```

---

### 2. Criar o arquivo `.dockerignore` na raiz do projeto
Crie um arquivo chamado `.dockerignore` ao lado do `Dockerfile` para evitar o envio de arquivos temporários:

```text
bin/
obj/
.git/
.vs/
.idea/
```

---

### 3. Enviar as alterações para o GitHub
No terminal do seu projeto:

```bash
git add .
git commit -m "Configura Dockerfile e instrucoes para o Render"
git push origin main
```

---

### 4. Configurar no Painel do Render.com
1. Acesse sua conta em [render.com](https://render.com).
2. Clique no botão **New +** ➔ **Web Service**.
3. Conecte o repositório do GitHub deste projeto.
4. Preencha as opções básicas:
   - **Name**: Nome da sua aplicação (ex: `abertura-fechamento-blazor`).
   - **Region**: Qualquer uma (ex: Ohio / Frankfurt).
   - **Runtime / Environment**: Selecione **Docker** (se já houver o `Dockerfile`, ele seleciona sozinho).
   - **Instance Type**: **Free** (gratuito).
5. Clique em **Create Web Service**.

---

### 🔄 Como funcionam as atualizações automáticas?
Assim como no projeto Python:
- Toda vez que você fizer alterações no código, fizer o commit e der `git push`, o Render detectará o novo commit automaticamente, recompilará a imagem e colocará a nova versão no ar em instantes.
