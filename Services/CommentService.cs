using AutoMapper;
using Entities.DTOs.CommentDto;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public CommentService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<CommentDto> CreateCommentAsync(CommentDtoForInsertion commentDtoForInsertion)
        {
            var comment = _mapper.Map<Entities.Models.Comment>(commentDtoForInsertion);
            var hasComment = await _manager.CommentRepository.GetCommentByUserIdAsync(commentDtoForInsertion.UserId!, false);
            if (hasComment != null)
            {
                throw new InvalidOperationException("Comment already exists for this user.");
            }
            _manager.CommentRepository.CreateComment(comment);
            await _manager.SaveAsync();
            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<CommentDto> DeleteCommentAsync(int id, bool? trackChanges)
        {
            var comment = await _manager.CommentRepository.GetCommentByIdAsync(id, trackChanges);
            _manager.CommentRepository.DeleteComment(comment!);
            await _manager.SaveAsync();
            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<IEnumerable<CommentDto>> GetAllCommentsAsync(bool? trackChanges)
        {
            var comment = await _manager.CommentRepository.GetAllCommentsAsync(trackChanges);
            return _mapper.Map<IEnumerable<CommentDto>>(comment);
        }

        public async Task<CommentDto> GetCommentByIdAsync(int id, bool? trackChanges)
        {
            var comment = await _manager.CommentRepository.GetCommentByIdAsync(id, trackChanges);
            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<CommentDto> GetCommentByUserIdAsync(string userId, bool? trackChanges)
        {
            var comment = await _manager.CommentRepository.GetCommentByUserIdAsync(userId, trackChanges);
            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<CommentDto> UpdateCommentAsync(CommentDtoForUpdate commentDtoForUpdate)
        {
            var comment = await _manager.CommentRepository.GetCommentByIdAsync(commentDtoForUpdate.ID, false);
            _mapper.Map(commentDtoForUpdate, comment);
            _manager.CommentRepository.UpdateComment(comment!);
            await _manager.SaveAsync();
            return _mapper.Map<CommentDto>(comment);
        }
    }
}
