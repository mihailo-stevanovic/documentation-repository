import * as React from 'react';

import AppBar from '@material-ui/core/AppBar';
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';

import MenuIcon from '@material-ui/icons/Menu';
import SearchIcon from '@material-ui/icons/Search';

import SearchBar from 'material-ui-search-bar';

import { createStyles, Theme, WithStyles, withStyles } from '@material-ui/core/styles';
import { Captions } from '../../translations/en-US';
import { DocRepoApi } from '../../util/DocRepoApi';

import { CustomError } from '../../models/customerror';
import { Document } from '../../models/document';

import { IDocGridSettings } from '../DocumentGrid/DocumentGrid';

const styles = (theme: Theme) => createStyles({
    appBar: {        
        // position: 'fixed',                
      },
    block: {
        display: 'block',       
      },              
    hidden: {
      display: 'none',
    },
    menuToolbar:{
        justifyContent: 'space-between',        
    },
    navHeadingText: {
      color: theme.palette.secondary.main,
      fontWeight: 'bold',
      },
    navIconHide: {
      [theme.breakpoints.up('md')]: {
        display: 'none',
      },
    },    
    toolbar: theme.mixins.toolbar,
  
});

interface IMainToolbarProps extends WithStyles<typeof styles>{    
    onDocumentsChanged(documents: Document[], docGridSettings: IDocGridSettings):void;
    onDrawerToggle(): void;
    onError(errors: CustomError):void;
    
  }

interface IMainToolbarState{
    searchBarOpen: boolean;
    searchTerms: string;
}

class MainToolbar extends React.Component<IMainToolbarProps, IMainToolbarState>{
    constructor(props: IMainToolbarProps){
        super(props);
        this.state = {
            searchBarOpen: false,
            searchTerms: ""
        };
        this.handleOpenSearch = this.handleOpenSearch.bind(this);
        this.handleOnRequestSearch = this.handleOnRequestSearch.bind(this);
        this.handleSearchOnChange = this.handleSearchOnChange.bind(this);
    }

    public render(){
        const { classes } = this.props;
        return(
            <AppBar className={classes.appBar}>
                <Toolbar className={classes.menuToolbar}>
                    <IconButton
                    color="inherit"
                    aria-label="open drawer"
                    onClick={this.props.onDrawerToggle}              
                    >
                        <MenuIcon />
                    </IconButton>
                    <Typography variant="title" color="inherit" >
                        {Captions.appBar.title}
                    </Typography>
                    <div>
                        <IconButton color="inherit" onClick={this.handleOpenSearch}>
                            <SearchIcon />
                        </IconButton>
                        <Button color="inherit">{Captions.appBar.help}</Button>
                    </div>
                </Toolbar>
                <div className={ this.state.searchBarOpen ? classes.block : classes.hidden }>
                    <SearchBar value={this.state.searchTerms} onChange={this.handleSearchOnChange} onRequestSearch={this.handleOnRequestSearch} />
                </div>
            </AppBar>
        );
    }
   

    private handleOpenSearch(event: React.MouseEvent<HTMLElement>){
        this.setState({searchBarOpen: !this.state.searchBarOpen});    
      }
      private handleSearchOnChange(value: string){
        this.setState({searchTerms: value});
      }
      private handleOnRequestSearch(){
        this.getDocumentsFromSearch(this.state.searchTerms);
      }
     
    
      private async getDocumentsFromSearch(searchTerms: string){
        const result: Document[] | CustomError = await DocRepoApi.getDocumentsFromSearch(searchTerms, true, 300);
        const docGridSettings: IDocGridSettings = {
          displayAuthors: true,
          displayCatalogs: true,
          displayDocType: true,
          displayIsFitForClients: true,
          displayPagination: true,
          displayProduct: true,
          displayShortDescription: true,
          displayVersion: true,        
          filterable: true,
          numberOfResults: 20,
          title: searchTerms  
        };
        if(result instanceof CustomError){
          this.props.onError(result);     
        }
        else{
          this.props.onDocumentsChanged(result, docGridSettings); 
        }
    }
}

export default withStyles(styles, { withTheme: true })(MainToolbar);