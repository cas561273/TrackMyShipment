import { PricingComponent } from './component/pricing/pricing.component';
import { RegistrationComponent } from './component/registration/registration.component';
import { MainComponent } from './component/main/main.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './component/login/login.component';
import { HomeComponent } from './component/home/home.component';
import { CarrierComponent } from './component/carrier/carrier.component';
import { AuthService } from './services/authService';

const routes: Routes = [ {path: '', redirectTo: 'login', pathMatch: 'full'},
{path: 'login', component: LoginComponent},
{path: 'registration', component: RegistrationComponent},
{path: 'main',
  component: MainComponent,
  children: [
    { path: '', redirectTo: 'home', pathMatch: 'full'},
    { path: 'home', component: HomeComponent },
    { path: 'pricing', component: PricingComponent },
    { path: 'carrier', component: CarrierComponent }
  ]
  },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

