export interface IProduct{
    id: number;
    fullName: string;
    shortName: string;
    alias: string;
}

export class Product {
    public id: number;
    public fullName: string;
    public shortName: string;
    public alias: string;

    constructor(prod: IProduct){
        this.id = prod.id;
        this.fullName = prod.fullName;
        this.shortName = prod.shortName;
        this.alias = prod.alias;
    }
}