import IUser from "./IUser";

interface IComment {
    childCount: number,
    id: string,
    parentId: string | null,
    blogPostId: string,
    user: IUser,
    content: string,
    createOn: Date
}

export default IComment;