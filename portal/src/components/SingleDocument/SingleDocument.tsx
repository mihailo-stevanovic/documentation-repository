import * as React from 'react';


import Button from '@material-ui/core/Button';
import Chip from '@material-ui/core/Chip';
import CircularProgress from '@material-ui/core/CircularProgress';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
// import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Grid  from '@material-ui/core/Grid';
import IconButton from '@material-ui/core/IconButton';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Typography from '@material-ui/core/Typography';

import InfoIcon from '@material-ui/icons/Info';

import { createStyles, Theme, WithStyles, withStyles } from '@material-ui/core/styles';

import withWidth, { WithWidth } from '@material-ui/core/withWidth';

import { CustomError } from '../../models/customerror';
import { Document } from '../../models/document';
import { Update } from '../../models/update';
import { DocRepoApi } from '../../util/DocRepoApi';

import { Captions } from '../../translations/en-US';
import AuthorsSingleDoc from '../AuthorsSingleDoc/AuthorsSingleDoc';




const styles = (theme: Theme) => createStyles({
    chip: {
        margin: theme.spacing.unit,
      },
    chipGrid: {
        marginBottom: theme.spacing.unit         
      },
    
});


interface ISingleDocumentProps extends WithStyles<typeof styles>, WithWidth{
    document: Document;  
    onError(error: CustomError):void;     
}

interface ISingleDocumentState{
    dataLoaded: boolean;
    open: boolean;
    updates: Update[];
    
}

export class SingleDocument extends React.Component<ISingleDocumentProps, ISingleDocumentState> {
    constructor(props: ISingleDocumentProps){
        super(props);
        this.state = {
            dataLoaded: false,
            open: false,
            updates: []
        };
    } 
 
  public handleClickOpen = () => {
        this.setState({ open: true });
      };

  public handleClose = () => {
    this.setState({ open: false });
  };

  public render() {
     const { classes, width} = this.props;

    return (
      <div>
        <IconButton onClick={this.handleClickOpen}><InfoIcon/></IconButton>            
        <Dialog
          fullScreen={width === "sm" || width === "xs"}
          open={this.state.open}
          onClose={this.handleClose}
          aria-labelledby="responsive-dialog-title"
        >
          <DialogTitle id="responsive-dialog-title">{Captions.singleDocument.document + " " + this.props.document.title}</DialogTitle>
          <DialogContent>            
            <Grid container={true} className={classes.chipGrid} justify="flex-start">      
                <Chip className={classes.chip} label={this.props.document.documentType} />
                <Chip className={classes.chip} label={this.props.document.product} />
                <Chip className={classes.chip} label={this.props.document.version} />
                {this.props.document.isFitForClients &&
                <Chip className={classes.chip} label={Captions.singleDocument.fitForClients} />
                }                                    
            </Grid>
            
              <Typography variant="title">{Captions.singleDocument.description}</Typography>
              <Typography variant="body2" paragraph={true}>
                {this.props.document.shortDescription}
              </Typography>   
              <Grid container={true} direction="column">
                <AuthorsSingleDoc authors={this.props.document.authors} documentTitle={this.props.document.title} />
                {this.props.document.isFitForClients &&
                <Grid>
                    <Typography variant="title">{Captions.singleDocument.catalogs}</Typography>
                    <Grid>
                    {this.props.document.clientCatalogs.map(cat => <Chip className={classes.chip} key={cat.id} label={cat.name} />)}
                    </Grid>
                </Grid>            
                }
            </Grid>     
              <Typography variant="title">{Captions.singleDocument.updates.title}</Typography>   
              
              {this.state.updates &&              
              <Table>
                  <TableHead>
                      <TableRow>
                          <TableCell>{Captions.singleDocument.updates.date}</TableCell>
                          <TableCell>{Captions.singleDocument.updates.description}</TableCell>
                      </TableRow>
                  </TableHead>
                  <TableBody>
                      {this.state.updates.map(up =>                          
                              <TableRow key={up.id}>
                                  <TableCell>{up.timestamp.toDateString()}</TableCell>
                                  <TableCell>{up.latestTopicsUpdated}</TableCell>
                              </TableRow>                      
                      )}
                  </TableBody>
              </Table>
            }         
            {!this.state.updates && 
             <CircularProgress size={50} />
            }  
          </DialogContent>
          <DialogActions>    
            {this.props.document.htmlLink &&
            <Button href={this.props.document.htmlLink} target="_blank">            {Captions.singleDocument.links.html}
            </Button>
            }
            {this.props.document.pdfLink &&
            <Button href={this.props.document.pdfLink} target="_blank">             {Captions.singleDocument.links.pdf}
            </Button>
            }
            {this.props.document.wordLink &&
            <Button href={this.props.document.wordLink} target="_blank">            {Captions.singleDocument.links.word}
            </Button>
            }
            {this.props.document.otherLink &&
            <Button href={this.props.document.otherLink} target="_blank">             {this.props.document.otherLink.split('.').pop()}
            </Button>
            }       
            <Button onClick={this.handleClose} color="secondary">
                {Captions.singleDocument.links.close}
            </Button>
          </DialogActions>
        </Dialog>        
      </div>
    );
  }
  public async componentDidUpdate(){
      if(!this.state.open || this.state.dataLoaded){
          return;
      }      
      const docUpdatesRes: Update[]|CustomError = await DocRepoApi.getDocumentUpdates(this.props.document.id);
      if(docUpdatesRes instanceof CustomError){
        this.props.onError(docUpdatesRes);
          return;
      }
      this.setState({
          dataLoaded: true,
          updates: docUpdatesRes
        });
  }
}

export default withWidth()(withStyles(styles, { withTheme: true })(SingleDocument));