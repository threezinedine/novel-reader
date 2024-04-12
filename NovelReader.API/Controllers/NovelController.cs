using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NovelReader.API.Context;
using NovelReader.API.Models;
using NovelReader.Common.ModelDtos.Chapter;
using NovelReader.Common.ModelDtos.Novel;

namespace NovelReader.API.Controllers
{
	[ApiController]
	[Route("api/novel")]
	public class NovelController: ControllerBase
	{
		private readonly CustomDbContext m_DbContext;
		private readonly IMapper m_Mapper;

        public NovelController(CustomDbContext customDbContext,
								IMapper mapper)
        {
			m_DbContext = customDbContext; 
			m_Mapper = mapper;
        }

		[HttpGet("author/{userId}")]
		public async Task<ActionResult<List<NovelDto>>> GetNovelsByUserId(string userId,
																			[FromQuery] int index = 0,
																			[FromQuery] int size = 1)
		{
			var novels = await m_DbContext.Novels
						.Include(novel => novel.Tags)
						.Where(x => x.AuthorId == userId)
						.Skip(index * size)
						.Take(size)
						.ToListAsync();

			return Ok(novels.Select(novel => m_Mapper.Map<NovelDto>(novel)).ToList());
		}

		[HttpPost("new")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult<NovelDto>> CreateNewNovel(NovelDto novelInfo)
		{
			var existedNovel = await m_DbContext.Novels
								.FirstOrDefaultAsync(novel => novel.Title == novelInfo.Title);

			if (existedNovel != null) return Conflict("Novel's title is already existed");

			var user = await m_DbContext.Users
						.FirstOrDefaultAsync(x => x.Username == User.Identity!.Name);

			if (user == null) return NotFound("User not found");

			var newNovel = new Novel
			{
				Id = Guid.NewGuid().ToString(),
				Title = novelInfo.Title,
				Description = novelInfo.Description,
				CoverURL = novelInfo.CoverURL,
				IsCompleted = false,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				Author = user,
			};

			m_DbContext.Novels.Add(newNovel);

			try
			{
				await m_DbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok(m_Mapper.Map<NovelDto>(newNovel));
		}

		[HttpPost("new-chapter")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult<ChapterDto>> CreateNewChapter(ChapterDto chapterInfo)
		{
			var novel = await m_DbContext.Novels
						.Include(novel => novel.Author)
						.Include(novel => novel.Chapters)
						.FirstOrDefaultAsync(novel => novel.Id == chapterInfo.NovelId);

			if (novel == null) return NotFound("Novel not found");

			if (novel.Author.Username != User.Identity!.Name) return Unauthorized("You are not the author of this novel");

			var newChapter = new Chapter
			{
				Id = Guid.NewGuid().ToString(),
				Novel = novel,
				Title = chapterInfo.Title,
				ChapterNumber = novel.Chapters.Count + 1,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				Content = chapterInfo.Content,
			};

			m_DbContext.Chapters.Add(newChapter);

			try
			{
				await m_DbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok(m_Mapper.Map<ChapterDto>(newChapter));
		}

		[HttpGet("{novelId}")]
		public async Task<ActionResult<NovelDto>> GetNovelByNovelId(string novelId)
		{
			var novel = await m_DbContext.Novels
						.Include(novel => novel.Author)
						.Include(novel => novel.Tags)
						.FirstOrDefaultAsync(novel => novel.Id == novelId);

			if (novel == null) return NotFound("Novel not found");

			return Ok(m_Mapper.Map<NovelDto>(novel));
        }

		[HttpGet("chapter/{chapterId}")]
		public async Task<ActionResult<ChapterDto>> GetChapterById(string chapterId)
		{
			var chapter = await m_DbContext.Chapters
						.Include(chapter => chapter.Novel)
						.FirstOrDefaultAsync(chapter => chapter.Id == chapterId);

			if (chapter == null) return NotFound("Chapter not found");

			var user = await m_DbContext.Users
						.Include(user => user.ReadNovels)
						.FirstOrDefaultAsync(user => user.Username == User.Identity!.Name);

			if (user != null)
			{
				var userNovel = await m_DbContext.UserNovels
								.FirstOrDefaultAsync(un => un.UserId == user.Id 
													&& un.NovelId == chapter.Novel.Id);

				if (userNovel == null)
				{
					userNovel = new UserNovel
					{
						UserId = user.Id,
						NovelId = chapter.Novel.Id,
						CurrentChapter = chapter.ChapterNumber,
						LastReadAt = DateTime.Now,
					};

					m_DbContext.UserNovels.Add(userNovel);
				}
				else
				{
					userNovel.CurrentChapter = chapter.ChapterNumber;
				}

				try
				{
					await m_DbContext.SaveChangesAsync();
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}

			return Ok(m_Mapper.Map<ChapterDto>(chapter));
		}

		[HttpGet("{novelId}/allchapters")]
		public async Task<ActionResult<List<ChapterDto>>> GetAllChapter(string novelId,
																		[FromQuery] int index = 1,
																		[FromQuery] int size = 10)
		{
			var novel = await m_DbContext.Novels
						.Include(novel => novel.Chapters)
						.FirstOrDefaultAsync(novel => novel.Id == novelId);

			if (novel == null) return NotFound("Novel not found");

			List<Chapter> chapters = new List<Chapter>();

			if (size != 0)
			{
				chapters = novel.Chapters
							.OrderBy(chapter => chapter.ChapterNumber)
							.Skip((index - 1) * size)
							.Take(size)
							.ToList();
			}
			else
			{
				chapters = novel.Chapters
							.OrderBy(chapter => chapter.ChapterNumber)
							.ToList();
			}

			return Ok(chapters.Select(chapter => m_Mapper.Map<ChapterDto>(chapter)).ToList());
		}

		[HttpPut]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult<NovelDto>> UpdateNovelInfo(NovelDto novelInfo)
		{
			var existedNovel = await m_DbContext.Novels
								.Include(novel => novel.Author)
								.FirstOrDefaultAsync(novel => novel.Id == novelInfo.Id);

			if (existedNovel == null) return NotFound("Novel not found");

			if (existedNovel.Author.Username != User.Identity!.Name)
			{
				return Unauthorized("You are not the author of this novel");
			}

			if (!novelInfo.Title.IsNullOrEmpty())
			{
				existedNovel.Title = novelInfo.Title;
			}

			if (!novelInfo.Description.IsNullOrEmpty())
			{
				existedNovel.Description = novelInfo.Description;
			}

			if (!novelInfo.CoverURL.IsNullOrEmpty())
			{
				existedNovel.CoverURL = novelInfo.CoverURL;
			}

			existedNovel.UpdatedAt = DateTime.Now;

			try
			{
				await m_DbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok(m_Mapper.Map<NovelDto>(existedNovel));
		}

		[HttpPut("chapter")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult<ChapterDto>> UpdateChapterInfo(ChapterDto chapterInfo)
		{
			var novel = await m_DbContext.Novels
						.Include(novel => novel.Author)
						.Include(novel => novel.Chapters)
						.FirstOrDefaultAsync(novel => novel.Id == chapterInfo.NovelId);

			if (novel == null) return NotFound("Novel not found");

			if (novel.Author.Username != User.Identity!.Name) 
			{
				return Unauthorized("You are not the author of this novel");
			};

			var existedChapter = novel.Chapters.FirstOrDefault(chapter => chapter.Id == chapterInfo.Id);

			if (existedChapter == null) return NotFound("Chapter not found");

			if (!chapterInfo.Title.IsNullOrEmpty())
			{
				existedChapter.Title = chapterInfo.Title;
			}

			if (!chapterInfo.Content.IsNullOrEmpty())
			{
				existedChapter.Content = chapterInfo.Content;
			}

			existedChapter.UpdatedAt = DateTime.Now;

			try
			{
				await m_DbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok(m_Mapper.Map<ChapterDto>(existedChapter));
		}

		[HttpDelete("chapter/{chapterId}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult> DeleteChapter(string chapterId)
		{
			var chapter = await m_DbContext.Chapters
						.Include(chapter => chapter.Novel)
						.Include(chapter => chapter.Novel.Author)
						.FirstOrDefaultAsync(chapter => chapter.Id == chapterId);

			if (chapter == null) return NotFound("Chapter not found");

			if (chapter.Novel.Author.Username != User.Identity!.Name)
			{
				return Unauthorized("You are not the author of this novel");
			}

			m_DbContext.Chapters.Remove(chapter);

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
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult> DeleteNovel(string novelId)
		{
			var novel = await m_DbContext.Novels
						.Include(novel => novel.Author)
						.Include(novel => novel.Chapters)
						.FirstOrDefaultAsync(novel => novel.Id == novelId);

			if (novel == null) return NotFound("Novel not found");

			if (novel.Author.Username != User.Identity!.Name)
			{
				return Unauthorized("You are not the author of this novel");
			}

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

		[HttpGet("read")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult<ReadNovelDto>> GetAllReadNovels()
		{
			var user = await m_DbContext.Users
						.Include(user => user.ReadNovels)
						.ThenInclude(un => un.Novel)
						.ThenInclude(novel => novel.Chapters)
						.FirstOrDefaultAsync(user => user.Username == User.Identity!.Name);

			if (user == null) return NotFound("User not found");

			return Ok(user.ReadNovels.Select(novel => m_Mapper.Map<ReadNovelDto>(novel)));
		}
	}
}
