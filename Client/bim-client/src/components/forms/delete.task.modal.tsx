import React from 'react'
import { Modal } from 'kantar-react-lib'
import { IDeleteTask, DeleteTaskForm } from './delete.task';


interface IDeleteModal extends IDeleteTask {
    id:number
    onHide: () => void
    show: boolean   
}

export const DeleteModal: React.FC<IDeleteModal> = ({ onHide, show, id }: IDeleteModal) => {

    const handleAfterSubmit = (m: any) => {
        if (m) onHide()
    }

    const modal = {
        body: <DeleteTaskForm afterSubmit={handleAfterSubmit} id={id} />, //body:['some text', 'KantarCZ a.s'],
        title: 'Delete Task',
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