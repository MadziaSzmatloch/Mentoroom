import { MenuItem } from 'primeng/api';

export class Assignment {
  public name: string;
  public description: string;
  public createDate: Date;
  public deadLineDate: Date;
  public isActive: boolean;
  public id: string;
  public courseId: string;
  public isCompleted: boolean | null;
  public assignmentFiles: AssignmentFile[];
  public requiredFiles: RequiredFile[];
  public menuItems?: MenuItem[];

  constructor(
    id?: string,
    courseId?: string,
    name?: string,
    description?: string,
    createDate?: Date,
    deadLineDate?: Date,
    isActive?: boolean,
    isCompleted?: boolean | null,
    assignmentFiles?: AssignmentFile[],
    requiredFiles?: RequiredFile[]
  ) {
    this.id = id || '';
    this.courseId = courseId || '';
    this.name = name || '';
    this.description = description || '';
    this.createDate = createDate || new Date();
    this.deadLineDate = deadLineDate || new Date();
    this.isActive = isActive || false;
    this.isCompleted = isCompleted;
    this.assignmentFiles = assignmentFiles || [];
    this.requiredFiles = requiredFiles || [];
  }
}

export class AssignmentFile {
  public id: string;
  public name: string;

  constructor(id: string, name: string) {
    this.id = id;
    this.name = name;
  }
}

export class RequiredFile {
  public id: string;
  public fileNameSuffix: string;
  public extension: string;
  public isSended?: boolean | null;
  public maxSizeInKb: number;

  constructor(
    id: string,
    fileNameSuffix: string,
    extension: string,
    maxSizeInKb: number,
    isSended?: boolean | null
  ) {
    this.id = id;
    this.fileNameSuffix = fileNameSuffix;
    this.extension = extension;
    this.maxSizeInKb = maxSizeInKb;
    this.isSended = isSended;
  }
}
