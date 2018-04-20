# login and check we are on correct subscription
az login
az account show --query Name -o table

# create resource group
$resourceGroup="CliWebAppDemo"
$location="westeurope"
az group create -n $resourceGroup -l $location

# create the app service plan
# allowed sku values B1, B2, B3, D1, F1, FREE, P1, P1V2, P2, P2V2, P3, P3V2, S1, S2, S3, SHARED.
$planName="CliWebAppDemo"
az appservice plan create -n $planName -g $resourceGroup -l $location --sku B1

# create the webapp
$appName="azcliwebappdemo"
az webapp create -n $appName -g $resourceGroup --plan $planName

# deploy from GitHub
$gitrepo="https://github.com/markheath/azure-cli-snippets"

az webapp deployment source config -n $appName -g $resourceGroup `
    --repo-url $gitrepo --branch master --manual-integration

# to resync
# az webapp deployment source sync -n $appName -g $resourceGroup


# create the SQL server
$sqlServerName="azclidemo"
$sqlServerUsername="mheath"
$sqlServerPassword='!SecureP@assword1'
az sql server create -n $sqlServerName -g $resourceGroup `
            -l $location -u $sqlServerUsername -p $sqlServerPassword


# create the database
$databaseName="SnippetsDatabase"
az sql db create -g $resourceGroup -s $sqlServerName -n $databaseName `
          --service-objective Basic

# get web app ip address
$ipAddresses = az webapp show -n $appName -g $resourceGroup --query "outboundIpAddresses" -o tsv

$i=1
ForEach ($ipAddress in $ipAddresses.Split(",")) 
{ 
    az sql server firewall-rule create -g $resourceGroup -s $sqlServerName `
     -n AllowWebApp$i --start-ip-address $ipAddress --end-ip-address $ipAddress 
    $i++
}

# short-cut - allow all azure traffic
#az sql server firewall-rule create -g $resourceGroup -s $sqlServerName `
#     -n AllowWebApp1 --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0

# to get conn string
# az sql db show-connection-string -s $sqlServerName -n $databaseName -c ado.net

#
$connectionString="Server=tcp:$sqlServerName.database.windows.net;Database=$databaseName;User ID=$sqlServerUsername@$sqlServerName;Password=$sqlServerPassword;Encrypt=True;Connection Timeout=30;"
az webapp config connection-string set `
    -n $appName -g $resourceGroup `
    --settings "SnippetsContext=$connectionString" `
    --connection-string-type SQLAzure

az webapp show -n $appName -g $resourceGroup --query "defaultHostName"

# load the special page that runs the database migrations
Start-Process "https://$appName.azurewebsites.net/migrate"