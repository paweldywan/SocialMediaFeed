import {
    Card,
    CardHeader,
    CardBody,
    CardFooter,
    CloseButton,
    CardText
} from "reactstrap";

interface Props {
    header: React.ReactNode;
    footer: React.ReactNode;
    text: React.ReactNode;
    className?: string;
    onClose?: () => void;
}

const AppCard = ({
    header,
    footer,
    text,
    className,
    onClose
}: Props) => {
    return (
        <Card className={className}>
            <CardHeader>
                {header}

                {onClose && (
                    <CloseButton
                        className="float-end"
                        onClick={onClose}
                    />
                )}
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