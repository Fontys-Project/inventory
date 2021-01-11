# login branch to use
$loginbranch = "develop"
$inventorybranch = "IntegratieTests"
$gitdirs = @(($env:TEMP + "/inventorysvc"),($env:TEMP + "/loginsvc"))
$workdirs = @(($env:TEMP + "/inventorysvc"),($env:TEMP + "/loginsvc/LoginService"))

Write-Progress -PercentComplete 0 -Activity 'Integration test: Login to Inventory, to create a product' -Status 'Cloning git repositories'

if($null -eq (Get-Module -Name posh-git))
{
  Install-Module posh-git -Scope CurrentUser -Force
}
Import-Module posh-git
# remove existing temp git repo's
foreach($dir in $gitdirs)
{
	if((Test-Path -Path $dir -PathType Container))
	{
	remove-item $dir -Recurse -Force
	}
}

# login token script
Write-Progress -PercentComplete 1 -Activity 'Integration test: Login to Inventory, to create a product' -Status 'Cloning git repositories: loginsvc'
start-process -Wait -FilePath "git" -ArgumentList @("clone", "-q", "-b", "$loginbranch", "https://github.com/Fontys-Project/login.git", "$env:TEMP/loginsvc")
Write-Progress -PercentComplete 5 -Activity 'Integration test: Login to Inventory, to create a product' -Status 'Cloning git repositories: inventorysvc'
start-process -Wait -FilePath "git" -ArgumentList @("clone", "-q", "-b", "$inventorybranch", "https://github.com/Fontys-Project/inventory.git", "$env:TEMP/inventorysvc")

$state = 10

foreach($dir in $workdirs)
{
Write-Progress -PercentComplete $state -Activity 'Integration test: Login to Inventory, to create a product' -Status "Docker compose: build service $dir"

cd $dir
start-process -Wait -FilePath "docker-compose" -ArgumentList @("build")
$state += 10

Write-Progress -PercentComplete $state -Activity 'Integration test: Login to Inventory, to create a product' -Status "Docker compose: up service $dir"
start-process -Wait -FilePath "docker-compose" -ArgumentList @("up", "-d")

#docker-compose up -d
start-sleep -Seconds 20
$state += 10
}
Write-Progress -PercentComplete 60 -Activity 'Integration test: Login to Inventory, to create a product' -Status "Docker compose: execute additional steps loginsvc"


# additional step LoginService
start-process -Wait -FilePath "docker-compose" -ArgumentList @("exec", "web","loginapi","db","upgrade")

#docker-compose exec web loginapi db upgrade

$url = "http://localhost:5000/auth/login"

$body = '
{: 
 "username": "gebruiker@wmstest.nl",
 "password": "WachtwoordGebruiker1"
}
'

#read-host "press key to continue"
Write-Progress -PercentComplete 75 -Activity 'Integration test: Login to Inventory, to create a product' -Status "send restapi request: retrieve admin login token"

$response = Invoke-RestMethod -Method Post -Uri $url -Body $bodyAdmin -ContentType "application/json" -verbose

#test inventory api create product
write-host "token is" $response.access_token
write-host "execute create product test"

start-sleep -Seconds 5


$testbody = '
{
  "name": "test product",
  "price": 100,
  "sku": "product sku test"
}
'

$headers = @{"Authorization"=("Bearer "+$response.access_token)}
$url = "http://localhost:5001/api/v0.1/Products"
Write-Progress -PercentComplete 90 -Activity 'Integration test: Login to Inventory, to create a product' -Status "send restapi request: create new product using admin token"

$responseProduct = Invoke-RestMethod -Method Put -Uri $url -Body $testbody -ContentType "application/json" -headers $headers -verbose
Out-String -InputObject $responseProduct

start-sleep -Seconds 20

read-host "press key to stop containers"

# cleanup
foreach($dir in $workdirs)
{
cd $dir
docker-compose down
docker-compose rm
docker image prune -f
docker volume prune -f
}

read-host "press entry to close test"
