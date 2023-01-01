using LanguageExt;
using static LanguageExt.Prelude;

namespace ConsoleApp1;

public interface IHello<RT> : IHas<RT, HttpClient>
    where RT : struct, IHello<RT>
{
}
