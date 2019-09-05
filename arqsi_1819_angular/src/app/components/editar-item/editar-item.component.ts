import { Component, OnInit, Inject } from '@angular/core';
import { EncomendaService } from 'src/app/services/encomenda.service';
import { ToastrService } from 'ngx-toastr';
import { ItemProduto, Dimensao } from 'src/app/models/itemproduto';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { CatalogoService } from 'src/app/services/catalogo.service';
import { FormGroup, FormControl } from '@angular/forms';
import { Produto } from 'src/app/models/produto';

@Component({
  selector: 'app-editar-item',
  templateUrl: './editar-item.component.html',
  styleUrls: ['./editar-item.component.css']
})
export class EditarItemComponent implements OnInit {

  form: FormGroup;
  nome: string = '';
  item_id: Number;
  produto: Produto;
  tipoComprimento: string;
  tipoAltura: string;
  tipoLargura: string;
  comprimento: Array<Number> = [];
  altura: Array<Number> = [];
  largura: Array<Number> = [];
  materialacabamento: Array<string> = [];

  constructor(private encomendaService: EncomendaService, private catalogoService: CatalogoService, private toastrService: ToastrService, public dialogRef: MatDialogRef<EditarItemComponent>,
    @Inject(MAT_DIALOG_DATA) item: ItemProduto) {
    this.catalogoService.getProduto(item.Produto_id).subscribe(e => {
      this.produto = e;
      this.nome = e.Nome;
      this.item_id = item.id;
      this.tipoComprimento = this.produto.Dimensao.TipoComprimento;
      this.tipoAltura = this.produto.Dimensao.TipoAltura;
      this.tipoLargura = this.produto.Dimensao.TipoLargura;
      this.comprimento = this.produto.Dimensao.Comprimento;
      this.altura = this.produto.Dimensao.Altura;
      this.largura = this.produto.Dimensao.Largura;
      this.produto.MaterialAcabamento.forEach(element => {
        this.materialacabamento.push(element.Material.Nome + ' ' + element.Acabamento.Nome);
      });
    });
    if (item.Dimensao != null) {
      this.form = new FormGroup({
        comprimento: new FormControl(item.Dimensao.Comprimento),
        altura: new FormControl(item.Dimensao.Altura),
        largura: new FormControl(item.Dimensao.Largura),
        materialacabamento: new FormControl(item.Material + ' ' + item.Acabamento),
      });
    } else {
      this.form = new FormGroup({
        comprimento: new FormControl(''),
        altura: new FormControl(''),
        largura: new FormControl(''),
        materialacabamento: new FormControl(''),
      });
    }
  }

  ngOnInit() {
  }

  onClose(): void {
    this.dialogRef.close();
  }

  save(): void {
    let materialacabamento = this.form.value.materialacabamento.split(" ");
    let dimensao = new Dimensao(this.form.value.comprimento, this.form.value.largura, this.form.value.altura);
    this.encomendaService.editItemProduto(this.item_id, new ItemProduto(this.produto.id, materialacabamento[0], materialacabamento[1], dimensao, this.item_id)).subscribe(() => {
      this.toastrService.success('Item Editado');
      this.dialogRef.close("add");
    }, error => {
      this.toastrService.error('Item não editado');
      this.dialogRef.close(error);
    });;
  }

}
