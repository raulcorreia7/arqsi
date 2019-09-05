import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material";
import { CatalogoService } from '../../services/catalogo.service';
import { Produto } from 'src/app/models/produto';
import { Agregacao, Restricao, RestricaoDTO, Ocupacao } from 'src/app/models/Agregacao';
import { RestricaoEnum, RestricaoMap } from 'src/app/models/restricao';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-criareditaragregacao',
  templateUrl: './criareditaragregacao.component.html',
  styleUrls: ['./criareditaragregacao.component.css']
})
export class CriareditaragregacaoComponent implements OnInit {

  public map = RestricaoMap;
  
  public produtoBase: Produto;
  public produtosPartes: Produto[];
  public agregacoes: Agregacao[];
  public produto: Produto;
  public agregacao: Agregacao = null;
  public restricao: RestricaoEnum;
  public larguraMin: Number = 0.0;
  public larguraMax: Number = 0.0;
  public alturaMin: Number = 0.0;
  public alturaMax: Number = 0.0;
  public comprimentoMin: Number = 0.0;
  public comprimentoMax: Number = 0.0;

  constructor(private catalogoService: CatalogoService, private toastrService: ToastrService,
    public dialogRef: MatDialogRef<CriareditaragregacaoComponent>,
    @Inject(MAT_DIALOG_DATA) produtoBase: Produto) {
    this.produtoBase = produtoBase;
    this.catalogoService.getProdutosParte(this.produtoBase)
      .subscribe((result) => {
        this.produtosPartes = result;
      });
    this.catalogoService.getAgregacoesProdutoBase(this.produtoBase)
      .subscribe((result) => {
        this.agregacoes = result;
      });
  }

  adicionarRestricao(r: RestricaoEnum) {
    let dto;
    var other: any = r;
    if (other.key == 5) {
      let ocupacao = new Ocupacao(this.larguraMin, this.larguraMax, this.alturaMin, this.alturaMax, this.comprimentoMin, this.comprimentoMax);
      dto = new RestricaoDTO(other.key, ocupacao);
    } else {
      dto = new RestricaoDTO(other.key);
    }
    this.catalogoService.adicionarRestricao(this.agregacao.id, dto).subscribe(() => {
      this.toastrService.success("Restricao adicionada");
      this.agregacao.Restricoes = [];
      this.catalogoService.getRestricoes(this.agregacao.id).subscribe((e) => {
        this.agregacao.Restricoes = e.Restricoes;
      });
    }, () => {
      this.toastrService.error("Restrição não adicionada");
    });
  }

  apagarRestricao(r: Restricao) {
    this.catalogoService.deleteRestricao(this.agregacao.id, r.id).subscribe(() => {
      this.toastrService.success("Restricao apagada");
      let index = this.agregacao.Restricoes.indexOf(r);
      this.agregacao.Restricoes.splice(index, 1);
    }, () => {
      this.toastrService.error("Restrição não apagada");
    });
  }

  apagarAgregacao() {
    this.catalogoService.deleteAgregacao(this.agregacao.id).subscribe(() => {
      this.toastrService.success("Agregação Apagada");
      let index = this.agregacoes.indexOf(this.agregacao);
      this.agregacoes.splice(index, 1);
      this.agregacao = null;
    }, () => {
      this.toastrService.error("Agregação não apagada");
    })
  }

  adicionarAgregacao(p: Produto) {
    this.catalogoService.addAgregacao(this.produtoBase.id, p.id).subscribe(() => {
      this.toastrService.success("Agregação adicionada");
      this.agregacoes = [];
      this.catalogoService.getAgregacoesProdutoBase(this.produtoBase)
        .subscribe((result) => {
          this.agregacoes = result;
        });
    }, () => {
      this.toastrService.error("Agregação não adicionada");
    });
  }

  ngOnInit() {
  }

  cancel() {
    this.dialogRef.close();
  }
}
