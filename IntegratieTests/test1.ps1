# login branch to use
$loginbranch = "develop"
$inventorybranch = "IntegratieTests"
$encryptionkey = "daarkomenwenogeenkeeropterug"

if($null -eq (Get-Module -Name posh-git))
{
  Install-Module posh-git -Scope CurrentUser -Force
}
Import-Module posh-git
if((Test-Path -Path $env:TEMP/loginsvc -PathType Container))
{
remove-item $env:TEMP/loginsvc -Recurse -Force
}

# login token script
git clone -q -b $loginbranch https://github.com/Fontys-Project/login.git $env:TEMP/loginsvc
git clone -q -b $inventorybranch https://github.com/Fontys-Project/inventory.git $env:TEMP/inventorysvc

cd $env:TEMP/loginsvc/LoginService
docker-compose build

docker-compose up -d
start-sleep -Seconds 20

docker-compose exec web loginapi db upgrade

cd $env:TEMP/inventorysvc
docker-compose build
docker-compose up -d
start-sleep -Seconds 20


$url = "http://localhost:5000/auth/login"

$body = '
{
 "username": "gebruiker@wmstest.nl",
 "password": "WachtwoordGebruiker1"
}
'

#read-host "press key to continue"

$response = Invoke-RestMethod -Method Post -Uri $url -Body $body -ContentType "application/json"

# test inventory api create product
write-host "token is" $response.access_token

# cleanup

docker-compose down
docker-compose rm
docker image prune -f
docker volume prune -f


read-host "press entry to close test"
cd