import { ItemProduto } from "./itemproduto";

export class Encomenda {
    constructor(
        public id: Number,
        public cliente: string,
        public em_construcao: Number,
        public items: Array<ItemProduto>
    ) { }
}