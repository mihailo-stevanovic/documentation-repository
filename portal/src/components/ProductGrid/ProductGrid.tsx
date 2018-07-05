import * as React from 'react';

import Grid from '@material-ui/core/Grid';
import GridList from '@material-ui/core/GridList';
import Typography from '@material-ui/core/Typography';

import { createStyles, Theme, WithStyles, withStyles } from '@material-ui/core/styles';

import { CustomError } from '../../models/customerror';
import { Document } from '../../models/document';
import { Product } from '../../models/product';
import { IDocGridSettings } from '../DocumentGrid/DocumentGrid';
import ProductCard  from '../ProductCard/ProductCard';

import { Captions } from '../../translations/en-US';



const styles = (theme: Theme) => createStyles({
    productGrid: {
        justifyContent: 'center',
        margin: 10,
    },
    root: {
        marginTop: '1rem',
    }
});

interface IProductProps extends WithStyles<typeof styles>{
    products: Product[];
    onDocumentsChanged(documents: Document[], docGridSettings: IDocGridSettings):void; 
    onError(error: CustomError):void;
}

class ProductGrid extends React.Component<IProductProps, {}>{
    constructor(props: IProductProps){
        super(props);        
    }    
    public render(){        
        const { classes } = this.props;
        return (            
                
                    <Grid className={classes.root}>
                        <Typography color="primary" variant="headline" component="h2">{Captions.productGrid.heading}</Typography>
                        <GridList cols={3} className={classes.productGrid}>
                        { this.props.products.map(p => <ProductCard key={p.id} product={p} onDocumentsChanged={this.props.onDocumentsChanged} onError={this.props.onError} />)}                    
                        </GridList>
                    </Grid>
                
        );
    }
}

export default withStyles(styles, { withTheme: true })(ProductGrid);