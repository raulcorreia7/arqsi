import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { ItemProduto } from '../models/itemproduto';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Encomenda } from '../models/encomenda';

@Injectable({
  providedIn: 'root'
})
export class EncomendaService {

  constructor(public http: Http) { }

  addItemProduto(itemproduto: ItemProduto) {
    let body = JSON.stringify(itemproduto);
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');

    return this.http.post(environment.API.Encomendas + 'ItemDeProduto/', body, { headers: headers }).pipe(map((response: Response) => response.status));
  }

  editItemProduto(id: Number, itemproduto: ItemProduto) {
    let body = JSON.stringify(itemproduto);
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');

    return this.http.put(environment.API.Encomendas + 'ItemDeProduto/' + id, body, { headers: headers }).pipe(map((response: Response) => response.status));;
  }

  getItensProduto(): Observable<ItemProduto[]> {
    return this.http.get(environment.API.Encomendas + 'ItemDeProduto').pipe(map((response: Response) => response.json()));
  }

  getNaoParte(id: Number): Observable<ItemProduto[]> {
    return this.http.get(environment.API.Encomendas + 'ItemDeProduto/' + id + '/NaoParte').pipe(map((response: Response) => response.json()));
  }

  deleteItem(id: Number) {
    return this.http.delete(environment.API.Encomendas + 'ItemdeProduto/' + id).pipe(map((response: Response) => response.status));
  }

  getAllAgregados(id: Number): Observable<ItemProduto[]> {
    return this.http.get(environment.API.Encomendas + 'ItemDeProduto/' + id + '/ItemsAgregados').pipe(map((response: Response) => response.json()));
  }

  adicionarParte(idBase: Number, idParte: Number) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http.put(environment.API.Encomendas + 'ItemdeProduto/' + idBase + '/AdicionarParte/' + idParte, null, { headers: headers }).pipe(map((response: Response) => response.status));
  }

  removerParte(idBase: Number, idParte: Number) {
    return this.http.put(environment.API.Encomendas + 'ItemdeProduto/' + idBase + '/RemoverParte/' + idParte, null).pipe(map((response: Response) => response.status));
  }

  getEncomendasNaoFinalizadas(): Observable<Encomenda[]> {
    return this.http.get(environment.API.Encomendas + 'Encomenda/NaoFinalizadas').pipe(map((response: Response) => response.json()));
  }

  addItemEncomenda(idEncomenda: Number, idItem: Number) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http.put(environment.API.Encomendas + 'Encomenda/' + idEncomenda + '/AdicionarItem/' + idItem, null, { headers: headers }).pipe(map((response: Response) => response.status));
  }

  deleteItemEncomenda(idEncomenda: Number, idItem: Number) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http.put(environment.API.Encomendas + 'Encomenda/' + idEncomenda + '/RemoverItem/' + idItem, null, { headers: headers }).pipe(map((response: Response) => response.status));
  }

  finalizarEncomenda(idEncomenda: Number) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http.put(environment.API.Encomendas + 'Encomenda/' + idEncomenda + '/Finalizar', null, { headers: headers }).pipe(map((response: Response) => response.status));
  }

  criarEncomenda(cliente: string) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    let dto = '{ "Cliente" : "' + cliente + '" }';
    return this.http.post(environment.API.Encomendas + 'Encomenda', dto, { headers: headers }).pipe(map((response: Response) => response.status));
  }
}