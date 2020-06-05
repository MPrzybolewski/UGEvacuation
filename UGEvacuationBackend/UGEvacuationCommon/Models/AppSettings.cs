namespace UGEvacuationCommon.Models
{
    public interface IAppSettings
    {
        IJwtSettings Jwt { get; }
    }

    public interface IJwtSettings
    {
       string Key { get; }
       string Issuer { get; }
    }

    public class AppSettings : IAppSettings
    {
        public IJwtSettings Jwt { get; }

        public AppSettings()
        {
            Jwt = new JwtSettings();
        }
    }

    public class JwtSettings : IJwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
    }
}