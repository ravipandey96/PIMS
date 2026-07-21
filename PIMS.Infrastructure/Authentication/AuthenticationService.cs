using Microsoft.Extensions.Options;
using PIMS.Application.Common.Exceptions;
using PIMS.Application.Common.Models.Authentication;
using PIMS.Application.DTOs.Authentication;
using PIMS.Application.Interfaces.Authentication;
using PIMS.Application.Interfaces.Persistence;
using PIMS.Domain.Entities;
using PIMS.Infrastructure.Configurations;


namespace PIMS.Infrastructure.Authentication;

/// <summary>
/// Provides authentication functionality including
/// login, refresh token rotation and logout.
/// </summary>
public sealed class AuthenticationService : IAuthenticationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly JwtOptions _jwtOptions;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="AuthenticationService"/> class.
    /// </summary>
    /// <param name="unitOfWork">
    /// Unit of Work.
    /// </param>
    /// <param name="jwtService">
    /// JWT service.
    /// </param>
    /// <param name="refreshTokenService">
    /// Refresh token service.
    /// </param>
    /// <param name="passwordHasher">
    /// Password hasher.
    /// </param>
    /// <param name="jwtOptions">
    /// JWT configuration.
    /// </param>
    public AuthenticationService(
        IUnitOfWork unitOfWork,
        IJwtService jwtService,
        IRefreshTokenService refreshTokenService,
        IPasswordHasher passwordHasher,
        IOptions<JwtOptions> jwtOptions)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _refreshTokenService = refreshTokenService;
        _passwordHasher = passwordHasher;
        _jwtOptions = jwtOptions.Value;
    }

    /// <inheritdoc/>
    /// <inheritdoc/>
    public async Task<LoginResponseDto> LoginAsync(
        LoginRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var user =
    await ValidateUser(
        request,
        cancellationToken);


        string accessToken =
            _jwtService.GenerateToken(
                user.Id,
                user.Email,
                user.Role.Name);


        RefreshTokenResult refreshToken =
            _refreshTokenService.GenerateRefreshToken(
                request.RememberMe);


        RefreshToken entity =
            CreateRefreshTokenEntity(
                user.Id,
                refreshToken);


        await _unitOfWork.RefreshTokens.AddAsync(
            entity,
            cancellationToken);


        await _unitOfWork.SaveChangesAsync(
            cancellationToken);


        return BuildLoginResponse(
            user,
            accessToken,
            refreshToken);
    }

    /// <inheritdoc/>
    /// <inheritdoc/>
    public async Task<LoginResponseDto> RefreshTokenAsync(
        RefreshTokenRequestDto request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);


        // 1. Validate refresh token input

        if (string.IsNullOrWhiteSpace(request.RefreshToken))
        {
            throw new UnauthorizedException(
                "Refresh token is required.");
        }



        // 2. Hash incoming refresh token

        string tokenHash =
            _refreshTokenService.ComputeHash(
                request.RefreshToken);



        // 3. Find refresh token from database

        RefreshToken? storedToken =
            await _unitOfWork.RefreshTokens
                .GetByTokenHashAsync(
                    tokenHash,
                    cancellationToken);



        // 4. Validate token exists

        if (storedToken == null)
        {
            throw new UnauthorizedException(
                "Invalid refresh token.");
        }



        // 5. Validate token status

        if (!storedToken.IsActive)
        {
            throw new UnauthorizedException(
                "Refresh token is expired or revoked.");
        }



        // 6. Get associated user

        User user =
            storedToken.User;



        if (user == null)
        {
            throw new UnauthorizedException(
                "User associated with token not found.");
        }



        // 7. Generate new JWT access token

        string accessToken =
            _jwtService.GenerateToken(
                user.Id,
                user.Email,
                user.Role.Name);



        // 8. Generate new refresh token

        RefreshTokenResult newRefreshToken =
            _refreshTokenService.GenerateRefreshToken();



        // 9. Revoke old refresh token

        RevokeToken(
    storedToken,
    "Token rotated.");


        storedToken.ReplacedByTokenHash =
            newRefreshToken.TokenHash;



        // 10. Create replacement refresh token entity

        var newRefreshTokenEntity =
            new RefreshToken
            {
                UserId = user.Id,

                TokenHash =
                    newRefreshToken.TokenHash,

                ExpiresOnUtc =
                    newRefreshToken.ExpiresOnUtc,

                IssuedOnUtc =
                    DateTime.UtcNow,

                CreatedOnUtc =
                    DateTime.UtcNow
            };



        // 11. Save new refresh token

        await _unitOfWork.RefreshTokens
            .AddAsync(
                newRefreshTokenEntity,
                cancellationToken);



        // 12. Save database changes

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);



        // 13. Return response

        return new LoginResponseDto
        {
            AccessToken =
                accessToken,


            RefreshToken =
                newRefreshToken.Token,


            AccessTokenExpiresOnUtc =
                DateTime.UtcNow.AddMinutes(
                    _jwtOptions.ExpiryInMinutes),


            RefreshTokenExpiresOnUtc =
                newRefreshToken.ExpiresOnUtc,


            UserId =
                user.Id,


            Username =
                user.Username,


            Email =
                user.Email,


            Role =
                user.Role.Name
        };
    }

    /// <inheritdoc/>
    /// <inheritdoc/>
    public async Task LogoutAsync(
        LogoutRequestDto request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);



        // 1. Validate refresh token input

        if (string.IsNullOrWhiteSpace(request.RefreshToken))
        {
            throw new UnauthorizedException(
                "Refresh token is required.");
        }



        // 2. Hash refresh token

        string tokenHash =
            _refreshTokenService.ComputeHash(
                request.RefreshToken);



        // 3. Find refresh token from database

        RefreshToken? storedToken =
            await _unitOfWork.RefreshTokens
                .GetByTokenHashAsync(
                    tokenHash,
                    cancellationToken);



        // 4. If token does not exist,
        // consider logout already completed

        if (storedToken == null)
        {
            return;
        }



        // 5. Check if already revoked

        if (storedToken.IsRevoked)
        {
            return;
        }



        // 6. Revoke refresh token

        RevokeToken(
    storedToken,
    "User logged out.");



        // 7. Save changes

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);
    }
    /// <summary>
    /// Validates user credentials.
    /// </summary>
    /// <param name="request">
    /// Login request.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Validated user.
    /// </returns>
    private async Task<User> ValidateUser(
        LoginRequestDto request,
        CancellationToken cancellationToken)
    {
        User? user =
            await _unitOfWork.Users
                .GetByEmailOrUsernameAsync(
                    request.EmailOrUsername,
                    cancellationToken);


        if (user == null)
        {
            throw new UnauthorizedException(
                "Invalid email/username or password.");
        }


        if (!user.IsActive)
        {
            throw new UnauthorizedException(
                "User account is inactive.");
        }


        bool passwordValid =
            _passwordHasher.VerifyPassword(
                request.Password,
                user.PasswordHash);


        if (!passwordValid)
        {
            throw new UnauthorizedException(
                "Invalid email/username or password.");
        }


        return user;
    }
    /// <summary>
    /// Creates a refresh token database entity.
    /// </summary>
    /// <param name="userId">
    /// User identifier.
    /// </param>
    /// <param name="refreshToken">
    /// Generated refresh token result.
    /// </param>
    /// <returns>
    /// Refresh token entity.
    /// </returns>
    private RefreshToken CreateRefreshTokenEntity(
        int userId,
        RefreshTokenResult refreshToken)
    {
        return new RefreshToken
        {
            UserId = userId,

            TokenHash =
                refreshToken.TokenHash,

            ExpiresOnUtc =
                refreshToken.ExpiresOnUtc,

            IssuedOnUtc =
                DateTime.UtcNow,

            CreatedOnUtc =
                DateTime.UtcNow
        };
    }
    /// <summary>
    /// Creates login response DTO.
    /// </summary>
    /// <param name="user">
    /// Authenticated user.
    /// </param>
    /// <param name="accessToken">
    /// JWT access token.
    /// </param>
    /// <param name="refreshToken">
    /// Refresh token result.
    /// </param>
    /// <returns>
    /// Login response.
    /// </returns>
    private LoginResponseDto BuildLoginResponse(
        User user,
        string accessToken,
        RefreshTokenResult refreshToken)
    {
        return new LoginResponseDto
        {
            AccessToken =
                accessToken,


            RefreshToken =
                refreshToken.Token,


            AccessTokenExpiresOnUtc =
                DateTime.UtcNow.AddMinutes(
                    _jwtOptions.ExpiryInMinutes),


            RefreshTokenExpiresOnUtc =
                refreshToken.ExpiresOnUtc,


            UserId =
                user.Id,


            Username =
                user.Username,


            Email =
                user.Email,


            Role =
                user.Role.Name
        };
    }
    /// <summary>
    /// Revokes a refresh token.
    /// </summary>
    /// <param name="token">
    /// Refresh token entity.
    /// </param>
    /// <param name="reason">
    /// Revocation reason.
    /// </param>
    private void RevokeToken(
        RefreshToken token,
        string reason)
    {
        token.RevokedOnUtc =
            DateTime.UtcNow;


        token.RevocationReason =
            reason;
    }
}