module kankasaur.JSONIO

open System.Text.Json

let formatJsonElement (jsonElement: JsonElement) =
    let options = JsonSerializerOptions(WriteIndented = true)
    JsonSerializer.Serialize(jsonElement, options)
    
let writeJasonToStream (jsonElement: JsonElement) (stream: System.IO.Stream) =
    let options = JsonSerializerOptions(WriteIndented = true)
    JsonSerializer.Serialize(stream, jsonElement, options)