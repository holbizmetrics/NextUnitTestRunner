using System.Text;

namespace NextUnit.TestMethodCompleteness.NewFolder1
{
    public class ReportGenerator
    {
        private readonly TestDetectionSettings _settings;

        public ReportGenerator(TestDetectionSettings settings)
        {
            _settings = settings;
        }

        public string GenerateReportContent(Dictionary<string, List<string>> methodTestMap)
        {
            var reportBuilder = new StringBuilder();
            reportBuilder.AppendLine("Test Coverage Report:");

            foreach (var entry in methodTestMap)
            {
                bool hasTests = entry.Value.Count > 0;
                switch (_settings.Mode)
                {
                    case ReportMode.All:
                        reportBuilder.AppendLine($"{entry.Key}: {(hasTests ? "Tested" : "No tests found")}");
                        break;

                    case ReportMode.WithTests:
                        if (hasTests) reportBuilder.AppendLine($"{entry.Key}: Tested");
                        break;

                    case ReportMode.WithoutTests:
                        if (!hasTests) reportBuilder.AppendLine($"{entry.Key}: No tests found");
                        break;
                }
            }

            return reportBuilder.ToString();
        }
    }
}
