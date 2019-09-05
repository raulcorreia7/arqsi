import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EncomendarItemComponent } from './encomendar-item.component';
import { MatSelectModule, MatOptionModule, MatDialogModule, MatFormFieldModule, MAT_DIALOG_DATA, MatDialogRef, MatInputModule } from '@angular/material';
import { ToastrModule } from 'ngx-toastr';
import { HttpModule } from '@angular/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('EncomendarItemComponent', () => {
  let component: EncomendarItemComponent;
  let fixture: ComponentFixture<EncomendarItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        FormsModule,
        ReactiveFormsModule,
        MatDialogModule,
        MatFormFieldModule,
        MatSelectModule,
        MatOptionModule,
        ToastrModule.forRoot(),
        HttpModule,
        BrowserAnimationsModule,
        MatInputModule
      ],
      providers: [{ provide: MAT_DIALOG_DATA, useValue: {} }, { provide: MatDialogRef, useValue: {} }],
      declarations: [ EncomendarItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EncomendarItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
