namespace maltedmoniker.result
{
    public abstract class Optional<T>
    {
        public static implicit operator Optional<T>(T value)
            => value is not null 
                ? new Some<T>(value) 
                : new None<T>();

        public static implicit operator Optional<T>(None _)
            => new None<T>();
    }

    public sealed class Some<T> : Optional<T>
    {
        private T Content { get; }

        public Some(T content)
        {
            Content = content;
        }

        public static implicit operator T(Some<T> value) 
            => value.Content;
    }

    public sealed class None<T> : Optional<T> { }

    public sealed class None
    {
        public static None Value { get; } = new None();
        private None() { }
    }
}
