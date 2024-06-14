import {
    useCallback,
    useEffect,
    useMemo,
    useState
} from 'react';

import {
    Container,
    Table
} from 'reactstrap';

import 'bootstrap/dist/css/bootstrap.min.css';

import {
    addPost,
    getPosts
} from './requests';

import {
    FormInput,
    Post,
    SimplePost
} from './interfaces';

import AppForm from './components/AppForm';

function App() {
    const [posts, setPosts] = useState<Post[]>();

    const [postToAdd, setPostToAdd] = useState<SimplePost>({
        content: ''
    });

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

    useEffect(() => {
        document.documentElement.setAttribute('data-bs-theme', window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light');

        populatePosts();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const contents = useMemo(() => posts === undefined
        ? <p><em>Loading...</em></p>
        : <Table
            striped
            hover
            responsive
            bordered
        >
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Content</th>
                    <th>Created at</th>
                    <th>User ID</th>
                </tr>
            </thead>

            <tbody>
                {posts.map(post =>
                    <tr key={post.id}>
                        <td>{post.id}</td>
                        <td>{post.content}</td>
                        <td>{new Date(post.createdAt).toLocaleString()}</td>
                        <td>{post.userId}</td>
                    </tr>
                )}
            </tbody>
        </Table>, [posts]);

    const inputs = useMemo<FormInput<SimplePost>[]>(() => [
        {
            field: 'content',
            label: 'Content',
            type: 'textarea'
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