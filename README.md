# git-diff-xlsx
Command line tool which produces a readable output from Excel xlsx files, enabling comparison with git diff tools.

## Pre-requisites

- .NET Runtime 9.0 (or greater) 
    
    Install via winget ``winget install Microsoft.DotNet.Runtime.9`` or download from https://dotnet.microsoft.com/en-us/download/dotnet/9.0/runtime

## Installation

### either via Chocolatey:
```
choco install git-diff-xlsx
```

### or install manually: 
- Download zip archive from https://github.com/webbwebbwebb/git-diff-xlsx/releases/latest
- Extract to folder
- Add folder to your path

### update your Git configuration

Add these lines to .gitconfig
```
[diff "git-diff-xlsx"]
textconv = git-diff-xlsx
```

Add this line to .gitattributes
```
*.xlsx diff=git-diff-xlsx
```

## Screenshots

Terminal
![git-diff-xlsx on command line](screenshot-cmd.png?raw=true)

Git Extensions
![git-diff-xlsx on command line](screenshot-gui.png?raw=true)