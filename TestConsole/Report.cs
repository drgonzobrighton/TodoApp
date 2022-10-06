using System;
using System.Collections.Generic;

namespace TestConsole;
public class Report
{
    public ReportInfo ReportInfo { get; set; }
    public List<IDataSource> DataSources { get; set; }
    public List<IReportItem> ReportItems { get; set; }

    public Report()
    {
        DataSources = new List<IDataSource>();
        ReportItems = new List<IReportItem>();
    }

}

public class ReportInfo
{
    public string Name { get; set; }
    public DateTime GeneratedDate { get; set; }
}

public enum SourceType
{

}

public interface IDataSource
{
    public SourceType SourceType { get; set; }
}

public class AccountsDataSource : IDataSource
{
    public SourceType SourceType { get; set; }

    public List<BankingAccount> Accounts { get; set; }

    public AccountsDataSource()
    {
        Accounts = new List<BankingAccount>();
    }
}

public class BankingAccount { }
public class BankingTransaction { }


public interface IReportItem { }

public interface IPagedReportItem : IReportItem
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalResults { get; set; }
}

public class TransactionsReportItem : IPagedReportItem
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalResults { get; set; }
    public List<BankingTransaction> Transactions { get; set; }
}
