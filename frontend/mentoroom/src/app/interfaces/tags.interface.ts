export interface Degree {
  id: string;
  name: string;
}

export interface Department {
  id: string;
  name: string;
  shortName: string;
}

export interface StudyProgram {
  id: string;
  name: string;
  departmentId: string;
}

export interface Semester {
  id: string;
  name: string;
  yearId: string;
}

export interface Year {
  id: string;
  name: string;
  degreeId: string;
}
