import { Subscription } from 'rxjs';
import { MatSnackBar } from '@angular/material';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';

import { CommentApiService } from '../../services/comment-api.service';
import { UserService } from './../../../shared/services/user/user.service';
import { DialogService } from './../../../shared/services/dialog/dialog.service';
import { snackbarConfiguration } from '../../../shared/configurations/snackbar.config';
import { MarkerComment } from '../../../shared/models/data/comment/comment.model';
import { BackendErrorResponse } from './../../../shared/models/responses/error-response.model';
import { Marker } from '../../../shared/models/data/map/marker.model';

@Component({
    templateUrl: './comments.component.html',
    styleUrls: ['./comments.component.css'],
    selector: 'app-comments'
})
export class CommentsComponent implements OnInit, OnDestroy {

    public subscriptions: Subscription[];
    public markerId: string;
    public userName: string;
    public isAuthenticated: boolean;
    public comments: MarkerComment[];
    public waitForRequest: boolean;
    public text: string;

    public constructor(private commentApiService: CommentApiService, private activatedRoute: ActivatedRoute,
        private snackBar: MatSnackBar, private userService: UserService, private dialogService: DialogService) {
        this.subscriptions = [];
    }

    public ngOnInit(): void {
        this.markerId = (this.activatedRoute.snapshot.data['marker'] as Marker).id;
        this.getComments();

        let subscription = this.userService.userName.subscribe((result: string) => this.userName = result);
        this.subscriptions.push(subscription);

        subscription = this.userService.isAuthenticated.subscribe((result: boolean) => this.isAuthenticated = result);
        this.subscriptions.push(subscription);
    }

    public ngOnDestroy(): void {
        this.subscriptions.forEach(sub => sub.unsubscribe());
    }

    public async addComment(): Promise<void> {
        const newComment = new MarkerComment(this.markerId, this.text);

        this.waitForRequest = true;

        try {
            await this.commentApiService.addComment(newComment).toPromise();
            this.text = '';
            this.getComments();
        } catch (error) {
            const errorMessage = (error.error as BackendErrorResponse).exception.message;
            this.snackBar.open(errorMessage, 'Error!', snackbarConfiguration);
        }

        this.waitForRequest = false;
    }

    public async removeComment(commentId: string): Promise<void> {
        const dialogResult = await this.dialogService
            .confirm('Delete comment', 'Do you want to delete this comment?').toPromise();

        if (dialogResult === true) {
            await this.commentApiService.removeComment(commentId).toPromise();
            this.getComments();
        }
    }

    public async getComments(): Promise<void> {
        this.comments = await this.commentApiService.getCommentsForMarker(this.markerId).toPromise();
    }
}
