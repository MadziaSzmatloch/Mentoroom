using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoroom.APPLICATION.Managements.Course.Queries.GetCourse
{
    public class GetCourse : IRequest<IEnumerable<CourseDto>>
    {
    }
}
