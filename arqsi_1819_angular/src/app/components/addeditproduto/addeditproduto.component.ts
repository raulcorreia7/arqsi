import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormControl, FormArray } from '@angular/forms';
import { Categoria, MaterialAcabamento, Dimensao, Produto } from 'src/app/models/produto';
import { CatalogoService } from 'src/app/services/catalogo.service';
import { ToastrService } from 'ngx-toastr';

enum TiposDimensao {
  Discreto = "discreto",
  Continuo = "continuo",
}

@Component({
  selector: 'app-addeditproduto',
  templateUrl: './addeditproduto.component.html',
  styleUrls: ['./addeditproduto.component.css']
})
export class AddEditProdutoComponent implements OnInit {

  form: FormGroup;
  formStatus: boolean = true;
  title: string = 'Adicionar';

  id: Number;
  nome: string;
  categoria: Categoria;
  categorias: Array<Categoria>;
  matacab: MaterialAcabamento;
  materialacabamento: Array<MaterialAcabamento> = [];
  materialacabamentodefinido: Array<MaterialAcabamento> = [];
  tiposdimensao: Array<string> = [];
  tipoComprimento: string = '';
  tipoAltura: string = '';
  tipoLargura: string = '';
  comprimento: Array<Number> = [];
  altura: Array<Number> = [];
  largura: Array<Number> = [];

  constructor(private catalogoService: CatalogoService, private toastrService: ToastrService, public dialogRef: MatDialogRef<AddEditProdutoComponent>,
    @Inject(MAT_DIALOG_DATA) p: Produto) {
    catalogoService.getCategorias().subscribe(element => {
      this.categorias = element;
    });
    catalogoService.getMaterialAcabamento().subscribe(element => {
      element.forEach(e => {
        this.materialacabamento.push(e);
      })
    });
    this.tiposdimensao = Object.values(TiposDimensao);

    if (p != null && p.Dimensao != null) {
      this.form = new FormGroup({
        nome: new FormControl(p.Nome),
        categoria: new FormControl(p.Categoria),
        materialacabamento: new FormArray([]),
        materialacabamentodefinido: new FormArray([]),
        tipoComprimento: new FormControl(p.Dimensao.TipoComprimento),
        tipoAltura: new FormControl(p.Dimensao.TipoAltura),
        tipoLargura: new FormControl(p.Dimensao.TipoLargura),
        comprimento: new FormArray([]),
        altura: new FormArray([]),
        largura: new FormArray([]),
      });
      this.id = p.id;
      this.title = 'Editar';
      this.form.controls['categoria'].setValue(p.Categoria);
      //this.categoria = p.Categoria;
      this.materialacabamentodefinido = p.MaterialAcabamento;
      this.altura = p.Dimensao.Altura;
      this.comprimento = p.Dimensao.Comprimento;
      this.largura = p.Dimensao.Largura;
      this.formStatus = false;
    } else {
      this.form = new FormGroup({
        nome: new FormControl(''),
        categoria: new FormControl(),
        materialacabamento: new FormArray([]),
        materialacabamentodefinido: new FormArray([]),
        tipoComprimento: new FormControl(''),
        tipoAltura: new FormControl(''),
        tipoLargura: new FormControl(''),
        comprimento: new FormArray([]),
        altura: new FormArray([]),
        largura: new FormArray([]),
      });
    }
  }

  ngOnInit() {
  }

  addMaterialAcabamento() {
    if (this.matacab) {
      this.materialacabamentodefinido.push(this.matacab);
      this.form.value.materialacabamentodefinido.push(this.matacab);
    }
  }

  removerMaterialAcabamento(ma: MaterialAcabamento) {
    let index = this.materialacabamentodefinido.indexOf(ma);
    this.form.value.materialacabamentodefinido.splice(index, 1);
    this.materialacabamentodefinido.splice(index, 1);
  }

  addComprimento(dimensao: Number) {
    if (dimensao) {
      this.comprimento.push(dimensao);
      this.form.value.comprimento.push(dimensao);
    }
  }

  removerComprimento(dimensao: Number) {
    let index = this.comprimento.indexOf(dimensao);
    this.form.value.comprimento.splice(index, 1);
    this.comprimento.splice(index, 1);
  }

  addAltura(dimensao: Number) {
    if (dimensao) {
      this.altura.push(dimensao);
      this.form.value.altura.push(dimensao);
    }
  }

  removerAltura(dimensao: Number) {
    let index = this.altura.indexOf(dimensao);
    this.form.value.altura.splice(index, 1);
    this.altura.splice(index, 1);
  }

  addLargura(dimensao: Number) {
    if (dimensao) {
      this.largura.push(dimensao);
      this.form.value.largura.push(dimensao);
    }
  }

  removerLargura(dimensao: Number) {
    let index = this.altura.indexOf(dimensao);
    this.form.value.largura.splice(index, 1);
    this.largura.splice(index, 1);
  }

  onClose(): void {
    this.dialogRef.close();
  }

  save(): void {
    if (this.formStatus) {
      let dimensao = new Dimensao(this.form.value.tipoComprimento, this.form.value.tipoAltura, this.form.value.tipoLargura, this.form.value.comprimento, this.form.value.altura, this.form.value.largura);
      this.catalogoService.addProduto(new Produto(null, this.form.value.nome, this.form.value.categoria, dimensao, this.form.value.materialacabamentodefinido)).subscribe(() => {
        this.toastrService.success('Produto Adicionado');
        this.dialogRef.close("add");
      }, error => {
        this.toastrService.error('Produto não adicionado');
        this.dialogRef.close(error);
      });
    } else {
      let dimensao = new Dimensao(this.form.value.tipoComprimento, this.form.value.tipoAltura, this.form.value.tipoLargura, this.form.value.comprimento, this.form.value.altura, this.form.value.largura);
      this.catalogoService.editProduto(this.id, new Produto(this.id, this.form.value.nome, this.form.value.categoria, dimensao, this.form.value.materialacabamentodefinido)).subscribe(data => {
        this.toastrService.success('Produto Editado');
        this.dialogRef.close("add");
      }, error => {
        this.toastrService.error('Produto não editado');
        this.dialogRef.close(error);
      });
    }
  }

  compareFn(x: Categoria, y: Categoria): boolean {
    return x && y ? x.id === y.id : x === y;
  }
}
