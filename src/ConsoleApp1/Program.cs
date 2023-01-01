
using Boost.Proto.Actor.DependencyInjection;
using Boost.Proto.Actor.Hosting.Cluster;
using ConsoleApp1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Proto;

var host = Host.CreateDefaultBuilder()
               .ConfigureServices(services =>
               {
                   services.AddHttpClient(); 
               })
               .UseProtoActorCluster((option, sp) =>
               {
                   option.Name = "poc";
                   option.FuncActorSystemStart = root =>
                   {
                       root.SpawnNamed(sp.GetRequiredService<IPropsFactory<HelloActor>>().Create(new CancellationTokenSource()), "HelloActor");

                       return root;
                   };
               })
               .Build();

await host.StartAsync();

var root = host.Services.GetRequiredService<IRootContext>();

root.Send(new(ActorSystem.NoHost, "HelloActor"), "");
root.Send(new(ActorSystem.NoHost, "HelloActor"), "");
root.Send(new(ActorSystem.NoHost, "HelloActor"), PoisonPill.Instance);

await host.StopAsync();



