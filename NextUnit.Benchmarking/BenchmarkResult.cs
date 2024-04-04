using System;

namespace NextUnit.Benchmarking
{
    public class BenchmarkResult
    {
        public object Value { get; private set; } = null;
        public string Text { get; } = string.Empty;
        public string Unit { get; } = string.Empty;

        public BenchmarkResult()
            : this(null, null)
        {

        }

        public BenchmarkResult(string text)
            : this(text, null)
        {

        }

        public BenchmarkResult(string text, object value, string unit)
        {
            this.Text = text;
            this.Value = value;
            this.Unit = unit;
        }

        public BenchmarkResult(object value)
            : this(null, value)
        {

        }

        public BenchmarkResult(string text, object value)
        {
            Text = text;
            this.Value = value;
        }

        public override string ToString()
        {
            return Text;
        }

        public static BenchmarkResult Empty()
        {
            return new BenchmarkResult();
        }

        public static implicit operator BenchmarkResult(string value)
        {
            if (double.TryParse(value, out var result))
            {
                return new BenchmarkResult(result);
            }
            else
            {
                throw new ArgumentException($"Cannot convert '{value}' to a BenchmarkResult.");
            }
        }
    }
}
