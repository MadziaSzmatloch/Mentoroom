using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Entities.Tags;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoroom.APPLICATION.Mappings
{
    [Mapper]
    public partial class TagsMapper
    {
        public partial DepartmentModel DepartmentToDepartmentModel(Department department);
        public partial MajorModel MajorToMajorModel(Major major);
        public partial DegreeModel DegreeToDegreeModel(Degree degree);
        public partial YearModel YearToYearModel(Year year);
        public partial SemesterModel SemesterToSemesterModel(Semester semester);

        public partial Department ModelToDepartemnt(DepartmentModel departmentModel);
        public partial Major ModelToMajor(MajorModel majorModel);
        public partial Degree ModelToDegree(DegreeModel degreeModel);
        public partial Year ModelToYear(YearModel yearModel);
        public partial Semester ModelToSemester(SemesterModel semesterModel);


    }
}
