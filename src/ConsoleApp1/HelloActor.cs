using Microsoft.Extensions.DependencyInjection;
using LanguageExt;
using LanguageExt.Common;
using Proto;
using static ConsoleApp1.IHello<ConsoleApp1.HelloActor>;

namespace ConsoleApp1;

public readonly record struct HelloActor
(
    IHttpClientFactory HttpClientFactory,
    CancellationTokenSource CancellationTokenSource
) : IHas<HelloActor, CancellationTokenSource>,
    IActor,
    IHello<HelloActor>
{
    public HelloActor LocalCancel => this;
    public CancellationToken CancellationToken => CancellationTokenSource.Token;
    HttpClient IHas<HelloActor, HttpClient>.It => HttpClientFactory.CreateClient();
    CancellationTokenSource IHas<HelloActor, CancellationTokenSource>.It => CancellationTokenSource;

    public Task ReceiveAsync(IContext context) 
    {
        var q = IHello<HelloActor>.Eff;
        var ret = q.Run(this).ThrowIfFail();

        Console.WriteLine(ret);

        return Task.CompletedTask;
    }
}

