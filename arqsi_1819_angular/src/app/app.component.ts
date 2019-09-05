import { Component } from '@angular/core';
import { Encomenda } from './models/encomenda';
import { EncomendaService } from './services/encomenda.service';
import { ItemProduto } from './models/itemproduto';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Closify';

  encomendas: Array<Encomenda> = [];
  encomenda: Encomenda;
  public _opened: boolean = false;

  constructor(private encomendaService: EncomendaService, private toastrService: ToastrService) {
    this.encomendaService.getEncomendasNaoFinalizadas().subscribe((e) => {
      this.encomendas = e;
    });
  }

  removerItemEncomenda(item: Number) {
    this.encomendaService.deleteItemEncomenda(this.encomenda.id, item).subscribe(() => {
      this.toastrService.success("Encomenda > Item removido");
      this.reset();
    }, () => {
      this.toastrService.error("Encomenda > Item não removido");
    });
  }

  finalizarEncomenda() {
    this.encomendaService.finalizarEncomenda(this.encomenda.id).subscribe(() => {
      this.toastrService.success("Encomenda finalizada");
      this.reset();
    }, () => {
      this.toastrService.error("Encomenda não finalizada");
    });
  }

  adicionarEncomenda(cliente: string) {
    this.encomendaService.criarEncomenda(cliente).subscribe(() => {
      this.toastrService.success("Encomenda criada");
      this.reset();
    }, () => {
      this.toastrService.error("Encomenda não criada");
    });
  }

  public reset() {
    this.encomendas = [];
    this.encomenda = null;
    this.encomendaService.getEncomendasNaoFinalizadas().subscribe((e) => {
      this.encomendas = e;
    });
  }


  public _toggleSidebar() {
    this._opened = !this._opened;
    this.encomendas = [];
    this.encomenda = null;
    this.encomendaService.getEncomendasNaoFinalizadas().subscribe((e) => {
      this.encomendas = e;
    });
  }
}
