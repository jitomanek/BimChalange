import React from 'react'
import { Modal } from 'kantar-react-lib'
import { ITaskUpdateForm, TaskUpdateForm } from './update.task';


interface IUpdateModal extends ITaskUpdateForm {
    id:number
    onHide: () => void
    show: boolean   
}

export const UpdateModal: React.FC<IUpdateModal> = ({ onHide, show, id }: IUpdateModal) => {

    const handleAfterSubmit = (m: any) => {
        if (m.id) onHide()
    }

    const modal = {
        body: <TaskUpdateForm afterSubmit={handleAfterSubmit} id={id} />, //body:['some text', 'KantarCZ a.s'],
        title: 'Update Task',
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