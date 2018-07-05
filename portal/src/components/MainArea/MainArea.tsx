import * as React from 'react';

import Paper from '@material-ui/core/Paper';
import { createStyles, Theme, WithStyles, withStyles } from '@material-ui/core/styles';

import { CustomError } from '../../models/customerror';
import { Document } from '../../models/document';
import { DocType } from '../../models/documenttype';
import { Product } from '../../models/product';

import  DocumentGrid, { IDocGridSettings } from '../DocumentGrid/DocumentGrid';
import  ProductGrid  from '../ProductGrid/ProductGrid';




const styles = (theme: Theme) => createStyles({
  root: theme.mixins.gutters({
    marginLeft: "auto",
    marginRight: "auto",  
    marginTop: theme.spacing.unit * 3,
    maxWidth: "95%", 
    [theme.breakpoints.up("lg")]: {
      maxWidth: "85%",
    },
    paddingBottom: 16,
    paddingTop: 16,         
  }),  
});

interface IMainAreaProps extends WithStyles<typeof styles> {
  docGridSettings: IDocGridSettings;
  documents: Document[];
  docTypes: DocType[];
  products: Product[];
  isHomepage: boolean; 
  onDocumentsChanged(documents: Document[], docGridSettings: IDocGridSettings):void;  
  onError(errors: CustomError):void;
}

class MainArea extends React.Component<IMainAreaProps>{
  constructor(props: IMainAreaProps){
    super(props);
  }
  public render(){
    const { classes } = this.props;
    return (
      <Paper className={classes.root} elevation={4}>           
          {this.props.isHomepage && this.props.products.length > 0 &&  
          <ProductGrid products={this.props.products} onDocumentsChanged={this.props.onDocumentsChanged} onError={this.props.onError} /> 
          }
          {this.props.documents.length > 0 &&          
          <DocumentGrid documents={this.props.documents} settings={this.props.docGridSettings} onError={this.props.onError} products={this.props.products} docTypes={this.props.docTypes} />
          }          
      </Paper>      
    );
  }
}

export default withStyles(styles, { withTheme: true })(MainArea);