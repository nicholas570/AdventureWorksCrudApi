# AdventureWorks CRUD API

A minimal .NET 10 API for learning Entity Framework Core. It exposes a full CRUD
surface over the `Production.Product` table of the **AdventureWorks2022** sample database.

The HTTP layer is complete; **the data access layer is intentionally left for you
to implement** as the exercise.

## Project layout

| File | Role |
|------|------|
| `Program.cs` | App startup: registers EF `AppDbContext`, the repository, and the endpoints |
| `Models/Product.cs` | EF entity mapped to `Production.Product` |
| `Data/AppDbContext.cs` | EF `DbContext` with the `Products` `DbSet` |
| `Data/IProductRepository.cs` | Data access contract used by the endpoints |
| `Data/ProductRepository.cs` | **← you implement this** (some methods still throw `NotImplementedException`) |
| `Endpoints/ProductEndpoints.cs` | The 5 CRUD endpoints (done) |
| `Dtos/ProductDtos.cs` | Create/update request payloads |
| `AdventureWorksCrudApi.http` | Ready-to-run requests for testing |

## Endpoints

| Method | Route | Action |
|--------|-------|--------|
| GET | `/api/products` | List all |
| GET | `/api/products/{id}` | Get one |
| POST | `/api/products` | Create |
| PUT | `/api/products/{id}` | Update |
| DELETE | `/api/products/{id}` | Delete |

## 1. Set up the database (SQL Server on localhost)

This project connects to a local **SQL Server** instance at `localhost` using
Windows authentication. If you already have `AdventureWorks2022` restored there,
skip to step 2.

To restore it yourself:

1. Download `AdventureWorks2022.bak` from
   <https://learn.microsoft.com/en-us/sql/samples/adventureworks-install-configure?view=sql-server-ver17&tabs=ssms>.
2. Restore it into your local SQL Server following the doc:

The connection string in `appsettings.json` already points at
`Server=localhost` / `Database=AdventureWorks2022` with `Trusted_Connection=True`.

## 2. Run

```powershell
dotnet run
```

Then open the OpenAPI spec at `http://localhost:<port>/openapi/v1.json`, or fire the
requests in `AdventureWorksCrudApi.http` from your IDE.
