# Tienda Friki - Backend

Backend del proyecto **Tienda Friki** desarrollado con **ASP.NET Core 8**, **Entity Framework Core** y **PostgreSQL**.

En esta fase se implementa la base del dominio y persistencia:

- Modelos de negocio (`Usuario`, `Producto`, `Categoria`, `Carrito`, `ItemCarrito`, `Pedido`, `DetallePedido`)
- `DBContext` con relaciones y restricciones
- Migraciones de Entity Framework Core

## Tecnologias

- .NET 8 (SDK)
- ASP.NET Core Web API
- Entity Framework Core 8
- Npgsql (PostgreSQL provider)
- PostgreSQL (local o remoto)

## Estructura principal

- `Models/`: entidades del dominio
- `Data/`: `DBContext`
- `Migrations/`: historial de migraciones de EF Core
- `Program.cs`: configuracion de servicios y pipeline

## Requisitos para levantar el proyecto

- Tener instalado **.NET SDK 8**
- Tener acceso a una instancia de **PostgreSQL**
- Tener `dotnet-ef` disponible

Si no tienes `dotnet-ef`, instala la herramienta global:

```bash
dotnet tool install --global dotnet-ef
```

Si ya la tienes:

```bash
dotnet tool update --global dotnet-ef
```

## Restaurar y ejecutar en otro equipo

1. Clonar repositorio y entrar al proyecto:

```bash
git clone <url-del-repo>
cd tienda-friki
```

2. Restaurar dependencias:

```bash
dotnet restore
```

3. Crear archivo de configuracion local `appsettings.json` (este archivo esta ignorado por Git para no subir conexiones):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5433;Database=Tienda-Friki;Username=postgres;Password=TU_PASSWORD"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

4. Aplicar migraciones a la base de datos:

```bash
dotnet ef database update
```

5. Levantar la API:

```bash
dotnet run
```

6. Probar Swagger:

- Abrir la URL que aparezca en consola (normalmente `https://localhost:<puerto>/swagger`)

## Comandos utiles

- Crear nueva migracion:

```bash
dotnet ef migrations add NombreMigracion
```

- Aplicar migraciones pendientes:

```bash
dotnet ef database update
```

- Eliminar ultima migracion (solo si aun no aplicaste cambios en BD):

```bash
dotnet ef migrations remove
```

## Notas importantes

- `appsettings.json` no se versiona para proteger credenciales y configuraciones locales.
- Carpetas de build (`bin/`, `obj/`) y otros artefactos pesados tambien estan excluidos por `.gitignore`.
- Si compartes el proyecto con tu equipo, cada integrante debe crear su propio `appsettings.json` con su conexion local.
