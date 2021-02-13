using maltedmoniker.result;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace maltedmoniker.cqrs
{
    internal sealed class Dispatcher : IDispatcher
    {
        readonly IServiceProvider ServiceProvider;
        public Dispatcher(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        public async Task<Result> ExecuteAsync<T>(T command) where T : ICommand
        {
            IHandler<T> handler = ServiceProvider.GetRequiredService<IHandler<T>>();
            return await handler.Execute(command);
        }

        public async Task<Result<TResult>> ExecuteAsync<T, TResult>(T command) where T : ICommand
        {
            IHandler<T, TResult> handler = ServiceProvider.GetRequiredService<IHandler<T, TResult>>();
            return await handler.Execute(command);
        }

        public Result Execute<T>(T command) where T : ICommand
        {
            IHandlerSync<T> handler = ServiceProvider.GetRequiredService<IHandlerSync<T>>();
            return handler.Execute(command);
        }

        public Result<TResult> Execute<T, TResult>(T command) where T : ICommand
        {
            IHandlerSync<T, TResult> handler = ServiceProvider.GetRequiredService<IHandlerSync<T, TResult>>();
            return handler.Execute(command);
        }

        public async Task<Result<TResult>> QueryAsync<T, TResult>(T query) where T : IQuery
        {
            IHandler<T, TResult> handler = ServiceProvider.GetRequiredService<IHandler<T, TResult>>();
            return await handler.Execute(query);
        }

        public Result<TResult> Query<T, TResult>(T query) where T : IQuery
        {
            IHandlerSync<T, TResult> handler = ServiceProvider.GetRequiredService<IHandlerSync<T, TResult>>();
            return handler.Execute(query);
        }
    }
}
