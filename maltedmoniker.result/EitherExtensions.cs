using System;
using System.Threading.Tasks;

namespace maltedmoniker.result
{
    public static class EitherExtensions
    {
        public static Either<TL, TNewR> Map<TL, TR, TNewR>(this Either<TL, TR> either, Func<TR, TNewR> map) 
            => either is Right<TL, TR> right
                ? (Either<TL, TNewR>)map(right)
                : (TL)(Left<TL, TR>)either;

        public static async Task<Either<TL, TNewR>> MapAsync<TL, TR, TNewR>(this Either<TL, TR> either, Func<TR, Task<TNewR>> map) 
            => either is Right<TL, TR> right
                ? (Either<TL, TNewR>)(await map(right))
                : (TL)(Left<TL, TR>)either;

        public static Either<TL, TNewR> Map<TL, TR, TNewR>(this Either<TL, TR> either, Func<TR, Either<TL, TNewR>> map) 
            => either is Right<TL, TR> right
                ? map(right)
                : (TL)(Left<TL, TR>)either;

        public static async Task<Either<TL, TNewR>> MapAsync<TL, TR, TNewR>(this Either<TL, TR> either, Func<TR, Task<Either<TL, TNewR>>> map) 
            => either is Right<TL, TR> right
                ? await map(right)
                : (TL)(Left<TL, TR>)either;

        public static TR Reduce<TL, TR>(this Either<TL, TR> either, Func<TL, TR> map) 
            => either is Left<TL, TR> left
                ? map(left)
                : (Right<TL, TR>)either;

        public static async Task<TR> ReduceAsync<TL, TR>(this Either<TL, TR> either, Func<TL, Task<TR>> map) 
            => either is Left<TL, TR> left
               ? await map(left)
               : (Right<TL, TR>)either;


        public static Either<TL, TR> Reduce<TL, TR>(this Either<TL, TR> either, Func<TL, TR> map, Func<TL, bool> mapWhen) 
            => either is Left<TL, TR> bound && mapWhen(bound)
                ? map(bound)
                : either;

        public static async Task<Either<TL, TR>> ReduceAsync<TL, TR>(this Either<TL, TR> either, Func<TL, Task<TR>> map, Func<TL, bool> mapWhen)
            => either is Left<TL, TR> bound && mapWhen(bound)
                ? await map(bound)
                : either;

        public static Task DoWhenRightAsync<TL, TR>(this Either<TL, TR> either, Func<TR, Task> action)
            => either is Right<TL, TR> bound
                ? action(bound)
                : Task.CompletedTask;

        public static void DoWhenRight<TL, TR>(this Either<TL, TR> either, Action<TR> action)
        {
            if (either is not Right<TL, TR> bound) return;
            action(bound);
        }

    }
}
