namespace Kanka.NET

open System
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
        |> Response.toJson
    let KankaPost endpoint data =
        let url = api + endpoint
        http {
            POST url
            CacheControl "no-cache"
            body 
            jsonSerialize data
        }
        |> Request.send
        |> Response.toJson
    let KankaPut endpoint data =
        let url = api + endpoint
        http {
            PUT url
            CacheControl "no-cache"
            body 
            jsonSerialize data
        }
        |> Request.send
        |> Response.toJson

    let GetProfile() =
        api + "profile"
        |> KankaGet 

    let GetCampaigns() =
        api + "campaigns"
        |> KankaGet 

    let GetCampaign id =
        api + "campaigns/" + id
        |> KankaGet 

    let GetEntities campaignId =
        api + "campaigns/" + campaignId + "/entities"
        |> KankaGet 
        
    let GetEntity campaignId entityId =
        api +  "campaigns/" + campaignId + "/entities/" + entityId
        |> KankaGet
    let GetCharacters campaignId =
        api + "campaigns/" + campaignId + "/characters"
        |> KankaGet
    let GetCharacter campaignId characterId =
        api + "campaigns/" + campaignId + "/characters/" + characterId
        |> KankaGet
    let GetLocations campaignId =
        api + "campaigns/" + campaignId + "/locations"
        |> KankaGet
    let GetLocation campaignId locationId =
         api + "campaigns/" + campaignId + "/locations/" + locationId
        |> KankaGet
    let CreateLocation campaignId data=
        api + "campaigns/" + campaignId + "/locations"
        |> KankaPost data  
    
        
    
        
   