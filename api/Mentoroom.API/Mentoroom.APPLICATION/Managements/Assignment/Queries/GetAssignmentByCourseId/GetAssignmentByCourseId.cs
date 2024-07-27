using MediatR;
using Mentoroom.APPLICATION.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoroom.APPLICATION.Managements.Assignment.Queries.GetAssignmentById
{
    public class GetAssignmentByCourseId : IRequest<IEnumerable<AssignmentDto>>
    {
        public Guid CourseId { get; set; }
    }
}
