import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material";
import { FormGroup, FormControl } from "@angular/forms";
import { Produto } from "../../models/produto";
import { EncomendaService } from '../../services/encomenda.service';
import { ItemProduto, Dimensao } from '../../models/itemproduto';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-itemdeproduto',
  templateUrl: './itemdeproduto.component.html',
  styleUrls: ['./itemdeproduto.component.css']
})
export class ItemdeprodutoComponent implements OnInit {

  form: FormGroup;
  id: Number;
  nome: string;
  tipoComprimento: string;
  tipoLargura: string;
  tipoAltura: string;
  comprimento: Array<Number>;
  largura: Array<Number>;
  altura: Array<Number>;
  materialacabamento: Array<string>;

  constructor(private encomendaService: EncomendaService, private toastrService: ToastrService,
    private dialogRef: MatDialogRef<ItemdeprodutoComponent>,
    @Inject(MAT_DIALOG_DATA) p: Produto) {
    if (p != null && p.Dimensao != null) {
      this.id = p.id;
      this.nome = p.Nome;
      this.tipoComprimento = p.Dimensao.TipoComprimento;
      this.tipoAltura = p.Dimensao.TipoAltura;
      this.tipoLargura = p.Dimensao.TipoLargura;
      this.comprimento = p.Dimensao.Comprimento;
      this.largura = p.Dimensao.Largura;
      this.altura = p.Dimensao.Altura;
      this.materialacabamento = [];
      p.MaterialAcabamento.forEach(element => {
        this.materialacabamento.push(element.Material.Nome + ' ' + element.Acabamento.Nome);
      });
    }
    this.form = new FormGroup({
      comprimento: new FormControl(''),
      largura: new FormControl(''),
      altura: new FormControl(''),
      materialacabamento: new FormControl('')
    });
  }

  ngOnInit() {
  }

  save() {
    let produto_id = this.id;
    let materialacabamento = this.form.value.materialacabamento.split(" ");
    let dimensao = new Dimensao(this.form.value.comprimento, this.form.value.largura, this.form.value.altura);
    this.encomendaService.addItemProduto(new ItemProduto(produto_id, materialacabamento[0], materialacabamento[1], dimensao, null, null, this.nome)).subscribe(() => {
      this.toastrService.success('Produto Concretizado');
      this.dialogRef.close();
    }, error => {
      this.toastrService.error('Produto não concretizado');
      this.dialogRef.close(error);
    });
  }

  close() {
    this.dialogRef.close();
  }

}
