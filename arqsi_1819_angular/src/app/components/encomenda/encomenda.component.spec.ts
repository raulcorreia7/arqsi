import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EncomendaComponent } from './encomenda.component';
import { MatDividerModule, MatProgressSpinnerModule, MatTableModule, MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material';
import { ToastrModule } from 'ngx-toastr';
import { HttpModule } from '@angular/http';

describe('EncomendaComponent', () => {
  let component: EncomendaComponent;
  let fixture: ComponentFixture<EncomendaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        MatDividerModule,
        MatProgressSpinnerModule,
        MatTableModule,
        ToastrModule.forRoot(),
        HttpModule,
        MatDialogModule,
        
      ],
      providers: [{ provide: MAT_DIALOG_DATA, useValue: {} }, { provide: MatDialogRef, useValue: {} }],
      declarations: [ EncomendaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EncomendaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
