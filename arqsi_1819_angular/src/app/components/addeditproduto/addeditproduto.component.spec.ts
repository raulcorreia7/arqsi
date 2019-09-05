import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditProdutoComponent } from './addeditproduto.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MatSelectModule, MatFormFieldModule, MatExpansionModule, MatDialogRef, MAT_DIALOG_DATA, MatInputModule } from '@angular/material';
import { HttpModule } from '@angular/http';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('AddEditProdutoComponent', () => {
  let component: AddEditProdutoComponent;
  let fixture: ComponentFixture<AddEditProdutoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        FormsModule,
        ReactiveFormsModule,
        MatDialogModule,
        MatSelectModule,
        MatFormFieldModule,
        MatExpansionModule,
        HttpModule,
        ToastrModule.forRoot(),
        BrowserAnimationsModule,
        MatInputModule
      ],
      providers: [{ provide: MAT_DIALOG_DATA, useValue: {} }, { provide: MatDialogRef, useValue: {} }],
      declarations: [AddEditProdutoComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditProdutoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
