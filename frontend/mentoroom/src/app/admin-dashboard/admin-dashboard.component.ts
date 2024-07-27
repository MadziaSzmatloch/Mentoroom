import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.css',
})
export class AdminDashboardComponent {
  menuItems: MenuItem[] | undefined;
  activeItem: MenuItem | undefined;

  constructor() {}

  ngOnInit() {
    this.menuItems = [
      {
        label: 'Users',
        icon: 'pi pi-user',
        routerLink: '/admin-dashboard/users',
      },
      {
        label: 'Access Code',
        icon: 'pi pi-key',
        routerLink: '/admin-dashboard/access-code',
      },
    ];

    this.activeItem = this.menuItems[0];
  }
}
