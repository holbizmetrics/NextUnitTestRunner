namespace NextUnit.TestMethodCompletionDetector.NewFolder1
{
    public class TestCoverageResult
    {
        public int TotalMethods { get; set; } = -1;
        public int TestedMethods { get; set; } = -1;
        public List<string> UntestedMethods { get; set; } = new List<string>();
        public double TestedPercentage => TotalMethods > 0 ? (double)TestedMethods / TotalMethods * 100 : 0;
        public double UntestedPercentage => TotalMethods > 0 ? 100 - TestedPercentage : 0;

        public override string ToString()
        {
            var summary = $"{TestedMethods} of {TotalMethods} methods are tested. Tested: {TestedPercentage:0.00}%, Untested: {UntestedPercentage:0.00}%";
            if (UntestedMethods.Any())
            {
                summary += "\nUntested methods:";
                foreach (var method in UntestedMethods)
                {
                    summary += $"\n- {method}";
                }
            }
            else
            {
                summary += "\nAll methods are tested!";
            }
            return summary;
        }
    }
}
