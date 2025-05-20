module kankasaur.MarkerGroup

open System.IO
open System.Text.Json
open Kanka.NET
open kankasaur.JSONIO

type CreateGroupRec = {
    name: string
    map_id: string
}


let CreateMarkerGroup  (campaignID:string) (mapID:string)
    (groupName:string)  (outStream:Stream)=
    let newGroup = {
        name = groupName
        map_id = mapID
    }
    use writer = StreamWriter (outStream, leaveOpen = true)
    writer.AutoFlush <- true
    let options = JsonSerializerOptions(PropertyNamingPolicy = JsonNamingPolicy.CamelCase)
    JsonSerializer.Serialize(newGroup, options)
    |> JsonDocument.Parse
    |> Kanka.CreateMarkerGroup campaignID mapID
    |> fun jel ->
        formatJsonElement jel
        |> writer.WriteLine
        ()
let ListMarkerGroups (campaignID:string) (mapID:string) (outStream:Stream)=
    use writer = StreamWriter (outStream, leaveOpen = true)
    writer.AutoFlush <- true
    // Assuming GetMap is a function that fetches a specific map for a given campaign ID
    Kanka.GetMarkerGroups campaignID mapID
    |> fun jel ->
        formatJsonElement jel
        |> writer.WriteLine
        ()