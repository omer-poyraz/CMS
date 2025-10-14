using AutoMapper;
using Entities.DTOs.BasketDto;
using Entities.DTOs.CommentDto;
using Entities.DTOs.ContentDto;
using Entities.DTOs.FilesDto;
using Entities.DTOs.GoogleAnalyticsDto;
using Entities.DTOs.LanguageDto;
using Entities.DTOs.LogDto;
using Entities.DTOs.MenuDto;
using Entities.DTOs.MenuGroupDto;
using Entities.DTOs.PageDto;
using Entities.DTOs.PopupDto;
using Entities.DTOs.ProductDto;
using Entities.DTOs.SettingsDto;
using Entities.DTOs.UnitDto;
using Entities.DTOs.UserDto;
using Entities.DTOs.UserProfileDto;
using Entities.DTOs.VersioningDto;
using Entities.DTOs.VideoDto;
using Entities.DTOs.VideoGroupDto;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketDtoForUpdate, Basket>().ReverseMap();
            CreateMap<Basket, BasketDto>();
            CreateMap<BasketDtoForInsertion, Basket>();

            CreateMap<CommentDtoForUpdate, Comment>().ReverseMap();
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDtoForInsertion, Comment>();

            CreateMap<ContentDtoForUpdate, Content>().ReverseMap();
            CreateMap<Content, ContentDto>();
            CreateMap<ContentDtoForInsertion, Content>();

            CreateMap<FilesDtoForUpdate, Files>().ReverseMap();
            CreateMap<Files, FilesDto>();
            CreateMap<FilesDtoForInsertion, Files>();

            CreateMap<GoogleAnalyticsDtoForUpdate, GoogleAnalytics>().ReverseMap();
            CreateMap<GoogleAnalytics, GoogleAnalyticsDto>();
            CreateMap<GoogleAnalyticsDtoForInsertion, GoogleAnalytics>();

            CreateMap<LanguageDtoForUpdate, Language>().ReverseMap();
            CreateMap<Language, LanguageDto>();
            CreateMap<LanguageDtoForInsertion, Language>();

            CreateMap<Log, LogDto>();
            CreateMap<LogDtoForInsertion, Log>();

            CreateMap<MenuDtoForUpdate, Menu>().ReverseMap();
            CreateMap<Menu, MenuDto>();
            CreateMap<MenuDtoForInsertion, Menu>();

            CreateMap<MenuGroupDtoForUpdate, MenuGroup>().ReverseMap();
            CreateMap<MenuGroup, MenuGroupDto>();
            CreateMap<MenuGroupDtoForInsertion, MenuGroup>();

            CreateMap<PageDtoForUpdate, Page>().ReverseMap();
            CreateMap<Page, PageDto>();
            CreateMap<PageDtoForInsertion, Page>();

            CreateMap<PopupDtoForUpdate, Popup>().ReverseMap();
            CreateMap<Popup, PopupDto>();
            CreateMap<PopupDtoForInsertion, Popup>();

            CreateMap<ProductDtoForUpdate, Product>().ReverseMap();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Contents, opt => opt.MapFrom(src => src.Contents));
            CreateMap<ProductDtoForInsertion, Product>();

            CreateMap<SettingsDtoForUpdate, Settings>().ReverseMap();
            CreateMap<Settings, SettingsDto>();
            CreateMap<SettingsDtoForInsertion, Settings>();

            CreateMap<UnitDtoForUpdate, Unit>().ReverseMap();
            CreateMap<Unit, UnitDto>();
            CreateMap<UnitDtoForInsertion, Unit>();

            CreateMap<UserForRegisterDto, User>();
            CreateMap<UserDtoForUpdate, User>().ReverseMap();
            CreateMap<User, UserDto>();
            CreateMap<IdentityResult, UserDto>().ReverseMap();

            CreateMap<UserProfileDtoForUpdate, UserProfile>().ReverseMap();
            CreateMap<UserProfile, UserProfileDto>();
            CreateMap<UserProfileDtoForInsertion, UserProfile>();

            CreateMap<Versioning, VersioningDto>();

            CreateMap<VideoDtoForUpdate, Video>().ReverseMap();
            CreateMap<Video, VideoDto>();
            CreateMap<VideoDtoForInsertion, Video>();

            CreateMap<VideoGroupDtoForUpdate, VideoGroup>().ReverseMap();
            CreateMap<VideoGroup, VideoGroupDto>();
            CreateMap<VideoGroupDtoForInsertion, VideoGroup>();
        }
    }
}
