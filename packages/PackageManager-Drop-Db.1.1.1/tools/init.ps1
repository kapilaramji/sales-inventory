<#
.SYNOPSIS
 Install a Drop-Db command into the Visual Studio package manager to allow you to quickly drop databases.
#>
param($installPath, $toolsPath, $package, $project)

function global:Drop-Db(
	[Parameter(Mandatory=$true, ValueFromPipeline=$true)] $DatabaseName,
	$ServerInstance = '(localdb)\v11.0'
	) {

	if($ServerInstance -match '\(localdb\)\\(\S+)') {
		SqlLocalDB.exe stop $Matches[1]
		if($LastExitCode) {
			throw "Could not stop localdb"
		}
	}

	SQLCMD.EXE -S $ServerInstance -d "master" -Q "drop database $DatabaseName" -b

	if($LastExitCode) {
		Write-Host "Could not drop database"
	}
}
