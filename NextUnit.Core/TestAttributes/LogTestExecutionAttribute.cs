namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// 
    /// </summary>
    public class LogTestExecutionAttribute : CommonTestAttribute, ItestRun
    {
        public LogTestExecutionAttribute()
        {
        }

        public virtual void BeforeTestExecution()
        {

        }

        public void AfterTestExecution()
        {
        }

        private void Log()
        {

        }
        public void BeforeTestRun()
        {
            throw new NotImplementedException();
        }

        public void AfterTestRun()
        {
            throw new NotImplementedException();
        }
    }

    public interface ItestRun: ITestContext
    {
        void BeforeTestRun();
        void AfterTestRun();
    }

    public interface ITestContext
    {
        void BeforeTestExecution();
        void AfterTestExecution();
    }
}
