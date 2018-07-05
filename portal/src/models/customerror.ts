import * as uniqid from 'uniqid';

export interface ICustomError {
    id: string;
    description: string;
    method: string;
}


export class CustomError {
    public id: string;
    public description: string;
    public method: string;
    public error: any;

    constructor(description: string, method: string, error?: any){
        this.description = description;        
        this.method = method;
        this.id = uniqid();
        if(error){
            this.error = error;
        }
    }    
}