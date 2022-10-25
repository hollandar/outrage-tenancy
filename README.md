# Outrage Tenancy

Multi-tenancy middleware for an asp.net core project, the example implementation supports connection string encruption and database isolation.
It also bases the tenancy on the host (and optionally port if using localhost) but an implementation is also available that uses X-TENANT-ID from the request header.
