namespace maltedmoniker.cqrs
{
    public interface IHandled { }
    public interface ICommand : IHandled { }
    public interface IQuery : IHandled { }
}
