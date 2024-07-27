export class CourseFilter {
  name: string = '';
  selectedDepartment: string | null = null;
  selectedStudyProgram: string | null = null;
  selectedDegree: string | null = null;
  selectedYear: number | null = null;
  selectedSemester: number | null = null;
  showInactive: boolean = false;
}
