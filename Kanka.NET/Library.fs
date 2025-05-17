namespace Kanka.NET

open System
open System.Text.Json
open FsHttp
open System.IO
open FsHttp.Response

module Kanka =
    let key =
        use f = File.OpenText("kanka.txt")
        f.ReadToEnd() |> fun (x: string) -> x.Trim()

    let api = "https://api.kanka.io/1.0/"

    let KankaGet endpoint =
        let url = api + endpoint
        http {
            GET url
            AuthorizationBearer key
            Accept "application/json"
            UserAgent "FsHttp"
        }
        |> Request.send
        |> toJson
    let KankaPost data endpoint=
        let url = api + endpoint
        http {
            POST url
            CacheControl "no-cache"
            body 
            jsonSerialize data
        }
        |> Request.send
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
        |> toJson
            
    let KankaDelete  data  endpoint=
            let url = api + endpoint
            http {
                DELETE url
                CacheControl "no-cache"
            }
            |> Request.send
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
    let GetMapMarkers mapid=
        "maps/" + mapid + "/markers"
           |> KankaGet
    let CreateMapMarker mapid data=
        "maps/" + mapid + "/map_markers/"
        |> KankaPost data
    
    
    