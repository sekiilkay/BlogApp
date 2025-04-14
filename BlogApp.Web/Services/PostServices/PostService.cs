using AutoMapper;
using Azure.Core;
using BlogApp.Web.Entities;
using BlogApp.Web.Models;
using BlogApp.Web.Models.PostViewModels;
using BlogApp.Web.Repositories.PostRepositories;
using BlogApp.Web.UnitOfWork;
using FluentValidation;
using Microsoft.Extensions.FileProviders;
using System.Security.Claims;

namespace BlogApp.Web.Services.PostServices
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileProvider _fileProvider;
        private readonly IValidator<CreatePostViewModel> _createValidator;
        private readonly IValidator<UpdatePostViewModel> _updateValidator;
        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, IFileProvider fileProvider, IValidator<CreatePostViewModel> createValidator, IValidator<UpdatePostViewModel> updateValidator)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _fileProvider = fileProvider;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task ChangeStatusAsync(int id)
        {
            var post = await _postRepository.ChangeStatusAsync(id);
            if (post.IsActive == true)
            {
                post.IsActive = false;
            }
            else
            {
                post.IsActive = true;
            }
            await _unitOfWork.SaveChangesAsync();
        }
        private string ConvertToUrl(string title)
        {
            return title.ToLower()
                .Replace("ı", "i")
                .Replace("ğ", "g")
                .Replace("ü", "u")
                .Replace("ş", "s")
                .Replace("ö", "o")
                .Replace("ç", "c")
                .Replace(" ", "-") 
                .Replace("'", "")
                .Replace(",", "") 
                .Replace(".", "") 
                .Replace("!", "") 
                .Replace("?", "") 
                .Trim(); 
        }

        public async Task<ResponseViewModel> CreateAsync(CreatePostViewModel model)
        {
            var validationResult = await _createValidator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return ResponseViewModel.Fail(validationErrors);
            }

            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            
            var post = _mapper.Map<Post>(model);
            post.PublishedDate = DateTime.Now;
            post.Url = ConvertToUrl(model.Title!);
            post.AppUserId = userId;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var wwwrootFolder = _fileProvider.GetDirectoryContents("wwwroot");

                string randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(model.ImageFile.FileName)}";

                var newPicturePath = Path.Combine(wwwrootFolder!.First(x => x.Name == "images").PhysicalPath!, randomFileName);

                using var stream = new FileStream(newPicturePath, FileMode.Create);

                await model.ImageFile.CopyToAsync(stream);

                post.Image = randomFileName;
            }
            else
            {
                post.Image = "not-available.png";
            }

            await _postRepository.CreateAsync(post);
            await _unitOfWork.SaveChangesAsync();

            return ResponseViewModel.Success();
        }

        public async Task DeleteAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            _postRepository.Remove(post);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ResultPostViewModel>> GetAllPostWithCategoryAsync()
        {
            var posts = await _postRepository.GetAllPostWithCategoryAsync();
            return _mapper.Map<List<ResultPostViewModel>>(posts);
        }

        public async Task<GetByUrlPostDetailViewModel> GetByUrlPostAsync(string url)
        {
            var post = await _postRepository.GetByUrlPostAsync(url);
            return _mapper.Map<GetByUrlPostDetailViewModel>(post);
        }

        public async Task<GetByIdPostViewModel> GetPostByIdAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            return _mapper.Map<GetByIdPostViewModel>(post);
        }

        public async Task<List<ResultPostViewModel>> GetPostsByCategoryAsync(string categoryUrl)
        {
            var posts = await _postRepository.GetPostsByCategoryAsync(categoryUrl);
            return _mapper.Map<List<ResultPostViewModel>>(posts);
        }

        public async Task<List<ResultPostViewModel>> GetUserPostsAsync()
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            var posts = await _postRepository.GetUserPostsAsync(userId);
            return _mapper.Map<List<ResultPostViewModel>>(posts);
        }

        public async Task<ResponseViewModel> UpdateAsync(UpdatePostViewModel model)
        {
            var validationResult = await _updateValidator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return ResponseViewModel.Fail(validationErrors);
            }

            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            var post = _mapper.Map<Post>(model);
            post.PostId = model.PostId;
            post.AppUserId = userId;
            post.Url = ConvertToUrl(model.Title!);

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var wwwrootFolder = _fileProvider.GetDirectoryContents("wwwroot");

                string randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(model.ImageFile.FileName)}";

                var newPicturePath = Path.Combine(wwwrootFolder!.First(x => x.Name == "images").PhysicalPath!, randomFileName);

                using var stream = new FileStream(newPicturePath, FileMode.Create);

                await model.ImageFile.CopyToAsync(stream);

                post.Image = randomFileName;
            }
            else
            {
                post.Image = "not-available.png";
            }

            _postRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();

            return ResponseViewModel.Success();
        }
    }
}
