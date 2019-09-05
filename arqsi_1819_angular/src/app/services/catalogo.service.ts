import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Produto, Categoria, MaterialAcabamento } from '../models/produto';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from './../../environments/environment'
import { Agregacao, RestricaoDTO, AgregacaoDTO } from '../models/Agregacao';

@Injectable({
  providedIn: 'root'
})
export class CatalogoService {

  constructor(public http: Http) { }

  getProdutos(): Observable<Produto[]> {
    return this.http.get(environment.API.Catalogo + 'Produto').pipe(map((response: Response) => response.json()));
  }

  getProduto(id: Number): Observable<Produto> {
    return this.http.get(environment.API.Catalogo + 'Produto/' + id).pipe(map((response: Response) => response.json()));
  }

  addProduto(produto: Produto): Observable<Number> {
    let body = JSON.stringify(produto);
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    console.log(body);
    return this.http.post(environment.API.Catalogo + 'Produto', body, { headers: headers }).pipe(map((response: Response) => response.status));
  }

  editProduto(id: Number, produto: Produto): Observable<Number> {
    let body = JSON.stringify(produto);
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');
    return this.http.put(environment.API.Catalogo + 'Produto/' + id, body, { headers: headers }).pipe(map((response: Response) => response.status));
  }

  deleteProduto(id: Number): Observable<Number> {
    return this.http.delete(environment.API.Catalogo + 'Produto/' + id).pipe(map((response: Response) => response.status));
  }

  getCategorias(): Observable<Categoria[]> {
    return this.http.get(environment.API.Catalogo + 'Categoria').pipe(map((response: Response) => response.json()));
  }

  getMaterialAcabamento(): Observable<MaterialAcabamento[]> {
    return this.http.get(environment.API.Catalogo + 'MaterialAcabamento').pipe(map((response: Response) => response.json()));
  }

  getProdutosParte(produto: Produto): Observable<Produto[]> {
    return this.http.get(environment.API.Catalogo + 'Produto/' + produto.id + '/NaoParte').pipe(map((response: Response) => response.json()));
  }

  getAgregacoesProdutoBase(produto: Produto): Observable<Agregacao[]> {
    return this.http.get(environment.API.Catalogo + 'Agregacao/' + produto.id + '/PBase').pipe(map((response: Response) => response.json()));
  }

  adicionarRestricao(idAgregacao: Number, r: RestricaoDTO): Observable<Number> {
    console.log(r);
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    let body = JSON.stringify(r);
    return this.http.put(environment.API.Catalogo + 'Agregacao/' + idAgregacao + '/Restricao', body, { headers: headers }).pipe(map((response: Response) => response.json()));
  }

  deleteRestricao(idAgregacao: Number, idRestricao: Number) {
    return this.http.delete(environment.API.Catalogo + 'Agregacao/' + idAgregacao + '/Restricao/' + idRestricao).pipe(map((response: Response) => response.status));
  }

  deleteAgregacao(idAgregacao: Number) {
    return this.http.delete(environment.API.Catalogo + 'Agregacao/' + idAgregacao).pipe(map((response: Response) => response.status));
  }

  getRestricoes(idAgregacao: Number): Observable<Agregacao> {
    return this.http.get(environment.API.Catalogo + 'Agregacao/' + idAgregacao).pipe(map((response: Response) => response.json()));
  }

  addAgregacao(idPBase: Number, idPParte: Number): Observable<Number> {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');

    let dto = new AgregacaoDTO(idPBase, idPParte);
    let body = JSON.stringify(dto);
    return this.http.post(environment.API.Catalogo + 'Agregacao/', body, { headers: headers }).pipe(map((response: Response) => response.json()));
  }
}

