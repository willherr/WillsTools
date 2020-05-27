# This script is to modify index.html to run on github and move output to /docs
$publishDirectory=$args[0]
$projectDirectory=$args[1]
$outputDirectory=$projectDirectory + "/docs"
$publishRoot=$publishDirectory + "wwwroot"
$indexHtml=$publishRoot + "/index.html"
$cacheBuster=[guid]::NewGuid().ToString()

# First, we need to remove all lines containing #DEL from index.html
Set-Content -Path $indexHtml -Value (Get-Content -Path $indexHtml | Select-String -pattern "#DEL" -notmatch)

# Next, we should update our file versions to cache bust in case we made changes
(Get-Content $indexHtml).Replace("?v=","?v=" + $cacheBuster) | Set-Content $indexHtml

# Finally, we can move our release to the /docs folder!
robocopy $publishRoot $outputDirectory *.* /E /XD $outputDirectory /move # /XF *.html