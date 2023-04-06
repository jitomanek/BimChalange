import React from 'react'
import { Modal } from 'kantar-react-lib'
import { TaskCreateForm } from './create.task';


interface ICreateModal {
    onHide: () => void
    show: boolean
}

export const CreateModal: React.FC<ICreateModal> = ({ onHide, show }: ICreateModal) => {

    const handleAfterSubmit = (m: any) => {
        if (m.id) onHide()
    }

    const modal = {
        body: <TaskCreateForm afterSubmit={handleAfterSubmit} />, //body:['some text', 'KantarCZ a.s'],
        title: 'Create Task',
    }

    return (
        <Modal
            body={modal.body}
            title={modal.title}
            show={show}
            onHide={onHide}
        />
    )
}