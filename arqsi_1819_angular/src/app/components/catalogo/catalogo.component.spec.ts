import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CatalogoComponent } from './catalogo.component';
import { MatDividerModule, MatTableModule, MatProgressSpinnerModule, MatDialogModule, MatDialogRef } from '@angular/material';
import { HttpModule } from '@angular/http';
import { ToastrModule } from 'ngx-toastr';

describe('CatalogoComponent', () => {
  let component: CatalogoComponent;
  let fixture: ComponentFixture<CatalogoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        MatDividerModule,
        MatTableModule,
        MatProgressSpinnerModule,
        HttpModule,
        ToastrModule.forRoot(),
        MatDialogModule
      ],
      providers: [{ provide: MatDialogRef, useValue: {} }],
      declarations: [CatalogoComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CatalogoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
