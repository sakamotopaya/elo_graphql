# elo_graphql
ASPNET Core 2.1, EF Core, GraphQL

This is a research project to help me explore the different challenges one might encounter wiring up GraphQL to a relational database.

Projects:
https://github.com/graphql-dotnet/graphql-dotnet


Prerequisites:
- ASPNET Core 2.1 
- PostGres
- Adventureworks DB

# Setup
- pull the repo
- set the postgres connection string as a user secret:
  - Secret Name: AdventureworksContext
  - Sample connection string:Server=localhost;Port=5432;Database=Adventureworks;User Id=youruserid;Password=yourpass;ApplicationName=someappname
