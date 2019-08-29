using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Service.Implementations;
using Service.Interfaces;
using Service.Repositories;

namespace Service.ServiceCollections
{
    public static class EntityDependencies
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticateService, TokenAuthenticationService>();
            services.AddTransient<IUserManagementService, UserManagementService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<ICommentService, CommentService>();

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));     
            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ISavedArticlesRepository, SavedArticlesRepository>();
            services.AddTransient<IConfirmRepository, ConfirmRepository> ();

            return services;
        }
    }
}
