### Migration ###

### add ###
dotnet ef --startup-project ../IntegrationTestDbDocker migrations add initial_db -o Persistence/Migrations -c ApplicationDbContext --verbose