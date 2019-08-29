using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Repositories
{
  
    
        public interface IConfirmRepository : IRepository<Confirmation>
        {

        }

        public class ConfirmRepository : Repository<Confirmation>, IConfirmRepository
        {
            public ConfirmRepository(BlogContext dbContext) : base(dbContext)
            {


            }
        }
    }
