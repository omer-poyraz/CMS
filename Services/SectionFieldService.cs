using AutoMapper;
using Entities.DTOs.SectionFieldDto;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class SectionFieldService : ISectionFieldService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public SectionFieldService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<SectionFieldDto> CreateSectionFieldAsync(SectionFieldDtoForInsertion sectionFieldGroupDtoForInsertion)
        {
            try
            {
                var sectionFieldGroup = _mapper.Map<Entities.Models.SectionField>(sectionFieldGroupDtoForInsertion);
                _manager.SectionFieldRepository.CreateSectionField(sectionFieldGroup);
                await _manager.SaveAsync();
                return _mapper.Map<SectionFieldDto>(sectionFieldGroup);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<SectionFieldDto> DeleteSectionFieldAsync(int id, bool? trackChanges)
        {
            var sectionFieldGroup = await _manager.SectionFieldRepository.GetSectionFieldByIdAsync(id, trackChanges);
            _manager.SectionFieldRepository.DeleteSectionField(sectionFieldGroup);
            await _manager.SaveAsync();
            return _mapper.Map<SectionFieldDto>(sectionFieldGroup);
        }

        public async Task<IEnumerable<SectionFieldDto>> GetAllSectionFieldsAsync(bool? trackChanges)
        {
            var sectionFields = await _manager.SectionFieldRepository.GetAllSectionFieldsAsync(trackChanges);
            return _mapper.Map<IEnumerable<SectionFieldDto>>(sectionFields);
        }

        public async Task<SectionFieldDto> GetSectionFieldByIdAsync(int id, bool? trackChanges)
        {
            var sectionFieldGroup = await _manager.SectionFieldRepository.GetSectionFieldByIdAsync(id, trackChanges);
            return _mapper.Map<SectionFieldDto>(sectionFieldGroup);
        }

        public async Task<SectionFieldDto> UpdateSectionFieldAsync(SectionFieldDtoForUpdate sectionFieldDtoForUpdate)
        {
            var sectionField = await _manager.SectionFieldRepository.GetSectionFieldByIdAsync(sectionFieldDtoForUpdate.ID, false);
            _mapper.Map(sectionFieldDtoForUpdate, sectionField);
            _manager.SectionFieldRepository.UpdateSectionField(sectionField);
            await _manager.SaveAsync();
            return _mapper.Map<SectionFieldDto>(sectionField);
        }
    }
}
