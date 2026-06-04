<div align="center">

# 🛰️ Satellite Emergency Hub

### Plataforma de Gerenciamento de Desastres Monitorados por Satélite

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-336791?style=for-the-badge&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?style=for-the-badge&logo=docker&logoColor=white)](https://www.docker.com/)
[![JWT](https://img.shields.io/badge/Auth-JWT-000000?style=for-the-badge&logo=jsonwebtokens)](https://jwt.io/)
[![Swagger](https://img.shields.io/badge/Docs-Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)](https://swagger.io/)

> Central inteligente para recebimento, organização e monitoramento de dados relacionados a desastres naturais e eventos climáticos extremos monitorados por satélite.
>
> Disciplina: **DevOps Tools & Cloud Computing** — FIAP

</div>

---

## 📋 Índice

- [Descrição da Solução](#-descrição-da-solução)
- [🖼️ Arquitetura Macro — Diagrama Visual](#%EF%B8%8F-arquitetura-macro--diagrama-visual)
- [Diagrama de Entidades e Relacionamentos](#-diagrama-de-entidades-e-relacionamentos)
- [Tecnologias e Infraestrutura](#-tecnologias-e-infraestrutura)
- [Estrutura dos Containers](#-estrutura-dos-containers)
- [Pré-requisitos](#-pré-requisitos)
- [Variáveis de Ambiente](#-variáveis-de-ambiente)
- [How To — Tutorial Completo](#-how-to--tutorial-completo)
- [Evidências de Persistência no Banco](#-evidências-de-persistência-no-banco)
- [Verificação dos Containers](#-verificação-dos-containers)
- [Estrutura de Pastas do Projeto](#-estrutura-de-pastas-do-projeto)
- [Autores](#-autores)

---

## 🌍 Descrição da Solução

O **Satellite Emergency Hub** é uma API REST desenvolvida em **.NET 10** que simula uma plataforma operacional utilizada por órgãos de defesa civil e centros de monitoramento climático para acompanhar situações críticas em tempo real.

A plataforma gerencia o ciclo completo de um desastre natural monitorado por satélite:

- **Regiões** — áreas geográficas monitoradas com coordenadas e raio de cobertura
- **Sensores** — pontos de coleta de dados (pluviométrico, sísmico, temperatura, etc.) vinculados a regiões
- **Ocorrências** — eventos de desastre registrados com tipo, severidade e ciclo de vida
- **Alertas** — notificações geradas a partir de ocorrências ativas
- **Equipes de Emergência** — times despachados para atender ocorrências (relacionamento N:N)

A solução é conteinerizada com **Docker Compose**, executando a API .NET e o banco de dados PostgreSQL em containers isolados na mesma rede Docker, com persistência de dados via volume nomeado e credenciais protegidas por variáveis de ambiente.

O **CRUD completo** está implementado em todas as entidades, com dados persistidos em **6 tabelas relacionadas** no PostgreSQL.

---

## 🖼️ Arquitetura Macro — Diagrama Visual

> ⚠️ **Esta seção contém o diagrama de arquitetura exigido pela disciplina.**
> O desenho foi produzido no **Draw.io / Azure Diagram Tool (Visual Paradigm)** e representa
> o fluxo completo da solução em nuvem: do usuário até a persistência dos dados nos containers.

<!-- ============================================================
     INSTRUÇÃO: substitua a linha abaixo pelo caminho da sua imagem
     após exportar o diagrama do Draw.io ou Visual Paradigm.

     Salve o arquivo exportado como:
         docs/architecture.png

     E substitua o bloco abaixo por:
         ![Arquitetura Macro](docs/architecture.png)

     O diagrama deve conter obrigatoriamente:
       - Ícone de usuário/browser fazendo a requisição HTTP
       - VM ou ambiente cloud envolvendo tudo
       - Docker network (satellite-network) visível
       - Container da aplicação .NET com porta 8080
       - Container do PostgreSQL com porta 5432
       - Volume nomeado satellite-pgdata conectado ao banco
       - Setas de fluxo com legendas de protocolo/porta
       - Legendas e rótulos em todos os componentes
============================================================ -->

**📌 COLOQUE AQUI A IMAGEM DO DIAGRAMA:**

```
[ Exportar o diagrama do Draw.io como PNG e salvar em /docs/architecture.png ]
[ Depois substituir este bloco por: ![Arquitetura Macro](docs/architecture.png) ]
```

> 🔗 Ferramentas utilizadas:
> - [Draw.io](https://app.diagrams.net)
> - [Azure Diagram Tool — Visual Paradigm](https://online.visual-paradigm.com/diagrams/features/azure-architecture-diagram-tool/)

### Componentes representados no diagrama

| Componente | Descrição |
|---|---|
| 👤 Usuário / Cliente | Acessa a API via Swagger ou Postman pela porta `8080` |
| ☁️ Cloud / VM | Ambiente de execução dos containers |
| 🐳 Docker Network `satellite-network` | Rede bridge isolada que conecta app e banco |
| 📦 Container `satellite-app-rm555125` | API REST em .NET 10, porta `8080`, usuário `satelliteuser` |
| 🗄️ Container `satellite-db-rm555125` | PostgreSQL 16, porta `5432` |
| 💾 Volume `satellite-pgdata` | Persistência dos dados do banco entre reinicializações |

---

## 🗄️ Diagrama de Entidades e Relacionamentos

O banco de dados possui **6 tabelas** com os seguintes relacionamentos:

```
┌──────────────────┐         ┌──────────────────┐
│      Region      │         │      Sensor      │
├──────────────────┤  1 : N  ├──────────────────┤
│ Id (PK)          │◄────────│ Id (PK)          │
│ Name             │         │ Name             │
│ Country          │         │ Type (enum)      │
│ State            │         │ Status (enum)    │
│ Latitude         │         │ Latitude         │
│ Longitude        │         │ Longitude        │
│ RadiusKm         │         │ RegionId (FK)    │
│ IsActive         │         │ CreatedAt        │
│ CreatedAt        │         │ UpdatedAt        │
│ UpdatedAt        │         └──────────────────┘
└────────┬─────────┘
         │ 1 : N
         ▼
┌──────────────────┐         ┌──────────────────┐
│   Occurrence     │  1 : N  │      Alert       │
├──────────────────┤────────►├──────────────────┤
│ Id (PK)          │         │ Id (PK)          │
│ Title            │         │ Title            │
│ Description      │         │ Message          │
│ Type (enum)      │         │ Level (enum)     │
│ Severity (enum)  │         │ Status (enum)    │
│ Status (enum)    │         │ OccurrenceId(FK) │
│ RegionId (FK)    │         │ CreatedAt        │
│ CreatedAt        │         │ UpdatedAt        │
│ UpdatedAt        │         └──────────────────┘
└────────┬─────────┘
         │ N : N  (via tabela pivot)
         ▼
┌──────────────────────────────┐
│   EmergencyTeamOccurrence    │  ← Tabela pivot do N:N
├──────────────────────────────┤
│ EmergencyTeamId (PK, FK)     │
│ OccurrenceId    (PK, FK)     │
│ AssignedAt                   │
│ Notes                        │
└──────────┬───────────────────┘
           │
           ▼
┌──────────────────┐
│  EmergencyTeam   │
├──────────────────┤
│ Id (PK)          │
│ Name             │
│ Specialization   │
│ ContactPhone     │
│ Status (enum)    │
│ CreatedAt        │
│ UpdatedAt        │
└──────────────────┘
```

| Relação | Tipo | Descrição |
|---|---|---|
| Region → Sensor | 1:N | Uma região possui vários sensores |
| Region → Occurrence | 1:N | Uma região registra várias ocorrências |
| Occurrence → Alert | 1:N | Uma ocorrência gera vários alertas |
| EmergencyTeam ↔ Occurrence | N:N | Equipes despachadas para ocorrências via pivot |

---

## 🛠️ Tecnologias e Infraestrutura

| Tecnologia | Versão | Papel |
|---|---|---|
| .NET / ASP.NET Core | 10.0 | Framework da API REST |
| Entity Framework Core | 9.x | ORM e migrations |
| PostgreSQL | 16-alpine | Banco de dados relacional |
| Docker | 24+ | Containerização |
| Docker Compose | v2 | Orquestração dos containers |
| JWT Bearer | - | Autenticação e autorização |
| BCrypt.Net | - | Hash seguro de senhas |
| Swagger / Swashbuckle | - | Documentação interativa da API |
| DotNetEnv | - | Carregamento do `.env` em desenvolvimento local |

---

## 🐳 Estrutura dos Containers

### Container da Aplicação — `satellite-app-rm555125`

| Configuração | Valor |
|---|---|
| Imagem base (build) | `mcr.microsoft.com/dotnet/sdk:10.0` |
| Imagem base (runtime) | `mcr.microsoft.com/dotnet/aspnet:10.0` |
| Estratégia de build | Multi-stage (SDK → Runtime) |
| Usuário de execução | `satelliteuser` (**não-root**) |
| Diretório de trabalho | `/app` |
| Porta exposta | `8080` |
| Variáveis de ambiente | `DB_HOST`, `DB_PORT`, `DB_NAME`, `DB_USER`, `DB_PASSWORD`, `JWT_KEY`, `JWT_ISSUER`, `ASPNETCORE_URLS` |
| Rede | `satellite-network` |

### Container do Banco de Dados — `satellite-db-rm555125`

| Configuração | Valor |
|---|---|
| Imagem | `postgres:16-alpine` (pública, sem Dockerfile) |
| Porta exposta | `5432` |
| Variáveis de ambiente | `POSTGRES_DB`, `POSTGRES_USER`, `POSTGRES_PASSWORD` |
| Volume nomeado | `satellite-pgdata` → `/var/lib/postgresql/data` |
| Rede | `satellite-network` |
| Health check | `pg_isready` a cada 10s — app só sobe após banco estar pronto |

### Rede e Volume

| Recurso | Nome | Tipo | Finalidade |
|---|---|---|---|
| Rede Docker | `satellite-network` | bridge | Comunicação isolada entre containers |
| Volume de persistência | `satellite-pgdata` | nomeado | Dados do banco sobrevivem ao `docker-compose down` |

---

## ✅ Pré-requisitos

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) instalado e em execução
- [Git](https://git-scm.com/)

> Não é necessário ter .NET ou PostgreSQL instalados localmente. O build da aplicação acontece inteiramente dentro do Docker.

---

## 🔒 Variáveis de Ambiente

As credenciais **nunca ficam expostas** no código-fonte ou no `docker-compose.yml`. Tudo é lido de variáveis de ambiente definidas no arquivo `.env`, que está listado no `.gitignore`.

| Arquivo | Vai ao GitHub? | Finalidade |
|---|---|---|
| `.env` | ❌ **Nunca** | Credenciais reais do seu ambiente |
| `.env.example` | ✅ Sim | Template documentando as variáveis necessárias |
| `appsettings.json` | ✅ Sim | Configurações da aplicação, sem nenhum segredo |

### Variáveis necessárias

| Variável | Descrição | Observação |
|---|---|---|
| `DB_HOST` | Host do banco | Em Docker: nome do container do banco |
| `DB_PORT` | Porta do PostgreSQL | Padrão: `5432` |
| `DB_NAME` | Nome do banco de dados | |
| `DB_USER` | Usuário do banco | |
| `DB_PASSWORD` | Senha do banco | |
| `JWT_KEY` | Chave secreta JWT | Mínimo 32 caracteres |
| `JWT_ISSUER` | Identificador do emissor JWT | |

---

## 🚀 How To — Tutorial Completo

Siga os passos abaixo desde o clone do repositório até a execução e testes em nuvem.

---

### Passo 1 — Clone o repositório

```bash
git clone https://github.com/seu-usuario/SatelliteEmergencyHub.git
cd SatelliteEmergencyHub
```

---

### Passo 2 — Configure as variáveis de ambiente

```bash
cp .env.example .env
```

Abra o `.env` e preencha com seus valores:

```env
DB_HOST=satellite-db-rm555125
DB_PORT=5432
DB_NAME=satellite_emergency_hub
DB_USER=postgres
DB_PASSWORD=sua_senha_segura

JWT_KEY=SatelliteEmergencyHub_SuperSecretKey_2024_MinimumOf32Chars!
JWT_ISSUER=SatelliteEmergencyHub
```

> ⚠️ O `DB_HOST` deve ser o nome exato do container do banco definido no `docker-compose.yml`. O Docker Compose resolve esse nome automaticamente na rede interna.

---

### Passo 3 — Suba os containers em background

```bash
docker-compose up -d --build
```

O `depends_on` com `healthcheck` garante que a API só sobe depois que o PostgreSQL estiver pronto para aceitar conexões. As migrations são aplicadas automaticamente na inicialização.

Verifique se os dois containers estão em execução:

```bash
docker ps
```

Saída esperada:

```
CONTAINER ID   IMAGE                        STATUS          PORTS                    NAMES
xxxxxxxxxxxx   satelliteemergencyhub-app    Up 30 seconds   0.0.0.0:8080->8080/tcp   satellite-app-rm555125
xxxxxxxxxxxx   postgres:16-alpine           Up 35 seconds   0.0.0.0:5432->5432/tcp   satellite-db-rm555125
```

---

### Passo 4 — Visualize os logs dos containers

**Logs da aplicação .NET:**

```bash
docker logs satellite-app-rm555125
```

**Logs do banco de dados PostgreSQL:**

```bash
docker logs satellite-db-rm555125
```

---

### Passo 5 — Acesse o Swagger e teste a API

Abra no navegador:

```
http://localhost:8080/swagger
```

#### 5.1 — Registre um usuário

```http
POST /api/auth/register
Content-Type: application/json

{
  "name": "Operador DevOps",
  "email": "devops@hub.com",
  "password": "Senha@123"
}
```

#### 5.2 — Faça login e copie o token JWT

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "devops@hub.com",
  "password": "Senha@123"
}
```

Clique em **Authorize 🔒** no Swagger e cole o token retornado.

#### 5.3 — Crie uma Região (tabela 1)

```http
POST /api/regions
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Vale do Paraíba",
  "country": "Brasil",
  "state": "São Paulo",
  "latitude": -22.9068,
  "longitude": -43.1729,
  "radiusKm": 150.0,
  "isActive": true
}
```

#### 5.4 — Crie um Sensor vinculado à Região (tabela 2)

```http
POST /api/sensors
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Sensor Pluviométrico SP-01",
  "type": 1,
  "status": 0,
  "latitude": -22.9105,
  "longitude": -43.1730,
  "regionId": "{id-retornado-no-passo-5.3}"
}
```

> **Enum `SensorType`**: `0` = Seismic, `1` = Rainfall, `2` = Temperature, `3` = Satellite, `4` = Wind

#### 5.5 — Registre uma Ocorrência (tabela 3)

```http
POST /api/occurrences
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "Enchente no Vale do Paraíba",
  "description": "Nível do rio subiu 3 metros acima do normal.",
  "type": 0,
  "severity": 2,
  "status": 0,
  "regionId": "{id-retornado-no-passo-5.3}"
}
```

> **Enum `OccurrenceType`**: `0` = Flood, `1` = Wildfire, `2` = Landslide, `3` = Storm
> **Enum `OccurrenceSeverity`**: `0` = Low, `1` = Moderate, `2` = Severe, `3` = Catastrophic

#### 5.6 — Emita um Alerta (tabela 4)

```http
POST /api/alerts
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "ALERTA CRÍTICO: Risco de inundação",
  "message": "Evacuar áreas de risco imediatamente.",
  "level": 2,
  "occurrenceId": "{id-retornado-no-passo-5.5}"
}
```

> **Enum `AlertLevel`**: `0` = Info, `1` = Warning, `2` = Critical, `3` = Emergency

#### 5.7 — Cadastre uma Equipe de Emergência (tabela 5)

```http
POST /api/emergencyteams
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Bombeiros SP - Grupo Alfa",
  "specialization": "Resgate em enchentes",
  "contactPhone": "+55 11 99999-0000",
  "status": 0
}
```

#### 5.8 — Despache a Equipe para a Ocorrência (tabela 6 — N:N)

```http
POST /api/emergencyteams/{id-da-equipe}/occurrences
Authorization: Bearer {token}
Content-Type: application/json

{
  "occurrenceId": "{id-da-ocorrencia}",
  "notes": "Equipe principal de resgate aquático."
}
```

---

## 🔍 Evidências de Persistência no Banco

Execute os SELECTs **conectando diretamente no container do banco**, conforme exigido pela disciplina.

### Acesse o container do banco

```bash
docker container exec -it satellite-db-rm555125 psql -U postgres -d satellite_emergency_hub
```

### Queries de evidência — execute uma a uma

```sql
-- Lista todas as tabelas criadas pelas migrations
\dt

-- Evidência 1: Usuários cadastrados
SELECT id, "Name", "Email", "Role", "CreatedAt" FROM "Users";

-- Evidência 2: Regiões monitoradas
SELECT id, "Name", "Country", "State", "IsActive" FROM "Regions";

-- Evidência 3: Sensores vinculados às regiões (JOIN)
SELECT s.id, s."Name", s."Type", s."Status", r."Name" AS "Region"
FROM "Sensors" s
JOIN "Regions" r ON s."RegionId" = r."Id";

-- Evidência 4: Ocorrências registradas
SELECT id, "Title", "Type", "Severity", "Status", "RegionId"
FROM "Occurrences";

-- Evidência 5: Alertas emitidos (JOIN)
SELECT a.id, a."Title", a."Level", a."Status", o."Title" AS "Occurrence"
FROM "Alerts" a
JOIN "Occurrences" o ON a."OccurrenceId" = o."Id";

-- Evidência 6: Equipes de emergência
SELECT id, "Name", "Specialization", "Status" FROM "EmergencyTeams";

-- Evidência 7: Tabela pivot N:N — equipes despachadas para ocorrências
SELECT
  et."Name"      AS "Team",
  o."Title"      AS "Occurrence",
  eto."AssignedAt",
  eto."Notes"
FROM "EmergencyTeamOccurrences" eto
JOIN "EmergencyTeams" et ON eto."EmergencyTeamId" = et."Id"
JOIN "Occurrences"     o ON eto."OccurrenceId"    =  o."Id";

-- Sair do psql
\q
```

---

## 🖥️ Verificação dos Containers

### Container da Aplicação

```bash
docker container exec -it satellite-app-rm555125 sh
```

Dentro do container:

```sh
pwd       # deve retornar: /app
ls -l     # lista os arquivos publicados da aplicação
whoami    # deve retornar: satelliteuser  (não-root — requisito da disciplina)
exit
```

### Container do Banco de Dados

```bash
docker container exec -it satellite-db-rm555125 sh
```

Dentro do container:

```sh
pwd       # diretório atual do container
ls -l     # estrutura de arquivos
whoami    # usuário conectado no container do banco
exit
```

---

## 📁 Estrutura de Pastas do Projeto

```
SatelliteEmergencyHub/
├── SatelliteEmergencyHub.slnx
├── docker-compose.yml                          # Orquestração dos dois containers
├── .env                                        # ❌ Não vai ao GitHub (credenciais reais)
├── .env.example                                # ✅ Template de variáveis — vai ao GitHub
├── .gitignore
├── docs/
│   └── architecture.png                        # ← DIAGRAMA DE ARQUITETURA (Draw.io/VP)
│
├── API/
│   ├── Dockerfile                              # Build multi-stage da imagem da aplicação
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── RegionsController.cs
│   │   ├── SensorsController.cs
│   │   ├── OccurrencesController.cs
│   │   ├── AlertsController.cs
│   │   └── EmergencyTeamsController.cs
│   ├── Extensions/
│   │   └── ServiceExtensions.cs
│   ├── Middleware/
│   │   └── ErrorHandlingMiddleware.cs
│   ├── appsettings.json
│   └── Program.cs
│
├── Application/
│   ├── DTOs/
│   │   ├── Request/
│   │   └── Response/
│   └── Services/
│       ├── Interfaces/
│       └── Implementations/
│
├── SatelliteEmergencyHub.Domain/
│   ├── Entities/
│   └── Enums/
│
└── SatelliteEmergencyHub.Infrastructure/
    ├── Data/
    │   ├── AppDbContext.cs
    │   └── AppDbContextFactory.cs
    ├── Migrations/
    └── Repositories/
        ├── Interfaces/
        └── Implementations/
```

---

## 👥 Autores

| Nome | RM | Turma |
|---|---|---|
| Mateus | RM555125 | 2TDSPV |

---

## 📄 Disciplina

Este projeto foi desenvolvido para a entrega da disciplina **DevOps Tools & Cloud Computing** — FIAP.

---

<div align="center">
  <sub>Desenvolvido com ☕ e muito Docker</sub>
</div>
