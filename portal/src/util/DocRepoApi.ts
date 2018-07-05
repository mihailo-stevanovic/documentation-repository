import { CustomError } from '../models/customerror';
import { Document, IDocument} from '../models/document';
import { DocType, IDocType } from '../models/documenttype';
import { IProduct, Product } from '../models/product';
import { IUpdate, Update } from '../models/update';
import { IVersion, Version } from '../models/version';

import { Captions } from '../translations/en-US';

const rootUrl = "/api";
const documentsEndpoint = "/v1/DocumentsInternal";
const docTypesEndpoint = "/v1/DocumentTypes";
const productsEndpoint = "/v1/Products";


export class DocRepoApi {  
  // public methods  
  public static getDocuments(results:number = 20, page:number = 1):Promise<Document[]|CustomError> {
    const action = "getDocuments";
    const endpoint = rootUrl + documentsEndpoint + "?limit="+results+"&page="+page;    
    return fetch(endpoint)
      .then(
        response => {
          if(response.ok){
            return response.json();
          }
          throw new Error();          
        },
        rejection => {
          throw new Error(rejection);
      })
      .then((responseJson: IDocument[]) => responseJson.map(doc => new Document(doc)),
      rejection => {
        throw new Error(rejection);
    }).catch(error => new CustomError(Captions.errors.docsNotFound, action, error));
    
  }
  public static getDocumentById(documentId: number):Promise<Document|CustomError>|CustomError{
    const action = "getDocumentById"
    const endpoint = rootUrl + documentsEndpoint + "/" + documentId;
    return fetch(endpoint)
      .then(
        response => {
          if(response.ok){
            return response.json();
          }
          throw new Error();          
        },
        rejection => {
          throw new Error(rejection);
      })
      .then((responseJson: IDocument) => new Document(responseJson),
      rejection => {
        throw new Error(rejection);
    }).catch(error => new CustomError(Captions.errors.docNotFound, action, error));
    
  }
  public static getDocumentsByDocType(docTypeId:number, results:number = 20, page:number = 1):Promise<Document[] | CustomError> {
    return DocRepoApi.getDocumentsByAnotherEntity("ByDocType", docTypeId, results, page);
  }
  public static getDocumentsByProduct(productId:number, results:number = 20, page:number = 1):Promise<Document[] | CustomError> {
    return DocRepoApi.getDocumentsByAnotherEntity("ByProduct", productId, results, page);
  }
  public static getDocumentsByProductVersion(productVersionId:number, results:number = 20, page:number = 1):Promise<Document[] | CustomError> {
    return DocRepoApi.getDocumentsByAnotherEntity("ByProductVersion", productVersionId, results, page);
  }
  public static getDocumentsFromSearch(searchTerms: string, exactMatch:boolean = true, results:number = 20, page:number = 1):Promise<Document[] | CustomError> {
    const action = "search";
    const endpoint = rootUrl + documentsEndpoint + "/Search?searchTerm=" + searchTerms + "&exactMatch=" + exactMatch + "&limit=" + results + "&page=" + page;
    
    return fetch(endpoint)
    .then(
      response => {
        if(response.ok){
          return response.json();
        }
        throw new Error();          
      },
      rejection => {
        throw new Error(rejection);
    })
    .then((responseJson: IDocument[]) => responseJson.map(doc => new Document(doc)),
    rejection => {
      throw new Error(rejection);
  }).catch(error => new CustomError(Captions.errors.docsFromSearchNotFound, action, error));
    
  }
  public static getDocumentUpdates(documentId: number):Promise<Update[] | CustomError> {
    const action = "getDocumentDocumentUpdates";
    const endpoint = rootUrl + documentsEndpoint + "/" + documentId + "/Updates";
    return fetch(endpoint)
      .then(
        response => {
          if(response.ok){
            return response.json();
          }
          throw new Error();          
        },
        rejection => {
          throw new Error(rejection);
      })
      .then((responseJson: IUpdate[]) => responseJson.map(up => new Update(up)),
      rejection => {
        throw new Error(rejection);
    }).catch(error => new CustomError(Captions.errors.docUpdateNotFound, action, error));
  }

  public static getDocumentTypes():Promise<DocType[]| CustomError>{
    const action = "getDocumentTypes";
    const endpoint = rootUrl + docTypesEndpoint;    
    
      return fetch(endpoint)
      .then(
        response => response.json(),
        rejection => {
          throw new Error(rejection);
      })
      .then((responseJson: IDocType[]) => responseJson.map(docType => new DocType(docType)),
      rejection => {
        throw new Error(rejection);
    }).catch(error => new CustomError(Captions.errors.docTypesNotFound, action, error));
    
  }

  public static getProducts():Promise<Product[]| CustomError>|CustomError{
    const action = "getProducts";
    const endpoint = rootUrl + productsEndpoint;    
    
      return fetch(endpoint)
      .then(
        response => {
          if(response.ok){
            return response.json();
          }
          throw new Error();          
        },
        rejection => {
          throw new Error(rejection);
      })
      .then((responseJson: IProduct[]) => responseJson.map(prod => new Product(prod)),
      rejection => {
        throw new Error(rejection);
    }).catch(error => new CustomError(Captions.errors.productsNotFound, action, error));
    
  }

  public static getProductVersions(productId:number):Promise<Version[]| CustomError>|CustomError{
    const action = "getProductVersions";
    const endpoint = rootUrl + productsEndpoint + "/" + productId + "/Versions";  
    return fetch(endpoint)
    .then(
      response => {
        if(response.ok){
          return response.json();
        }
        throw new Error();          
      },
      rejection => {
        throw new Error(rejection);
    })
      .then((responseJson: IVersion[]) => responseJson.map(ver => new Version(ver)),
      rejection => {
        throw new Error(rejection);
    }).catch(error => new CustomError(Captions.errors.productVersionNotFound, action, error));    
  }


  // private methods
  private static getDocumentsByAnotherEntity(entityEndpoint: string, enitityId:number, results:number = 20, page:number = 1):Promise<Document[] | CustomError>{
    const action = entityEndpoint;
    const endpoint = rootUrl + documentsEndpoint + "/" + entityEndpoint + "/" +enitityId + "?limit="+results+"&page="+page;    
    return fetch(endpoint)
      .then(
        response => {
          if(response.ok){
            return response.json();
          }
          throw new Error();          
        },
        rejection => {
          throw new Error(rejection);
      })
      .then((responseJson: IDocument[]) => responseJson.map(doc => new Document(doc)),
      rejection => {
        throw new Error(rejection);
    }).catch(error => new CustomError(Captions.errors.docsNotFound, action, error));

  }      
}
