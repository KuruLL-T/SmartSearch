import {ChangeDetectorRef, Component, OnDestroy, OnInit, ViewEncapsulation} from '@angular/core';
import {Subscription} from 'rxjs';
import {NavigationEnd, Router} from '@angular/router';
import {DrawerItemExpandedFn, DrawerSelectEvent} from '@progress/kendo-angular-layout';
import {BreadCrumbCollapseMode} from '@progress/kendo-angular-navigation';
import {
  AlertStatistics,
  AlertStatisticsService,
  AuthService,
  GetDataService,
  LKPerson,
  Role,
  TokenStorageService,
  breadcrumbItemsMap,
  tokenStore,
} from 'common';
import {DisableBreadCrumbRoutes, MenuItem} from '../models/home/menu.model';
import {JwtHelperService} from '@auth0/angular-jwt';
import {PersonService} from '../services/lkperson.service';
import {PublicationsBadgeService} from 'common';
import {SummCommentsStatusesService} from 'common';
import {environment} from '../environments/environment';

const menuExpanded = 'menuExpanded';

@Component({
  selector: 'contingent-app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class AppComponent implements OnInit, OnDestroy {
  public fullTitle = environment.headerTitle.full;
  public shortTitle = environment.headerTitle.short;

  public hidden = true;
  protected readonly isAdmin = true;
  public isSwitchActive = false;
  public breadcrumbitems: DisableBreadCrumbRoutes[] = [];
  public expandedIndices = [2];

  public photoUrl = '';
  public selected = 'Inbox';
  public expanded: boolean = (localStorage.getItem(menuExpanded) ?? 'true') == 'true';
  private routesData: Subscription = new Subscription();
  private services: Array<MenuItem> = [
    {text: 'Новости и объявления', icon: 'k-i-parameter-date-time', url: '/announcements', path: '/announcements', id: Role.Announcements, default: true, return: false},
    {text: 'Контингент', icon: 'k-i-user', url: '/contingent', path: '/contingent', id: Role.Contingent, return: false},
    {text: 'Промежуточный контроль', icon: 'k-i-chart-pie', url: '/middlecontrol', path: '/middlecontrol', id: Role.Session, return: false},
    {text: 'Текущий контроль', icon: 'k-i-chart-doughnut', url: '/currentcontrol', path: '/currentcontrol', id: Role.CurrentControl, return: false},
    {text: 'Образование', icon: 'k-i-delicious', url: '/education', path: '/education', id: Role.Projecting, return: false},
    {text: 'Сопряжение и нагрузка', icon: 'k-i-gear', url: '/disciplineworkload', path: '/disciplineworkload', id: Role.Disciplineworkload, return: false},
    {text: 'Справочники', icon: 'k-i-dictionary-add', url: '/dicts', path: '/dicts', id: Role.Dictionaries, return: false},
    {text: 'Публикации', icon: 'k-i-document-manager', url: '/publications', id: Role.Publication, path: '/publications', return: false},
    {text: 'Аудиторный фонд', icon: 'k-i-pane-freeze', url: '/classroom', id: Role.Classroom, path: '/classroom', return: false},
    {text: "Зарплатный лист", icon: "k-i-calculator", url: "/payslip", id: Role.LKPerson, path: "/payslip", default: true, return: false},
    {text: "Карты", icon: "k-i-detail-section", url: "/cards", id: Role.Cards, path: "/cards", return: false},
    {text: "Конструктор отчетов", icon: "k-i-file-report", url: "/reportdesigner", id: Role.ReportDesigner, path: "/reportdesigner", return: false},
  ];
  private externalLinks: Array<MenuItem> = environment.menuItems.map((item: any) => ({
    ...item,
    return: true,
    default: true,
  }));
  public items: Array<MenuItem> = [
    {icon: 'k-i-menu', return: false, padding: '30px', default: true, selected: false},
    {text: 'Главная', icon: 'k-i-home', selected: true, path: '/profile', id: 'home', url: '/profile', default: true, return: false},
    ...this.services,
    ...this.externalLinks,
    {text: 'Технические администраторы', icon: 'k-i-myspace-box', url: '/technicaladministrators', path: '/technicaladministrators', return: false},
  ];

  public person: LKPerson = {
    personId: 1,
    personExternalId: 'e38d630d-5630-4ada-8834-883e87c8ae08',
    fullName: 'Иванов Иван Иванович',
    firstName: 'Иван',
    lastName: 'Иванов',
    middleName: 'Иванович',
    login: 'ivan.ivanov',
    birthday: new Date(),
    hasPps: false
  };

  public collapseMode: BreadCrumbCollapseMode = 'auto';
  public newPublicationCount = 0;
  public calculatedParametr: AlertStatistics['calculatedParametr'] = 0;
  public countComments = 0;

  public onSelect(ev: DrawerSelectEvent): void {
    localStorage.setItem(menuExpanded, ev.sender.expanded ? 'false' : 'true');
    if (ev.item.icon == 'k-i-menu') {
      ev.preventDefault();
      this.expanded = !this.expanded;
    } else if (ev.item.path != undefined) {
      this.router.navigated = false;
      this.router.navigate([ev.item.path]);
    }
    localStorage.removeItem('middle_control_settings');
    localStorage.removeItem('current_control_settings');
  }

  public menuClick(value: MenuItem) {
    return typeof value.return === 'boolean' ? value.return : false;
  }

  public menuVisible(value: MenuItem) {
    return (
      this.isAdmin ||
      value.default ||
      (this.person.hasPps && value.id == Role.Disciplineworkload)
    );
  }

  constructor(
    private router: Router,
    private cdRef: ChangeDetectorRef,
    private getDataService: GetDataService,
    private tokenStore: TokenStorageService,
    private jwtHelper: JwtHelperService,
    public authService: AuthService,
    private alertStatisticsServise: AlertStatisticsService,
    private notificationService: PublicationsBadgeService,
    private personService: PersonService,
    private summCommentsStatusesService: SummCommentsStatusesService
  ) {
    this.routesData = this.router.events.subscribe((e) => {
      if (e instanceof NavigationEnd) {
        const splitURL = e.url.split('/', 3);
        const prevSelected = this.items.find((i) => i.selected);
        if (prevSelected) {
          prevSelected.selected = false;
        }
        const newSelected = this.items.find(
          (i) => i.path === `/${splitURL[1]}` || i.path === `/${splitURL[1]}/${splitURL[2]}`
        );
        if (newSelected && newSelected.icon !== 'k-i-menu') {
          newSelected.selected = true;
        } else {
          this.items[1].selected = true;
        }
        this.items = [...this.items];
        console.log(this.items);
        this.breadcrumbitems = this.initRoutes();
        this.getDataService.addBreadCrumbs$.subscribe((value) => {
          this.breadcrumbitems = [...this.breadcrumbitems, ...value]
            .filter((a) => a.text)
            .map((a) => {
              const object = value.find((b) => b.text === a.text);
              return object ?? a;
            });
        });
      }
    });
  }

  public isAlertEnabled(text: string) {
    return (
      (text === 'Новости и объявления' && this.calculatedParametr !== 0) ||
      (text === 'Публикации' && this.newPublicationCount !== 0) ||
      (text === 'Образование' && this.countComments !== 0)
    );
  }

  public getBadgeCount(text: string) {
    switch (text) {
      case 'Новости и объявления':
        return this.calculatedParametr;
      case 'Публикации':
        return this.newPublicationCount;
      case 'Образование':
        return this.countComments;
      default:
        return 0;
    }
  }

  public getAlertStatistics() {
    this.alertStatisticsServise.getAlertStatistics().subscribe((response) => {
      this.calculatedParametr = response['calculatedParametr'];
    });
  }

  public getNewPublicationCount() {
    this.notificationService.getNotification().subscribe((response) => {
      this.newPublicationCount = response.unreadMyPublications + response.unreadAllPublications;
    });
  }

  public getNewCommentsCount() {
    this.summCommentsStatusesService.getSumComments().subscribe((response) => {
      this.countComments = response;
    });
  }

  ngOnInit(): void {
    if (this.authService.isUserAuthenticated()) {
      this.getCurrentPerson();
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;

      if (this.items.find((item) => item.id === Role.Announcements)) {
        this.getAlertStatistics();
        this.alertStatisticsServise.subscriber$.subscribe(() => {
          this.getAlertStatistics();
        });
      }

      if (this.items.find((item) => item.id === Role.Publication)) {
        this.getNewPublicationCount();
        this.notificationService.subscriber$.subscribe(() => {
          this.getNewPublicationCount();
        });
      }

      if (this.items.find((item) => item.id === Role.Projecting)) {
        this.getNewCommentsCount();
      }
    }

    const switchUser = localStorage.getItem('switchPerson');
    if (switchUser === 'true') this.isSwitchActive = true;
    this.cdRef.detectChanges();
  }

  public getCurrentPerson() {
    this.personService.getCurrentPerson().subscribe((response) => {
      this.person = response;
      this.photoUrl = `url('${environment.apiEndpoint}lkperson/profile/GetPersonPhoto/${response.personExternalId}')`;
    });
  }

  public onItemClick(item: DisableBreadCrumbRoutes): void {
    const selectedItemIndex = this.breadcrumbitems.findIndex((i) => i.address === item.address);
    const url = this.breadcrumbitems
      .slice(0, selectedItemIndex + 1)
      .map((i) => `${i.address?.toLowerCase()}`);
    // Починка хлебных крошек для УП
    if (
      this.router.url.includes('edit-discipline') ||
      this.router.url.includes('add-plan') ||
      this.router.url.includes('add-discipline')
    ) {
      const splitURL = this.router.url.split('/', 5);
      this.router.navigate([`education/${splitURL[2]}/plans/plan/${splitURL[4]}/`]);
      return;
    }
    this.router.navigate(url);
  }

  public isItemExpanded: DrawerItemExpandedFn = (item): boolean => {
    return this.expandedIndices.indexOf(item.id) >= 0;
  };

  public ngOnDestroy(): void {
    this.routesData.unsubscribe();
  }

  private initRoutes() {
    const route = this.router.url;
    const breadcrumbs = route
      .substring(0, route.indexOf('?') !== -1 ? route.indexOf('?') : route.length)
      .split('/')
      .map((segment, index, array) =>
        index < array.length - 1 && breadcrumbItemsMap.has(`${segment}/`) ? `${segment}/` : segment
      )
      .filter((segment) => breadcrumbItemsMap.has(segment))
      .map((segment) => ({
        text: breadcrumbItemsMap.get(segment),
        address: segment,
      }))
      .filter(
        (item: DisableBreadCrumbRoutes, index: number, self) =>
          index === self.findIndex((t) => t.address === item.address)
      );
    return breadcrumbs;
  }

  public switchUser() {
    this.router.navigate(['/profile/switchuser']);
  }

  public stopUserPreview() {
    const currentToken = localStorage.getItem('currentToken');
    localStorage.setItem('switchPerson', 'false');
    if (currentToken !== null) {
      localStorage.setItem(tokenStore, currentToken);
      localStorage.removeItem('currentToken');
      window.location.reload();
    }
  }

  public logOut = () => {
    localStorage.removeItem('switchPerson');
    localStorage.removeItem('last_url');
    if (sessionStorage.getItem('temp_display_settings')) sessionStorage.removeItem('temp_display_settings');
    window.location.reload();
    this.tokenStore.deleteToken();
  };
}
