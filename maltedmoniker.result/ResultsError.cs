using System;

namespace maltedmoniker.result
{
    public abstract class ResultsError
    {
        public string Message { get; }

        protected ResultsError(string message)
        {
            Message = message;
        }

        public override bool Equals(object? obj)
        {
            return obj is ResultsError error &&
                   Message == error.Message;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * Message?.GetHashCode() ?? 1;
        }
    }

    public sealed class ResultsCustomError : ResultsError
    {
        internal ResultsCustomError(string message) : base(message) { }
        public static ResultsCustomError Default(string message) => new ResultsCustomError(message);
    }

    public sealed class ResultsNotFound : ResultsError
    {
        internal ResultsNotFound() : base("Resource Not Found") { }
        public static ResultsNotFound Default => new ResultsNotFound();
    }

    public sealed class ResultsUnauthorized : ResultsError
    {
        internal ResultsUnauthorized() : base("Not Authorized") { }
        public static ResultsUnauthorized Default => new ResultsUnauthorized();
    }

    public sealed class ResultsForbidden : ResultsError
    {
        internal ResultsForbidden() : base("Forbidden") { }
        public static ResultsForbidden Default => new ResultsForbidden();
    }

    public sealed class ResultsNotUpdated : ResultsError
    {
        internal ResultsNotUpdated() : base("Not Updated") { }
        public static ResultsNotUpdated Default => new ResultsNotUpdated();
    }
}
