import * as React from 'react';

import MenuItem from '@material-ui/core/MenuItem';

import { CustomError } from '../../models/customerror';
import { Document } from '../../models/document';
import { Version } from '../../models/version';
import { DocRepoApi } from '../../util/DocRepoApi';
import { IDocGridSettings } from '../DocumentGrid/DocumentGrid';



interface IVersionMenuItemProps{
    version: Version;
    onDocumentsChanged(documents: Document[], docGridSettings: IDocGridSettings):void; 
}

export class VersionMenuItem extends React.Component<IVersionMenuItemProps>{
    constructor(props: IVersionMenuItemProps){
        super(props);
        this.handleClick = this.handleClick.bind(this);
    }
    public render(){
        return <MenuItem onClick={this.handleClick}>{this.props.version.release}</MenuItem>
    }
    private handleClick(event: React.MouseEvent<HTMLElement>){
        this.getDocsByVersionId(this.props.version.id);
    }
    private async getDocsByVersionId(versionId: number){
        const versionDocsResult: Document[] | CustomError= await DocRepoApi.getDocumentsByProductVersion(versionId, 100);
        if(versionDocsResult instanceof CustomError){
            return;
        }
        const docGridSettings: IDocGridSettings = {
            displayAuthors: true,
            displayCatalogs: true,
            displayDocType: true,
            displayIsFitForClients: true,
            displayPagination: true,
            displayProduct: false,
            displayShortDescription: true,
            displayVersion: false,        
            filterable: true,
            numberOfResults: 20,
            title: this.props.version.product + " " + this.props.version.release  
        };
        this.props.onDocumentsChanged(versionDocsResult, docGridSettings);
    }   
}