import { Produto } from './produto';

export class Agregacao {
    constructor(
        public id: Number,
        public ProdutoBase: Produto,
        public ProdutoParte: Produto,
        public Restricoes: Array<Restricao>
    ) { }
}

export class Restricao {
    constructor(
        public id: Number,
        public Tipo: Number,
        public Nome: string
    ) { }
}

export class RestricaoDTO {
    constructor(public Restricao: Number,
        public Ocupacao?: Ocupacao) { }
}

export class AgregacaoDTO {
    constructor(
        public Base: Number,
        public Parte: Number
    ) { }
}

export class Ocupacao {
    constructor(public LarguraMin: Number,
        public LarguraMax: Number,
        public AlturaMin: Number,
        public AlturaMax: Number,
        public ComprimentoMin: Number,
        public ComprimentoMax: Number) { }
}