function Get-StrokesPlusPortableSource {
    $url = "https://cdn.strokesplus.net/download/latest/portable"
    $name = "splus-lib"
    $zip = $name + ".zip"

    Invoke-WebRequest $url -OutFile $zip
    Expand-Archive $zip -DestinationPath $name
    Remove-Item $zip
}

Get-StrokesPlusPortableSource