import { PricingComponent } from './component/pricing/pricing.component';
import { RegistrationComponent } from './core/auth/registration/registration.component';
import { MainComponent } from './core/static-page/main/main.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './core/auth/login/login.component';
import { HomeComponent } from './component/home/home.component';
import { UsersCarrierComponent } from "./carrier/component/users-carrier/users-carrier.component";
import { CarrierDetailComponent } from './carrier/component/carrier-detail/carrier-detail.component';
import { MyCarriersComponent } from "./carrier/component/my-carriers/my-carriers.component";
import { UserProfileComponent } from "./user/component/user-profile/user-profile.component";
import { AddressComponent } from "./component/address/address.component";

const routes: Routes = [ {path: '', redirectTo: 'login', pathMatch: 'full'},
{path: 'login', component: LoginComponent},
{path: 'registration', component: RegistrationComponent},
{path: 'main',
  component: MainComponent,
  children: [
    { path: '', redirectTo: 'home', pathMatch: 'full'},
    { path: 'home', component: HomeComponent },
    { path: 'pricing', component: PricingComponent },
    { path: 'carriers', component: UsersCarrierComponent },
    { path: 'carrier/:id', component: UsersCarrierComponent },
    { path: 'carrier-detail/:id', component: CarrierDetailComponent },
    { path: 'my-carriers', component: MyCarriersComponent },
    { path: 'user-profile', component: UserProfileComponent },
    { path: 'my-address', component: AddressComponent }
  ]
  },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

