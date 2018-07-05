export interface IDocType{
    id: number;
    fullName: string;
    shortName: string;
    documentCategory: number;
}

export enum DocumentCategory{
    FunctionalDocumentation = 0,
    TechnicalDocumentation = 1,
    ReleaseNotes = 2,
    Other = 3
}

export class DocType{
    public id: number;
    public fullName: string;
    public shortName: string;
    public documentCategoryId: number;
    public documentCategoryName: string;

    constructor(type: IDocType){
        this.id = type.id;
        this.fullName = type.fullName;
        this.shortName = type.shortName;
        this.documentCategoryId = type.documentCategory;
        if(type.documentCategory === DocumentCategory.FunctionalDocumentation){
            this.documentCategoryName = "Functional Documentation";
        } else if(type.documentCategory === DocumentCategory.TechnicalDocumentation){
            this.documentCategoryName = "Technical Documentation";
        } else if(type.documentCategory === DocumentCategory.ReleaseNotes){
            this.documentCategoryName = "Release Notes";
        } else {
            this.documentCategoryName = "Other";
        }
    }
}