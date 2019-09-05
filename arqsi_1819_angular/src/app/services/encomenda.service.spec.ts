import { TestBed, inject } from '@angular/core/testing';

import { EncomendaService } from './encomenda.service';
import { HttpModule, XHRBackend, Response, ResponseOptions } from '@angular/http';
import { MockBackend } from '@angular/http/testing';

describe('EncomendaService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpModule],
      providers: [EncomendaService, { provide: XHRBackend, useClass: MockBackend }]
    })
  });

  it('should be created', () => {
    const service: EncomendaService = TestBed.get(EncomendaService);
    expect(service).toBeTruthy();
  });

  it('getItens()', (inject([EncomendaService, XHRBackend], (encomendaService, mockBackend) => {

    const mockResponse = [
      {
        "id": 17,
        "Nome": "Gaveta 1",
        "Produto_id": 4,
        "Em_construcao": 1,
        "Material": "Madeira",
        "Acabamento": "Natural",
        "Dimensao": {
          "Altura": 10,
          "Largura": 5,
          "Comprimento": 20
        },
        "Partes": []
      },
      {
        "id": 21,
        "Nome": "Armário Xpto",
        "Produto_id": 1,
        "Em_construcao": 1,
        "Material": "Madeira",
        "Acabamento": "Envernizado",
        "Dimensao": {
          "Altura": 200,
          "Largura": 20,
          "Comprimento": 40
        },
        "Partes": [
          {
            "id": 23,
            "Nome": "Modulo Gavetas 1",
            "Produto_id": 3,
            "Em_construcao": 1,
            "Material": "Madeira",
            "Acabamento": "Natural",
            "Dimensao": {
              "Altura": 97,
              "Largura": 17,
              "Comprimento": 24
            },
            "Partes": []
          }
        ]
      },
      {
        "id": 23,
        "Nome": "Modulo Gavetas 1",
        "Produto_id": 3,
        "Em_construcao": 1,
        "Material": "Madeira",
        "Acabamento": "Natural",
        "Dimensao": {
          "Altura": 97,
          "Largura": 17,
          "Comprimento": 24
        },
        "Partes": []
      }
    ];

    mockBackend.connections.subscribe((connection) => {
      connection.mockRespond(new Response(new ResponseOptions({ body: mockResponse })))
    });

    encomendaService.getItensProduto().subscribe((itens) => {
      expect(itens.length).toBe(3);
    })
  })));
});
