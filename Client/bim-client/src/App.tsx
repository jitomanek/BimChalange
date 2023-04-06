import React from 'react';
import './App.scss';


import { Provider, appStore } from 'kantar-react-lib';
import { TaskTable } from './components/tables/task.table';
import { ButtonCreate } from './components/buttons/button.create';
import { BimNavbar } from './components/navbar';

function App() {
  return (
    <Provider store={appStore}>
      {/* No routing */}
      <BimNavbar />

      <div className='container margin-top-15'>
        <ButtonCreate />
        <TaskTable />
      </div>


    </Provider>
  );
}

export default App;
