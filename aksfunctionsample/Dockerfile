FROM microsoft/dotnet:2.1-sdk AS installer-env

COPY . /src/dotnet-function-app
RUN cd /src/dotnet-function-app && \
    mkdir -p /home/site/wwwroot && \
    dotnet publish *.csproj --output /home/site/wwwroot

FROM mcr.microsoft.com/azure-functions/dotnet:2.0
ENV AzureWebJobsScriptRoot=/home/site/wwwroot

ENV AzureWebJobsStorage="{BLOB_STORAGE_CONNECTION_STRING}"
ENV AzureBlobStorage="{BLOB_STORAGE_CONNECTION_STRING_OF_CONTAINER_HAVING_TRIGGER}"
ENV AzureEventHubConnectionString="{EVENT_HUB_CONNECTION_STRING}"
ENV TestCosmonDBConnection="{COSMOS_DB_CONNECTION_STRING}"
ENV AzureSignalRConnectionString="{AZURE_SIGNALR_CONNECTION_STRING}"

COPY --from=installer-env ["/home/site/wwwroot", "/home/site/wwwroot"]