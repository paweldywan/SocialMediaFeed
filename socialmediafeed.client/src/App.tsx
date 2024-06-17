import {
    useCallback,
    useEffect,
    useMemo,
    useState
} from 'react';

import {
    Col,
    Container,
    Row
} from 'reactstrap';

import 'bootstrap/dist/css/bootstrap.min.css';

import {
    addPost,
    deletePost,
    editPost,
    getPosts,
    likePost
} from './requests';

import {
    FormInput,
    Post,
    SimplePost
} from './interfaces';

import AppForm from './components/AppForm';

import AppCard from './components/AppCard';

import {
    faCancel,
    faEdit,
    faThumbsUp as faThumbsUpSolid,
    faTrash
} from '@fortawesome/free-solid-svg-icons';

import {
    faThumbsUp as faThumbsUpRegular
} from '@fortawesome/free-regular-svg-icons';

function App() {
    const [posts, setPosts] = useState<Post[]>();

    const [postToAdd, setPostToAdd] = useState<SimplePost>({
        content: ''
    });

    const [postToEdit, setPostToEdit] = useState<Post>();

    const populatePosts = useCallback(async () => {
        const data = await getPosts();

        setPosts(data);
    }, []);

    const executeAddPost = useCallback(async () => {
        await addPost(postToAdd);

        setPostToAdd({
            content: ''
        });

        await populatePosts();
    }, [postToAdd, populatePosts]);

    const executeEditPost = useCallback(async () => {
        if (postToEdit === undefined)
            return;

        await editPost(postToEdit);

        setPostToEdit(undefined);

        await populatePosts();
    }, [postToEdit, populatePosts]);

    const executeDeletePost = useCallback(async (post: Post) => {
        await deletePost(post);

        await populatePosts();
    }, [populatePosts]);

    const executeLikePost = useCallback(async (post: Post) => {
        await likePost(post);

        await populatePosts();
    }, [populatePosts]);

    useEffect(() => {
        document.documentElement.setAttribute('data-bs-theme', window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light');

        populatePosts();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const contents = useMemo(() => posts === undefined
        ? <p><em>Loading...</em></p>
        : posts.map(post =>
            <AppCard
                key={post.id}
                header={(
                    <Row>
                        <Col>
                            {post.userName}
                        </Col>

                        <Col className="text-end">
                            {new Date(post.createdAt).toLocaleString()}
                        </Col>
                    </Row>
                )}
                buttons={[
                    {
                        onClick: () => executeLikePost(post),
                        title: 'Like',
                        icon: post.liked ? faThumbsUpSolid : faThumbsUpRegular,
                        text: post.likesCount.toString()
                    },
                    {
                        onClick: () => post.id === postToEdit?.id ? setPostToEdit(undefined) : setPostToEdit(post),
                        title: post.id === postToEdit?.id ? 'Cancel' : 'Edit',
                        icon: post.id === postToEdit?.id ? faCancel : faEdit,
                        visible: post.canEdit
                    },
                    {
                        onClick: () => executeDeletePost(post),
                        title: 'Delete',
                        icon: faTrash,
                        visible: post.canDelete
                    }
                ]}
                text={(
                    post.id === postToEdit?.id
                        ? <AppForm
                            inputs={[
                                {
                                    field: 'content',
                                    label: 'Content',
                                    type: 'textarea',
                                    required: true
                                }
                            ]}
                            data={postToEdit}
                            setData={setPostToEdit}
                            onSubmit={executeEditPost}
                        />
                        : post.content
                )}
                className="mb-3"
            />
        ), [executeDeletePost, executeEditPost, executeLikePost, postToEdit, posts]);

    const inputs = useMemo<FormInput<SimplePost>[]>(() => [
        {
            field: 'content',
            label: 'Content',
            type: 'textarea',
            required: true
        }
    ], []);

    return (
        <Container fluid>
            <h1>Social media feed</h1>

            <AppForm
                inputs={inputs}
                data={postToAdd}
                setData={setPostToAdd}
                onSubmit={executeAddPost}
                className="mb-3"
            />

            {contents}
        </Container>
    );
}

export default App;