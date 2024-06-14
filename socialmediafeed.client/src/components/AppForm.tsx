import {
    Button,
    Form,
    FormGroup,
    Input,
    Label
} from "reactstrap";

import {
    FormInput
} from "../interfaces";

interface Props<T> {
    inputs: FormInput<T>[];
    data: T;
    setData: (data: T) => void;
    onSubmit: (data: T) => void;
    className?: string;
}

const AppForm = <T,>({
    inputs,
    data,
    setData,
    onSubmit,
    className
}: Props<T>) => {
    return (
        <Form
            onSubmit={(e) => {
                e.preventDefault();

                onSubmit(data);
            }}
            className={className}
        >
            {inputs.map((input, index) => (
                <FormGroup key={index}>
                    <Label for={input.field as string}>{input.label}</Label>
                    <Input
                        id={input.field as string}
                        type={input.type}
                        value={data[input.field] as string}
                        onChange={(e) => setData({
                            ...data,
                            [input.field]: e.target.value
                        })}
                        required={input.required}
                    />
                </FormGroup>
            ))}

            <Button>Submit</Button>
        </Form>
    );
};

export default AppForm;