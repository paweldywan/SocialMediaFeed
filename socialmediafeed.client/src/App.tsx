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

interface Post {
    id: number;
    content: string;
    createdAt: Date;
    userId: string;
}

function App() {
    const [posts, setPosts] = useState<Post[]>();

    const populatePosts = useCallback(async () => {
        const response = await fetch('/api/post');

        const data = await response.json();

        setPosts(data);
    }, []);

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

    return (
        <Container fluid>
            <h1>Social media feed</h1>

            {contents}
        </Container>
    );
}

export default App;