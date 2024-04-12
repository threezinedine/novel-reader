using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovelReader.API.Context;

namespace NovelReader.API.Controllers
{
	[ApiController]
	[Route("api/admin/comment")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
	public class CommentAdminController : ControllerBase
	{
		private readonly CustomDbContext m_DbContext;
		private readonly IMapper m_Mapper;

        public CommentAdminController(CustomDbContext customDbContext,
										IMapper mapper)
        {
			m_DbContext = customDbContext;
			m_Mapper = mapper;
        }

		[HttpDelete("{commentId}")]
		public async Task<ActionResult> DeleteCommentById(string commentId)
		{
			var comment = await m_DbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);

			if (comment == null) return NotFound("Comment not found");

			m_DbContext.Comments.Remove(comment);

			try
			{
				await m_DbContext.SaveChangesAsync();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			return Ok();
		}
    }
}
