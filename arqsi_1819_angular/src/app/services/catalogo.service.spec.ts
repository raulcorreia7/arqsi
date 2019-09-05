import { TestBed, inject } from '@angular/core/testing';

import { CatalogoService } from './catalogo.service';
import { HttpModule, XHRBackend, Response, ResponseOptions } from '@angular/http';
import { MockBackend } from '@angular/http/testing';

describe('CatalogoService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpModule
    ],
    providers: [CatalogoService, { provide: XHRBackend, useClass: MockBackend }]
  }));

  it('should be created', () => {
    const service: CatalogoService = TestBed.get(CatalogoService);
    expect(service).toBeTruthy();
  });

  it('getCategorias()', (inject([CatalogoService, XHRBackend], (catalogoService, mockBackend) => {
    const mockResponse = [
      {
        "id": 1,
        "Nome": "Armário",
        "superCategoria": null,
        "SubCategorias": [
          {
            "id": 7,
            "nome": "Armário de Sala"
          },
          {
            "id": 8,
            "nome": "Armário de Jardim"
          }
        ]
      },
      {
        "id": 2,
        "Nome": "Módulo Gavetas",
        "superCategoria": null,
        "SubCategorias": []
      },
      {
        "id": 3,
        "Nome": "Gavetas",
        "superCategoria": null,
        "SubCategorias": []
      },
      {
        "id": 4,
        "Nome": "Portas",
        "superCategoria": null,
        "SubCategorias": []
      },
      {
        "id": 5,
        "Nome": "Cabide",
        "superCategoria": null,
        "SubCategorias": []
      },
      {
        "id": 6,
        "Nome": "Espelhos",
        "superCategoria": null,
        "SubCategorias": []
      },
      {
        "id": 7,
        "Nome": "Armário de Sala",
        "superCategoria": 1,
        "SubCategorias": []
      },
      {
        "id": 8,
        "Nome": "Armário de Jardim",
        "superCategoria": 1,
        "SubCategorias": []
      }
    ];

    mockBackend.connections.subscribe((connection) => {
      connection.mockRespond(new Response(new ResponseOptions({ body: mockResponse })))
    });

    catalogoService.getCategorias().subscribe((categorias) => {
      expect(categorias.length).toBe(8);
    });
  })))
});
