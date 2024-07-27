export class Course {
  public id: string;
  public name: string;
  public description: string;
  public isActive: boolean;
  public isConfirmed: boolean | null;
  public department: string;
  public shortDepartment: string;
  public major: string;
  public degree: string;
  public year: string;
  public semester: string;
  public author: {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    role: string;
  };
  public coAuthors: {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    role: string;
  }[];

  constructor(
    id: string,
    name: string,
    description: string,
    isActive: boolean,
    isConfirmed: boolean | null,
    department: string,
    shortDepartment: string,
    major: string,
    degree: string,
    year: string,
    semester: string,
    author: {
      id: string;
      firstName: string;
      lastName: string;
      email: string;
      role: string;
    },
    coAuthors: {
      id: string;
      firstName: string;
      lastName: string;
      email: string;
      role: string;
    }[]
  ) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.isActive = isActive;
    this.isConfirmed = isConfirmed;
    this.department = department;
    this.shortDepartment = shortDepartment;
    this.major = major;
    this.degree = degree;
    this.year = year;
    this.semester = semester;
    this.author = author;
    this.coAuthors = coAuthors;
  }
}
