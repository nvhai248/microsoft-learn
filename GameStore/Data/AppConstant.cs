using Microsoft.Extensions.Configuration;

namespace GameStore.Data;

public static class AppConstant
{
    private static JwtSetting? _jwtSettings;

    // Initialize from configuration (call this once in Program.cs)
    public static void Init(IConfiguration configuration)
    {
        _jwtSettings = configuration.GetSection("Jwt").Get<JwtSetting>()
            ?? throw new InvalidOperationException("JWT settings are missing in configuration");
    }

    public static JwtSetting Jwt =>
        _jwtSettings ?? throw new InvalidOperationException("AppConstant not initialized. Call AppConstant.Init in Program.cs");
}
