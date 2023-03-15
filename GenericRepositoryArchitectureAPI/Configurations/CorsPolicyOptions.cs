namespace GenericRepositoryArchitectureAPI.Configurations
{
    public class CorsPolicyOptions
    {
        public const string CorsPolicy = "CorsPolicy";

        public bool AllowAnyHeader { get; set; }
        public bool AllowAnyMethod { get; set; }
        public bool AllowAnyOrigin { get; set; }
        public bool AllowCredentials { get; set; }
        public bool DisallowCredentials { get; set; }

        public string[] ExposedHeaders { get; set; } = Array.Empty<string>();
        public string[] Headers { get; set; } = Array.Empty<string>();
        public string[] Methods { get; set; } = Array.Empty<string>();
        public string[] Origins { get; set; } = Array.Empty<string>();

        public TimeSpan? PreflightMaxAge { get; set; }
    }
}
