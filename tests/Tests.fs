module Tests

open System
open Xunit
open Kanka.NET
open Xunit.Abstractions

type Tests(output:ITestOutputHelper) =
    [<Fact>]
    member this.FetchProfile() =
           let profile = Kanka.GetProfile()
           output.WriteLine $"Profile:\n {profile}"