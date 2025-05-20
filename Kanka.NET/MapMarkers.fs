module kankasaur.MapMarkers

open System.IO
open System.Text.Json
open System.Text.Json.Nodes
open Kanka.NET
open kankasaur.JSONIO

type NewMapMarkerRec = {
    name: string
    map_id: string
    latitude: float
    longitude: float
    icon: int
    shape_id: int
}

let GetMapMarkers (campaignID:string) (mapID:string) (outStream:Stream)=
    use writer = StreamWriter (outStream, leaveOpen = true)
    // Assuming GetMap is a function that fetches a specific map for a given campaign ID
    Kanka.GetMapMarkers campaignID mapID
    |> fun jel ->
        formatJsonElement jel
        |> writer.WriteLine
        ()
        
let CreateMapMarker (campaignID:string) (mapID:string)
    (name:string) (location: float * float)
    (iconType:int) (shapeType:int) (outStream)=
    let newMarker = {
        name = name
        map_id = mapID
        latitude = fst location |> float
        longitude = snd location |> float
        icon = iconType
        shape_id = shapeType
    }
    use writer = StreamWriter (outStream, leaveOpen = true)
    writer.AutoFlush <- true
    let options = JsonSerializerOptions(PropertyNamingPolicy = JsonNamingPolicy.CamelCase)
    JsonSerializer.Serialize(newMarker, options)
    |> JsonDocument.Parse
    |> Kanka.CreateMapMarker campaignID mapID
    |> fun jel ->
        formatJsonElement jel
        |> writer.WriteLine
        ()

    
    
    