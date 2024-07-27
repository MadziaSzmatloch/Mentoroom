using MediatR;
using Mentoroom.APPLICATION.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoroom.APPLICATION.Managements.Assignment.Queries.GetAssignments
{
    public class GetAssignments : IRequest<IEnumerable<AssignmentDto>>
    {
    }
}
