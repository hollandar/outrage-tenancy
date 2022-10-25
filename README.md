# Outrage Tenancy

Multi-tenancy middleware for an asp.net core project, the example implementation supports connection string encruption and database isolation.

It also bases the tenancy on the host (and optionally port if using localhost) but an implementation is also available that uses X-TENANT-ID from the request header.
To do so, inject HttpHeaderTenancyIdProvider as ITenancyIdProvider prior to calling AddTenancy.

The test implementation also uses specifically postgres for tenancy data management, but this could be extended with migrations for other physical database implementations; and connection strings.

## Tenancy Builder

The TenancyBuilder in the test project is responsible for establishing the tenancy:

  * EstablishTenancyAsync - Create the isolated database and user for the tenancy and store its connection string securely in the tenancy database.
  * BuildTenancyAsync - Create and add a tenancy feature to the tenancy definition to pass a DbContext configured specifically for the tenancy.
  * MigrateTenancyAsync - Run migration on the database one per startup, migrations having been run is tracked by the infrastructure, there is no need to track that in the builder.

