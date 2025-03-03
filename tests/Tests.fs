module Tests

open System
open Xunit
open Kanka.NET

[<Fact>]
let ``FetchProfile`` () =
    let profile = Kanka.GetProfile()
    printfn $"Profile:\n {profile}"