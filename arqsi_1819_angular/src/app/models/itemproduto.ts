export class ItemProduto {

    constructor(
        public Produto_id: Number,
        public Material: string,
        public Acabamento: string,
        public Dimensao: Dimensao,
        public id?: Number,
        public Partes?: Array<ItemProduto>,
        public Nome?: string) { }
}

export class Dimensao {
    constructor(
        public Comprimento: Number,
        public Largura: Number,
        public Altura: Number) { }
}
