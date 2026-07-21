using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PIMS.Application.Interfaces.Authentication;
using PIMS.Infrastructure.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PIMS.Infrastructure.Authentication;

/// <summary>
/// Provides JWT token generation and validation functionality.
/// </summary>
public sealed class JwtService : IJwtService
{
    private readonly JwtOptions _jwtOptions;


    /// <summary>
    /// Initializes a new instance of JwtService.
    /// </summary>
    /// <param name="jwtOptions">
    /// JWT configuration.
    /// </param>
    public JwtService(
        IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }



    /// <inheritdoc/>
    public string GenerateToken(
        int userId,
        string email,
        string role)
    {
        var claims = new List<Claim>
        {
            new Claim(
                JwtRegisteredClaimNames.Sub,
                userId.ToString()),


            new Claim(
                JwtRegisteredClaimNames.Email,
                email),


            new Claim(
                ClaimTypes.Role,
                role),


            new Claim(
                JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
        };



        var key =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _jwtOptions.Key));



        var credentials =
            new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);



        var expiry =
            DateTime.UtcNow.AddMinutes(
                _jwtOptions.ExpiryInMinutes);



        var token =
            new JwtSecurityToken(

                issuer:
                    _jwtOptions.Issuer,


                audience:
                    _jwtOptions.Audience,


                claims:
                    claims,


                expires:
                    expiry,


                signingCredentials:
                    credentials
            );



        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }





    /// <inheritdoc/>
    public ClaimsPrincipal? ValidateToken(
        string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return null;
        }


        var tokenHandler =
            new JwtSecurityTokenHandler();



        var key =
            Encoding.UTF8.GetBytes(
                _jwtOptions.Key);



        try
        {
            var principal =
                tokenHandler.ValidateToken(
                    token,

                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,


                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                key),


                        ValidateIssuer = true,


                        ValidIssuer =
                            _jwtOptions.Issuer,


                        ValidateAudience = true,


                        ValidAudience =
                            _jwtOptions.Audience,


                        ValidateLifetime = true,


                        ClockSkew =
                            TimeSpan.Zero
                    },

                    out SecurityToken validatedToken);



            if (validatedToken is not JwtSecurityToken jwtToken)
            {
                return null;
            }



            if (!jwtToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }



            return principal;
        }
        catch
        {
            return null;
        }
    }
}