$projectpath = Join-Path -Path $PSScriptRoot -ChildPath ..\src -Resolve

Write-Output $projectpath

Write-Host "Deleting obj folders in " + $projectpath -ForegroundColor Green
Get-ChildItem -Directory -Recurse -Path $projectpath |  where {$_.Name -eq "obj" } | Get-ChildItem | Remove-Item -Recurse
    
Write-Host "Deleting bin folders in " + $projectpath -ForegroundColor Green
Get-ChildItem -Directory -Recurse -Path $projectpath |  where {$_.Name -eq "bin" } | Get-ChildItem | Remove-Item -Recurse

Write-Host "Sucessful" -ForegroundColor Green

Write-Host "Press any key to continue..."
$x = $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")