import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { environment } from './../../../environments/environment';

import { BaseHttpService } from '../../shared/services/http-services/base-http.service';
import { MarkerComment } from './../../shared/models/data/comment/comment.model';

@Injectable()
export class CommentApiService extends BaseHttpService {

    public constructor(client: HttpClient) {
        super(client, 'Comment');
    }

    public getCommentsForMarker(markerId: string): Observable<MarkerComment[]> {
        const params = new HttpParams().set('markerId', markerId);
        let result = this.get<MarkerComment[]>('GetCommentsForMarker', params);

        if (environment.production === false) {
            result = result.do((resultComments: MarkerComment[]) => {
                resultComments.forEach(comment => {
                    comment.commentAuthor.image = environment.backendHotst + comment.commentAuthor.image;
                });
            });
        }

        return result;
    }

    public addComment(comment: MarkerComment): Observable<MarkerComment> {
        return this.post<MarkerComment>('AddComment', comment);
    }

    public updateComment(comment: MarkerComment): Observable<MarkerComment> {
        return this.post<MarkerComment>('UpdateComment', comment);
    }

    public removeComment(commentId: string): Observable<MarkerComment> {
        const params = new HttpParams().set('commentId', commentId);
        return this.delete<MarkerComment>('RemoveComment', params);
    }
}
