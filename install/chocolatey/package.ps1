$script_dir = $PSScriptRoot
$package_dir = "$script_dir\git-diff-xlsx"
$source_dir = "$script_dir\..\..\src\git-diff-xlsx\bin\Release\netcoreapp3.1\publish"
$output_dir = "$package_dir\tools"

# Purge output directory
remove-item "$output_dir" -recurse -force

# Copy contents of publish directory
robocopy "$source_dir" "$output_dir" /MIR
# Copy licence
Copy-Item "$script_dir\..\..\LICENSE" "$output_dir\LICENSE.txt"
Copy-Item "$package_dir\VERIFICATION.txt" "$output_dir\VERIFICATION.txt"

Remove-Item "$script_dir\*.nupkg"

# Create package
choco pack git-diff-xlsx/git-diff-xlsx.nuspec
