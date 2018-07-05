import { Author, IAuthor } from "./author";
import { Catalog, ICatalog } from "./catalog";

const rootUrl: string = "/"

export class Document{
    
    public id: number;
    public title: string;
    public product: string;
    public version: string;
    public htmlLink: string | null;
    public pdfLink: string | null;
    public wordLink: string | null;
    public otherLink: string | null;
    public isFitForClients: boolean;
    public shortDescription: string;
    public documentType: string;
    public latestUpdate: Date;
    public latestTopicsUpdated: string;
    public authors: Author[];
    public clientCatalogs: Catalog[];

    constructor(doc: IDocument){
        this.id = doc.id;
        this.title = doc.title;
        this.product = doc.product;
        this.version = doc.version;
        this.htmlLink = doc.htmlLink ? rootUrl + doc.htmlLink : null;
        this.pdfLink = doc.pdfLink ? rootUrl + doc.pdfLink : null;
        this.wordLink = doc.wordLink ? rootUrl + doc.wordLink : null;
        this.otherLink = doc.otherLink ? rootUrl + doc.otherLink : null;
        this.isFitForClients = doc.isFitForClients;
        this.shortDescription = doc.shortDescription;
        this.documentType = doc.documentType;
        this.latestUpdate = new Date(doc.latestUpdate);
        this.latestTopicsUpdated = doc.latestTopicsUpdated;
        this.authors = doc.authors.map(a => new Author(a));
        this.clientCatalogs = doc.clientCatalogs.map(c => new Catalog(c));
    }   

}

export interface IDocument{
    
    id: number;
    title: string;
    product: string;
    version: string;
    htmlLink: string;
    pdfLink: string;
    wordLink: string;
    otherLink: string;
    isFitForClients: boolean;
    shortDescription: string;
    documentType: string;
    latestUpdate: Date;
    latestTopicsUpdated: string;
    authors: IAuthor[];
    clientCatalogs: ICatalog[];

}