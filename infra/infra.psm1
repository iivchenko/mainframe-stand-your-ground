$gVersion = "4.0.2"
$subVersion=""

$urlFolder=If ($subVersion) {"$gVersion/$subVersion"} Else {"$gVersion"}
$folder=If ($subVersion) {"Godot_v$gVersion-$subVersion" + "_mono_win64"} Else {"Godot_v$gVersion-stable_mono_win64"}

$url = "https://downloads.tuxfamily.org/godotengine/$urlFolder/mono/$folder.zip"
$out = ".tools"

function Download-Godot {

    New-Item -ItemType Directory -Force -Path $out
    Invoke-WebRequest -Uri $url -OutFile "$out/godot.zip"
    expand-archive -path "$out/godot.zip" -DestinationPath "$out/"
    Remove-Item "$out/godot.zip"
}

function Run-Editor {
    
    param([string]$src)

    if (-not (Test-Path -Path "$out/$folder/"))
    {
        Download-Godot
    }

    & "$out/$folder/$folder.exe" --editor --path $src
}

Export-ModuleMember -Function * -Alias *