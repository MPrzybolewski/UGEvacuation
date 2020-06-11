using System.Collections.Generic;
using UGEvacuationCommon.Enums;
using UGEvacuationCommon.Models;
using UGEvacuationDAL.Entities;
using UGEvacuationDAL.Repositories.Interfaces;

namespace UGEvacuationDAL.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly UGEvacuationContext _dbContext;

        public AppUserRepository(UGEvacuationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new UGEvacuationException("AppUserRepository - token cannot be null or empty", type: ErrorType.InvalidArgument);
            
            var appUser = new AppUser
            {
                Token = token
            };
            
            _dbContext.AppUsers.Add(appUser);
            _dbContext.SaveChanges();
        }

        public IEnumerable<AppUser> GetAll()
        {
            return _dbContext.AppUsers.AsQueryable();
        }
    }
}