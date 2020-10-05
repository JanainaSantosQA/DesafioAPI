namespace AutomacaoApiMantis.Domain
{
    public class ProjectDomain
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int ProjectStatusId { get; set; }
        public int Enabled { get; set; }
        public int ViewState { get; set; }
        public int AccessMin { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int InheritGlobal { get; set; }
        public int ChildId { get; set; }
        public int ParentId { get; set; }
        public int InheritParent { get; set; }
        public int VersionId { get; set; }
        public string VersionName { get; set; }
        public string VersionDescription { get; set; }
        public int VersionReleased { get; set; }
        public int VersionObsolete { get; set; }
    }
}