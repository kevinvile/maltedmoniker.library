using FluentAssertions;
using maltedmoniker.result;
using System;
using System.Threading.Tasks;
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
        public void Result_Test4()
        {
            Result<long> r = Result<long>.Make(1);
            Result<long> r2 = Result<long>.Make(1);
            Result<long> r3 = (Result<long>)ResultsForbidden.Default;
            Result<long> r4 = (Result<long>)ResultsForbidden.Default;

            r.Should().Be(r2);
            r.Should().NotBe(r3);
            r.Should().NotBe(r4);
            r3.Should().Be(r4);
            r3.Should().NotBe(r);
            r3.Should().NotBe(r2);
        }

        [Fact]
        public async Task Result_Test5()
        {
            Result<long> r = ResultsCustomError.Default("error");
            var r2 = await r.MapToResultAsync(async l =>
            {
                if (true == false)
                {
                    await Task.Delay(10);
                }
                return Result.Success();
            });

            r2.Should().NotBeNull();
            r2.Should().Be((Result)ResultsCustomError.Default("error"));
        }

        [Fact]
        public void Error_Test1()
        {
            Result r = ResultsCustomError.Default("");
            Result r2 = ResultsCustomError.Default("");

            r.Should().Be(r2);
        }

        [Fact]
        public void Error_Test2()
        {
            Result<string> r = ResultsCustomError.Default("");
            Result<string> r2 = ResultsCustomError.Default("");

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

        [Fact]
        public void Optional_Test3()
        {
            Result<Optional<string>> test = (Optional<string>)"hi";
            Result<Optional<string>> test2 = (Optional<string>)"hi";

            object.Equals(test, test2).Should().BeTrue();
            test.Equals(test2).Should().BeTrue();
            test.Should().Be(test2);
        }

        [Fact]
        public void Optional_Test4()
        {
            Optional<string> myValue = None.Value;
            string? result = myValue.EscapeToNullable();

            result.Should().NotBe(string.Empty);
            result.Should().BeNull();

            string? result2 = myValue.EscapeToNullable(() => { });

            result2.Should().NotBe(string.Empty);
            result2.Should().BeNull();
        }


        [Fact]
        public void Optional_Test5()
        {
            Optional<DateTime> myDate = None.Value;
            DateTime? result = myDate.EscapeToNullable();

            result.Should().NotBe(default);
            result.Should().BeNull();

            DateTime? result2 = myDate.EscapeToNullable(() => { });

            result2.Should().NotBe(default);
            result2.Should().BeNull();
        }
    }
}
