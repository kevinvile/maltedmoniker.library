using maltedmoniker.result;
using System.Threading.Tasks;

namespace maltedmoniker.cqrs.demo.Commands
{
    public class BreaksSomethingCommand : ICommand
    {
        
    }

    internal class BreaksSomethingCommandHandler : IHandler<BreaksSomethingCommand>
    {
        public async Task<Result> Execute(BreaksSomethingCommand handled)
        {
            await Task.Delay(10);
            return ResultsCustomError.Default("Something bad happened!");
        }
    }
}
