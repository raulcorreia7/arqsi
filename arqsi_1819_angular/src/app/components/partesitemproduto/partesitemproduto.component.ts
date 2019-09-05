import { Component, OnInit, Inject } from '@angular/core';
import { EncomendaService } from 'src/app/services/encomenda.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ItemProduto } from 'src/app/models/itemproduto';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-partesitemproduto',
  templateUrl: './partesitemproduto.component.html',
  styleUrls: ['./partesitemproduto.component.css']
})
export class PartesItemProdutoComponent implements OnInit {

  public item:ItemProduto;
  public itemid: Number;
  public partes: Array<ItemProduto> = [];
  public listaitens: Array<ItemProduto> = [];

  constructor(private encomendaService: EncomendaService, private toastrService: ToastrService, private dialogRef: MatDialogRef<PartesItemProdutoComponent>,
    @Inject(MAT_DIALOG_DATA) i: ItemProduto) {
    this.itemid = i.id;
    this.partes = i.Partes;
    this.encomendaService.getAllAgregados(this.itemid).subscribe((e) => {
      this.listaitens = e;
    }, () => {
      this.toastrService.error("Sem itens disponiveis");
    });
  }

  ngOnInit() {
  }

  adicionarParte(item: ItemProduto) {
    if (item) {
      this.encomendaService.adicionarParte(this.itemid, item.id).subscribe(() => {
        this.toastrService.success("Parte adicionada");
        this.partes.push(item);
      }, (error) => {
        console.log(error);
        this.toastrService.error("Parte não adicionada");
      });
    }
  }

  removerParte(item: ItemProduto) {
    this.encomendaService.removerParte(this.itemid, item.id).subscribe(() => {
      this.toastrService.success("Parte removida");
      let index = this.partes.indexOf(item);
      this.partes.splice(index, 1);
    }, () => {
      this.toastrService.error("Parte não removida");
    });
  }

  close() {
    this.dialogRef.close();
  }

}
