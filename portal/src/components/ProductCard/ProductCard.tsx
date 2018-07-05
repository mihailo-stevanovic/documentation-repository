import * as React from'react';

import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import GridListTile from '@material-ui/core/GridListTile';
import Menu from '@material-ui/core/Menu';
import MenuItem from '@material-ui/core/MenuItem';
import Typography from '@material-ui/core/Typography';

import { createStyles, Theme, WithStyles, withStyles } from '@material-ui/core/styles';

import { IDocGridSettings } from '../DocumentGrid/DocumentGrid';
import { VersionMenuItem } from '../VersionMenuItem/VersionMenuItem';

import { CustomError } from '../../models/customerror';
import { Document } from '../../models/document';
import { Product } from "../../models/product";
import { Version } from '../../models/version';
import { DocRepoApi } from '../../util/DocRepoApi';

import { Captions } from '../../translations/en-US';


const styles = (theme: Theme) => createStyles({
    cardAvatar: {
        marginLeft: 'auto',
        marginRight: 'auto',
    },
    root: {
        padding: 5,
    }
});

interface IProductCardProps extends WithStyles<typeof styles>{
    product: Product;
    onDocumentsChanged(documents: Document[], docGridSettings: IDocGridSettings):void; 
    onError(error: CustomError):void;
}

interface IProductCardState{
    versions: Version[]
    anchorEl: any
}

class ProductCard extends React.Component<IProductCardProps, IProductCardState>{
    constructor(props:IProductCardProps){
        super(props);
        this.state = {
            anchorEl: null,
            versions: []            
        }
        this.handleDocumentsOnClick = this.handleDocumentsOnClick.bind(this);        
    }
    
    public async componentDidMount(){
        const versionRes: Version[] | CustomError =  await DocRepoApi.getProductVersions(this.props.product.id);
        if(versionRes instanceof CustomError){
            this.props.onError(versionRes);
            return;
        }
        this.setState({versions: versionRes});
    }

    public handleDocumentsOnClick(event: React.MouseEvent<HTMLElement>): any{
        this.getDocsByProductId(this.props.product.id);
    }    
    
    public render(){
        const { anchorEl } = this.state;
        const { classes } = this.props;
        return (
            <GridListTile className={classes.root} key={this.props.product.id}>
                <Card>                    
                    <CardContent>
                        <Avatar className={classes.cardAvatar} style={{backgroundColor: "red"}}> {this.props.product.shortName} </Avatar>
                        <Typography color="primary" variant="subheading">{this.props.product.fullName}</Typography>
                        <Typography color="textSecondary">{this.props.product.alias}</Typography>                                                                                        
                    </CardContent>                                      
                    <CardActions>
                        <Button onClick={this.handleMenuButtonClick}>{Captions.productGrid.viewDocs}</Button>
                        <Menu anchorEl={anchorEl} open={Boolean(anchorEl)} onClose={this.handleMenuClose}>
                            <MenuItem onClick={this.handleDocumentsOnClick}>{Captions.productGrid.allVersions}</MenuItem>
                            {this.state.versions.map(v => 
                            <VersionMenuItem key={v.id} version={v} onDocumentsChanged={this.props.onDocumentsChanged} />
                            )}
                        </Menu>                    
                    </CardActions>
                </Card>
            </GridListTile> 
        );
    }

    private handleMenuButtonClick = (event:React.MouseEvent<HTMLElement>) => {
        this.setState({ anchorEl: event.currentTarget });
      };
    
    private handleMenuClose = () => {
        this.setState({ anchorEl: null });
      };
    private async getDocsByProductId(productId: number){
        const productDocsResult: Document[] | CustomError = await DocRepoApi.getDocumentsByProduct(productId, 150);
        if(productDocsResult instanceof CustomError){
            this.props.onError(productDocsResult);
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
            title: this.props.product.fullName + " - All Versions"  
        };
        this.props.onDocumentsChanged(productDocsResult, docGridSettings); 
    }    
    
}

export default withStyles(styles, { withTheme: true })(ProductCard);