using System;
using System.Threading.Tasks;

namespace maltedmoniker.result
{
    public static class ResultExtensions
    {
        public static Result<TResult> Map<T, TResult>(this Result<T> result, Func<T, TResult> map)
            => result is Success<T> some
                ? (Result<TResult>)map(some)
                : (ResultsError)(Error<T>)result;

        public static async Task<Result<TResult>> MapAsync<T, TResult>(this Result<T> result, Func<T, Task<TResult>> map)
            => result is Success<T> some 
                ? (Result<TResult>)await map(some) 
                : (ResultsError)(Error<T>)result;

        public static Result<TResult> MapToResult<T, TResult>(this Result<T> result, Func<T, Result<TResult>> map)
            => result is Success<T> some 
                ? map(some) 
                : (ResultsError)(Error<T>)result;

        public static async Task<Result<TResult>> MapToResultAsync<T, TResult>(this Result<T> result, Func<T, Task<Result<TResult>>> map)
            => result is Success<T> some 
                ? await map(some) 
            : (ResultsError)(Error<T>)result;

        public static async Task<Result<T>> DoWhenSuccessfulAsync<T>(this Result<T> result, Func<T, Task> map)
        {
            if (result is Success<T> some) await map(some);
            return result;
        }
        public static Result<T> DoWhenSuccessful<T>(this Result<T> result, Action<T> map)
        {
            if (result is Success<T> some) map(some);
            return result;
        }

        public static async Task<Result<T>> DoWhenErrorAsync<T>(this Result<T> result, Func<ResultsError, Task> map)
        {
            if (result is Error<T> error) await map(error);
            return result;
        }
        public static Result<T> DoWhenError<T>(this Result<T> result, Action<ResultsError> map)
        {
            if (result is Error<T> error) map(error);
            return result;
        }

        public static T Reduce<T>(this Result<T> result, T whenError)
            => result is Success<T> some 
                ? some 
                : whenError;

        public static T Reduce<T>(this Result<T> result, Func<ResultsError, T> whenError)
            => result is Success<T> some 
                ? some 
                : whenError((Error<T>)result);

        public static async Task<T> ReduceAsync<T>(this Result<T> result, Func<ResultsError, Task<T>> whenError)
            => result is Success<T> some 
                ? some 
                : await whenError((Error<T>)result);

        public static Type GetType<T>(this Result<T> _)
            => typeof(T);


        public static Result<TResult> Map<TResult>(this Result result, Func<TResult> map)
            => result is Success 
                ? map() 
                : Result<TResult>.Error((Error)result);

        public static async Task<Result<TResult>> MapTAsync<TResult>(this Result result, Func<Task<TResult>> map)
            => result is Success 
                ? await map() 
                : Result<TResult>.Error((Error)result);

        public static Result<TResult> MapToResult<TResult>(this Result result, Func<Result<TResult>> map)
            => result is Success 
                ? map() 
                : Result<TResult>.Error((Error)result);

        public static async Task<Result<TResult>> MapToResultAsync<T, TResult>(this Result result, Func<Task<Result<TResult>>> map)
            => result is Success 
                ? await map() 
                : Result<TResult>.Error((Error)result);

        public static Result MapToResultAsync<TResult>(this Result<TResult> result, Func<TResult, Result> map)
            => result is Success<TResult> success
                ? map(success)
                : result;

        public static async Task<Result> MapToResultAsync<TResult>(this Result<TResult> result, Func<TResult, Task<Result>> map)
            => result is Success<TResult> success
                ? await map(success)
                : result;

        public static async Task<Result> DoWhenSuccessfulAsync(this Result result, Func<Task> map)
        {
            if (result is Success) await map();
            return result;
        }

        public static Result DoWhenSuccessful(this Result result, Action map)
        {
            if (result is  Success) map();
            return result;
        }

        public static async Task<Result> DoWhenErrorAsync(this Result result, Func<ResultsError, Task> map)
        {
            if (result is Error error) await map(error);
            return result;
        }
        public static Result DoWhenError(this Result result, Action<ResultsError> map)
        {
            if (result is Error error) map(error);
            return result;
        }

    }
}
