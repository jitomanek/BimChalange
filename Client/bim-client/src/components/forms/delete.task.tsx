import React from 'react'
import { ButtonGeneric } from '../buttons/button.generic'
import { ButtonPosition } from 'kantar-react-lib'
import { Client } from '../../clients/client'

export interface IDeleteTask {
    afterSubmit: (id: number) => void
    id: number
}


const client = new Client()


export const DeleteTaskForm: React.FC<IDeleteTask> = ({ id, afterSubmit }: IDeleteTask) => {
    const handleClick = () => {
        client.apiTaskDelete(id)
            .then(m => {
                console.log('Delete Success', m)
                afterSubmit(id)
            })
            .catch(m => {
                console.log('Delete Task error occured', m)
            })
    }
    return (
        <div>
            <p>
                Permanently delete task with id: {id}?
            </p>
            <ButtonGeneric
                label='Delete'
                variant='danger'
                position={ButtonPosition.Middle}
                onClick={handleClick}
            />
        </div>

    )
}