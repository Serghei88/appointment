Start mssql server in docker
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyYourStrong@Passw0rd" \ 
   -p 1433:1433 --name mssql \
   -d microsoft/mssql-server-linux