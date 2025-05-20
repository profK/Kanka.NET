module Kanka.Net.Campaigns

open System.IO
open System.Reflection
open Kanka.NET
open kankasaur.JSONIO



module Campaigns =
    let ListCampaigns outStream =
        let writer = StreamWriter (outStream, leaveOpen = true)
        writer.AutoFlush <- true
        Kanka.GetCampaigns()
        |> fun jel ->
            jel.GetProperty "data"
            |> fun data ->
                data.EnumerateArray()
                |> Seq.iter (
                    fun camp ->
                        let name = camp.GetProperty("name").GetString()
                        let id = camp.GetProperty("id").GetInt32()                        
                        writer.WriteLine $"{name} {id}"
                    )
        //writer.Flush()
                
    let GetCampaign (id: string) (outStream:Stream) =
        use writer = StreamWriter (outStream, leaveOpen = true)
        writer.AutoFlush <- true
        Kanka.GetCampaign(id)
        |>  fun jel ->
                  formatJsonElement jel
                  |> writer.WriteLine
               