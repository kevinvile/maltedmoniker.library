using FluentAssertions;
using maltedmoniker.result;
using System;
using Xunit;

namespace maltedmoniker.library.unittests
{
    public class ResultTests
    {
        [Fact]
        public void Result_Test1()
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

        [Fact]
        public void Result_Test2()
        {
            Result r = Result.Success();
            Result e = ResultsCustomError.Default("anything");
            Result e2 = ResultsCustomError.Default("anything");

            r.Should().Be(Result.Success());
            r.Should().NotBe(e);

            e.Should().Be(e2);
            e.Should().NotBe(r);
            
        }

        [Fact]
        public void Result_Test3()
        {
            Result<string> r = Result<string>.Make("hey there");
            Result<string> r2 = Result<string>.Make("hey there");

            r.Should().Be(r2);
        }

        [Fact]
        public void Optional_Test1()
        {
            Optional<string> optional = None.Value;

            optional.Should().Be((Optional<string>)None.Value);
            optional.Should().NotBe((Optional<int>)None.Value);
            optional.Should().Be(None.Value);
        }

        [Fact]
        public void Optional_Test2()
        {
            Optional<string> optional = "hello";
            var other = (Optional<string>)"hello";
            var other2 = (Optional<string>)"something else";
            object.Equals(optional, other).Should().BeTrue();
            optional.Should().Be(other);
            optional.Should().NotBe(other2);
            optional.Should().NotBe("hello");
        }
    }
}
