﻿using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using MVC.Core.Database.Config;
using MVC.Core.Entities;
using MVC.Interfaces;
using MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class PostService : IPostService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IMongoContext _context;
        private readonly IMapper _mapper;
        private readonly IArtistService _artistService;

        public PostService(IMongoContext context, IMapper mapper, IArtistService ArtistService)
        {
            _artistService = ArtistService;
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PostViewModel>> GetAllAsync()
        {
            var result = await _context.Posts.AsQueryable().ToListAsync();

            return result.Select(p => _mapper.Map<PostViewModel>(p)).OrderBy(p => p.Time).Reverse();
        }
        public async Task<IEnumerable<PostViewModel>> GetByAuthorAsync(string author)
        {
            var result = await _context
                .Posts
                .AsQueryable()
                .ToListAsync();

            return result.Select(p => _mapper.Map<PostViewModel>(p)).Where(p => p.AuthorEmail == author);
        }

        public async Task<PostViewModel> GetByIdAsync(string postId)
        {
            var result = await _context
                .Posts
                .AsQueryable()
                .ToListAsync();

            return _mapper.Map<PostViewModel>(result.Single(p => p.Id == postId));
        }

        public async Task<int> GetReactionsByIdAsync(string id)
        {
            var builder = Builders<Post>.Filter;
            var filter = builder.Eq(el => el.Id, id);

            var result = await _context
                .Posts
                .Find(filter)
                .Limit(1)
                .SingleAsync();

            return result.Reactions.Count;
        }

        public async void AddNewPostAsync(string creatorEmail, string postText)
        {
            var user = await _artistService.GetByEmailAsync(creatorEmail);
            var newel = new Post()
            {
                AuthorEmail = user.Email,
                AuthorName = user.Name,
                AuthorNickname = user.Nickname,
                Text = postText,
                Reactions = new List<Reactions>(),
                Views = 0,
                Time = DateTime.Now,
                AuthorAvatar = user.Avatar,
                Comments = new List<Comment>()
            };
            InsertPostAsync(newel);
        }
        public async void AddNewCommentAsync(string creatorEmail, string commentText, string postId)
        {
            var user = await _artistService.GetByEmailAsync(creatorEmail);
            var newel = new Comment()
            {
                AuthorEmail = user.Email,
                AuthorName = user.Name,
                AuthorNickname = user.Nickname,
                Text = commentText,
                AuthorAvatar = user.Avatar,
                Time = DateTime.Now
            };

            InsertCommentAsync(newel, postId);
        }
        public async Task<PostViewModel> IncViewsAsync(string id)
        {
            var builder = Builders<Post>.Filter;
            var filter = builder.Eq(el => el.Id, id);

            var update = new UpdateDefinitionBuilder<Post>().Inc(el => el.Views, 1);
            var options = new FindOneAndUpdateOptions<Post>();
            options.ReturnDocument = ReturnDocument.After;
            options.Projection = new ProjectionDefinitionBuilder<Post>().Include(el => el.Reactions);
            var result = await _context.Posts.FindOneAndUpdateAsync<Post>(filter, update, options);

            return _mapper.Map<PostViewModel>(result);
        }
        public async void ReactionsClicked(string userEmail, string postId)
        {
            if (await IsPostLikedByUser(userEmail, postId))
            {
                RemoveLike(userEmail, postId);
            }
            else
            {
                AddLike(userEmail, postId);
            }
        }
        public async Task<bool> IsPostLikedByUser(string userEmail, string postId)
        {
            var post = await GetByIdAsync(postId);
            return post.Reactions.AsEnumerable().Any(p => p.Email == userEmail); 
        }

        private async void InsertPostAsync(Post post)
        {
            var builder = Builders<Post>.Filter;
            post.Id = Convert.ToString(ObjectId.GenerateNewId());
            await _context.Posts.InsertOneAsync(post);
        }
        private async void InsertCommentAsync(Comment comment, string postId)
        {
            var filter = Builders<Post>.Filter.Eq(el => el.Id, postId);
            var update = Builders<Post>.Update
                    .Push<Comment>(el => el.Comments, comment);

            await _context.Posts.FindOneAndUpdateAsync(filter, update);
        }

        private async void AddLike(string userEmail, string postId)
        {
            var like = _mapper.Map<Reactions>(await _artistService.GetByEmailAsync(userEmail));

            var filter = Builders<Post>.Filter.Eq(el => el.Id, postId);
            var update = Builders<Post>.Update
                    .Push<Reactions>(el => el.Reactions, like);

            await _context.Posts.FindOneAndUpdateAsync(filter, update);
        }
        private async void RemoveLike(string userEmail, string postId)
        {
            var like = _mapper.Map<Reactions>(await _artistService.GetByEmailAsync(userEmail));

            var filter = Builders<Post>.Filter.Eq(el => el.Id, postId);
            var update = Builders<Post>.Update
                    .Pull<Reactions>(el => el.Reactions, like);

            await _context.Posts.FindOneAndUpdateAsync(filter, update);
        }
    }
}
