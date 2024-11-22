using EcommerceApplication.Application.DtoModel;
using EcommerceApplication.Application.ITokenService;
using EcommerceApplication.Application.ITokenService.EcommerceApplication.Application.ITokenService;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenHelper : ITokenHelper
{
    private readonly IConfiguration _configuration;

    public TokenHelper(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public string CreateToken(TokenUser user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var secretKey = _configuration["Token:SecretKey"];
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new ArgumentNullException("SecretKey is missing from the configuration.");
        }

            var claims = new[]
               {
                    new Claim(JwtRegisteredClaimNames.Sub, "user_id"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //var claims = new[]
        //{
        //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //        new Claim(ClaimTypes.Name, user.UserName),
        //        new Claim(ClaimTypes.Email, user.Email),
        //        new Claim(ClaimTypes.Role, user.RoleName)
        //};

        //var token = new JwtSecurityToken(
        //     _configuration["Token:Issuer"],
        //     _configuration["Token:Audience"],
        //    claims,
        //    expires: DateTime.Now.AddMinutes(30),
        //    signingCredentials: credentials
        //);

        //return new JwtSecurityTokenHandler().WriteToken(token);






    }

    // Token'ı doğrulayan metod
    public ClaimsPrincipal ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token))
            throw new ArgumentNullException(nameof(token), "Token cannot be null or empty.");

        var secretKey = _configuration["Token:SecretKey"];
        if (string.IsNullOrEmpty(secretKey))
            throw new ArgumentNullException("SecretKey is missing from the configuration.");

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            // Token'ı doğrulama ve claimsleri almak
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true, // Expiration kontrolü
                ClockSkew = TimeSpan.Zero, // Saat farkı toleransı
                ValidIssuer = _configuration["Token:Issuer"],
                ValidAudience = _configuration["Token:Audience"],
                IssuerSigningKey = signingKey
            };

            // Token'ı doğrulama ve token içindeki claimleri almak
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

            // Token'ın exp (expiration) claim'ini kontrol et
            var jwtToken = validatedToken as JwtSecurityToken;
            if (jwtToken != null && jwtToken.ValidTo < DateTime.UtcNow)
            {
                // Token süresi dolmuşsa
                throw new SecurityTokenExpiredException("The token has expired.");
            }

            return principal;
        }
        catch (SecurityTokenExpiredException)
        {
            // Expired token hatasını burada fırlatıyoruz
            throw; // Bu hata, try-catch içinde yakalanacak
        }
        catch (SecurityTokenException ex)
        {
            // Geçersiz token hatası
            throw new SecurityTokenException("Invalid token.", ex);
        }
        catch (Exception ex)
        {
            // Genel hata durumu
            throw new SecurityTokenException("An error occurred while validating the token.", ex);
        }
    }

}
