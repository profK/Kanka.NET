module Tests

open System
open Xunit
open Kanka.NET
open Xunit.Abstractions
open System.Text.Json

type Tests(output:ITestOutputHelper) =
   
    
    [<Fact>]
    member this.FetchProfile() =
           let profile = Kanka.GetProfile()
           output.WriteLine $"Profile:\n {profile}"
    [<Fact>]
    member this.FetchCampaigns() =
           let campaigns = Kanka.GetCampaigns()
           output.WriteLine $"Campaigns:\n {campaigns}"
           let mutable dummy = Unchecked.defaultof<JsonElement>
           campaigns.TryGetProperty("error",&dummy)
           |> Assert.False
          
    [<Fact>] 
    member this.FetchEntities() =
           let entities = Kanka.GetEntities "1"
           output.WriteLine $"Entities:\n {entities}"
    [<Fact>]
       member this.FetchCampaign() =
           let campaign = Kanka.GetCampaign "1"
           output.WriteLine $"Campaign:\n {campaign}"
           
       [<Fact>]
       member this.CreateImage() =
           let image =
                Kanka.KankaPostImage 
                    "test_data/Waterdeep-reborn.png"
                    "MyImage"
                    "campaigns/308706/images"
           output.WriteLine $"Image:\n {image}"
       