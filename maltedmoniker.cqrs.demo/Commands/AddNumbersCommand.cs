using maltedmoniker.result;
using System.Threading.Tasks;

namespace maltedmoniker.cqrs.demo.Commands
{
    public class AddNumbersCommand : ICommand
    {
        public int A { get; }
        public int B { get; }

        public AddNumbersCommand(int a, int b)
        {
            A = a;
            B = b;
        }
    }

    internal class AddNumbersCommandHandler : IHandler<AddNumbersCommand, int>
    {
        public async Task<Result<int>> Execute(AddNumbersCommand handled)
        {
            await Task.Delay(10);

            return handled.A + handled.B;
        }
    }
}
