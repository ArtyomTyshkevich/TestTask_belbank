using AutoMapper;
using MediatR;
using TestTask.Application.DTOs;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Infrastructure.Features.Categories.Commands;

namespace TestTask.Infrastructure.Features.Categories.Handlers.CommandHandlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDTO?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryDTO?> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {

            var category = await _unitOfWork.Categories.GetByIdAsync(request.Category.Id.Value, cancellationToken);

            _mapper.Map(request.Category, category);

            await _unitOfWork.Categories.UpdateAsync(category, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
