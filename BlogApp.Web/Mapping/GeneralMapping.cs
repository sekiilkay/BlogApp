using AutoMapper;
using BlogApp.Web.Entities;
using BlogApp.Web.Models.CategoryViewModels;
using BlogApp.Web.Models.CommentViewModels;
using BlogApp.Web.Models.PostViewModels;

namespace BlogApp.Web.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {

            //Post ile ViewModel arası mapping
            CreateMap<Post, CreatePostViewModel>().ReverseMap();
            CreateMap<Post, UpdatePostViewModel>().ReverseMap();
            CreateMap<Post, GetByUrlPostDetailViewModel>().ReverseMap();
            CreateMap<Post, GetByIdPostViewModel>().ReverseMap();
            CreateMap<Post, ResultPostViewModel>().ReverseMap();

            //Category ile ViewModel arası mapping
            CreateMap<Category, ResultCategoryViewModel>().ReverseMap();

            //Comment ile ViewModel arası mapping
            CreateMap<Comment, CreateCommentViewModel>().ReverseMap();
        }
    }
}
