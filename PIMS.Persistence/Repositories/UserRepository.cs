using Microsoft.EntityFrameworkCore;
using PIMS.Application.Interfaces.Persistence;
using PIMS.Domain.Entities;
using PIMS.Persistence.Context;

namespace PIMS.Persistence.Repositories;

/// <summary>
/// Repository implementation for User.
/// </summary>
public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(PimsDbContext context)
        : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(
                u => u.Email == email,
                cancellationToken);
    }

    public async Task<User?> GetByUsernameAsync(
        string username,
        CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(
                u => u.Username == username,
                cancellationToken);
    }

    public async Task<bool> EmailExistsAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AnyAsync(
                u => u.Email == email,
                cancellationToken);
    }

    public async Task<bool> UsernameExistsAsync(
        string username,
        CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AnyAsync(
                u => u.Username == username,
                cancellationToken);
    }

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(
        int roleId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .Where(u => u.RoleId == roleId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> GetActiveUsersAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .Where(u => u.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<User?> GetByEmailOrUsernameAsync(
    string emailOrUsername,
    CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(
                u =>
                    u.Email == emailOrUsername ||
                    u.Username == emailOrUsername,
                cancellationToken);
    }
}