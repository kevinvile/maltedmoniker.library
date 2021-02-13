using maltedmoniker.cqrs.demo.Commands;
using maltedmoniker.result;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace maltedmoniker.cqrs.demo
{
    class Program
    {
        static IDispatcher dispatcher;
        static async Task Main()
        {
            var collection = ConfigureServices();
            var provider = collection.BuildServiceProvider();

            dispatcher = provider.GetRequiredService<IDispatcher>();

            var command = AddNumbers(1, 5);
            var initialCommandResult = await dispatcher.ExecuteAsync<AddNumbersCommand, int>(command);
            var result = await initialCommandResult
                .MapToResultAsync(SuccessfulAdditionOfNumbers);

            result.DoWhenSuccessful(() => Console.WriteLine("Unexpectedly, we didn't break anything!"));
            result.DoWhenError(HandleError);
        }

        public static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            return services
                .AddCQRS(typeof(Program).Assembly);
        }

        private static AddNumbersCommand AddNumbers(int a, int b) => new AddNumbersCommand(a, b);
        private static BreaksSomethingCommand Breaksomething => new BreaksSomethingCommand();

        private static async Task<Result> SuccessfulAdditionOfNumbers(int sum)
        {
            Console.WriteLine($"Command result is {sum}");
            return await dispatcher.ExecuteAsync(Breaksomething);
        }

        private static void HandleError(ResultsError error)
        {
            Console.WriteLine($"Uh oh. We broke something: {error.Message}");
        }

    }
}
