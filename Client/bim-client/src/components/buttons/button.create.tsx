import { ButtonPosition } from "kantar-react-lib";
import { ButtonGeneric } from "./button.generic";
import { useState } from "react";
import { CreateModal } from "../forms/create.task.modal";


export const ButtonCreate: React.FC = () => {
    const [show, setShow] = useState(false)

    const openCreateTask = () => setShow(true);
    const closeCreateTask = () => setShow(false);

    return (
        <div>
            {show === false ? undefined :
                <CreateModal
                    show={show}
                    onHide={closeCreateTask}
                />
            }
            <ButtonGeneric
                label="Create Task"
                onClick={openCreateTask}
                position={ButtonPosition.Left}
            />

        </div>
    )
}