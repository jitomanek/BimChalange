import {
    Table,
    decoratorsTable,
    FilterTypeTable,
} from 'kantar-react-lib'

import { DataTableFilter, FilterType, TaskResponseDataTableReply, TaskTableRequest } from '../../clients/client.generated'
import { Client } from '../../clients/client'

export const TaskTable: React.FC = () => {

    const { display } = decoratorsTable

    class ClientModel extends Client {
        request = this.apiTasks
    }
    const clientTable = new ClientModel()

    //TODO: add generic typy for value in DataTableFilter on BE
    const statusOptions: any[] =
        //Object.keys(TaskStatusEnum).map(k => ({ text: k, value: (TaskStatusEnum as any)[k] }));
        [
            { text: 'Initial', value: '1' },
            { text: 'In progress', value: '2' },
            { text: 'Completed', value: '3' },
        ]

    const priorityOptions: any[] = [
        { text: '1', value: '1' },
        { text: '2', value: '2' },
        { text: '3', value: '3' },
        { text: '4', value: '4' },
        { text: '5', value: '5' }
    ]


    class DTFHelp extends DataTableFilter {
        constructor(filterType: FilterType) {
            super()
            this.filterType = filterType
        }
    }

    class TaskTableModel extends TaskTableRequest {
        @display('Task name')
        name?: DataTableFilter = new DTFHelp(FilterType.Contains)

        @display('Description')
        description?: DataTableFilter = new DTFHelp(FilterType.Contains)

        @display('Status', false, FilterTypeTable.selectMulti, statusOptions as any)
        status?: DataTableFilter = new DTFHelp(FilterType.MultiEqual)

        @display('Priority', false, FilterTypeTable.selectMulti, priorityOptions)
        priority?: DataTableFilter = new DTFHelp(FilterType.MultiEqual)
    }

    return (
        <div>
            <Table<TaskTableModel, TaskResponseDataTableReply>
                client={clientTable}
                header={new TaskTableModel()}
                rowsPerPage={10}
            />
        </div>
    )
}