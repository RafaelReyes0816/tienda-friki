# Tienda Friki - Backend

Backend del proyecto **Tienda Friki** desarrollado con **ASP.NET Core 8**, **Entity Framework Core** y **PostgreSQL**.

En esta fase se implementa la base del dominio, persistencia y API REST inicial:

- Modelos de negocio (`Usuario`, `Producto`, `Categoria`, `Carrito`, `ItemCarrito`, `Pedido`, `DetallePedido`)
- `DBContext` con relaciones y restricciones
- Migraciones de Entity Framework Core
- Repositorios (`GetAll` / `Add`) y controladores por recurso (mismo estilo que el proyecto de referencia para productos)

## Tecnologias

- .NET 8 (SDK)
- ASP.NET Core Web API
- Entity Framework Core 8
- Npgsql (PostgreSQL provider)
- PostgreSQL (local o remoto)

## Estructura principal

- `Models/`: entidades del dominio
- `Data/`: `DBContext`
- `Repositories/`: interfaces e implementaciones (acceso a datos vía EF)
- `Controllers/`: API REST (inyeccion de repositorios, sin usar `DbContext` directo en controladores)
- `Migrations/`: historial de migraciones de EF Core
- `Program.cs`: configuracion de servicios y pipeline

## Endpoints (GET listar / POST crear)

Cada ruta expone `GET` (todos los registros) y `POST` (alta). Probar desde Swagger (`/swagger`).

| Ruta | Recurso |
|------|---------|
| `/api/productos` | Productos |
| `/api/categorias` | Categorias |
| `/api/usuarios` | Usuarios |
| `/api/carritos` | Carritos |
| `/api/pedidos` | Pedidos |
| `/api/items-carrito` | Items del carrito |
| `/api/detalles-pedido` | Detalles de pedido |

## Pruebas en Swagger (POST)

Swagger muestra un **JSON de ejemplo muy grande** porque el modelo incluye **navegaciones** (objetos y listas relacionadas). **No hace falta rellenar todo**: deja solo los campos de esta sección y **borra** objetos anidados como `categoria`, `productos`, `usuario`, `items`, `detalles`, etc. Si aparece `id` en el alta, usa **`0`** (lo genera la base).

**Orden sugerido** (primero lo que no depende de otras tablas): `Categorias` → `Usuarios` → `Productos` → `Carritos` → `Pedidos` / `ItemsCarrito` / `DetallesPedido` (estos tres necesitan ids que ya existan).

### Categorias — `POST /api/categorias`

| Campo | Obligatorio | Notas |
|-------|-------------|--------|
| `nombre` | Si | Texto, ej. `"Manga"` |
| `id` | No | `0` si Swagger lo muestra |
| `productos` | No | Borrar o `[]` |

Ejemplo minimo:

```json
{ "nombre": "Manga" }
```

### Usuarios — `POST /api/usuarios`

| Campo | Obligatorio | Notas |
|-------|-------------|--------|
| `nombre` | Si | |
| `email` | Si | Formato email |
| `telefono` | Si | |
| `contrasena` | Si | En produccion se hashea; aqui texto de prueba |
| `rol` | Si | Ej. `"Cliente"` |
| `fechaRegistro` | Segun Swagger | Si exige fecha, una fecha ISO valida |
| `id` | No | `0` |
| `pedidos`, `carrito` | No | Borrar |

### Productos — `POST /api/productos`

Antes debe existir una **categoria** (`categoriaId`).

| Campo | Obligatorio | Notas |
|-------|-------------|--------|
| `nombre` | Si | |
| `precio` | Si | Decimal |
| `franquicia` | Si | Texto |
| `oferta`, `destacado` | Si | `true` / `false` |
| `stock` | Si | Entero >= 0 |
| `categoriaId` | Si | Id de una categoria creada antes |
| `imagen` | No | URL o vacio |
| `id` | No | `0` |
| `categoria`, `itemsCarrito`, `detallesPedido` | No | Borrar |

### Carritos — `POST /api/carritos`

Antes debe existir un **usuario** (`usuarioId`).

| Campo | Obligatorio | Notas |
|-------|-------------|--------|
| `usuarioId` | Si | Id de usuario existente |
| `fechaCreacion` | Segun Swagger | Si exige fecha, fecha valida |
| `id` | No | `0` |
| `usuario`, `items` | No | Borrar |

### Pedidos — `POST /api/pedidos`

Antes debe existir un **usuario** (`usuarioId`).

| Campo | Obligatorio | Notas |
|-------|-------------|--------|
| `codigo` | Si | Ej. `"PED-001"` (conveniente que sea unico) |
| `estado` | Si | Ej. `"Pendiente"` |
| `total` | Si | Decimal |
| `fecha` | Segun Swagger | Fecha del pedido |
| `usuarioId` | Si | Id de usuario existente |
| `id` | No | `0` |
| `usuario`, `detalles` | No | Borrar |

### Items del carrito — `POST /api/items-carrito`

Antes deben existir **carrito** y **producto**.

| Campo | Obligatorio | Notas |
|-------|-------------|--------|
| `cantidad` | Si | Entero > 0 |
| `carritoId` | Si | Id de carrito existente |
| `productoId` | Si | Id de producto existente |
| `id` | No | `0` |
| `carrito`, `producto` | No | Borrar |

### Detalles de pedido — `POST /api/detalles-pedido`

Antes deben existir **pedido** y **producto**.

| Campo | Obligatorio | Notas |
|-------|-------------|--------|
| `cantidad` | Si | Entero > 0 |
| `precioUnitario` | Si | Decimal |
| `subtotal` | Si | Suele ser cantidad x precio unitario |
| `pedidoId` | Si | Id de pedido existente |
| `productoId` | Si | Id de producto existente |
| `id` | No | `0` |
| `pedido`, `producto` | No | Borrar |

Si un `POST` falla, revisa que los **ids foraneos** (`categoriaId`, `usuarioId`, etc.) apunten a filas que **ya esten** en la base.

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
