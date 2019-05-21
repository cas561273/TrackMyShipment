import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './core/auth/login/login.component';
import { FormsModule } from '@angular/forms';
import { HeaderComponent } from './component/header/header.component';
import { HomeComponent } from './component/home/home.component';
import { MainComponent } from './core/static-page/main/main.component';
import { PricingComponent } from './component/pricing/pricing.component';
import { SidebarComponent } from './component/sidebar/sidebar.component';
import { AuthService } from './core/auth/authService';
import { DataSharingService } from './services/dataSharing';
import { DialogOverviewComponent } from "./carrier/ui/dialog-overview/dialog-overview.component";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UsersCarrierComponent } from "./carrier/component/users-carrier/users-carrier.component";
import { RegistrationComponent } from './core/auth/registration/registration.component';
import { CarrierDetailComponent } from "./carrier/component/carrier-detail/carrier-detail.component";
import { MyCarriersComponent } from "./carrier/component/my-carriers/my-carriers.component";
import { MaterialModule } from './material-module';
import { CarrierService } from './carrier/shared/carrierService';
import { DialogAddUserCarrierComponent } from './carrier/ui/dialog.add-user-carrier/dialog.add-user-carrier.component';
import { UserProfileComponent } from "./user/component/user-profile/user-profile.component";
import { UserService } from "./user/shared/userService";
import { AddressComponent } from "./component/address/address.component";


@NgModule(({
  declarations: [
    AppComponent,
    HomeComponent,
    MainComponent,
    LoginComponent,
    HeaderComponent,
    SidebarComponent,
    PricingComponent,
    AddressComponent,
    MyCarriersComponent,
    UserProfileComponent,
    UsersCarrierComponent,
    RegistrationComponent,
    CarrierDetailComponent,
    DialogOverviewComponent,
    DialogAddUserCarrierComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    MaterialModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  entryComponents: [DialogOverviewComponent,DialogAddUserCarrierComponent],
  providers: [AuthService, DataSharingService, CarrierService,UserService],
  bootstrap: [AppComponent]
}) as any)
export class AppModule { }
