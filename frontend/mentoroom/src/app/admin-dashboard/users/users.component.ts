import { Component, OnDestroy, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { UsersService } from '../../services/users.service';
import { Observable, Subscription } from 'rxjs';
import { AuthService } from '../../services/auth.service';
import UserData from '../../interfaces/user-data.interface';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrl: './users.component.css',
  providers: [UsersService],
})
export class UsersComponent implements OnInit, OnDestroy {
  usersSubscription: Subscription;
  users!: UserData[];
  isLoading = false;
  roles!: any[];
  userMenuItems: MenuItem[] | undefined;
  selectedUser: UserData;

  visible = false;

  constructor(
    private usersService: UsersService,
    private authService: AuthService
  ) {}

  selectedRole: { label: string; value: string } | null;
  studentIndex: string;
  errorMessage: string | null;

  ngOnInit() {
    this.isLoading = true;
    this.usersSubscription = this.usersService
      .getUsers()
      .subscribe((responseData: UserData[]) => {
        this.users = responseData;
        this.isLoading = false;
      });

    this.roles = [
      { label: 'Admin', value: 'Admin' },
      { label: 'Lecturer', value: 'Lecturer' },
      { label: 'Student', value: 'Student' },
    ];

    this.userMenuItems = [
      {
        label: 'Promote to',
        icon: 'pi pi-refresh',
        command: () => {
          this.visible = true;
        },
      },
      {
        label: 'Delete',
        icon: 'pi pi-times',
        command: () => {
          this.deleteUser(this.selectedUser.id);
        },
      },
    ];
  }

  ngOnDestroy(): void {
    this.usersSubscription.unsubscribe();
  }

  first = 0;
  rows = 10;

  next() {
    this.first = this.first + this.rows;
  }

  prev() {
    this.first = this.first - this.rows;
  }

  reset() {
    this.first = 0;
  }

  pageChange(event) {
    this.first = event.first;
    this.rows = event.rows;
  }

  isLastPage(): boolean {
    return this.users ? this.first === this.users.length - this.rows : true;
  }

  isFirstPage(): boolean {
    return this.users ? this.first === 0 : true;
  }

  deleteUser(userId: string) {
    console.log(userId);
  }

  promoteTo() {
    let subscription: Observable<UserData>;
    switch (this.selectedRole.value) {
      case 'Student': {
        subscription = this.authService.promoteToStudent(
          this.selectedUser.id,
          this.studentIndex
        );
        break;
      }
      case 'Lecturer': {
        subscription = this.authService.promoteToLecturer(this.selectedUser.id);
        break;
      }
      case 'Admin': {
        subscription = this.authService.promoteToAdmin(this.selectedUser.id);
        break;
      }
    }

    subscription.subscribe({
      next: (response: UserData) => {
        this.updateUser(response);
        this.hidePromoteModal();
      },
      error: (error) => {
        this.errorMessage = error;
      },
    });
  }

  updateUser(response: UserData) {
    const index = this.users.findIndex((user) => user.id === response.id);
    if (index !== -1) {
      this.users[index] = response;
    }
  }

  hidePromoteModal() {
    this.visible = false;
    this.studentIndex = '';
    this.selectedRole = { label: '', value: '' };
    this.errorMessage = null;
  }
}
