namespace IdealTImeTracker.API.Models
{
    public class ApplicationConfiguration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan? Value { get; set; }  
    }
}
