namespace Kanka.NET

open System
open System.Text.Json
open System.Threading
open FsHttp
open System.IO
open FsHttp.Response

module Kanka =
    
    let key =
        use f = File.OpenText("kanka.txt")
        f.ReadToEnd() |> fun (x: string) -> x.Trim()

    let api = "https://api.kanka.io/1.0/"

    // HTTP API functions. Automatically throttles requests to avoid hitting the rate limit.
    let Throttle (result: Response) =
        match result.headers.TryGetValues("X-RateLimit-Remaining") with
        | true, values ->
            let remaining = values |> Seq.head |> int
            printfn "Requests remaining: %d" remaining
            if remaining = 0 then
                printfn "Rate limit reached. Waiting..."
                Thread.Sleep(1000 * 60) // Wait for 1 minute
        | _ ->
            printfn "Rate limit information not available" 
        result
        
    let KankaGet endpoint =
        let url = api + endpoint
        http {
            GET url
            AuthorizationBearer key
            Accept "application/json"
            UserAgent "FsHttp"
        }
        |> Request.send
        |> Throttle
        |> toJson
    let KankaPost data endpoint=
        let url = api + endpoint
        http {
            POST url
            CacheControl "no-cache"
            AuthorizationBearer key
            body 
            jsonSerialize data
        }
        |> Request.send
        |> Throttle
        |> toJson
    let KankaPut  data  endpoint=
        let url = api + endpoint
        http {
            PUT url
            CacheControl "no-cache"
            body 
            jsonSerialize data
        }
        |> Request.send
        |> Throttle
        |> toJson
            
    let KankaDelete  data  endpoint=
            let url = api + endpoint
            http {
                DELETE url
                CacheControl "no-cache"
            }
            |> Request.send
            |> Throttle
            |> toJson
    let KankaPostImage  filepath imageName endpoint=
        let url = api + endpoint
        let atpath = "@" + filepath
        http {
            POST url
            AuthorizationBearer key
            Accept "*/*"
            UserAgent "FsHttp"
            multipart
            filePart filepath "file[]"
            textPart "visibility_id" "1" 
           
        }
        |> Request.send
        |> Throttle
        |> toJson
         
         
        

    let GetProfile() =
        "profile"
        |> KankaGet 

    let GetCampaigns() =
        "campaigns"
        |> KankaGet 

    let GetCampaign id =
        "campaigns/" + id
        |> KankaGet 

    let GetEntities campaignId =
        "campaigns/" + campaignId + "/entities"
        |> KankaGet 
        
    let GetEntity campaignId entityId =
       "campaigns/" + campaignId + "/entities/" + entityId
        |> KankaGet
    let GetCharacters campaignId =
      "campaigns/" + campaignId + "/characters"
        |> KankaGet
    let GetCharacter campaignId characterId =
      "campaigns/" + campaignId + "/characters/" + characterId
        |> KankaGet
    let GetLocations campaignId =
      "campaigns/" + campaignId + "/locations"
        |> KankaGet
    let GetLocation campaignId locationId =
       "campaigns/" + campaignId + "/locations/" + locationId
        |> KankaGet
    let CreateLocation campaignId data=
      "campaigns/" + campaignId + "/locations"
        |> KankaPost data
    let UpdateLocation campaignId locationId data=
      "campaigns/" + campaignId + "/locations/" + locationId
        |> KankaPut data
    
    let DeleteLocation campaignId locationId=
      "campaigns/" + campaignId + "/locations/" + locationId
        |> KankaDelete
    
    let GetMaps campaignId =
      "campaigns/" + campaignId + "/maps"
        |> KankaGet
    let GetMap campaignId mapId =
      "campaigns/" + campaignId + "/maps/" + mapId
        |> KankaGet
    let GetMapMarkers campaignID mapid=
        "campaigns/" + campaignID + "/maps/" + mapid + "/map_markers"
           |> KankaGet
    let CreateMapMarker campaignid mapid data=
        let endpoint =
            "campaigns/" + campaignid + "/maps/" + mapid + "/map_markers"
        printfn $"{endpoint}"
        KankaPost data endpoint
    let CreateMarkerGroup campaignid mapid data=
        let endpoint =
            "campaigns/" + campaignid + "/maps/" + mapid + "/map_groups"
        KankaPost data endpoint
    let GetMarkerGroups campaignid mapid=
        "campaigns/" + campaignid + "/maps/" + mapid + "/map_groups"
        |> KankaGet 
    
    
    