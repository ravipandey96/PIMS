using Microsoft.EntityFrameworkCore;
using PIMS.Application.Interfaces.Persistence;
using PIMS.Domain.Entities;
using PIMS.Persistence.Context;

namespace PIMS.Persistence.Repositories;

/// <summary>
/// Repository implementation for Role entity.
/// </summary>
public class RoleRepository : Repository<Role>, IRoleRepository
{
    private readonly PimsDbContext _context;


    /// <summary>
    /// Initializes a new instance of RoleRepository.
    /// </summary>
    /// <param name="context">
    /// Database context.
    /// </param>
    public RoleRepository(PimsDbContext context)
        : base(context)
    {
        _context = context;
    }


    /// <summary>
    /// Gets role by name.
    /// </summary>
    public async Task<Role?> GetByNameAsync(
        string roleName,
        CancellationToken cancellationToken = default)
    {
        return await _context.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Name.ToLower() == roleName.ToLower(),
                cancellationToken);
    }



    /// <summary>
    /// Checks whether role exists.
    /// </summary>
    public async Task<bool> RoleExistsAsync(
        string roleName,
        CancellationToken cancellationToken = default)
    {
        return await _context.Roles
            .AnyAsync(
                x => x.Name.ToLower() == roleName.ToLower(),
                cancellationToken);
    }



    /// <summary>
    /// Gets role with users.
    /// </summary>
    public async Task<Role?> GetRoleWithUsersAsync(
        int roleId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Roles
            .Include(x => x.Users)
            .FirstOrDefaultAsync(
                x => x.Id == roleId,
                cancellationToken);
    }
}