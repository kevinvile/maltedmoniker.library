using FluentAssertions;
using maltedmoniker.result;
using System;
using Xunit;

namespace maltedmoniker.library.unittests
{
    public class ResultTests
    {
        [Fact]
        public void Test1()
        {
            ResultsError error = ResultsNotFound.Default;
            object f = new { };
            Result<object> r = error;

            var result = r.Reduce(e =>
            {
                e.Should().Be(error);
                return f;
            });

            result.Should().Be(f);

        }

        
    }
}
