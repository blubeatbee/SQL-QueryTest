# LINQ queries
A console application for testing various LINQ queries, as well as Repository and Service patterns. 
Tested out:
- IQueryable and AsQueryable()
- AsNoTracking()

Note: The SQL file for recreating the database, as well as the SQL query file, is stored inside the Script folder.

## Improvements to be made:
- I was unfortunately unable to use Dependency Injections so much of the code is still very coupled to each other.
- `appsettings.json` exposes the connection string.
