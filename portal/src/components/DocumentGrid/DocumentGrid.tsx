import * as React from 'react';
import ReactTable, { Column } from 'react-table';
import 'react-table/react-table.css';

import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';

import { createStyles, Theme, WithStyles, withStyles } from '@material-ui/core/styles';

import { CustomError } from '../../models/customerror';
import { Document } from '../../models/document';
import { DocType } from '../../models/documenttype';
import { Product } from '../../models/product';
import { Captions } from '../../translations/en-US';

import AuthorsDocGridLinks from '../AuthorsDocGridLinks/AuthorsDocGridLinks';
import DocumentTitleWithLinks from '../DocumentTitleWithLinks/DocumentTitleWithLinks';

import CatalogsDocGrid from '../CatalogsDocGrid/CatalogsDocGrid';
import SingleDocument from '../SingleDocument/SingleDocument';

import withWidth, { WithWidthProps } from '@material-ui/core/withWidth';

import { Breakpoint } from '@material-ui/core/styles/createBreakpoints';

const styles = (theme: Theme) => createStyles({  
  leftAligned: {
    textAlign: "left",      
    whiteSpace: "normal"
  }
});

const normalWhiteSpace = (state:any, rowInfo:any, column:any) => {
  return {
    style: {
      whiteSpace: "normal"
    }
  }};

export interface IDocGridSettings{
  title: string;
  filterable: boolean;
  displayAuthors: boolean;
  displayCatalogs: boolean;
  displayProduct: boolean;
  displayVersion: boolean;
  displayDocType: boolean;
  displayIsFitForClients: boolean;
  displayPagination: boolean;
  displayShortDescription: boolean;
  numberOfResults: number;  
}


const onLarge = (size: Breakpoint) => {
  return size === 'xl' || size === 'lg';
};

const onMedium = (size: Breakpoint) => {
  return size === 'xl' || size === 'lg' || size === 'md';
};

const onSmall = (size: Breakpoint) => {
  return size === 'xl' || size === 'lg' || size === 'md' || size === 'sm';
};

interface IDocumentGridProps extends WithStyles<typeof styles>, WithWidthProps{
  documents: Document[];
  docTypes: DocType[];
  products: Product[];
  settings: IDocGridSettings;  
  onError(error: CustomError):void;
}


class DocumentGrid extends React.Component<IDocumentGridProps> {
  private docTypeFilterMethod: any;
  private fitForClientsFilterMethod: any;
  private productFilterMethod: any;

  constructor(props: IDocumentGridProps){
        super(props);          
        this.handleDocTypeChange = this.handleDocTypeChange.bind(this);
        this.handleFitForClientsChange = this.handleFitForClientsChange.bind(this);    
        this.handleProductChange = this.handleProductChange.bind(this);
    }   

    

    public render(){ 
      const { classes, width } = this.props;      
      const columns: Column[] = [{        
        Cell: (current: any) => <DocumentTitleWithLinks document={current.original} />,
        Header: Captions.docGrid.columnHeaders.title,
        className: classes.leftAligned,
        filterMethod: (filter:any, row:any) => {
          if (!filter.value) {
            return true;
          }              
          return row.title.toLowerCase().includes(filter.value.toLowerCase());
          
        },
        headerClassName: classes.leftAligned,        
        id: 'document'     
      },
      {
        accessor: 'title',
        id: 'title', 
        show: false
      }
    
    ];     

      if(this.props.settings.displayProduct){        
          columns.push({        
            Filter: ({ filter, onChange }) => {
              this.docTypeFilterMethod = onChange; 
              return (
                    <select                                            
                      onChange={this.handleDocTypeChange}  
                      style={{ width: "100%" }}
                      value={filter ? filter.value : "all"}
                    >
                      <option value="all" />
                      {this.props.products && this.props.products.map(p => 
                      <option key={p.id} value={p.fullName}>{p.fullName}</option>   
                    )
                      }
                    </select>);
                    }
                ,
            Header: Captions.docGrid.columnHeaders.product,
            accessor: 'product',
            filterMethod: (filter:any, row:any) => {
              if (filter.value === "all") {
                return true;
              }              
              return filter.value === row[filter.id];
            },
            maxWidth: 200,
            show: onMedium(width)
          });
      }

      if(this.props.settings.displayVersion){        
        columns.push({
          Header: Captions.docGrid.columnHeaders.version,
          accessor: 'version', 
          filterMethod: (filter:any, row:any) => {
            if (!filter.value) {
              return true;
            }              
            return row[filter.id].toLowerCase().includes(filter.value.toLowerCase());
          },
          maxWidth: 95,
          show: onMedium(width)
        });
      }

      if(this.props.settings.displayDocType){
        columns.push({
          Filter: ({ filter, onChange }) => {
            this.docTypeFilterMethod = onChange; 
            return (
                  <select                                            
                    onChange={this.handleDocTypeChange}  
                    style={{ width: "100%" }}
                    value={filter ? filter.value : "all"}
                  >
                    <option value="all" />
                    {this.props.docTypes && this.props.docTypes.map(d => 
                    <option key={d.id} value={d.fullName}>{d.fullName}</option>   
                  )
                    }
                  </select>);
                  }
              ,
          Header: Captions.docGrid.columnHeaders.docType,
          accessor: 'documentType',
          filterMethod: (filter:any, row:any) => {
            if (filter.value === "all") {
              return true;
            }              
            return filter.value === row[filter.id];
          },
          show: onMedium(width)
        });
      }

      if(this.props.settings.displayShortDescription){
        columns.push({
          Header: Captions.docGrid.columnHeaders.description,
          accessor: 'shortDescription',
          className: classes.leftAligned,
          filterMethod: (filter:any, row:any) => {
            if (!filter.value) {
              return true;
            }              
            return row[filter.id].toLowerCase().includes(filter.value.toLowerCase());
          },
          headerClassName: classes.leftAligned,
          show: onLarge(width),
          sortable: false,
                    
        });
      }

      columns.push({
        Header: Captions.docGrid.columnHeaders.latestTopicsUpdated,
        accessor: 'latestTopicsUpdated',
        className: classes.leftAligned,
        filterMethod: (filter:any, row:any) => {
          if (!filter.value) {
            return true;
          }              
          return row[filter.id].toLowerCase().includes(filter.value.toLowerCase());
        },
        headerClassName: classes.leftAligned,
        show: onSmall(width)
      });

      columns.push({        
        Cell: (props: any) => props.value.toDateString(),
        Header: Captions.docGrid.columnHeaders.updatedOn,
        accessor: 'latestUpdate',
        maxWidth: 150,
        show: onSmall(width)    
      });
      
      if(this.props.settings.displayIsFitForClients){
        columns.push(
          {
            Cell: (current: any) => current.value ? 'Yes' : 'No',
            Filter: ({ filter, onChange }) => {
              this.fitForClientsFilterMethod = onChange; 
              return (
                    <select                                            
                      onChange={this.handleFitForClientsChange}  
                      style={{ width: "100%" }}
                      value={filter ? filter.value : "all"}
                    >
                      <option value="all" />
                      <option value="true">Yes</option>
                      <option value="false">No</option>
                    </select>);
                    }
                ,
            Header: Captions.docGrid.columnHeaders.fitForCliens,
            accessor: 'isFitForClients',
            filterMethod: (filter:any, row:any) => {
              if (filter.value === "all") {
                return true;
              }              
              return filter.value === "true" ? row[filter.id] : !row[filter.id];
            },
            maxWidth: 115,
            show: onLarge(width)
          }
        );
      }

      if(this.props.settings.displayCatalogs){
        columns.push({
          Cell: (current: any) => <CatalogsDocGrid catalogs={current.value} isFitForClients={current.original.isFitForClients}/>,
          Header: Captions.docGrid.columnHeaders.catalogs,
          accessor: 'clientCatalogs',
          filterable: false,          
          show: onLarge(width),
          sortable: false,                              
        });
      }

      if(this.props.settings.displayAuthors){        
        columns.push({          
          Cell: (current: any) => <AuthorsDocGridLinks authors={current.value} documentTitle={current.original.title} />,
          Header: Captions.docGrid.columnHeaders.authors,
          accessor: 'authors',
          filterable: false,
          maxWidth: 175,     
          show: onLarge(width),     
          sortable: false,
          });
      }

      columns.push({
        Cell: (current: any) => <SingleDocument document={current.original} onError={this.props.onError} />,
        Header: Captions.docGrid.columnHeaders.more,
        filterable: false,
        id: "more",        
        maxWidth: 60,
        sortable: false,
      });
        return (
        <Grid>
          <Typography variant="headline" color="primary" component="h2" paragraph={true}>
            {this.props.settings.title}
          </Typography>
          <ReactTable className="-striped" data={this.props.documents} columns={columns} filterable={this.props.settings.filterable} showPagination={this.props.settings.displayPagination} defaultPageSize={this.props.settings.numberOfResults} getTdProps={normalWhiteSpace} />
        </Grid>
        );
    }

    private handleDocTypeChange(event: React.ChangeEvent<HTMLSelectElement>){
      this.docTypeFilterMethod(event.target.value);
    }

    private handleFitForClientsChange(event: React.ChangeEvent<HTMLSelectElement>){
      this.fitForClientsFilterMethod(event.target.value);
    }

    private handleProductChange(event: React.ChangeEvent<HTMLSelectElement>){
      this.productFilterMethod(event.target.value);
    }
  }

  export default withWidth() (withStyles(styles, { withTheme: true })(DocumentGrid));

