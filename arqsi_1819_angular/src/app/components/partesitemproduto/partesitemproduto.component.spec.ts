import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PartesItemProdutoComponent } from './partesitemproduto.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule, MatOptionModule, MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { HttpModule } from '@angular/http';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('PartesItemProdutoComponent', () => {
  let component: PartesItemProdutoComponent;
  let fixture: ComponentFixture<PartesItemProdutoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        FormsModule,
        ReactiveFormsModule,
        MatSelectModule,
        MatOptionModule,
        MatDialogModule,
        HttpModule,
        ToastrModule.forRoot(),
        BrowserAnimationsModule,
      ],
      providers: [{ provide: MAT_DIALOG_DATA, useValue: {} }, { provide: MatDialogRef, useValue: {} }],
      declarations: [PartesItemProdutoComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PartesItemProdutoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
