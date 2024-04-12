using AutoMapper;
using NovelReader.API.Models;
using NovelReader.Common.ModelDtos.Chapter;
using NovelReader.Common.ModelDtos.Comment;
using NovelReader.Common.ModelDtos.Novel;
using NovelReader.Common.ModelDtos.Rate;
using NovelReader.Common.ModelDtos.Tag;
using NovelReader.Common.ModelDtos.User;

namespace NovelReader.API.Profiles
{
	public class AutoMapperProfile : Profile
	{
        public AutoMapperProfile()
        {
            CreateMap<Novel, NovelDto>();
			CreateMap<NovelDto, Novel>();

			CreateMap<Chapter, ChapterDto>();
			CreateMap<ChapterDto, Chapter>();

			CreateMap<Comment, CommentDto>()
				.ForMember(des => des.IsEdited, 
							opt => opt.MapFrom(comment => comment.CreatedAt != comment.UpdatedAt));
			CreateMap<CommentDto, Comment>();

			CreateMap<Rate, RateDto>()
				.ForMember(des => des.IsEdited, 
							opt => opt.MapFrom(rate => rate.CreatedAt != rate.UpdatedAt));
			CreateMap<RateDto, Rate>();

			CreateMap<Tag, TagDto>();
			CreateMap<TagDto, Tag>();

			CreateMap<User, UserDto>();
			CreateMap<UserDto, User>();

			CreateMap<UserNovel, ReadNovelDto>()
				.ForMember(des => des.NovelId,
							opt => opt.MapFrom(un => un.Novel.Id))
				.ForMember(des => des.Title,
							opt => opt.MapFrom(un => un.Novel.Title))
				.ForMember(des => des.TotalChapters,
							opt => opt.MapFrom(un => un.Novel.Chapters.Count))
				.ForMember(des => des.CurrentChapter,
							opt => opt.MapFrom(un => un.CurrentChapter))
				.ForMember(des => des.LastReadAt,
							opt => opt.MapFrom(un => un.LastReadAt));
        }
    }
}
