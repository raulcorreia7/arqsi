import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemdeprodutoComponent } from './itemdeproduto.component';
import { MatDialogModule, MatFormFieldModule, MatSliderModule, MatOptionModule, MatSelectModule, MAT_DIALOG_DATA, MatDialogRef, MatInputModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { ToastrModule } from 'ngx-toastr';

describe('ItemdeprodutoComponent', () => {
  let component: ItemdeprodutoComponent;
  let fixture: ComponentFixture<ItemdeprodutoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        FormsModule,
        MatDialogModule,
        MatFormFieldModule,
        ReactiveFormsModule,
        MatSliderModule,
        MatOptionModule,
        MatSelectModule,
        HttpModule,
        ToastrModule.forRoot(),
        MatInputModule
      ],
      providers: [{ provide: MAT_DIALOG_DATA, useValue: {} }, { provide: MatDialogRef, useValue: {} }],
      declarations: [ItemdeprodutoComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemdeprodutoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  // it('should create', () => {
  //   expect(component).toBeTruthy();
  // });
});
