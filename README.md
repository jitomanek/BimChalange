# BimChalange

Server be sure to check/modify appsettings CorsOrigins for enabling CORS other than localhost:3000

Server project in main branch uses InMemory dbContext provider

Server project in main Microsoft.SqlServer uses Microsoft.SqlServer dbContext provider 
    Change the connectionString in appsettings
    Update-database


Client uses npm library kantar-react-lib, which is still fully without bugs. 
    Mainly does not update form after unsuccessful submit
