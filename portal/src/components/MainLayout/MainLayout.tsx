import * as React from 'react';

import CircularProgress from '@material-ui/core/CircularProgress';
import Collapse from '@material-ui/core/Collapse';
import Divider from '@material-ui/core/Divider';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import { createStyles, Theme, WithStyles, withStyles } from '@material-ui/core/styles';
import SwipeableDrawer from '@material-ui/core/SwipeableDrawer';

// import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import HomeIcon from '@material-ui/icons/Home';
import LocalOfferIcon from '@material-ui/icons/LocalOffer';
import StyleIcon from '@material-ui/icons/Style';

import { CustomError } from '../../models/customerror';
import { Document } from '../../models/document';
import { DocType } from '../../models/documenttype';
import { Product } from '../../models/product';

import { Captions } from '../../translations/en-US';

import { DocTypeListItem } from '../DocTypeListItem/DocTypeListItem';
import { IDocGridSettings } from '../DocumentGrid/DocumentGrid';
import MainArea from '../MainArea/MainArea';
import MainToolbar from '../MainToolbar/MainToolbar';
import { ProductListItem } from '../ProductListItem/ProductListItem';







const drawerWidth = 260;

const styles = (theme: Theme) => createStyles({    
 content: {
      backgroundColor: theme.palette.background.default,
      flexGrow: 1,    
      marginLeft: 'auto',      
      padding: theme.spacing.unit * 3,            
    },
    drawerPaper: {      
      width: drawerWidth,      
    },      
    progress: {
      marginTop: 50,
    },
    root: {
      display: 'flex',
      flexGrow: 1,      
      overflow: 'hidden',    
      position: 'relative',    
      width: '100%',
      zIndex: 1,
    },
    toolbar: theme.mixins.toolbar,
  
});

interface IMainLayoutProps extends WithStyles<typeof styles>{
    docGridSettings: IDocGridSettings;  
    docTypes: DocType[];
    products: Product[];  
    documents: Document[];
    hasDataLoaded: boolean;
    isHomepage: boolean;
    onDocumentsChanged(documents: Document[], docGridSettings: IDocGridSettings):void;
    onError(errors: CustomError):void;    
    
  }

interface IMainLayoutState{
    docTypeCollapseOpen: boolean;
    mobileOpen: boolean;
    productCollapseOpen: boolean;
    searchBarOpen: boolean;
    searchTerms: string;
}

class MainLayout extends React.Component<IMainLayoutProps, IMainLayoutState> {
  constructor(props: IMainLayoutProps){
    super(props);
    this.state = {
      docTypeCollapseOpen: false,
      mobileOpen: false,
      productCollapseOpen: false,
      searchBarOpen: false,
      searchTerms: "",
    };    
    this.handleDrawerToggle = this.handleDrawerToggle.bind(this);
    this.handleHomeClick = this.handleHomeClick.bind(this);
    this.handleDocTypeCollapse = this.handleDocTypeCollapse.bind(this);
    this.handleProductsCollapse = this.handleProductsCollapse.bind(this);
    this.handleDrawerMenuClick = this.handleDrawerMenuClick.bind(this);
  }
   

  public handleDrawerToggle() {
    this.setState(state => ({ mobileOpen: !state.mobileOpen }));
  };

  public render() {
    const { classes } = this.props;

    const drawer = (
      <div>
        <List>
          <ListItem button={true} onClick={this.handleHomeClick}>
            <ListItemIcon>
              <HomeIcon />
            </ListItemIcon>
            <ListItemText primary="Home" />
          </ListItem>
        </List>         
        <Divider />  
        <List>
            <ListItem button={true} onClick={this.handleDocTypeCollapse}>
              <ListItemIcon>
                <StyleIcon />
              </ListItemIcon>
              <ListItemText primary={Captions.menu.docTypesHeading} primaryTypographyProps={{color: 'secondary', variant: 'subheading'}} />                
            </ListItem>
            <Collapse in={this.state.docTypeCollapseOpen}>
            {this.props.docTypes.map(docType =>
            <DocTypeListItem key={docType.id} docType={docType} onDocumentsChanged={this.props.onDocumentsChanged} onError={this.props.onError} onMenuButtonClick={this.handleDrawerMenuClick} />   
            )}
            </Collapse>            
        </List>        
        <Divider />                
        <List>
            <ListItem button={true} onClick={this.handleProductsCollapse}>
              <ListItemIcon>
                <LocalOfferIcon />
              </ListItemIcon>
              <ListItemText primary={Captions.menu.productHeading} primaryTypographyProps={{color: 'secondary', variant: 'subheading'}} />                
            </ListItem>
            <Collapse in={this.state.productCollapseOpen}>
            {this.props.products.map(prod =>
            <ProductListItem key={prod.id} product={prod} onDocumentsChanged={this.props.onDocumentsChanged} onError={this.props.onError} onMenuButtonClick={this.handleDrawerMenuClick} />          
            )}            
            </Collapse>      
        </List>
      </div>
    );

    return (
      <div className={classes.root}>      
        <MainToolbar onDocumentsChanged={this.props.onDocumentsChanged} onDrawerToggle={this.handleDrawerToggle} onError={this.props.onError} />
        <SwipeableDrawer
            variant="temporary"
            anchor="left"
            open={this.state.mobileOpen}
            onClose={this.handleDrawerToggle}
            classes={{
              paper: classes.drawerPaper,
            }}
            ModalProps={{
              keepMounted: true, // Better open performance on mobile.              
            }}            
            onOpen={this.handleDrawerToggle}
          >
            {drawer}
        </SwipeableDrawer>              
        
        <main className={classes.content}>
          <div className={classes.toolbar} />
          {this.props.hasDataLoaded &&
          <MainArea documents={this.props.documents} products={this.props.products} onDocumentsChanged={this.props.onDocumentsChanged} docGridSettings={this.props.docGridSettings} isHomepage={this.props.isHomepage} onError={this.props.onError} docTypes={this.props.docTypes} />
          }
          {!this.props.hasDataLoaded &&
          <CircularProgress size={50} />
          }
        </main>                 
      </div>
    );
  }
  private handleHomeClick(event: React.MouseEvent<HTMLElement>){
    location.reload();
  }
  private handleDocTypeCollapse(event: React.MouseEvent<HTMLElement>){
    this.setState({docTypeCollapseOpen: !this.state.docTypeCollapseOpen});
  }
  
  private handleProductsCollapse(event: React.MouseEvent<HTMLElement>){
    this.setState({productCollapseOpen: !this.state.productCollapseOpen});
  }

  private handleDrawerMenuClick(){
    this.setState({mobileOpen: false});
  }
}

export default withStyles(styles, { withTheme: true })(MainLayout);