import React from 'react';
import './App.scss';


import { Provider, appStore } from 'kantar-react-lib';
import { TaskTable } from './components/tables/task.table';

function App() {
  return (
    <Provider store={appStore}>
      {/* no routing */}

<TaskTable />

    </Provider>
  );
}

export default App;
