import IUser from './IUser';


interface IBlogPost {
    bannerUrl: string | undefined
    commentCount: number
    describe: string
    id: number
    title: string
    content: string
    view: number
    read: number
    like: number
    user: IUser | null
    tags: any[]
    createOn: Date
}

export default IBlogPost;