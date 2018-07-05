import * as React from 'react';

import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';

import { CustomError } from '../../models/customerror';
import { Document } from '../../models/document';
import { DocType } from '../../models/documenttype';
import { DocRepoApi } from '../../util/DocRepoApi';
import { IDocGridSettings } from '../DocumentGrid/DocumentGrid';

import { Captions } from '../../translations/en-US';

interface IDocTypeListItemProps{
    docType: DocType;
    onDocumentsChanged(documents: Document[], docGridSettings: IDocGridSettings):void; 
    onError(errors: CustomError):void;
    onMenuButtonClick():void;
}

export class DocTypeListItem extends React.Component<IDocTypeListItemProps>{
    constructor(props: IDocTypeListItemProps){
        super(props);
        this.handleOnClick = this.handleOnClick.bind(this);
    }
    public render(){
        return (
            <ListItem button={true} onClick={this.handleOnClick}>
                <ListItemText primary={this.props.docType.fullName} />
            </ListItem> 
        );
    }

    private handleOnClick(event: React.MouseEvent<HTMLElement>){
        this.getDocumentsByDocumentType(this.props.docType.id);
        this.props.onMenuButtonClick();
    }

    private async getDocumentsByDocumentType(docTypeId: number){
        const documentsResult: Document[] | CustomError = await DocRepoApi.getDocumentsByDocType(docTypeId, 100);
        if(documentsResult instanceof CustomError){
            this.props.onError(documentsResult);
            return;
        }
        const docGridSettings: IDocGridSettings = {
            displayAuthors: true,
            displayCatalogs: true,
            displayDocType: false,
            displayIsFitForClients: true,
            displayPagination: true,
            displayProduct: true,
            displayShortDescription: true,
            displayVersion: true,        
            filterable: true,
            numberOfResults: 20,
            title: Captions.docGrid.headings.docType + this.props.docType.fullName + " (" + this.props.docType.documentCategoryName + ")"
        };
        this.props.onDocumentsChanged(documentsResult, docGridSettings);        
    }
}