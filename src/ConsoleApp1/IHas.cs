using LanguageExt;
using LanguageExt.Effects.Traits;
using static LanguageExt.Prelude;

namespace ConsoleApp1;

public interface IHas<RT, T> : HasCancel<RT>, IDisposable
    where RT: struct, IHas<RT, T>
{
    protected T It { get; }

    static Eff<RT, T> Eff => Eff<RT, T>(static rt => rt.It);

    void IDisposable.Dispose()
    {
        if (It is IDisposable n)
        {
            n.Dispose();
        }
    }
}
