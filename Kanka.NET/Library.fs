namespace Kanka.NET
open System
open  FsHttp
open System.IO
open FsHttp.Response

module Kanka =
    let  key =
        use f = File.OpenText("kanka.txt")
        f.ReadToEnd() |> fun ( x:string) -> x.Trim()
        
    let  api = "https://api.kanka.io/1.0/"
    let GetProfile()  =
       let url = api + "profile"
       http {
           GET url
           
           AuthorizationBearer key
           Accept "application/vnd.github.v3+json"
           UserAgent "FsHttp"
           header "X-GitHub-Api-Version" "2022-11-28"
          
        }
        |> Request.send
        |> Response.toString(Some 1024)
      