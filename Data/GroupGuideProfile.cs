using AutoMapper;
using Group_Guide.Data.Entities;
using Group_Guide.Data.Dtos.Games;
using Group_Guide.Data.Dtos.Campaigns;
using Group_Guide.Data.Dtos.Sessions;
using Group_Guide.Data.Dtos.Topics;
using Group_Guide.Data.Dtos.Posts;

namespace Group_Guide.Data
{
    public class GroupGuideProfile : Profile
    {
        public GroupGuideProfile()
        {
            CreateMap<Game, GameDto>();
            CreateMap<CreateGameDto, Game>();
            CreateMap<UpdateGameDto, Game>();

            CreateMap<Campaign, CampaignDto>();
            CreateMap<CreateCampaignDto, Campaign>();
            CreateMap<UpdateCampaignDto, Campaign>();

            CreateMap<Session, SessionDto>();
            CreateMap<CreateSessionDto, Session>();
            CreateMap<UpdateSessionDto, Session>();

            CreateMap<Topic, TopicDto>();
            CreateMap<CreateTopicDto, Topic>();
            CreateMap<UpdateTopicDto, Topic>();

            CreateMap<Post, PostDto>();
            CreateMap<CreatePostDto, Post>();
            CreateMap<UpdatePostDto, Post>();
        }
    }
}
