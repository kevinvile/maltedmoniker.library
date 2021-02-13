using maltedmoniker.result;
using System.Threading.Tasks;

namespace maltedmoniker.cqrs
{
    public interface IDispatcher
    {
        Task<Result> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;
        Task<Result<TResult>> ExecuteAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand;
        Result Execute<TCommand>(TCommand command) where TCommand : ICommand;
        Result<TResult> Execute<TCommand, TResult>(TCommand command) where TCommand : ICommand;

        Task<Result<TResult>> QueryAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery;
        Result<TResult> Query<TQuery, TResult>(TQuery query) where TQuery : IQuery;
    }
}
