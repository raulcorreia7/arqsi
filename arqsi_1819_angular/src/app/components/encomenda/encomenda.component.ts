import { Component, OnInit } from '@angular/core';
import { ItemProduto } from 'src/app/models/itemproduto';
import { EncomendaService } from 'src/app/services/encomenda.service';
import { MatDialog } from '@angular/material';
import { PartesItemProdutoComponent } from '../partesitemproduto/partesitemproduto.component';
import { ToastrService } from 'ngx-toastr';
import { EditarItemComponent } from '../editar-item/editar-item.component';
import { EncomendarItemComponent } from '../encomendar-item/encomendar-item.component';

@Component({
  selector: 'app-encomenda',
  templateUrl: './encomenda.component.html',
  styleUrls: ['./encomenda.component.css']
})
export class EncomendaComponent implements OnInit {

  itensproduto: ItemProduto[];
  itemproduto: ItemProduto;
  isLoading: boolean = true;

  constructor(private toastrService: ToastrService, private encomendaService: EncomendaService, public dialog: MatDialog) { }

  ngOnInit() {
    this.getItensProdutos();
  }

  getItensProdutos(): void {
    this.encomendaService.getItensProduto().subscribe(itensproduto => {
      this.itensproduto = itensproduto;
      this.isLoading = false;
    },
      err => {
        console.log(err);
        this.isLoading = false;
      });
  }

  public partesItemProduto(event, row) {
    event.stopPropagation();
    this.dialog.open(PartesItemProdutoComponent, {
      data: row,
      width: '400px',
    });
  }

  deleteItemProduto(event, id: Number) {
    event.stopPropagation();
    this.encomendaService.deleteItem(id).subscribe(response => {
      this.itensproduto = [];
      this.isLoading = true;
      this.getItensProdutos();
      this.toastrService.success("Produto apagado com sucesso");
    }, error => {
      this.toastrService.error("Produto não apagado");
    })
  }

  editItemProduto(event, row) {
    event.stopPropagation();
    const dialogref = this.dialog.open(EditarItemComponent, {
      data: row,
      width: '400px',
    });
    dialogref.afterClosed().subscribe(result => {
      if (result == "add") {
        this.itensproduto = [];
        this.isLoading = true;
        this.getItensProdutos();
      }
    });
  }

  encomendarItem(event, row) {
    event.stopPropagation();
    const dialogRef = this.dialog.open(EncomendarItemComponent, {
      data: row,
      width: '300px',
    });
  }

  displayedColumns: string[] = ['acoes', 'nome', 'materialacabamento', 'comprimento', 'largura', 'altura'];


}
