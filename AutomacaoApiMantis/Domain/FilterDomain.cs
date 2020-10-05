namespace AutomacaoApiMantis.Domain
{
    public class FilterDomain
    {
        public int FilterId { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string IsPublic { get; set; }
        public string FilterName { get; set; }
        public string FilterString { get; set; }
    }
}