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

# Purge output directory
remove-item "$output_dir" -recurse -force

# Copy contents of publish directory
robocopy "$source_dir" "$output_dir" /MIR
# Copy licence
Copy-Item "$script_dir\..\..\LICENSE" "$output_dir\LICENSE.txt"
Copy-Item "$package_dir\VERIFICATION.txt" "$output_dir\VERIFICATION.txt"

Remove-Item "$script_dir\*.nupkg"

# Create package
choco pack --version $version_number git-diff-xlsx/git-diff-xlsx.nuspec
