import * as React from 'react';

import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';

import { CustomError } from '../../models/customerror';
import { Document } from '../../models/document';
import { Product } from '../../models/product';
import { DocRepoApi } from '../../util/DocRepoApi';
import { IDocGridSettings } from '../DocumentGrid/DocumentGrid';

import { Captions } from '../../translations/en-US';

interface IProductListItemProps{
    product: Product;    
    onDocumentsChanged(documents: Document[], docGridSettings: IDocGridSettings):void;
    onError(errors: CustomError):void;
    onMenuButtonClick():void;
}

export class ProductListItem extends React.Component<IProductListItemProps>{
    constructor(props: IProductListItemProps){
        super(props);
        this.handleOnClick = this.handleOnClick.bind(this);
    }
    public render(){
        return (
            <ListItem button={true} onClick={this.handleOnClick}>
                <ListItemText primary={this.props.product.fullName} />
            </ListItem> 
        );
    }

    private handleOnClick(event: React.MouseEvent<HTMLElement>){
        this.getDocumentsByDocumentType(this.props.product.id);
        this.props.onMenuButtonClick();
    }

    private async getDocumentsByDocumentType(productId: number){
        const documentsResult: Document[] | CustomError = await DocRepoApi.getDocumentsByProduct(productId, 200);
        if(documentsResult instanceof CustomError){
            this.props.onError(documentsResult);
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
            displayVersion: true,        
            filterable: true,
            numberOfResults: 20,
            title: this.props.product.fullName + Captions.docGrid.headings.allVersions 
        };
        this.props.onDocumentsChanged(documentsResult, docGridSettings);
    }
}