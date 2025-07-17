namespace Organiser.Core.Exceptions.Reports
{
    public class ReportsExceptions : Exception
    {
        public ReportsExceptions(string message) : base(message)
        {
        }
    }

    public class ReportNotFoundExceptions : ReportsExceptions
    {
        public ReportNotFoundExceptions(string message) : base(message)
        {
        }
    }

    //public class BugTitleRequiredExceptions : ReportsExceptions
    //{
    //    public BugTitleRequiredExceptions(string message) : base(message)
    //    {
    //    }
    //}

    //public class BugTitleMax200Exceptions : ReportsExceptions
    //{
    //    public BugTitleMax200Exceptions(string message) : base(message)
    //    {
    //    }
    //}

    //public class BugTextRequiredExceptions : ReportsExceptions
    //{
    //    public BugTextRequiredExceptions(string message) : base(message)
    //    {
    //    }
    //}

    //public class BugTextMax4000Exceptions : ReportsExceptions
    //{
    //    public BugTextMax4000Exceptions(string message) : base(message)
    //    {
    //    }
    //}
}