import {
    Form,
    decoratorsForm,
    ButtonPosition
} from 'kantar-react-lib'

import { Client } from '../../clients/client'
import { CreateModel } from './create.task'


const { hidden, disabled } = decoratorsForm


export interface ITaskUpdateForm {
    afterSubmit: (m: any) => void
    id: number
}

class UpdateModel extends CreateModel {
    @hidden(true)
    @disabled()
    id?: number;
}

class UpdateClient extends Client {
    request = this.apiTaskPut as any
}
const client = new UpdateClient()


export const TaskUpdateForm: React.FC<ITaskUpdateForm> = ({ afterSubmit, id }: ITaskUpdateForm) => {

    return (
        <Form<UpdateModel>
            model={new UpdateModel()}
            submitBtnLabel='Update'
            submitBtnPosition={ButtonPosition.Middle}
            submitClient={client}
            onSubmit={afterSubmit}
            fetch={client.apiTaskGet(id) as any}
        />
    )
}