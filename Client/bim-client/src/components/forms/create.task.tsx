import {
    Form,
    decoratorsForm,
    ButtonPosition
} from 'kantar-react-lib'

import { TaskCreateRequest, TaskResponse, TaskStatusEnum } from '../../clients/client.generated'
import { statusOptions_Form, priorityOptions } from '../../utils/task.util'
import { Client } from '../../clients/client'


const { display, selectItems, hidden, textArea, required, regex } = decoratorsForm

export class CreateModel extends TaskCreateRequest {

    @display('Task name')
    @required('Field {0} is required!')
    @regex(/^.{0,128}$/, 'Field {0} has maximum of 128 characters!')
    name: string = ''

    @display('Description')
    @textArea()
    @regex(/^.{0,256}$/, 'Field {0} has maximum of 256 characters!')
    description?: string | undefined = ''

    @display('Priority')
    @selectItems('priorityList')
    @required()
    priority!: number;

    @hidden()
    priorityList = priorityOptions

    @display('Status')
    @selectItems('statusList')
    @required()
    status!: TaskStatusEnum;

    @hidden()
    statusList = statusOptions_Form
}


interface ICreateTask {
    afterSubmit: (m: any) => void
}

class CreateClient extends Client {
    request = this.apiTaskPost as any
}
const client = new CreateClient()

export const TaskCreateForm: React.FC<ICreateTask> = ({ afterSubmit }: ICreateTask) => {
    return (
        <Form<CreateModel>
            model={new CreateModel()}
            submitBtnLabel='Create'
            submitBtnPosition={ButtonPosition.Middle}
            submitClient={client}
            onSubmit={afterSubmit}
        />
    )
}