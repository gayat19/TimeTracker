namespace IdealTImeTracker.API.Models.DTOs.ApplicationConfiguration
{
    public class ApplicationConfigurationResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan? Value { get; set; }

        public Double Mins { get { return Value?.TotalMinutes ?? 0; } }

    }
}
