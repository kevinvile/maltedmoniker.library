﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace maltedmoniker.result
{
    public abstract class Optional<T> : IEquatable<Optional<T>>
    {
        public static implicit operator Optional<T>(T value)
            => value is not null 
                ? new Some<T>(value) 
                : new None<T>();

        public static implicit operator Optional<T>(None _)
            => new None<T>();

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is Optional<T> optional) Equals(this, optional);

            return false;
        }

        public override int GetHashCode()
        {
            int hash = base.GetHashCode();

            if (this is Some<T> a)
            {
                hash = (hash * 7) + a.GetHashCode();
            }

            return hash;
        }

        private static bool Equals(Optional<T> first, Optional<T>? second)
        {
            if (second is None<T> && first is None<T>) return true;
            if (second is Some<T> a && first is Some<T> b) return a.Equals(b);
            return false;
        }

        public bool Equals(Optional<T>? other)
        {
            return Equals(this, other);
        }
    }

    public sealed class Some<T> : Optional<T>, IEquatable<Some<T>>
    {
        private T Content { get; }

        public Some(T content)
        {
            Content = content;
        }

        public static implicit operator T(Some<T> value) 
            => value.Content;

        public override bool Equals(object? obj)
        {
            return obj is Some<T> some &&
                   InternalEquals(some);
        }

        public bool Equals(Some<T>? other)
        {
            return InternalEquals(other);
        }

        private bool InternalEquals(Some<T>? second)
        {
            return second is Some<T> some &&
                   EqualityComparer<T>.Default.Equals(Content, some.Content);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * Content?.GetHashCode() ?? 1;
        }
    }

    public sealed class None<T> : Optional<T> 
    {
        public override bool Equals(object? obj)
        {
            if (obj is None) return true;
            if (obj is None<T>) return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public sealed class None
    {
        public static None Value { get; } = new None();
        private None() { }
    }
}
