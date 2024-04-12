using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovelReader.API.Context;
using NovelReader.Common.ModelDtos.Novel;
using NovelReader.Common.ModelDtos.Tag;
using System.Runtime.InteropServices;

namespace NovelReader.API.Controllers
{
	[ApiController]
	[Route("api/tag")]
	public class TagController: ControllerBase
	{
		private readonly CustomDbContext m_DbContext;
		private readonly IMapper m_Mapper;
        public TagController(CustomDbContext customDbContext,
						  IMapper mapper)
        {
			m_DbContext = customDbContext;
			m_Mapper = mapper;
        }
        [HttpGet("alltags")]
		public async Task<ActionResult<List<TagDto>>> GetAllTags()
		{
			var tags = await m_DbContext.Tags.ToListAsync();

			return Ok(tags.Select(tag => m_Mapper.Map<TagDto>(tag)));
		}

		[HttpPost("addtag/{novelId}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult<NovelDto>> AddTag(string novelId, [FromBody] List<TagDto> tagInfos)
		{
			var novel = await m_DbContext.Novels
								.Include(novel => novel.Author)
								.Include(novel => novel.Tags)
								.FirstOrDefaultAsync(novel => novel.Id == novelId);

			if (novel == null) return NotFound("The novel does not exist.");

			if (novel.Author.Username != User.Identity!.Name) return Unauthorized("You are not the author of this novel.");

			foreach (var tagInfo in tagInfos)
			{
				var tag = await m_DbContext.Tags.FirstOrDefaultAsync(tag => tag.Id == tagInfo.Id);

				if (tag == null) return NotFound("The tag does not exist.");

				if (novel.Tags.Any(novelTag => novelTag.Id == tag.Id)) continue;

				novel.Tags.Add(tag);
			}

			try
			{
				await m_DbContext.SaveChangesAsync();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			return Ok(m_Mapper.Map<NovelDto>(novel));
		}

		[HttpDelete("removetag/{novelId}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult<NovelDto>> RemoveTag(string novelId, 
															[FromBody] List<TagDto> tagInfos)
		{
			var novel = await m_DbContext.Novels
								.Include(novel => novel.Author)
								.Include(novel => novel.Tags)
								.FirstOrDefaultAsync(novel => novel.Id == novelId);

			if (novel == null) return NotFound("The novel does not exist.");

			if (novel.Author.Username != User.Identity!.Name) return Unauthorized("You are not the author of this novel.");

            foreach (var tagInfo in tagInfos)
            {
				var existedTag = novel.Tags
									.FirstOrDefault(tag => tag.Id == tagInfo.Id);

				if (existedTag== null) continue;

				novel.Tags.Remove(existedTag);
            }

			try
			{
				await m_DbContext.SaveChangesAsync();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			return Ok(m_Mapper.Map<NovelDto>(novel));
        }

		[HttpGet("{tagId}/allnovel")]
		public async Task<ActionResult<List<NovelDto>>> GetAllNovelFromTags(string tagId,
																			[FromQuery] int size = 1,
																			[FromQuery] int index = 0)
		{
			var tag = await m_DbContext.Tags
							.FirstOrDefaultAsync(tag => tag.Id == tagId);

			if (tag == null) return NotFound("The tag does not exist.");

			var novels = await m_DbContext.Novels
							.Include(novel => novel.Author)
							.Include(novel => novel.Tags)
							.Where(novel => novel.Tags.Any(novelTag => novelTag.Id == tagId))
							.Skip(size * index)
							.Take(size)
							.ToListAsync();

			return Ok(novels.Select(novel => m_Mapper.Map<NovelDto>(novel)));
		}
	}
}
