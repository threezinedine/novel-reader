using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovelReader.API.Context;
using NovelReader.API.Models;
using NovelReader.Common.ModelDtos.Comment;

namespace NovelReader.API.Controllers
{
	[ApiController]
	[Route("api/comment")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class CommentController: ControllerBase
	{
		private readonly CustomDbContext m_DbContext;
		private readonly IMapper m_Mapper;
        public CommentController(CustomDbContext customDbContext,
								IMapper mapper)
        {
			m_DbContext = customDbContext;
			m_Mapper = mapper;
        }
		[HttpGet("novel/{novelId}")]
		[AllowAnonymous]
		public async Task<ActionResult<List<CommentDto>>> GetCommentsByNovelId(string novelId,
																				[FromQuery] int index = 0,
																				[FromQuery] int size = 1)
		{
			var novel = await m_DbContext.Novels
						.Include(novel => novel.Comments)
						.FirstOrDefaultAsync(novel => novel.Id == novelId);

			if (novel == null) return NotFound("Novel not found");

			var comments = novel.Comments
							.OrderByDescending(comment => comment.CreatedAt)
							.Skip(index * size)
							.Take(size)
							.ToList();

			return Ok(comments.Select(comment => m_Mapper.Map<CommentDto>(comment)));
		}
        [HttpPost("new")]
		public async Task<ActionResult<CommentDto>> CreateNewComment(CommentDto commentInfo)
		{
			var novel = await m_DbContext.Novels
						.FirstOrDefaultAsync(x => x.Id == commentInfo.NovelId);

			if (novel == null) return NotFound("Novel not found");

			var user = await m_DbContext.Users
						.FirstOrDefaultAsync(x => x.Username == User.Identity!.Name);

			if (user == null) return NotFound("User not found");

			var newComment = new Comment
			{
				Id = Guid.NewGuid().ToString(),
				Content = commentInfo.Content,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				User = user,
				Novel = novel,
			};

			m_DbContext.Comments.Add(newComment);

			try
			{
				await m_DbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok(m_Mapper.Map<CommentDto>(newComment));
		}

		[HttpPut]
		public async Task<ActionResult<CommentDto>> UpdateComment(CommentDto commentInfo)
		{
			var existedComment = await m_DbContext.Comments
								.Include(comment => comment.User)
								.FirstOrDefaultAsync(comment => comment.Id == commentInfo.Id);

			if (existedComment == null) return NotFound("Comment not found");

			if (existedComment.User.Username != User.Identity!.Name) {
				return Unauthorized("You are not the owner of this comment");
					}

			existedComment.Content = commentInfo.Content;

			try
			{
				await m_DbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok(m_Mapper.Map<CommentDto>(existedComment));
		}

		[HttpDelete("{commentId}")]
		public async Task<ActionResult> DeleteCommentById(string commentId)
		{
			var existedComment = await m_DbContext.Comments
								.Include(comment => comment.User)
								.FirstOrDefaultAsync(comment => comment.Id == commentId);

			if (existedComment == null) return NotFound("Comment not found");

			if (existedComment.User.Username != User.Identity!.Name)
			{
				return Unauthorized("You are not the owner of this comment");
			}

			m_DbContext.Comments.Remove(existedComment);

			try
			{
				await m_DbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok();
		}
	}
}
