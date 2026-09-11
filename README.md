# 🛠️ ITSM Field Service & Ticket Automation Platform

[![.NET Version](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Blazor](https://img.shields.io/badge/Blazor-Interactive_Server-512BD4?logo=blazor&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?logo=docker&logoColor=white)](https://www.docker.com/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?logo=bootstrap&logoColor=white)](https://getbootstrap.com/)
[![Render](https://img.shields.io/badge/Deploy-Render.com-46E3B7?logo=render&logoColor=black)](https://render.com/)

Aplicação web moderna desenvolvida em **Blazor Interactive Server (.NET 10)** para padronização, agilização e automação do fluxo de atendimento de suporte técnico em campo (Field Service) e gestão de inventário de ativos corporativos de TI.

---

## 🎯 Objetivo do Projeto

Eliminar gargalos no preenchimento manual de chamados técnicos corporativos, integrando busca ultrarrápida de hardware, geração automática de templates padronizados de **Abertura** e **Encerramento**, fluxo dinâmico de **Substituição de Equipamentos (Swaps)** e histórico consolidado com cópia em 1 clique para ferramentas de ITSM (ServiceNow, Jira Service Desk, Zendesk, Remedy, etc.).

---

## ✨ Funcionalidades Principais

- ⚡ **Registro Inteligente de Chamados**: Preenchimento ágil de falha, ação corretiva, solicitante, localização física (Galpão / Coluna / Setor) e contatos.
- 🔍 **Lookup Instantâneo de Hardware em Memória**:
  - Mecanismo em memória de alta performance capaz de indexar dezenas de milhares de ativos de hardware.
  - Busca instantânea por **Número de Série**, **Etiqueta Patrimonial (Asset Tag / CI)** ou **Hostname**.
  - Auto-preenchimento automático dos dados técnicos do dispositivo ao selecionar o ativo.
- 🔄 **Gestão Dinâmica de Substituições (Hardware Swap)**:
  - Comparativo entre o equipamento recolhido e o novo equipamento instalado.
  - Busca assistida para auto-preencher tanto o item recolhido quanto o instalado.
- 📋 **Templates Padronizados com 1-Click Copy**:
  - Geração automática e padronizada dos blocos textuais de **Abertura** e **Fechamento**.
  - Botões de cópia rápida com feedback visual instantâneo para colar direto no ticket do ITSM.
- 📊 **Painéis de Resumo e Histórico da Sessão**:
  - Visão geral das atividades realizadas no plantão.
  - Tabela de registros detalhados com filtros e exportação rápida.
- 🐳 **Pronto para Nuvem (Container-Native)**:
  - Arquitetura leve empacotada em contêiner Docker multi-stage build.
  - Deploy em 1 clique em provedores como **Render.com**, Azure App Service, AWS ou Google Cloud Run.

---

## 🏗️ Arquitetura & Tecnologias

- **Framework**: [.NET 10](https://dotnet.microsoft.com/)
- **UI / Frontend**: Blazor Interactive Server (C# + Razor Components)
- **Estilização**: Bootstrap 5 + CSS3 Moderno
- **Armazenamento / Serviços**: 
  - `HardwareService`: Singleton de consulta e indexação em memória com carregamento assíncrono.
  - `AtividadeStateService`: Gerenciador reativo de estado dos atendimentos e histórico de sessão.
- **Contêiner**: Docker (Multi-stage build com runtime leve Alpine/Linux)

---

## 🚀 Como Rodar Localmente

### Pré-requisitos
- [.NET 10 SDK](https://dotnet.microsoft.com/download) instalado na máquina.

### Executando
1. Clone o repositório:
   ```bash
   git clone https://github.com/andradelipe/AberturaFechamentoBlazor.git
   cd AberturaFechamentoBlazor
   ```

2. Restaure as dependências e inicie a aplicação:
   ```bash
   dotnet run --launch-profile http
   ```

3. Acesse no seu navegador:
   ```
   http://localhost:5144
   ```

---

## 🐳 Como Rodar com Docker

Para compilar e rodar a imagem localmente:

```bash
# Construir a imagem Docker
docker build -t abertura-fechamento-blazor .

# Iniciar o container na porta 10000
docker run -d -p 10000:10000 --name field-service-app abertura-fechamento-blazor
```

Acesse em `http://localhost:10000`.

---

## ☁️ Deploy no Render.com

Este projeto já está 100% configurado para rodar no **Render**:

1. Acesse o [dashboard.render.com](https://dashboard.render.com/) e clique em **New + ➔ Web Service**.
2. Conecte este repositório do GitHub.
3. Configure os campos:
   - **Environment / Runtime**: `Docker` (detectado automaticamente pelo `Dockerfile`).
   - **Instance Type**: `Free`.
4. Clique em **Create Web Service**.
5. O Render construirá e publicará a aplicação automaticamente a cada novo `git push`.

---

## 📄 Licença

Projeto distribuído sob a licença [MIT](LICENSE).
