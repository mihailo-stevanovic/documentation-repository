export class Catalog {
    public id: number;
    public name: string;

    constructor(cat: ICatalog){
        this.id = cat.id;
        this.name = cat.name;
    }   
}
export interface ICatalog {
    id: number;
    name: string;
    internalId: string;
}