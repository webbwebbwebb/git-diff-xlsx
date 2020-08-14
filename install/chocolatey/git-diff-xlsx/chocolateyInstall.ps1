$ErrorActionPreference = 'Stop';

$version_number  = "$env:ChocolateyPackageVersion"
$tools_dir     = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"

Get-ChocolateyUnzip -FileFullPath "$tools_dir\git-diff-xlsx.$version_number.zip" -Destination $tools_dir