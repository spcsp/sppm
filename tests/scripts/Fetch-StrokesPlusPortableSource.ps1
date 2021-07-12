function Fetch-StrokesPlusPortableSource {
    $url = "https://cdn.strokesplus.net/download/latest/portable"
    $name = "splus-lib"
    $zip = $name + ".zip"

    Invoke-WebRequest $url -OutFile $zip
    Expand-Archive $zip -DestinationPath $name
    del $zip
}

Fetch-StrokesPlusPortableSource