using System;
using System.Collections.Generic;
using System.Linq;

namespace NextUnit.Benchmarking.Statistics
{
    /// <summary>
    /// Statistics calculation.
    /// 
    /// For now provides:
    /// 
    /// - the mean (same as average, just called "mean" in English in the statistical context
    /// - the median
    /// - max
    /// - min
    /// 
    /// - the standard deviation
    /// - the standard error
    /// 
    /// - skewness, kurtosis, etc. are not calculated, yet.
    /// 
    /// Most important values to get a reliable interpretation of the result is at least:
    /// 
    /// - Mean, Standard Deviation, Skewness and Kurtosis.
    /// 
    /// Explanation:
    /// 
    /// - The mean is the simplest way to aggregate that data.
    /// - The variance (aka Standard Deviation) shows the data spread.
    /// - The Skewness is a measure of asymmetry
    /// - The kurtosis is a measure of peakedness.
    /// 
    /// Many times it may make sense to start with a sample size of between 15 and 3 (a larger sample size will be definitely neeeded 
    /// if the distribution is very skewed or there are many outliers.
    /// 
    /// Be aware that the sample size here is important for a critical accuracy.
    /// </summary>
    public class StatisticsCalculator
    {
        /// <summary>
        /// Calculates the mean (the average is called mean in the statistics context, in English)
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double CalculateMean(IEnumerable<double> values)
        {
            return values.Average();
        }

        /// <summary>
        /// Calculates the median.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double CalculateMedian(IEnumerable<double> values)
        {
            var sortedValues = values.OrderBy(x => x).ToList();
            int size = sortedValues.Count;
            double median = size % 2 == 0 ? (sortedValues[size / 2 - 1] + sortedValues[size / 2]) / 2.0 : sortedValues[size / 2];
            return median;
        }

        /// <summary>
        /// Calculates the standard deviation.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double CalculateStandardDeviation(IEnumerable<double> values)
        {
            double mean = CalculateMean(values);
            double sumOfSquaresOfDifferences = values.Sum(val => (val - mean) * (val - mean));
            return Math.Sqrt(sumOfSquaresOfDifferences / values.Count());
        }

        /// <summary>
        /// Calculates the standard error.
        /// 
        /// You can interpret the standard error as a measure of accuracy:
        /// a smaller standard error means that you have a better estimation of the true mean.
        /// 
        /// (it depends on the sample size (values) and the standard deviation.)
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double CaclulateStandardError(IEnumerable<double> values)
        {
            return CalculateStandardDeviation(values) / Math.Sqrt(values.Count());
        }

        /// <summary>
        /// Calculates the maximum.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double CalculateMax(IEnumerable<double> values)
        {
            return values.Max();
        }

        /// <summary>
        /// Calculates the minimum.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double CalculateMin(IEnumerable<double> values)
        {
            return values.Min();
        }

        /// <summary>
        /// This is an own implementation which is calculating the asymptoticness (how much the curve is skewed).
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double CalculateSkewness(IEnumerable<double> values)
        {
            double mean = values.Average();
            double n = values.Count();
            double sumCubeDifferences = values.Sum(x => Math.Pow(x - mean, 3)); // here we need the cubics root. Not the square root.
            double cubedStandardDeviation = Math.Pow(CalculateStandardDeviation(values), 3);
            return (n / ((n - 1) * (n - 2))) * (sumCubeDifferences / cubedStandardDeviation);
        }

        /// <summary>
        /// This is the pearson median skewness (as a simplified model)
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double CalculatePearsonMedianSekewness(IEnumerable<double> values)
        {
            double mean = CalculateMean(values);
            double median = CalculateMedian(values);
            double standardDeviation = CalculateStandardDeviation(values);
            return 3 * (mean - median) / standardDeviation;
        }

        /// <summary>
        /// This is an own implementation which is calculating the asymptoticness (how much the curve is skewed). <summary>
        /// 
        /// Explanation : Kurtosis is the measure of "peakedness".
        /// A high kurtosis means that the distribution peak is sharp.
        /// A small kurtosis means that the distribution peak is flat.
        /// 
        /// This function here explicitly will concentrate on calculating the Excess Kurtosis.
        /// In many books this will be wrongly denoted as the Standard aka Default Kurtosis. Which this definitely isn't.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double CalculateExcessKurtosis(IEnumerable<double> values)
        {
            double mean = values.Average();
            double n = values.Count();
            double sumQuartedDifferences = values.Sum(x => Math.Pow(x - mean, 4));
            double quartedStandardDeviation = Math.Pow(CalculateStandardDeviation(values), 4);
            double kurtosis = (n * (n + 1) / ((n - 1) * (n - 2) * (n - 3))) * (sumQuartedDifferences / quartedStandardDeviation) - (3 * Math.Pow(n - 1, 2) / ((n - 2) * (n - 3)));
            return kurtosis;
        }

        //public static void Result(IEnumerable<double> values)
        //{
        //    List<Delegate> resultsDelegates = new List<Delegate>();
        //    resultsDelegates.Add(CalculateMean);

        //    foreach (Delegate @delegate in resultsDelegates)
        //    {
        //         @delegate.DynamicInvoke(values);
        //    }
        //}

        public StatisticsCalculator Empty()
        {
            return new StatisticsCalculator();
        }

        public static StatisticsCalculator Statistics
        {
            get { return new StatisticsCalculator(); }
        }
    }
}
