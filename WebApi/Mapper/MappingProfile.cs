using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Requests;
using Service.Helper;

namespace WebApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCategoryRequest, Category>()
                .ForMember(c => c.Name, map => map.MapFrom(m => m.Name))
                .ForMember(c => c.ID, map => map.Ignore())
                //.ForMember(c => c.Articles, map => map.Ignore())
                .ForMember(c => c.Description, map => map.MapFrom(m => m.Description))
                .ForMember(c => c.ArticleCount, map => map.Ignore())
                .ForMember(c => c.ViewCount, map => map.Ignore())
            .ForMember(c => c.Created_at, map => map.MapFrom(m => DateTime.Now))
            .ForMember(c => c.Updated_at, map => map.Ignore());

            CreateMap<PatchCategoryRequest, Category>()
                .ForMember(c => c.Name, map => map.MapFrom(m => m.Name))
                .ForMember(c => c.Description, map => map.MapFrom(m => m.Description))
                .ForMember(c => c.ID, map => map.Ignore())
                 .ForMember(c => c.ArticleCount, map => map.Ignore())
                .ForMember(c => c.ViewCount, map => map.Ignore())
                .ForMember(c => c.Created_at, map => map.Ignore())
                .ForMember(c => c.Updated_at, map => map.MapFrom(m => DateTime.Now));




            CreateMap<CreateArticleRequestcs, Article>()
                .ForMember(a => a.Name, map => map.MapFrom(m => m.Name))
                .ForMember(a => a.Text, map => map.MapFrom(m => m.Text))
                .ForMember(a => a.CategoryID, map => map.MapFrom(m => m.CategoryID))
                .ForMember(a => a.ID, map => map.Ignore())
                .ForMember(a => a.ViewCount, map => map.MapFrom(m => 0))
                .ForMember(a => a.Created_at, map => map.MapFrom(m => DateTime.Now))
                .ForMember(a => a.Updated_at, map => map.MapFrom(m => DateTime.Now))
                .ForMember(a => a.Deleted_at, map => map.Ignore())
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
                .ForMember(a => a.Created_at, map => map.Ignore())
                .ForMember(a => a.Updated_at, map => map.MapFrom(m => DateTime.Now))
                .ForMember(a => a.Deleted_at, map => map.Ignore())
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
                .ForMember(u => u.Created_at, map => map.MapFrom(u => DateTime.Now))
                .ForMember(u => u.RoleID, map => map.Ignore())
                .ForMember(u => u.Activated, map => map.Ignore())
                .ForMember(u => u.Updated_at, map => map.MapFrom(u => DateTime.Now))
                .ForMember(u => u.Deleted_at, map => map.Ignore());

            CreateMap<CreateConfirmRequest, Confirmation>()
                .ForMember(a => a.ID, map => map.Ignore())
                .ForMember(a => a.Rand, map => map.MapFrom(a => a.Number))
                .ForMember(a => a.Email, map => map.MapFrom(a => a.Mail))
                .ForMember(a => a.Created_at, map => map.MapFrom(a => DateTime.Now));
        }
    }
}
