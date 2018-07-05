import * as React from 'react';

import CloseIcon from '@material-ui/icons/Close';
import ErrorIcon from '@material-ui/icons/Error';

import IconButton from '@material-ui/core/IconButton';
import Snackbar from '@material-ui/core/Snackbar';
import SnackbarContent from '@material-ui/core/SnackbarContent';

import { createStyles, Theme, WithStyles, withStyles } from '@material-ui/core/styles';




const styles = (theme: Theme) => createStyles({        
    content: {
        backgroundColor: theme.palette.error.dark,
    },
    icon: {
        fontSize: 20,
    },
    iconError: {
        fontSize: 20,
        marginRight: theme.spacing.unit,
        opacity: 0.9,  
    },
    message: {
        alignItems: 'center',        
        display: 'flex',        
    },
    root: {
        marginBottom: 10,
    },
    
});

interface IErrorHandlerProps extends WithStyles<typeof styles>{
    errorDescription: string;    
    onErrorClose():void;
}

interface IErrorHandlerState {
    isOpen: boolean;
}

class ErrorHandler extends React.Component<IErrorHandlerProps, IErrorHandlerState>{
    constructor(props: IErrorHandlerProps){
        super(props);
        this.state = {
            isOpen: true
        };
        this.handleCloseClick = this.handleCloseClick.bind(this);
        this.handleSnackbarClose = this.handleSnackbarClose.bind(this);
        this.closeSnackBar = this.closeSnackBar.bind(this);
    }

    public render(){
        const { classes } = this.props;
        return(
            <Snackbar className={classes.root} anchorOrigin={{horizontal: "center", vertical: "bottom"}} open={true} onClose={this.handleSnackbarClose} autoHideDuration={6000} >
                <SnackbarContent className={classes.content} message={
                    <span className={classes.message}>
                        <ErrorIcon className={classes.iconError} />
                        {this.props.errorDescription}
                    </span>
                }
                action={
                    <IconButton onClick={this.handleCloseClick}>
                        <CloseIcon className={classes.icon} />
                    </IconButton>
                } />
            </Snackbar> 
        );
    }
  
    private handleCloseClick(){
        this.closeSnackBar();
    }
    private handleSnackbarClose(event: React.MouseEvent<HTMLElement>, reason: string){
        this.closeSnackBar();
    }

    private closeSnackBar(){                              
        this.props.onErrorClose();
    }
}

export default withStyles(styles, { withTheme: true })(ErrorHandler);