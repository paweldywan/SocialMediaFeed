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
    faMagnifyingGlass,
    faPlus,
    faThumbsUp as faThumbsUpSolid,
    faTrash
} from '@fortawesome/free-solid-svg-icons';

import {
    faThumbsUp as faThumbsUpRegular
} from '@fortawesome/free-regular-svg-icons';

import React from 'react';

function App() {
    const [posts, setPosts] = useState<Post[]>();

    const [postToAdd, setPostToAdd] = useState<SimplePost>({
        content: ''
    });

    const [postToEdit, setPostToEdit] = useState<Post>();

    const [subpostToAdd, setSubpostToAdd] = useState<SimplePost>({
        content: ''
    });

    const [postPreviewIds, setPostPreviewIds] = useState<number[]>([]);

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

    const mapPosts = useCallback((posts: Post[]) => posts.map(post =>
        <React.Fragment key={post.id}>
            <AppCard
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
                    },
                    {
                        onClick: () => setSubpostToAdd({
                            content: '',
                            postId: subpostToAdd.postId !== post.id ? post.id : undefined
                        }),
                        title: subpostToAdd.postId !== post.id ? 'Reply' : 'Cancel reply',
                        icon: subpostToAdd.postId !== post.id ? faPlus : faCancel
                    },
                    {
                        onClick: () => setPostPreviewIds(postPreviewIds.includes(post.id) ? postPreviewIds.filter(id => id !== post.id) : [...postPreviewIds, post.id]),
                        title: postPreviewIds.includes(post.id) ? 'Hide replies' : 'Show replies',
                        icon: postPreviewIds.includes(post.id) ? faCancel : faMagnifyingGlass,
                        visible: post.posts?.length > 0
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

            {subpostToAdd.postId === post.id && (
                <AppForm
                    inputs={[
                        {
                            field: 'content',
                            label: 'Content',
                            type: 'textarea',
                            required: true
                        }
                    ]}
                    data={subpostToAdd}
                    setData={setSubpostToAdd}
                    onSubmit={async () => {
                        await addPost({
                            ...subpostToAdd,
                            postId: post.id
                        });

                        setSubpostToAdd({
                            content: '',
                            postId: undefined
                        });

                        await populatePosts();

                        setPostPreviewIds(postPreviewIds.includes(post.id) ? postPreviewIds : [...postPreviewIds, post.id]);
                    }}
                    className="mb-3"
                />
            )}

            {postPreviewIds.includes(post.id) && (
                <AppCard
                    body={mapPosts(post.posts)}
                    className="mb-3"
                />
            )}
        </React.Fragment>
    ), [executeDeletePost, executeEditPost, executeLikePost, populatePosts, postPreviewIds, postToEdit, subpostToAdd]);

    useEffect(() => {
        document.documentElement.setAttribute('data-bs-theme', window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light');

        populatePosts();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const contents = useMemo(() => posts === undefined
        ? <p><em>Loading...</em></p>
        : mapPosts(posts), [mapPosts, posts]);

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