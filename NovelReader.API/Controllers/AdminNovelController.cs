using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovelReader.API.Context;

namespace NovelReader.API.Controllers
{
	[ApiController]
	[Route("api/admin/novel")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
	public class AdminNovelController: ControllerBase
	{
		private readonly CustomDbContext m_DbContext;
		private readonly IMapper m_Mapper;
		public AdminNovelController(CustomDbContext customDbContext,
												IMapper mapper)
		{
			m_DbContext = customDbContext;
			m_Mapper = mapper;
		}

		[HttpDelete("chapter/{chapterId}")]
		public async Task<ActionResult> DeleteChapterByChapterId(string chapterId)
		{
			var existedChapter = await m_DbContext.Chapters
								.FirstOrDefaultAsync(chapter => chapter.Id == chapterId);

			if (existedChapter == null) return NotFound("Chapter not found");

			m_DbContext.Chapters.Remove(existedChapter);

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

		[HttpDelete("{novelId}")]
		public async Task<ActionResult> DeleteNovelByNovelId(string novelId)
		{
			var novel = await m_DbContext.Novels
						.Include(novel => novel.Author)
						.Include(novel => novel.Chapters)
						.FirstOrDefaultAsync(novel => novel.Id == novelId);

			if (novel == null) return NotFound("Novel not found");

			m_DbContext.Novels.Remove(novel);

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
