import { CommentAuthor } from './comment-author.model';

export class MarkerComment {
    public id: string;
    public text: string;
    public markerId: string;
    public commentAuthor: CommentAuthor;
    public createDate: Date;
    public modificationDate: Date;
}
