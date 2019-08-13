﻿using Service.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICategoryService
    {
      
        Task<Category> GetByIDAsync(int id);
        Task<bool> CreateAsync(Category category);
        Task<bool> DeleteByIDAsync(int id);
        List<Category> GetList(string orderBy);

        Task<bool> PatchAsync(Category category, int id);

    }
}
