using System;
using System.Collections.Generic;

namespace maltedmoniker.result
{
    public abstract class Result : IEquatable<Result>
    {
        public static Result Success()
            => new Success();

        public static implicit operator Result(ResultsError error)
            => new Error(error);

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is Result res) return Equals(res);

            return false;
            
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(Result? other)
        {
            if (other is Success && this is Success) return true;
            if (other is Error a && this is Error b) return a.Equals(b);

            return false;
        }
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

        public override bool Equals(object? obj)
        {
            return obj is Error error &&
                   EqualityComparer<ResultsError>.Default.Equals(Value, error.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Value);
        }
    }

    public class Success : Result
    {

    }

    public abstract class Result<T> : IEquatable<Result<T>>
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

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is Result<T> res) return Equals(res);
            
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(Result<T>? other)
        {
            if (other is Success<T> sA && this is Success<T> sB) return sA.Equals(sB);
            if (other is Error<T> a && this is Error<T> b) return a.Equals(b);
            return false;
        }
    }

    public sealed class Error<T> : Result<T>
    {
        private ResultsError Value { get; }

        public Error(ResultsError error)
        {
            Value = error;
        }

        public override bool Equals(object? obj)
        {
            return obj is Error<T> error &&
                   EqualityComparer<ResultsError>.Default.Equals(Value, error.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Value);
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

        public override bool Equals(object? obj)
        {
            return obj is Success<T> success &&
                   EqualityComparer<T>.Default.Equals(Content, success.Content);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Content);
        }

        public static implicit operator T(Success<T> obj)
            => obj.Content;

    }
}
