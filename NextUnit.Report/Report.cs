namespace NextUnit.Report
{
    public class Report
    {
        public IReportCreationBehavior ReportCreationBehavior { get; set; } = new ReportCreationBehavior();

        public void Create()
        {

        }


        public void Update()
        {

        }
    }

    public interface IReportCreationBehavior
    {
        string GenerateReportContent();
    }

    public class ReportCreationBehavior : IReportCreationBehavior
    {
        public string GenerateReportContent()
        {
            return string.Empty;
        }
    }
}
