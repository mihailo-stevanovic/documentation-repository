export interface IUpdate{
    id: number,
    timestamp: string,
    latestTopicsUpdated: string,
    isPublished: boolean,
    documentId: number,
    rowVersion: string
}

export class Update{
    public id: number;
    public timestamp: Date;
    public latestTopicsUpdated: string;
    public documentId: number;

    constructor(up: IUpdate){
        this.id = up.id;
        this.timestamp = new Date(up.timestamp);
        this.latestTopicsUpdated = up.latestTopicsUpdated;
        this.documentId = up.documentId;
    }

}