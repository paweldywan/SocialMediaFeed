import {
    InputType
} from "reactstrap/types/lib/Input";

export interface SimplePost {
    content: string;
}

interface User {
    id: string;
    userName: string;
}

export interface Post extends SimplePost {
    id: number;
    createdAt: Date;
    userId: string;
    canDelete: boolean;
    user: User;
}

export interface FormInput<T> {
    field: keyof T;
    label: string;
    type: InputType;
    required?: boolean;
}