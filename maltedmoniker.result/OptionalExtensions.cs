using System;
using System.Threading.Tasks;

namespace maltedmoniker.result
{
    public static class OptionalExtensions
    {
        public static Optional<TResult> MapToOption<T, TResult>(this Optional<T> option, Func<T, TResult> map)
            => option is Some<T> some ? (Optional<TResult>)map(some) : None.Value;

        public static async Task<Optional<TResult>> MapToOptionAsync<T, TResult>(this Optional<T> option, Func<T, Task<TResult>> map)
            => option is Some<T> some ? (Optional<TResult>)await map(some) : None.Value;

        public static Optional<TResult> Map<T, TResult>(this Optional<T> option, Func<T, Optional<TResult>> map)
            => option is Some<T> some ? map(some) : None.Value;

        public static async Task<Optional<TResult>> MapAsync<T, TResult>(this Optional<T> option, Func<T, Task<Optional<TResult>>> map)
            => option is Some<T> some ? await map(some) : None.Value;

        public static Task DoWhenHasValueAsync<T>(this Optional<T> option, Func<T, Task> map)
            => option is Some<T> some ? map(some) : Task.CompletedTask;

        public static void DoWhenHasValue<T>(this Optional<T> option, Action<T> map)
        {
            if (option is not Some<T> some) return;
            map(some);
        }

        public static bool HasValue<T>(this Optional<T> option)
            => option is Some<T>;

        public static bool HasNoValue<T>(this Optional<T> option)
            => !option.HasValue();

        public static DateTime? EscapeToNullable(this Optional<DateTime> option)
            => option
                .MapToNullable()
                .ReduceToValue((DateTime?)null);

        public static DateTime? EscapeToNullable(this Optional<DateTime> option, Action whenNull)
           => option
               .MapToNullable()
               .ReduceToValue(InvokeAndReturn<DateTime?>(whenNull));

        private static Optional<DateTime?> MapToNullable(this Optional<DateTime> option)
            => option.Map<DateTime, DateTime?>(o => o);

        public static T? EscapeToNullable<T>(this Optional<T> option) //where T : class
            => option
                .MapToNullable()
                .ReduceToValue(default(T?));

        public static T? EscapeToNullable<T>(this Optional<T> option, Action whenNull) //where T : class
            => option
                .MapToNullable()
                .ReduceToValue(InvokeAndReturn<T?>(whenNull));

        private static Optional<T?> MapToNullable<T>(this Optional<T> option) //where T : class
            => option.Map<T, T?>(o => o);

        private static Func<T?> InvokeAndReturn<T>(Action invoke)
            => () =>
            {
                invoke.Invoke();
                return default;
            };

        public static Optional<T> When<T>(this T value, Func<T, bool> predicate)
            => predicate(value) ? (Optional<T>)value : None.Value;

        public static Optional<T> When<T>(this Optional<T> value, Func<T, bool> predicate)
            => value is Some<T> some && predicate((T)some) ? some : None.Value;

        public static T ReduceToValue<T>(this Optional<T> option, T whenNone)
            => option is Some<T> some ? some : whenNone;

        public static T ReduceToValue<T>(this Optional<T> option, Func<T> whenNone)
            => option is Some<T> some ? some : whenNone();

        public static async Task<T> ReduceToValueAsync<T>(this Optional<T> option, Func<Task<T>> whenNone)
            => option is Some<T> some ? some : await whenNone();

        public static Type GetType<T>(this Optional<T> _)
            => typeof(T);
    }
}
