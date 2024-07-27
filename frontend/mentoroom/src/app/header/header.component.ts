import { Component, OnDestroy, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { AuthService } from '../services/auth.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit, OnDestroy {
  isAuthenticated = false;
  userRole: string | null;
  firstName: string | null;

  items: MenuItem[] | undefined;
  private userSubscription: Subscription;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.items = [
      {
        items: [
          {
            label: 'My courses',
            routerLink: '/my-courses',
          },
          {
            label: 'My assignment',
            routerLink: '/my-assignment',
          },
        ],
      },
      {
        items: [
          {
            label: 'Logout',
            icon: 'pi pi-sign-out',
            command: () => {
              this.onLogout();
            },
          },
        ],
      },
    ];

    this.userSubscription = this.authService.user.subscribe((user) => {
      this.isAuthenticated = this.authService.isAuthenticated();
      this.userRole = user?.getUserRole;
      this.firstName = user?.getFirstName;
    });
  }

  ngOnDestroy(): void {
    this.userSubscription.unsubscribe();
  }

  onLogout() {
    this.authService.logout().subscribe();
    this.router.navigate(['/']);
  }
}
