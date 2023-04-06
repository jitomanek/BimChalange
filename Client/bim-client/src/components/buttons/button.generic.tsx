import { ButtonPosition } from 'kantar-react-lib'
import React from 'react'
import { Button, FormGroup } from 'react-bootstrap'


interface IButtonGeneric {
    label?: string
    variant?: string
    position?: ButtonPosition
    onClick?: () => void
}

export const ButtonGeneric: React.FC<IButtonGeneric> = ({
    label = 'Create',
    variant = 'primary',
    onClick,
    position = ButtonPosition.Right
}: IButtonGeneric) => {

    const getPositionClass = () => {
        switch (position) {
            case ButtonPosition.Left:
                return ''
            case ButtonPosition.Middle:
                return 'text-center'
            default:
                return 'text-right'
        }
    }

    return (
        <FormGroup className={'mb-3 ' + getPositionClass()}>
            <Button onClick={onClick} variant={variant}>
                {label}
            </Button>
        </FormGroup>
    )
}
