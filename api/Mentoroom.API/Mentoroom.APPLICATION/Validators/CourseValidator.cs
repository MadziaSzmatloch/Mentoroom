using FluentValidation;
using Mentoroom.APPLICATION.Managements.Course;
using Mentoroom.DOMAIN.Entities.Tags;
using Mentoroom.INFRASTRACTURE.Persistence;

namespace Mentoroom.APPLICATION.Validators
{
    public class CourseValidator : AbstractValidator<CoursePost>
    {
        public CourseValidator(AppDbContext dbContext)
        {
            Degree? existingDegree = new Degree();
            Year? existingYear = new Year();
            Department? existingDepartment = new Department();
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description cannot be empty");
            RuleFor(c => c.DegreeId)
                .NotEmpty().WithMessage("Must assign degree")
                .Custom((id, context) =>
                {
                    existingDegree = dbContext.Degrees.FirstOrDefault(d => d.Id == id);
                    if (existingDegree == null)
                    {
                        context.AddFailure($"No such degree");
                    }
                });
            RuleFor(c => c.YearId)
                .NotEmpty().WithMessage("Must assign year")
                .Custom((id, context) =>
                {
                    if (existingDegree != null)
                    {
                        existingYear = dbContext.Years.FirstOrDefault(y => y.Id == id);
                        if (existingYear == null)
                        {
                            context.AddFailure($"No such year");
                        }
                        if (existingYear != null && existingYear.DegreeId != existingDegree.Id)
                        {
                            context.AddFailure($"This year does not belong to that degree");
                        }
                    }
                });
            RuleFor(c => c.SemesterId)
                .NotEmpty().WithMessage("Must assign semester")
                .Custom((id, context) =>
                {
                    if (existingYear != null)
                    {
                        var existingSemester = dbContext.Semesters.FirstOrDefault(s => s.Id == id);
                        if (existingSemester == null)
                        {
                            context.AddFailure($"No such semester");
                        }
                        if (existingSemester != null && existingSemester.YearId != existingYear.Id)
                        {
                            context.AddFailure($"This semster does not belong to that year");
                        }
                    }
                });
            RuleFor(c => c.DepartmentId)
                .NotEmpty().WithMessage("Must assign department")
                .Custom((id, context) =>
                {
                    existingDepartment = dbContext.Departments.FirstOrDefault(d => d.Id == id);
                    if (existingDepartment == null)
                    {
                        context.AddFailure($"No such department");
                    }
                });
            RuleFor(c => c.MajorId)
                .NotEmpty().WithMessage("Must assign major")
                .Custom((id, context) =>
                {
                    if (existingDepartment != null)
                    {
                        var existingMajor = dbContext.Majors.FirstOrDefault(m => m.Id == id);
                        if (existingMajor == null)
                        {
                            context.AddFailure($"No such major");
                        }
                        if (existingMajor != null && existingMajor.DepartmentId != existingDepartment.Id)
                        {
                            context.AddFailure($"This major does not belong to that department");
                        }
                    }
                });
        }
    }
}
