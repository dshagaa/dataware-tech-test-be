namespace SurveysApi.Utilities
{
    public class DateTimeUtils
    {
        private readonly IConfiguration _configuration;
        public DateTimeUtils(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DateTime Now()
        {
            return DateTimeOffset.UtcNow.DateTime;
        }
    }
}