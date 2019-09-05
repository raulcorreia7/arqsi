import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CriareditaragregacaoComponent } from './criareditaragregacao.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MatSelectModule, MatFormFieldModule, MatExpansionModule, MatInputModule, MAT_DIALOG_DATA, MatDialogRef, MatTabsModule, MatSliderModule } from '@angular/material';
import { HttpModule } from '@angular/http';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('CriareditaragregacaoComponent', () => {
  let component: CriareditaragregacaoComponent;
  let fixture: ComponentFixture<CriareditaragregacaoComponent>;

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
        MatTabsModule,
        MatSliderModule
      ],
      providers: [{ provide: MAT_DIALOG_DATA, useValue: {} }, { provide: MatDialogRef, useValue: {} }],
      declarations: [ CriareditaragregacaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CriareditaragregacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
