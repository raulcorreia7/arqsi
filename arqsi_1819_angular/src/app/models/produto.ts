export class Produto {
    constructor(
        public id: Number,
        public Nome: string,
        public Categoria: Categoria,
        public Dimensao: Dimensao,
        public MaterialAcabamento: Array<MaterialAcabamento>) { }
}

export class Categoria {
    public id: Number;
    public Nome: string;
}

export class Dimensao {
    constructor(
        public TipoComprimento: string,
        public TipoAltura: string,
        public TipoLargura: string,
        public Comprimento: Array<Number>,
        public Altura: Array<Number>,
        public Largura: Array<Number>) { }
}

export class MaterialAcabamento {
    public Material: Material;
    public Acabamento: Acabamento;
}

export class Material {
    public id: Number;
    public Nome: string;
}

export class Acabamento {
    public id: Number;
    public Nome: string;
}