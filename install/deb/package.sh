script_dir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
package_dir="$script_dir"/git-diff-xlsx
source_dir="$script_dir"/../../src/git-diff-xlsx/bin/Release/netcoreapp3.1/publish

# determine version number
version_number="$(grep -Po -m1 '(?<=git-diff-xlsx\/)(\d+\.)?(\d+\.)?(\d+)' "$source_dir/git-diff-xlsx.deps.json")"
if [ -z "$version_number" ]; then
    echo "Cannot determine version number"
    exit 1
fi
echo "Version number is $version_number"

# Purge package directory
rm -rf "$package_dir"
mkdir -p "$package_dir"
mkdir -p "$package_dir/DEBIAN"
mkdir -p "$package_dir/usr/lib/git-diff-xlsx"
mkdir -p "$package_dir/usr/bin"

# Create control file from template
sed "s/##<VERSION_NUMBER>##/$version_number/g" "$script_dir/control.template" > "$package_dir/DEBIAN/control"

# Copy shim
cp "$script_dir/git-diff-xlsx.shim" "$package_dir/usr/bin/git-diff-xlsx"

# Copy contents of publish directory
rsync -avr --exclude="*.exe" "$source_dir"/ "$package_dir/usr/lib/git-diff-xlsx"
# Copy licence
cp "$script_dir"/../../LICENSE "$package_dir/usr/lib/git-diff-xlsx"/

rm -f "$script_dir"/*.deb

sudo chmod -R 0775 "$package_dir"

# Create package
dpkg-deb --build git-diff-xlsx
