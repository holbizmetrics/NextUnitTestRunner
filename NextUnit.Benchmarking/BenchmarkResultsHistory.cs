using NextUnit.Benchmarking.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NextUnit.Benchmarking
{
    /// <summary>
    /// Use this only if the results are still needed after running the benchmarks.
    /// Otherwise they will all be cleaned up by the attribute itself.
    /// </summary>
    public class BenchmarkResultsHistory : Dictionary<Type, List<BenchmarkResult>>, IDisposable
    {
        private bool disposedValue;

        // Other members remain unchanged...

        public double Mean(Type type) => StatisticsCalculator.CalculateMean(ConvertBenchmarkResultsToDoubles(this[type]));
        public double Deviation(Type type) => StatisticsCalculator.CalculateStandardDeviation(ConvertBenchmarkResultsToDoubles(this[type]));
        public double Skewness(Type type) => StatisticsCalculator.CalculateSkewness(ConvertBenchmarkResultsToDoubles(this[type]));
        public double Kurtosis(Type type) => StatisticsCalculator.CalculateExcessKurtosis(ConvertBenchmarkResultsToDoubles(this[type]));
        public double Median(Type type) => StatisticsCalculator.CalculateMedian(ConvertBenchmarkResultsToDoubles(this[type]));
        public double Min(Type type) => StatisticsCalculator.CalculateMin(ConvertBenchmarkResultsToDoubles(this[type]));
        public double Max(Type type) => StatisticsCalculator.CalculateMax(ConvertBenchmarkResultsToDoubles(this[type]));
        public double StandardError(Type type) => StatisticsCalculator.CaclulateStandardError(ConvertBenchmarkResultsToDoubles(this[type]));
        public double StandardDeviation(Type type) => StatisticsCalculator.CalculateStandardDeviation(ConvertBenchmarkResultsToDoubles(this[type]));
        private IEnumerable<double> ConvertBenchmarkResultsToDoubles(List<BenchmarkResult> results)
        {
            return results.Select(result =>
            {
                // Attempt to convert the Value to double, return 0 or a default value if unsuccessful
                if (result.Value is double valueAsDouble)
                {
                    // Directly return the double value
                    return valueAsDouble;
                }
                else if (result.Value is int valueAsInt)
                {
                    // Convert int to double
                    return Convert.ToDouble(valueAsInt);
                }
                else if (result.Value != null && double.TryParse(result.Value.ToString(), out double parsedDouble))
                {
                    // Successfully parsed the string representation to a double
                    return parsedDouble;
                }
                else
                {
                    // Return a default value (e.g., 0) if none of the above conversions are possible
                    // Alternatively, you might choose to skip these values entirely or handle them differently
                    return 0.0;
                }
            });
        }

        //public object AllResultsPerType()
        //{
        //    List<Delegate> calculationFunctions = new List<Delegate>() {
        //        Mean,Deviation, Skewness, Kurtosis, Median, Min, Max, StandardError, StandardDeviation
        //    };

        //    foreach (KeyValuePair<Type, List<BenchmarkResult>> result in this)
        //    {
        //        IEnumerable<double> values = ConvertBenchmarkResultsToDoubles(this[result.Key]);
        //        foreach (Delegate @delegate in calculationFunctions)
        //        {
        //            double currentResult = (double)@delegate.DynamicInvoke(values);
        //        }
        //    }
        //    return null;
        //}

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    this.Clear();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BenchmarkResultsHistory()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public override string ToString()
        {
            StringBuilder resultsStringBuilder = new StringBuilder();
            foreach (KeyValuePair<Type, List<BenchmarkResult>> resultsPerType in this)
            {
                foreach (BenchmarkResult result in resultsPerType.Value)
                {
                    resultsStringBuilder.AppendLine(result.ToString());
                }
            }
            return resultsStringBuilder.ToString();
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
