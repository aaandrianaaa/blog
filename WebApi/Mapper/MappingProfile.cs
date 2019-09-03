using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Requests;
using Service.Helper;
using WebApi.ViewModel;

namespace WebApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCategoryRequest, Category>()
                .ForMember(c => c.Name, map => map.MapFrom(m => m.Name))
                .ForMember(c => c.ID, map => map.Ignore())
                .ForMember(c => c.Description, map => map.MapFrom(m => m.Description))
                .ForMember(c => c.ArticleCount, map => map.Ignore())
                .ForMember(c => c.ViewCount, map => map.Ignore())
            .ForMember(c => c.CreatedAt, map => map.MapFrom(m => DateTime.Now))
            .ForMember(c => c.UpdatedAt, map => map.MapFrom(m => DateTime.Now));

            CreateMap<PatchCategoryRequest, Category>()
                .ForMember(c => c.Name, map => map.MapFrom(m => m.Name))
                .ForMember(c => c.Description, map => map.MapFrom(m => m.Description))
                .ForMember(c => c.ID, map => map.Ignore())
                 .ForMember(c => c.ArticleCount, map => map.Ignore())
                .ForMember(c => c.ViewCount, map => map.Ignore())
                .ForMember(c => c.CreatedAt, map => map.Ignore())
                .ForMember(c => c.UpdatedAt, map => map.MapFrom(m => DateTime.Now));




            CreateMap<CreateArticleRequestcs, Article>()
                .ForMember(a => a.Name, map => map.MapFrom(m => m.Name))
                .ForMember(a => a.Text, map => map.MapFrom(m => m.Text))
                .ForMember(a => a.CategoryID, map => map.MapFrom(m => m.CategoryID))
                .ForMember(a => a.ID, map => map.Ignore())
                .ForMember(a => a.ViewCount, map => map.MapFrom(m => 0))
                .ForMember(a => a.CreatedAt, map => map.MapFrom(m => DateTime.Now))
                .ForMember(a => a.UpdatedAt, map => map.MapFrom(m => DateTime.Now))
                .ForMember(a => a.DeletedAt, map => map.Ignore())
                .ForMember(a => a.Category, map => map.Ignore())
                .ForMember(a => a.Rating, map => map.Ignore())
                .ForMember(a => a.AuthorID, map => map.Ignore())
                .ForMember(a => a.Author, map => map.Ignore());


            CreateMap<PatchArticleRequest, Article>()
                .ForMember(a => a.Name, map => map.MapFrom(m => m.Name))
                .ForMember(a => a.Text, map => map.MapFrom(m => m.Text))
                .ForMember(a => a.CategoryID, map => map.Ignore())
                .ForMember(a => a.ID, map => map.Ignore())
                .ForMember(a => a.ViewCount, map => map.Ignore())
                .ForMember(a => a.CreatedAt, map => map.Ignore())
                .ForMember(a => a.UpdatedAt, map => map.MapFrom(m => DateTime.Now))
                .ForMember(a => a.DeletedAt, map => map.Ignore())
                 .ForMember(a => a.Category, map => map.Ignore())
                  .ForMember(a => a.AuthorID, map => map.Ignore())
                .ForMember(a => a.Author, map => map.Ignore());

            CreateMap<CreateUserRequest, User>()
                .ForMember(u => u.ID, map => map.Ignore())
                .ForMember(u => u.FirstName, map => map.MapFrom(m => m.FirstName))
                .ForMember(u => u.LastName, map => map.MapFrom(m => m.LastName))
                .ForMember(u => u.Nickname, map => map.MapFrom(m => m.Nickname))
                .ForMember(u => u.Email, map => map.MapFrom(m => m.Email))
                .ForMember(u => u.Password, map => map.MapFrom(m => Secure.Encryptpass(m.Password)))
                .ForMember(u => u.AboutUser, map => map.MapFrom(m => m.AboutUser))
                .ForMember(u => u.Age, map => map.MapFrom(m => m.Age))
                .ForMember(u => u.BirthdayDate, map => map.MapFrom(m => m.BirthdayDate))
                .ForMember(u => u.Role, map => map.Ignore())
                .ForMember(u => u.CreatedAt, map => map.MapFrom(u => DateTime.Now))
                .ForMember(u => u.RoleID, map => map.Ignore())
                .ForMember(u => u.Activated, map => map.Ignore())
                .ForMember(u => u.UpdatedAt, map => map.MapFrom(u => DateTime.Now))
                .ForMember(u => u.DeletedAt, map => map.Ignore())
                .ForMember(u => u.Blocked, map => map.Ignore())
                .ForMember(u => u.BlockedUntil, map => map.Ignore());


            CreateMap<CreateConfirmRequest, Confirmation>()
                .ForMember(a => a.ID, map => map.Ignore())
                .ForMember(a => a.Rand, map => map.MapFrom(a => a.Number))
                .ForMember(a => a.Email, map => map.MapFrom(a => a.Mail))
                .ForMember(a => a.CreatedAt, map => map.MapFrom(a => DateTime.Now));

            CreateMap<User, UsersView>()

                .ForMember(u=> u.ID, map => map.MapFrom(u=> u.ID))
                .ForMember(u => u.Nickname, map => map.MapFrom(m => m.Nickname));



            CreateMap<Article, ArticlesView>()
                .ForMember(a => a.ID, map => map.MapFrom(a => a.ID))
                .ForMember(a => a.Name, map => map.MapFrom(m => m.Name))
                .ForMember(a => a.Text, map => map.MapFrom(m => m.Text))
                .ForMember(a => a.CategoryID, map => map.MapFrom(m => m.CategoryID))
                .ForMember(a => a.CategoryName, map => map.MapFrom(m => m.Category.Name))
                .ForMember(a => a.ViewCount, map => map.MapFrom(m => m.ViewCount))
                .ForMember(a => a.UpdatedAt, map => map.MapFrom(m => m.UpdatedAt))
                .ForMember(a => a.CreatedAt, map => map.MapFrom(m => m.CreatedAt))
                .ForMember(a => a.AuthorID, map => map.MapFrom(m => m.AuthorID))
                .ForMember(a => a.AuthorNickname, map => map.MapFrom(m => m.Author.Nickname));

            CreateMap<Category, CategoriesView>()
               .ForMember(c => c.Name, map => map.MapFrom(m => m.Name))
                .ForMember(c => c.ID, map => map.MapFrom(m => m.ID))
                .ForMember(c => c.Description, map => map.MapFrom(m => m.Description))
                .ForMember(c => c.ArticleCount, map => map.MapFrom(m => m.ArticleCount))
                .ForMember(c => c.ViewCount, map => map.MapFrom(m => m.ViewCount));


            CreateMap<PatchUserRequest, User>()
                .ForMember(u => u.ID, map => map.Ignore())
                .ForMember(u => u.FirstName, map => map.MapFrom(m => m.FirstName))
                .ForMember(u => u.LastName, map => map.MapFrom(m => m.LastName))
                .ForMember(u => u.Nickname, map => map.MapFrom(m => m.Nickname))
                .ForMember(u => u.Email, map => map.Ignore())
                .ForMember(u => u.Password, map => map.Ignore())
                .ForMember(u => u.AboutUser, map => map.MapFrom(m => m.AboutUser))
                .ForMember(u => u.Age, map => map.MapFrom(m => m.Age))
                .ForMember(u => u.BirthdayDate, map => map.MapFrom(m => m.BirthdayDate))
                .ForMember(u => u.Role, map => map.Ignore())
                .ForMember(u => u.CreatedAt, map => map.Ignore())
                .ForMember(u => u.RoleID, map => map.Ignore())
                .ForMember(u => u.Activated, map => map.Ignore())
                .ForMember(u => u.UpdatedAt, map => map.MapFrom(u => DateTime.Now))
                .ForMember(u => u.DeletedAt, map => map.Ignore())
                .ForMember(u => u.Blocked, map => map.Ignore())
                .ForMember(u => u.BlockedUntil, map => map.Ignore());

            CreateMap<Article, ArticleView>()
                .ForMember(a => a.ID, map => map.MapFrom(m => m.ID))
                .ForMember(a => a.Name, map => map.MapFrom(m => m.Name))
                .ForMember(a => a.Text, map => map.MapFrom(m => m.Text))
                .ForMember(a => a.Rating, map => map.MapFrom(a => a.Rating))
                .ForMember(a => a.CategoryID, map => map.MapFrom(a => a.CategoryID))
                .ForMember(a => a.AuthorID, map => map.MapFrom(a => a.AuthorID))
                .ForMember(a => a.UpdatedAt, map => map.MapFrom(a => a.UpdatedAt))
                .ForMember(a => a.CreatedAt, map => map.MapFrom(a => a.CreatedAt))
                .ForMember(a => a.ViewCount, map => map.MapFrom(a => a.ViewCount))
                .ForMember(a => a.Category, map => map.MapFrom(a => a.Category))
                .ForMember(a => a.Author, map => map.MapFrom(a => a.Author));
               
                

            CreateMap<User, UserView>()
                 .ForMember(u => u.ID, map => map.MapFrom(u => u.ID))
                .ForMember(u => u.FirstName, map => map.MapFrom(m => m.FirstName))
                .ForMember(u => u.LastName, map => map.MapFrom(m => m.LastName))
                .ForMember(u => u.Nickname, map => map.MapFrom(m => m.Nickname))
                .ForMember(u => u.AboutUser, map => map.MapFrom(m => m.AboutUser))
                .ForMember(u => u.Age, map => map.MapFrom(m => m.Age))
                .ForMember(u => u.BirthdayDate, map => map.MapFrom(m => m.BirthdayDate))
                .ForMember(u => u.CreatedAt, map => map.MapFrom(u => u.CreatedAt));

            CreateMap<Category, CategoryView>()
                 .ForMember(c => c.Name, map => map.MapFrom(m => m.Name))
                .ForMember(c => c.Description, map => map.MapFrom(m => m.Description))
                .ForMember(c => c.ID, map => map.MapFrom(m => m.ID))
                 .ForMember(c => c.ArticleCount, map => map.MapFrom(m => m.ArticleCount))
                .ForMember(c => c.ViewCount, map => map.MapFrom(m => m.ViewCount))
                .ForMember(c => c.CreatedAt, map => map.MapFrom(m => m.CreatedAt));

            CreateMap<SavedArticles, SavedArticleView>()

                .ForMember(c => c.ID, map => map.MapFrom(a => a.ID))
                .ForMember(c => c.Article, map => map.MapFrom(a => a.Article))
                .ForMember(c => c.Author, map => map.MapFrom(c => c.Article.Author))
                .ForMember(c => c.AuthorID, map => map.MapFrom(c => c.Article.Author.ID));

            CreateMap<Comment, CommentsView>()
                .ForMember(c => c.CreatedAt, map => map.MapFrom(m => m.CreatedAt))
            .ForMember(c => c.Likes, map => map.MapFrom(m => m.Likes))
            .ForMember(c => c.Text, map => map.MapFrom(m => m.Text))
            .ForMember(c => c.User, map => map.MapFrom(m => m.User.Nickname))
            .ForMember(c => c.UpdatedAt, map => map.MapFrom(m => m.UpdatedAt))
            .ForMember(c => c.userId, map => map.MapFrom(m => m.User.ID))
            .ForMember(c => c.Id, map => map.MapFrom(m => m.ID));







        }

    }
}
