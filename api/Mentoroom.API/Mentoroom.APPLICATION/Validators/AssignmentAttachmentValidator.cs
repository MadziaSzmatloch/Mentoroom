using FluentValidation;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.INFRASTRACTURE.Persistence;

namespace Mentoroom.APPLICATION.Validators
{
    public class AssignmentAttachmentValidator : AbstractValidator<AssignmentAttachment>
    {
        public AssignmentAttachmentValidator(AppDbContext dbContext)
        {
            var existingAttachment = new AssignmentAttachment();
            RuleFor(x => x.Uri).Custom((uri, context) =>
            {
                existingAttachment = dbContext.AssigmnentAttachments.FirstOrDefault(a => a.Uri == uri);
                if (existingAttachment != null)
                {
                    context.AddFailure($"File with this name already exists!");
                }
            });
        }
    }
}
