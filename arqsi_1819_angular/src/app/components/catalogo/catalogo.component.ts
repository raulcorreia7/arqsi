import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { CatalogoService } from '../../services/catalogo.service';
import { Produto } from '../../models/produto';
import { ItemdeprodutoComponent } from '../itemdeproduto/itemdeproduto.component'
import { AddEditProdutoComponent } from '../addeditproduto/addeditproduto.component';
import { CriareditaragregacaoComponent } from '../criareditaragregacao/criareditaragregacao.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-catalogo',
  templateUrl: './catalogo.component.html',
  styleUrls: ['./catalogo.component.css']
})
export class CatalogoComponent implements OnInit {

  produtos: Produto[];
  produto: Produto;
  isLoading: boolean = true;

  constructor(private catalogoService: CatalogoService, private toastrService: ToastrService, public dialog: MatDialog) { }

  ngOnInit() {
    this.getProdutos();
  }

  getProdutos(): void {
    this.catalogoService.getProdutos().subscribe(produtos => {
      this.produtos = produtos;
      this.isLoading = false;
    },
      err => {
        console.log(err);
        this.isLoading = false;
      });
  }

  public definirItemProduto(event, row) {
    event.stopPropagation();
    this.dialog.open(ItemdeprodutoComponent, {
      data: row,
      width: '400px',
    });
  }

  public editarProduto(event, element) {
    event.stopPropagation();
    const dialogref = this.dialog.open(AddEditProdutoComponent, {
      data: element,
      width: '40%'
    });
    dialogref.afterClosed().subscribe(result => {
      if (result == "add") {
        this.produtos = [];
        this.isLoading = true;
        this.getProdutos();
      }
    });
  }

  adicionarProduto(): void {
    const dialogref = this.dialog.open(AddEditProdutoComponent, {
      width: '40%'
    });
    dialogref.afterClosed().subscribe(result => {
      if (result == "add") {
        this.produtos = [];
        this.isLoading = true;
        this.getProdutos();
      }
    });
  }

  deleteProduto(event, id: Number) {
    event.stopPropagation();
    this.catalogoService.deleteProduto(id).subscribe(() => {
      this.produtos = [];
      this.isLoading = true;
      this.getProdutos();
      this.toastrService.success("Produto apagado com sucesso");
    }, () => {
      this.toastrService.error("Produto não apagado");
    });
  }

  criarEditarAgregacoes(event, produto: Produto) {
    event.stopPropagation();
    this.dialog.open(CriareditaragregacaoComponent, {
      data: produto,
      width: '40%'
    });
  }

  displayedColumns: string[] = ['acoes', 'nome', 'categoria'];

}
