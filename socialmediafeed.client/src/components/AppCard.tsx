import {
    FontAwesomeIcon
} from "@fortawesome/react-fontawesome";

import {
    Card,
    CardHeader,
    CardBody,
    CardFooter,
    CardText,
    Col,
    Row
} from "reactstrap";

import {
    ButtonProps
} from "../interfaces";

interface Props {
    header: React.ReactNode;
    footer?: React.ReactNode;
    text: React.ReactNode;
    className?: string;
    buttons?: ButtonProps[];
}

const AppCard = ({
    header,
    footer,
    text,
    className,
    buttons
}: Props) => {
    return (
        <Card className={className}>
            <CardHeader>
                {header}
            </CardHeader>

            {text &&
                <CardBody>
                    <CardText>
                        {text}
                    </CardText>
                </CardBody>}

            {(footer || buttons) &&
                <CardFooter>
                    {footer}

                    {buttons &&
                        <Row>
                            {buttons
                                .filter(button => button.visible !== false)
                                .map((button, index) => (
                                    <Col
                                        xs="auto"
                                        key={index}
                                    >
                                        <FontAwesomeIcon
                                            onClick={button.onClick}
                                            title={button.title}
                                            role="button"
                                            icon={button.icon}
                                        />
                                        {button.text && " "}
                                        {button.text}
                                    </Col>
                                ))}
                        </Row>}
                </CardFooter>}
        </Card>
    );
};

export default AppCard;