using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UGEvacuationCommon.Enums;
using UGEvacuationCommon.Models;
using UGEvacuationDAL.Entities;
using UGEvacuationDAL.Repositories.Interfaces;

namespace UGEvacuationDAL.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        public async Task<AppUser> Create(string token)
        {
            using (var context = new UGEvacuationContext())
            {
                if (string.IsNullOrEmpty(token))
                    throw new UGEvacuationException("AppUserRepository - token cannot be null or empty",
                        type: ErrorType.InvalidArgument);

                var appUser = new AppUser
                {
                    Token = token
                };

                await context.AppUsers.AddAsync(appUser);
                await context.SaveChangesAsync();
                return appUser;
            }
        }

        public async Task<List<AppUser>> GetAll()
        {
            using (var context = new UGEvacuationContext())
            {
                return await context.AppUsers.ToListAsync();
            }
        }
    }
}