script_dir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
package_dir="$script_dir"/git-diff-xlsx
source_dir="$script_dir"/../../src/git-diff-xlsx/bin/Release/netcoreapp3.1/publish
output_dir="$package_dir"/usr/lib/git-diff-xlsx

sudo chmod -R 0777 "$package_dir"

# Purge output directory
rm -rf "$output_dir"
mkdir -p "$output_dir"

# Copy contents of publish directory
rsync -avr --exclude="*.exe" "$source_dir"/ "$output_dir"
# Copy licence
cp "$script_dir"/../../LICENSE "$output_dir"/

rm -f "$script_dir"/*.deb

sudo chmod -R 0775 "$package_dir"

# Create package
dpkg-deb --build git-diff-xlsx