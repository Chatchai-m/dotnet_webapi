rm -rf Migrations
dotnet ef database drop --context MSSQLSchemaContext -f
dotnet ef migrations add build1 --context MSSQLSchemaContext
dotnet ef database update  --context MSSQLSchemaContext
