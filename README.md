# git-diff-xlsx
Command line tool which provides a readable git diff of Excel .xslx files

## Installation
Add these lines to .gitconfig
```
[diff "git-diff-xlsx"]
	textconv = path/to/git-diff-xlsx.exe
```

Add this line to .gitattributes
```
*.xlsx diff=git-diff-xlsx
```
