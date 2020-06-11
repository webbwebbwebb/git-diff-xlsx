script_dir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
package_dir="$script_dir"/git-diff-xlsx
source_dir="$script_dir"/../../src/git-diff-xlsx/bin/Release/netcoreapp3.1/publish
output_dir="$package_dir"/usr/lib/git-diff-xlsx

# Create usr/lib/git-diff-xlsx directory
sudo chmod -R 0777 "$package_dir"
rm -rf "$output_dir"
mkdir -p "$output_dir"

# Copy contents of publish directory
rsync -avr --exclude="*.exe" "$source_dir"/ "$output_dir"

sudo chmod -R 0775 "$package_dir"

# Create package
dpkg-deb --build git-diff-xlsx