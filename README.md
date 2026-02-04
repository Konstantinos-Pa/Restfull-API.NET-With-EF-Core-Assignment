The packets i use are:

🤖 AI / OpenAI

OpenAI (2.8.0)
Official OpenAI SDK (non-Azure)

🗺️ Object Mapping

Mapster (7.4.0)
Lightweight object-to-object mapper (DTOs ↔ entities)

🔐 Authentication & Identity

Microsoft.AspNetCore.Authentication.JwtBearer (8.0.22)
JWT bearer authentication for APIs

Microsoft.AspNetCore.Identity.EntityFrameworkCore (8.0.22)
ASP.NET Core Identity with EF Core backing store

🧱 Entity Framework Core

Microsoft.EntityFrameworkCore (8.0.22)
Core EF runtime

Microsoft.EntityFrameworkCore.Design (8.0.22)
EF Core design-time support (migrations, scaffolding)
(PrivateAssets = all → not shipped to production)

Microsoft.EntityFrameworkCore.Tools (8.0.22)
EF CLI tools (dotnet ef)
(PrivateAssets = all → dev-only)

🐘 PostgreSQL

Npgsql (8.0.8)
PostgreSQL ADO.NET provider

Npgsql.EntityFrameworkCore.PostgreSQL (8.0.11)
EF Core provider for PostgreSQL

🪵 Logging (Serilog)

Serilog.AspNetCore (8.0.3)
Serilog integration with ASP.NET Core

Serilog.Enrichers.Environment (3.0.1)
Adds machine name, environment, etc.

Serilog.Enrichers.Process (3.0.0)
Adds process ID / name

Serilog.Enrichers.Thread (4.0.0)
Adds thread ID
