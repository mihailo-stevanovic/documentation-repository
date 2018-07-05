import * as React from 'react';

import { Document } from '../../models/document';

import { createStyles, Theme, WithStyles, withStyles } from '@material-ui/core/styles';

import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import { Captions } from '../../translations/en-US';


const styles = (theme: Theme) => createStyles({    
    root: {
        padding: 3,
        width: "100%",
    },
    title: {        
        whiteSpace: "normal",
    }
});

interface IDocumentTitleWithLinksProps extends WithStyles<typeof styles>{
    document: Document;    
}

class DocumentTitleWithLinks extends React.Component<IDocumentTitleWithLinksProps>{
    constructor(props: IDocumentTitleWithLinksProps){
        super(props);
    }

    public render(){
        const { classes } = this.props;
        
        return(
            <Grid className={classes.root} container={true} direction="column" alignContent="center">
                <span className={classes.title}>{this.props.document.title}</span>
                <Grid container={true} >
                    {this.props.document.htmlLink &&
                        <Button href={this.props.document.htmlLink} target="_blank">{Captions.docGrid.links.html}</Button>
                    }
                    {this.props.document.pdfLink &&
                        <Button href={this.props.document.pdfLink} target="_blank">{Captions.docGrid.links.pdf}</Button>
                    }
                    {this.props.document.wordLink &&
                        <Button href={this.props.document.wordLink} target="_blank">{Captions.docGrid.links.word}</Button>
                    }
                    {this.props.document.otherLink &&
                        <Button href={this.props.document.otherLink} target="_blank">{this.props.document.otherLink.split('.').pop()}</Button>
                    }
                </Grid>
            </Grid>
        );        
    }
}

export default withStyles(styles, { withTheme: true })(DocumentTitleWithLinks);