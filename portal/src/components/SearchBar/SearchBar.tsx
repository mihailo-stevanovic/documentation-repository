import FormControlLabel from '@material-ui/core/FormControlLabel';
import Switch from '@material-ui/core/Switch';
import TextField from '@material-ui/core/TextField';
import * as React from 'react';

export class SearchBar extends React.Component {
      public render(){
          return(
              <div className="search-bar">
                <TextField type="search" placeholder="Search for documents" />
                <FormControlLabel
                    control={
                        <Switch
                        // checked={this.state.checkedB}
                        // onChange={this.handleChange('checkedB')}
                        value="true"
                        color="primary"
                        />
                    }
                    label="Fuzzy Search"
                    />
              </div>
          );
      }
}