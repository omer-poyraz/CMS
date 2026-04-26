using AutoMapper;
using Entities.DTOs.CommentDto;
using Entities.DTOs.FilesDto;
using Entities.DTOs.GoogleAnalyticsDto;
using Entities.DTOs.LanguageDto;
using Entities.DTOs.LogDto;
using Entities.DTOs.MenuDto;
using Entities.DTOs.MenuGroupDto;
using Entities.DTOs.PageDto;
using Entities.DTOs.PageSectionDto;
using Entities.DTOs.PageTranslationDto;
using Entities.DTOs.PopupDto;
using Entities.DTOs.SectionFieldDto;
using Entities.DTOs.SectionItemDto;
using Entities.DTOs.SettingsDto;
using Entities.DTOs.UserDto;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CommentDtoForUpdate, Comment>().ReverseMap();
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDtoForInsertion, Comment>();

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
            CreateMap<Page, PageDto>()
                .ForMember(dest => dest.Translations, opt => opt.MapFrom(src => src.Translations))
                .ForMember(dest => dest.Sections, opt => opt.MapFrom(src => src.Sections));
            CreateMap<PageDtoForInsertion, Page>();

            CreateMap<PageSectionDtoForUpdate, PageSection>().ReverseMap();
            CreateMap<PageSection, PageSectionDto>()
                .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src.Fields))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.PageID, opt => opt.MapFrom(src => src.PageID));
            CreateMap<PageSectionDtoForInsertion, PageSection>();

            CreateMap<PageTranslationDtoForUpdate, PageTranslation>().ReverseMap();
            CreateMap<PageTranslation, PageTranslationDto>()
                .ForMember(dest => dest.PageID, opt => opt.MapFrom(src => src.PageID));
            CreateMap<PageTranslationDtoForInsertion, PageTranslation>();

            CreateMap<SectionFieldDtoForUpdate, SectionField>().ReverseMap();
            CreateMap<SectionField, Entities.DTOs.SectionFieldDto.SectionFieldDto>()
                .ForMember(dest => dest.PageSectionID, opt => opt.MapFrom(src => src.PageSectionID))
                .ForMember(dest => dest.SectionItemID, opt => opt.MapFrom(src => src.SectionItemID));
            CreateMap<SectionFieldDtoForInsertion, SectionField>();

            CreateMap<SectionItemDtoForUpdate, SectionItem>().ReverseMap();
            CreateMap<SectionItem, Entities.DTOs.SectionItemDto.SectionItemDto>()
                .ForMember(dest => dest.PageSectionID, opt => opt.MapFrom(src => src.PageSectionID))
                .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src.Fields));
            CreateMap<SectionItemDtoForInsertion, SectionItem>();

            CreateMap<PopupDtoForUpdate, Popup>().ReverseMap();
            CreateMap<Popup, PopupDto>();
            CreateMap<PopupDtoForInsertion, Popup>();

            CreateMap<SectionFieldDtoForUpdate, SectionField>().ReverseMap();
            CreateMap<SectionField, SectionFieldDto>();
            CreateMap<SectionFieldDtoForInsertion, SectionField>();

            CreateMap<SectionItemDtoForUpdate, SectionItem>().ReverseMap();
            CreateMap<SectionItem, SectionItemDto>();
            CreateMap<SectionItemDtoForInsertion, SectionItem>();

            CreateMap<SettingsDtoForUpdate, Settings>().ReverseMap();
            CreateMap<Settings, SettingsDto>();
            CreateMap<SettingsDtoForInsertion, Settings>();

            CreateMap<UserForRegisterDto, User>();
            CreateMap<UserDtoForUpdate, User>().ReverseMap();
            CreateMap<User, UserDto>();
            CreateMap<IdentityResult, UserDto>().ReverseMap();
        }
    }
}
