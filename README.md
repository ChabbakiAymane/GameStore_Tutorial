# GAME-STORE API

## Starting SQL Server

```powershell
$sa_password = "[SA PASSWORD HERE]";
# -e: set environment variables
# -p: map the host port to the container port 1433
# SA_PASSWORD: SQL Server password, better not hardcode it, use a secret manager/variable 
# -v: create a volume for the SQL Server data to persist across container restarts
# 'sqlvolume=/...': volume will be created in the '/...' directory
# --rm: container will be removed once it is stopped
# --name: logical name of the container
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$sa_password" -p 1433:1433 -v sqlvolume=/var/opt/mssql --rm --name mssql -d mcr.microsoft.com/mssql/server:2022-latest
```
