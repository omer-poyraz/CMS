using Entities.Models;

namespace Repositories.Contracts
{
    public interface ISectionFieldRepository : IRepositoryBase<SectionField>
    {
        Task<IEnumerable<SectionField>> GetAllSectionFieldsAsync(bool? trackChanges);
        Task<SectionField> GetSectionFieldByIdAsync(int id, bool? trackChanges);
        SectionField CreateSectionField(SectionField sectionField);
        SectionField UpdateSectionField(SectionField sectionField);
        SectionField DeleteSectionField(SectionField sectionField);
    }
}
