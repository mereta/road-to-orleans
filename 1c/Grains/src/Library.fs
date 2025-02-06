namespace Grains

open System
open System.Threading
open System.Threading.Tasks
open Orleans

type IYourReminderGrain =
    inherit IGrainWithStringKey
    inherit IRemindable
    
    abstract member WakeUp : unit -> Task

type YourReminderGrain(client: IYourGrainServiceClient) =
    inherit Grain()
    interface IYourReminderGrain with
        member this.WakeUp() = client.HelloWorld()
        member this.ReceiveReminder(reminderName, status) =
            Console.WriteLine(reminderName, status)
            client.HelloWorld()
        
    override _.OnActivateAsync(cancellationToken:CancellationToken) = 
        let _periodInSeconds = TimeSpan.FromSeconds 60.0
        let _timeInSeconds = TimeSpan.FromSeconds 60.0
        let _reminder = base.RegisterOrUpdateReminder(base.GetPrimaryKeyString(), _periodInSeconds, _timeInSeconds)
        base.OnActivateAsync(cancellationToken)
        