import {NgModule} from '@angular/core';
import {MatInputModule, MatProgressSpinnerModule, MatDialogModule} from '@angular/material';
import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatTableModule } from '@angular/material/table';
import { MatRadioModule } from '@angular/material/radio';



@NgModule({
    imports: [
        MatProgressSpinnerModule,
        MatCardModule,
        MatInputModule,
        MatFormFieldModule,
        MatDialogModule,
        MatCheckboxModule,
        MatTableModule,
        MatRadioModule  


    ],
    exports: [
        MatProgressSpinnerModule,
        MatCardModule,
        MatInputModule,
        MatFormFieldModule,
        MatDialogModule,
        MatCheckboxModule,
        MatTableModule,
        MatRadioModule
    ]
})
export class MaterialModule{}
