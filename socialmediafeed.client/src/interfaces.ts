import {
    InputType
} from "reactstrap/types/lib/Input";

import {
    IconProp
} from "@fortawesome/fontawesome-svg-core";

export interface SimplePost {
    content: string;
    postId?: number;
}

export interface Post extends SimplePost {
    id: number;
    createdAt: Date;
    userId: string;
    canDelete: boolean;
    canEdit: boolean;
    userName: string;
    likesCount: number;
    liked: boolean;
    posts: Post[];
}

export interface FormInput<T> {
    field: keyof T;
    label: string;
    type: InputType;
    required?: boolean;
}

export interface ButtonProps {
    onClick?: () => void;
    title: string;
    icon: IconProp;
    visible?: boolean;
    text?: string;
}