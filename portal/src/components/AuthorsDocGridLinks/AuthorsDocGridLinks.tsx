import * as React from 'react';

import {Author} from '../../models/author';

import { createStyles, Theme, WithStyles, withStyles } from '@material-ui/core/styles';

import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import { Captions } from '../../translations/en-US';


const styles = (theme: Theme) => createStyles({    
    root: {
        padding: 1,
    }
});

interface IAuthorsDocGridLinksProps extends WithStyles<typeof styles>{
    authors: Author[];
    documentTitle: string;
}

class AuthorsDocGridLinks extends React.Component<IAuthorsDocGridLinksProps>{
    constructor(props: IAuthorsDocGridLinksProps){
        super(props);
    }

    public render(){
        const { classes } = this.props;

        if(!this.props.authors){
            return <span>{Captions.authors.notAvailable}</span>;
        }
        return(
            <Grid className={classes.root} container={true} direction="column" alignContent="center">
                {this.props.authors.map(a =>
                <Button key={a.id} disabled={a.isFormerAuthor} color="secondary" href={"mailto:" + a.email + "?Subject=" + encodeURI(this.props.documentTitle)} title={Captions.authors.sendFeedbackTo + " " + a.fullName}>{a.fullName}</Button>                
                )}
            </Grid>
        );        
    }
}

export default withStyles(styles, { withTheme: true })(AuthorsDocGridLinks);