import * as React from 'react';

import { CustomError } from '../../models/customerror';
import { Document } from '../../models/document';
import { DocType } from '../../models/documenttype';
import { Product } from '../../models/product';
import { DocRepoApi } from '../../util/DocRepoApi';
import { IDocGridSettings } from '../DocumentGrid/DocumentGrid';

import ErrorHandler from '../ErrorHandler/ErrorHandler';
import MainLayout from '../MainLayout/MainLayout';

import './App.css';


interface IAppState{
  currentError: CustomError|undefined;
  docGridSettings: IDocGridSettings;
  documents: Document[];
  documentTypes: DocType[];
  errors: CustomError[];
  products: Product[];
  hasDataLoaded: boolean;
  isHomepage: boolean;
}

class App extends React.Component<{},IAppState> {  
  constructor(props: any){
    super(props);
    this.handleDocumentChanges = this.handleDocumentChanges.bind(this);
    this.handleErrors = this.handleErrors.bind(this);
    this.handleErrorClose = this.handleErrorClose.bind(this);
    this.state = {
      currentError: undefined,
      docGridSettings: {                
        displayAuthors: false,
        displayCatalogs: false,
        displayDocType: false,
        displayIsFitForClients: false,
        displayPagination: false,
        displayProduct: true,
        displayShortDescription: false,
        displayVersion: true,        
        filterable: false,
        numberOfResults: 10,
        title: "Recent Documents"        
      },
      documentTypes: [],
      documents: [],     
      errors: [], 
      hasDataLoaded: false,
      isHomepage: true,
      products: []
      
    }
  }

  public handleDocumentChanges(documentList: Document[], newDocGridSettings: IDocGridSettings){
    this.setState({
      docGridSettings: newDocGridSettings,
      documents: documentList,
      isHomepage: false
    });
  }

  public handleErrors(newError: CustomError){
    this.state.errors.push(newError);
    const currentError: CustomError|undefined = this.state.errors.shift();
    this.setState({'currentError': currentError});
  }

  public handleErrorClose(): void{
    const currentError: CustomError|undefined = this.state.errors.shift();
    this.setState({'currentError': currentError});
  }

  public async componentDidMount(){
    if(this.state.hasDataLoaded){
      return;
    }
    const errors: CustomError[] = [];
    let productList: Product[] = [];
    let recentDocuments: Document[] = [];
    let docTypes: DocType[] = [];

    const productListResult: Product[] | CustomError = await DocRepoApi.getProducts();
    if(productListResult instanceof CustomError){
      errors.push(productListResult);
    } else {
      productList = productListResult;
    }    

    const recentDocumentsResult: Document[] | CustomError = await DocRepoApi.getDocuments(10);
    if(recentDocumentsResult instanceof CustomError){
      errors.push(recentDocumentsResult);
    } else {
      recentDocuments = recentDocumentsResult;
    }

    const docTypesResult: DocType[] | CustomError = await DocRepoApi.getDocumentTypes();
    if(docTypesResult instanceof CustomError){
      errors.push(docTypesResult);
    } else {
      docTypes = docTypesResult;
    }

    const currentError: CustomError|undefined = errors.shift();

    this.setState({
      'currentError': currentError,
      documentTypes: docTypes,
      documents: recentDocuments,
      'errors': errors,
      hasDataLoaded: true,
      products: productList
    });
  }

  public componentDidUpdate(){
    window.scroll(0,0);
  }
  public render() {
    
    return (
      <div className="App">        
        <MainLayout products={this.state.products} docTypes={this.state.documentTypes} documents={this.state.documents} onDocumentsChanged={this.handleDocumentChanges} docGridSettings={this.state.docGridSettings} isHomepage={this.state.isHomepage} onError={this.handleErrors} hasDataLoaded={this.state.hasDataLoaded} /> 
        {this.state.currentError &&       
          <ErrorHandler errorDescription={this.state.currentError.description} onErrorClose={this.handleErrorClose} />           
        }                    
      </div>
    );
  }
}

export default App;
