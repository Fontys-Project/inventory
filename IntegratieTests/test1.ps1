# login branch to use
$loginbranch = "develop"
$inventorybranch = "master"
$gitdirs = @(($env:TEMP + "/inventorysvc"),($env:TEMP + "/loginsvc"))
$workdirs = @(($env:TEMP + "/inventorysvc"),($env:TEMP + "/loginsvc/LoginService"))

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
git clone -q -b $loginbranch https://github.com/Fontys-Project/login.git $env:TEMP/loginsvc
Write-Host "Cloned Login repo"
git clone -q -b $inventorybranch https://github.com/Fontys-Project/inventory.git $env:TEMP/inventorysvc
Write-Host "Cloned Inventory repo"

foreach($dir in $workdirs)
{
cd $dir
docker-compose build

docker-compose up -d
start-sleep -Seconds 20
}

# additional step LoginService
docker-compose exec web loginapi db upgrade

$url = "http://localhost:5000/auth/login"

$body = '
{
 "username": "gebruiker@wmstest.nl",
 "password": "WachtwoordGebruiker1"
}
'

$bodyAdmin = '
{
 "username": "admin@wmstest.nl",
 "password": "WachtwoordAdmin1"
}
'
#read-host "press key to continue"
$response = Invoke-RestMethod -Method Post -Uri $url -Body $bodyAdmin -ContentType "application/json" -verbose

#test inventory api create product
write-host "token is" $response.access_token
write-host "execute create product test"

$testbody = '
{
  "name": "test product",
  "price": 100,
  "sku": "product sku test"
}
'

$headers = @{"Authorization"=("Bearer "+$response.access_token)}
$url = "http://localhost:5001/api/v0.1/Products"

$responseProduct = Invoke-RestMethod -Method Post -Uri $url -Body $testbody -ContentType "application/json" -headers $headers -verbose
Out-String -InputObject $responseProduct

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
