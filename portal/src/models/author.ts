export class Author {
    public id: number;
    public fullName: string;    
    public email: string;
    public alias: string;  
    public isFormerAuthor: boolean; 
        
    constructor(aut: IAuthor){
        this.id = aut.id;
        this.fullName = aut.firstName + " " + aut.lastName;        
        this.email = aut.email;        
        this.alias = aut.alias;      
        this.isFormerAuthor = aut.isFormerAuthor;  
    }    
}

export interface IAuthor {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    alias: string;
    isFormerAuthor: boolean;
    aitName: string;   
    
}