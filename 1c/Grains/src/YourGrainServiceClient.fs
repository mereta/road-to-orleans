namespace Grains

open System
open Orleans.Runtime.Services
open Orleans.Services

type IYourGrainServiceClient =
    inherit IGrainServiceClient<IYourGrainService>
    inherit IYourGrainService

type YourGrainServiceClient(serviceProvider: IServiceProvider) =
    inherit GrainServiceClient<IYourGrainService>(serviceProvider)
    
    let grainService =
        base.GetGrainService(base.CurrentGrainReference.GrainId)
    
    interface IYourGrainServiceClient with
        member this.HelloWorld() =
            grainService.HelloWorld()
        