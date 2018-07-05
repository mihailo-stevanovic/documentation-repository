export interface IVersion {
    id: number;
    product: string;
    release: string;
    endOfSupport: string;
    isSupported: boolean;
}

export class Version {
    public id: number;
    public product: string;
    public release: string;
    public endOfSupport: Date;
    public isSupported: boolean;

    constructor(ver: IVersion){
        this.id = ver.id;
        this.product = ver.product;
        this.release = ver.release;
        this.endOfSupport = new Date(ver.endOfSupport);
        this.isSupported = ver.isSupported;        
    }
}