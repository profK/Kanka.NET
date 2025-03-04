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
        
    
        
   