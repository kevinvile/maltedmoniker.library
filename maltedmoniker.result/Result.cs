﻿namespace maltedmoniker.result
{
    public abstract class Result
    {
        public static Result Success()
            => new Success();

        public static implicit operator Result(ResultsError error)
            => new Error(error);
    }

    public sealed class Error : Result
    {
        private ResultsError Value { get; }

        public Error(ResultsError value)
        {
            Value = value;
        }


        public static implicit operator ResultsError(Error obj)
            => obj.Value;
    }

    public class Success : Result
    {

    }

    public abstract class Result<T>
    {
        public static Result<T> Make(T obj)
            => new Success<T>(obj);

        public static Result<T> Error(ResultsError error)
            => new Error<T>(error);

        public static implicit operator Result<T>(T obj)
            => new Success<T>(obj);

        public static implicit operator Result<T>(ResultsError error)
            => new Error<T>(error);

        public static implicit operator Error(Result<T> result)
            => result is Error err ? err : default!;

    }

    public sealed class Error<T> : Result<T>
    {
        private ResultsError Value { get; }

        public Error(ResultsError error)
        {
            Value = error;
        }

        public static implicit operator ResultsError(Error<T> obj)
            => obj.Value;
    }

    public sealed class Success<T> : Result<T>
    {
        private T Content { get; }

        public Success(T content)
        {
            Content = content;
        }

        public static implicit operator T(Success<T> obj)
            => obj.Content;

    }
}