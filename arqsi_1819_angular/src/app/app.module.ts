import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CatalogoComponent } from './components/catalogo/catalogo.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule, MatProgressSpinnerModule, MatCardModule, MatSidenavModule, MatDialogModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatOptionModule, MatSelectModule, MatSliderModule, MatDividerModule, MatSortModule, MatSnackBarModule, MatExpansionModule, MatTabsModule, MatIconModule, MatBadgeModule } from '@angular/material';
import { ItemdeprodutoComponent } from './components/itemdeproduto/itemdeproduto.component';
import { EncomendaComponent } from './components/encomenda/encomenda.component';
import { PartesItemProdutoComponent } from './components/partesitemproduto/partesitemproduto.component';
import { AddEditProdutoComponent } from './components/addeditproduto/addeditproduto.component';
import { ToastrModule } from 'ngx-toastr';
import { CriareditaragregacaoComponent } from './components/criareditaragregacao/criareditaragregacao.component';
import { EditarItemComponent } from './components/editar-item/editar-item.component';
import { EncomendarItemComponent } from './components/encomendar-item/encomendar-item.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    CatalogoComponent,
    ItemdeprodutoComponent,
    EncomendaComponent,
    PartesItemProdutoComponent,
    AddEditProdutoComponent,
    CriareditaragregacaoComponent,
    EditarItemComponent,
    EncomendarItemComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    ReactiveFormsModule,
    MatTableModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatOptionModule,
    MatSelectModule,
    MatSliderModule,
    MatDividerModule,
    MatSortModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatCardModule,
    MatExpansionModule,
    MatTabsModule,
    MatIconModule, 
    MatBadgeModule,
    MatSidenavModule
  ],
  providers: [],
  entryComponents: [
    ItemdeprodutoComponent,
    PartesItemProdutoComponent,
    AddEditProdutoComponent,
    CriareditaragregacaoComponent,
    EditarItemComponent,
    EncomendarItemComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
