import {
    Post,
    SimplePost
} from "./interfaces";

export const getPosts = async (): Promise<Post[] | undefined> => {
    const response = await fetch('/api/post');

    const status = response.status;

    if (status === 401) {
        window.location.href = "/Identity/Account/Login";

        return;
    }
    else if (status === 403) {
        window.location.href = "/Identity/Account/AccessDenied";

        return;
    }

    return await response.json();
};

export const addPost = (post: SimplePost) =>
    fetch('/api/post', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(post)
    });

export const deletePost = (post: Post) =>
    fetch(`/api/post/${post.id}`, {
        method: 'DELETE'
    });