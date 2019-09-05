import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditarItemComponent } from './editar-item.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MatSelectModule, MatFormFieldModule, MatExpansionModule, MatInputModule, MatSliderModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { HttpModule } from '@angular/http';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('EditarItemComponent', () => {
  let component: EditarItemComponent;
  let fixture: ComponentFixture<EditarItemComponent>;

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
        MatInputModule,
        MatSliderModule
      ],
      providers: [{ provide: MAT_DIALOG_DATA, useValue: {} }, { provide: MatDialogRef, useValue: {} }],
      declarations: [ EditarItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditarItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
