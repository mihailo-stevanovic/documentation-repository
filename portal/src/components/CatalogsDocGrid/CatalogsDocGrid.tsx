import * as React from 'react';

import { Catalog } from '../../models/catalog';

import { createStyles, Theme, WithStyles, withStyles } from '@material-ui/core/styles';

import Grid from '@material-ui/core/Grid';
import { Captions } from '../../translations/en-US';

const styles = (theme: Theme) => createStyles({    
    catalog: {
        marginBottom: 5,
        marginTop: 5,  
    },
    root: {
        padding: 1,
    }
});

interface ICatalogsDocGridProps extends WithStyles<typeof styles>{
    catalogs: Catalog[];
    isFitForClients: boolean;
}

class CatalogsDocGrid extends React.Component<ICatalogsDocGridProps>{
    constructor(props: ICatalogsDocGridProps){
        super(props);
    }

    public render(){
        const { classes } = this.props;

        if(!this.props.catalogs || !this.props.isFitForClients){
            return <span>{Captions.catalogs.notAvailable}</span>;
        }
        return(
            <Grid className={classes.root} container={true} direction="column" alignContent="center">
                {this.props.catalogs.map(cat =>
                <span key={cat.id} className={classes.catalog}>{cat.name}</span>                
                )}
            </Grid>
        );        
    }
}

export default withStyles(styles, { withTheme: true })(CatalogsDocGrid);