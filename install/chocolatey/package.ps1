$script_dir = $PSScriptRoot
$package_dir = "$script_dir\git-diff-xlsx"
$source_dir = "$script_dir\..\..\src\git-diff-xlsx\bin\Release\netcoreapp3.1\publish"
$output_dir = "$package_dir\tools"

# determine version number
$package_information = Get-Content "$source_dir\git-diff-xlsx.deps.json" | Out-String

if($package_information -match '(?<=git-diff-xlsx\/)(\d+\.)?(\d+\.)?(\d+)') {
    $version_number = $Matches[0]
    Write-Output "Version number is $version_number"
} else {
    throw 'Cannot determine version number'
}

Remove-Item "$script_dir\*.nupkg"
Remove-Item "$script_dir\*.zip"

# Purge output directory
Remove-Item "$output_dir" -Recurse -Force
New-Item "$output_dir" -Force -ItemType "directory"

# Archive contents of publish directory
$release_zip_filename = "git-diff-xlsx.$version_number.zip"
Compress-Archive "$source_dir\*" "$script_dir\$release_zip_filename"
$checksum = (Get-FileHash "$script_dir\$release_zip_filename").Hash
Copy-Item "$script_dir\$release_zip_filename" "$output_dir\$release_zip_filename"

# Generate verification file
$verification_content = Get-Content -Path "$package_dir\VERIFICATION.template.txt" -Raw
$verification_content = $verification_content.Replace("[!!VERSION_NUMBER!!]", $version_number).Replace("[!!CHECKSUM!!]", $checksum)
Out-File -FilePath "$output_dir\VERIFICATION.txt" -InputObject $verification_content

# Generate licence file
$licence_content = Get-Content -Path "$script_dir\..\..\LICENSE" -Raw
$licence_content = "From https://github.com/webbwebbwebb/git-diff-xlsx/blob/master/LICENSE `n`n" + $licence_content
Out-File -FilePath "$output_dir\LICENSE.txt" -InputObject $licence_content

Copy-Item "$package_dir\chocolateyInstall.ps1" "$output_dir\chocolateyInstall.ps1"

# Create package
choco pack --version $version_number git-diff-xlsx/git-diff-xlsx.nuspec

# Test install
choco uninstall git-diff-xlsx --force
choco install git-diff-xlsx -dv -source "'.;https://chocolatey.org/api/v2/'"