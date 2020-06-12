# git-diff-xlsx
Command line tool which enables git to provide a readable diff of Excel .xslx files

## Installation
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