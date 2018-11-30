import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
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
        return this.get<MarkerComment[]>('GetCommentsForMarker', params);
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
