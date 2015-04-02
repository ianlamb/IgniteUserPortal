# Ignite User Portal

User portal web application for a fitness solution. Project was cancelled half-way through development, this is here to showcase the work I did on it and what the UI looked like.

### Running the project

- make sure you have Visual Studio 2012 installed as well as Microsoft Azure SDK Tools http://azure.microsoft.com/en-us/downloads/
- get an instance of SQL Server running or create a LocalDB in VS
- run `aspnet_regsql.exe -S (localdb)\v11.0 -E -A all -d IgniteDB` to setup the ASP.NET tables
- go to Project > ASP Configuration and create 3 roles/users: 
    - admin
    - client
    - pt