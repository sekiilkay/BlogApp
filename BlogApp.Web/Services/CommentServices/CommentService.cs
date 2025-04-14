using AutoMapper;
using BlogApp.Web.Entities;
using BlogApp.Web.Models.CommentViewModels;
using BlogApp.Web.Repositories.CommentRepositories;
using BlogApp.Web.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlogApp.Web.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        public CommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task CreateAsync(CreateCommentViewModel model)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var comment = _mapper.Map<Comment>(model);
            comment.PublishedDate = DateTime.Now;
            comment.AppUserId = userId;
            model.UserName = user.UserName;
            model.Avatar = user.ImageUrl;
            model.PublishedDate = comment.PublishedDate;
            await _commentRepository.CreateAsync(comment);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
