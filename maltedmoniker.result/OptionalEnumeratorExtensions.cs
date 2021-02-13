using System;
using System.Collections.Generic;
using System.Linq;

namespace maltedmoniker.result
{
    public static class OptionalEnumeratorExtensions
    {
        public static IEnumerable<TOut> Flatten<TIn, TOut>(this IEnumerable<TIn> sequence, Func<TIn, Optional<TOut>> map)
            => sequence.Select(map)
                .ExcludeWhenNone();

        public static IEnumerable<TIn> ExcludeWhenNone<TIn>(this IEnumerable<Optional<TIn>> sequence)
            => sequence
                .OfType<Some<TIn>>()
                .Select(x => (TIn)x);

        public static Optional<TIn> FirstOrNone<TIn>(this IEnumerable<TIn> sequence, Func<TIn, bool> predicate)
            => sequence.Where(predicate)
                .Select<TIn, Optional<TIn>>(x => x)
                .DefaultIfEmpty(None.Value)
                .First();
    }
}
