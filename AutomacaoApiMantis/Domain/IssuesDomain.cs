namespace AutomacaoApiMantis.Domain
{
    public class IssuesDomain
    {
        public int BugId { get; set; }
        public int ProjectId { get; set; }
        public int ReporterId { get; set; }
        public int HandlerId { get; set; }
        public int DuplicateId { get; set; }
        public int Priority { get; set; }
        public int Severity { get; set; }
        public int Reproducibility { get; set; }
        public int Status { get; set; }
        public int Resolution { get; set; }
        public int Projection { get; set; }
        public int Eta { get; set; }
        public int BugTextId { get; set; }
        public string Os { get; set; }
        public string OsBuild { get; set; }
        public string Platform { get; set; }
        public string Version { get; set; }
        public string FixedInVersion { get; set; }
        public string Build { get; set; }
        public int ProfileId { get; set; }
        public int ViewState { get; set; }
        public string Summary { get; set; }
        public int SponsorshipTotal { get; set; }
        public int Sticky { get; set; }
        public string TargetVersion { get; set; }
        public int CategoryId { get; set; }
        public int DateSubmitted { get; set; }
        public int DueDate { get; set; }
        public int LastUpdated { get; set; }
    }
}