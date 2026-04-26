using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class SectionFieldRepository : RepositoryBase<SectionField>, ISectionFieldRepository
    {

        public SectionFieldRepository(RepositoryContext context) : base(context) { }

        public SectionField CreateSectionField(SectionField sectionField)
        {
            Create(sectionField);
            return sectionField;
        }

        public SectionField DeleteSectionField(SectionField sectionField)
        {
            Delete(sectionField);
            return sectionField;
        }

        public async Task<IEnumerable<SectionField>> GetAllSectionFieldsAsync(bool? trackChanges)
        {
            var sectionFields = await FindAll(trackChanges)
                .OrderByDescending(s => s.ID)
                .ToListAsync();

            return sectionFields;
        }

        public async Task<SectionField> GetSectionFieldByIdAsync(int id, bool? trackChanges)
        {
            return await FindByCondition(s => s.ID.Equals(id), trackChanges)
                .SingleOrDefaultAsync();
        }

        public SectionField UpdateSectionField(SectionField sectionField)
        {
            Update(sectionField);
            return sectionField;
        }
    }
}
