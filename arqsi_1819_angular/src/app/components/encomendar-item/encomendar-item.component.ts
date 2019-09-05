import { Component, OnInit, Inject } from '@angular/core';
import { EncomendaService } from 'src/app/services/encomenda.service';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ItemProduto } from 'src/app/models/itemproduto';
import { Encomenda } from 'src/app/models/encomenda';

@Component({
  selector: 'app-encomendar-item',
  templateUrl: './encomendar-item.component.html',
  styleUrls: ['./encomendar-item.component.css']
})
export class EncomendarItemComponent implements OnInit {
  encomendas: Array<Encomenda> = [];

  encomenda: Encomenda;
  itemid: Number;

  constructor(private encomendaService: EncomendaService, private toastrService: ToastrService, public dialogRef: MatDialogRef<EncomendarItemComponent>,
    @Inject(MAT_DIALOG_DATA) item: ItemProduto) {
    this.itemid = item.id;
    this.encomendaService.getEncomendasNaoFinalizadas().subscribe((e) => {
      this.encomendas = e;
    }, () => {
      this.toastrService.error("Encomendas nao disponiveis");
    });
  }

  ngOnInit() {
  }

  adicionarItemEncomenda() {
    if (this.encomenda) {
      this.encomendaService.addItemEncomenda(this.encomenda.id, this.itemid).subscribe(() => {
        this.toastrService.success("Item adicionado");
        this.dialogRef.close();
      }, () => {
        this.toastrService.error("Item não adicionado");
      });
    }
  }

  close() {
    this.dialogRef.close();
  }

}
