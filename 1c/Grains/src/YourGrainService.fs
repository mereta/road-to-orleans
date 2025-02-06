namespace Grains

open System
open System.Threading.Tasks
open Microsoft.Extensions.Logging
open Orleans.Concurrency
open Orleans.Runtime
open Orleans.Services

type IYourGrainService =
    inherit IGrainService
    
    abstract member HelloWorld : unit -> Task

[<Reentrant>]
type YourGrainService(
    grainId: GrainId,
    silo: Silo,
    loggerFactory: ILoggerFactory) =
    inherit GrainService(grainId, silo, loggerFactory)

    interface IYourGrainService with
        member this.HelloWorld() = task{
            Console.WriteLine("Hello from Grain Service")
            Task.CompletedTask |> ignore
        }
        