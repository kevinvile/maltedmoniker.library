using maltedmoniker.result;
using System.Threading.Tasks;

namespace maltedmoniker.cqrs
{
    public interface IHandler<T> where T : IHandled
    {
        Task<Result> Execute(T handled);
    }

    public interface IHandlerSync<T> where T : IHandled
    {
        Result Execute(T handled);
    }

    public interface IHandler<THandled, TResult> where THandled : IHandled
    {
        Task<Result<TResult>> Execute(THandled handled);
    }

    public interface IHandlerSync<THandled, TResult> where THandled : IHandled
    {
        Result<TResult> Execute(THandled handled);
    }
}
