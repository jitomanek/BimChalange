import { TaskStatusEnum } from "../clients/client.generated";


//TODO: add generic typy for value in DataTableFilter on BE
export const statusOptions_Table: any[] =
    [
        { text: 'Initial', value: '1' },
        { text: 'In progress', value: '2' },
        { text: 'Completed', value: '3' },
    ]

export const statusOptions_Form: any[] =
    Object.keys(TaskStatusEnum).map(k => ({ text: k, value: (TaskStatusEnum as any)[k] }));

export const priorityOptions: any[] = [
    { text: '1', value: '1' },
    { text: '2', value: '2' },
    { text: '3', value: '3' },
    { text: '4', value: '4' },
    { text: '5', value: '5' }
]