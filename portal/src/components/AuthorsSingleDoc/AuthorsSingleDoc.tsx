import * as React from 'react';

import {Author} from '../../models/author';

import { createStyles, Theme, WithStyles, withStyles } from '@material-ui/core/styles';

import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { Captions } from '../../translations/en-US';




const styles = (theme: Theme) => createStyles({    
    root: {
        padding: 1,
    }
});

interface IAuthorsSingleDocProps extends WithStyles<typeof styles>{
    authors: Author[];
    documentTitle: string;
}

class AuthorsSingleDoc extends React.Component<IAuthorsSingleDocProps>{
    constructor(props: IAuthorsSingleDocProps){
        super(props);
    }

    public render(){
        const { classes } = this.props;

        if(!this.props.authors){
            return;
        }
        return(
            <Grid className={classes.root}>
                <Typography variant="title">
                    {Captions.singleDocument.authors}
                </Typography>
                <Grid direction="row" container={true}>
                {this.props.authors.map(a =>                 
                    <Button disabled={a.isFormerAuthor} key={a.id} color="secondary" href={"mailto:" + a.email + "?Subject=" + encodeURI(this.props.documentTitle)} title={Captions.authors.sendFeedbackTo + " " + a.fullName}>             {a.fullName}
                    </Button>                  
                )
                }   
                </Grid>           
            </Grid>
        );        
    }
}

export default withStyles(styles, { withTheme: true })(AuthorsSingleDoc);