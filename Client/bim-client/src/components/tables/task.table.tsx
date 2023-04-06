import {
    Table,
    decoratorsTable,
    FilterTypeTable,
} from 'kantar-react-lib'
import {FaTrashAlt, FaPen} from 'react-icons/fa'

import { DataTableFilter, FilterType, TaskResponse, TaskResponseDataTableReply, TaskTableRequest } from '../../clients/client.generated'
import { Client } from '../../clients/client'
import { statusOptions_Table, priorityOptions } from '../../utils/task.util'
import { useState } from 'react'
import { UpdateModal } from '../forms/update.task.modal'
import { DeleteModal } from '../forms/delete.task.modal'


const { display } = decoratorsTable

class ClientModel extends Client {
    request = this.apiTasks
}
const clientTable = new ClientModel()




export const TaskTable: React.FC = () => {
    const [updateFormId, setUpdateFormId] = useState(
        undefined as (number | undefined)
    )
    const [deleteFormId, setDeleteFormId] = useState(
        undefined as (number | undefined)
    )

    const customClickable = () => {
        return [
            {
                htmlElement:<FaPen /> ,//<a>Update</a>,
                onClick: (m: any) => {
                    console.log('Update click: ', m)
                    const model = m as TaskResponse
                    setUpdateFormId(model.id as number)
                }
            },
            {
                htmlElement: <FaTrashAlt className='text-danger'/>,//<a>Delete</a>,
                onClick: (m: any) => {
                    console.log('Delete click: ', m)
                    const model = m as TaskResponse
                    setDeleteFormId(model.id as number)
                }
            }
        ]
    }


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

        @display('Status', false, FilterTypeTable.selectMulti, statusOptions_Table as any)
        status?: DataTableFilter = new DTFHelp(FilterType.MultiEqual)

        @display('Priority', false, FilterTypeTable.selectMulti, priorityOptions)
        priority?: DataTableFilter = new DTFHelp(FilterType.MultiEqual)

        @display('Actions', false, undefined, undefined, undefined, customClickable())
        update: DataTableFilter = new DTFHelp(FilterType.Equals)
    }


    return (
        <div>


            {updateFormId === undefined ? undefined : <UpdateModal
                id={updateFormId}
                show={true}
                onHide={() => setUpdateFormId(undefined)}
                afterSubmit={() => setUpdateFormId(undefined)}
            />}

            {deleteFormId === undefined ? undefined : <DeleteModal
                id={deleteFormId}
                show={true}
                onHide={() => setDeleteFormId(undefined)}
                afterSubmit={() => setDeleteFormId(undefined)}
            />}

            <Table<TaskTableModel, TaskResponseDataTableReply>
                client={clientTable}
                header={new TaskTableModel()}
                rowsPerPage={10}
            />
        </div>
    )
}