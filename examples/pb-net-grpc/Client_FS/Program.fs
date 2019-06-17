﻿open System
open ProtoBuf.Grpc.Client
open Shared_FS
open System.Net.Http
open FSharp.Control.Tasks

[<EntryPoint>]
let main argv =
    ClientFactory.AllowUnencryptedHttp2 <- true
    task {
        use http = new HttpClient ( BaseAddress = new Uri("http://localhost:10042") )
        let client = ClientFactory.Create<ICalculator>(http)
        let! result = client.MultiplyAsync(new MultiplyRequest(X = 12, Y = 4))
        printfn "%i" result.Result
        return 0
    } |> fun t -> t.Result