import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './component/login/login.component';
import { FormsModule } from '@angular/forms';
import { HeaderComponent } from './component/header/header.component';
import { MenuComponent } from './component/menu/menu.component';
import { HomeComponent } from './component/home/home.component';
import { MainComponent } from './component/main/main.component';
import { CarrierComponent } from './component/carrier/carrier.component';
import { RegistrationComponent } from './component/registration/registration.component';
import { PricingComponent } from './component/pricing/pricing.component';
import { SidebarComponent } from './component/sidebar/sidebar.component';
import { AuthService } from './services/authService';
import { DataSharingService } from './services/dataSharing';
import { CarrierService } from './services/carrierService';


@NgModule(({
  declarations: [
    AppComponent,
    LoginComponent,
    HeaderComponent,
    MenuComponent,
    HomeComponent,
    MainComponent,
    CarrierComponent,
    RegistrationComponent,
    PricingComponent,
    SidebarComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [AuthService, DataSharingService, CarrierService],
  bootstrap: [AppComponent]
}) as any)
export class AppModule { }
