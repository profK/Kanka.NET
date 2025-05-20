module kankasaur.Maps

open System.IO
open Kanka.NET
open kankasaur.JSONIO

let ListMaps (campaignID: string)(outStream:Stream) =
    use writer = StreamWriter (outStream, leaveOpen = true)
    writer.AutoFlush <- true
    // Assuming GetMaps is a function that fetches maps for a given campaign ID
    let maps = Kanka.GetMaps(campaignID)
    maps.GetProperty("data").EnumerateArray()
    |> Seq.iter (fun map ->
            let name = map.GetProperty("name").GetString()
            let id = map.GetProperty("id").GetInt32()
            writer.WriteLine $"{name} {id}"
    )
let GetMap (campaignID: string) (mapID:string) (outStream:Stream)=
    use writer = StreamWriter (outStream, leaveOpen = true)
    writer.AutoFlush <- true
    // Assuming GetMap is a function that fetches a specific map for a given campaign ID
    Kanka.GetMap campaignID mapID
    |> fun jel ->
        formatJsonElement jel
        |> writer.WriteLine 
    
 
    