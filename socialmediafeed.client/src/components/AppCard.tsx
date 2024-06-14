import {
    Card,
    CardHeader,
    CardBody,
    CardFooter,
    CardText
} from "reactstrap";

interface Props {
    header: React.ReactNode;
    footer: React.ReactNode;
    text: React.ReactNode;
    className?: string;
}

const AppCard = ({
    header,
    footer,
    text,
    className
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

            <CardFooter>
                {footer}
            </CardFooter>
        </Card>
    );
};

export default AppCard;