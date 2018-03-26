import { Subscription } from 'rxjs/Subscription';
import { MatSnackBar } from '@angular/material';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';

import { CommentApiService } from '../../services/comment-api.service';
import { UserService } from './../../../shared/services/user/user.service';
import { DialogService } from './../../../shared/services/dialog/dialog.service';
import { MarkerComment } from '../../../shared/models/data/comment/comment.model';
import { BackendErrorResponse } from './../../../shared/models/responses/error-response.model';

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
        this.comments = [];
    }

    public ngOnInit(): void {
        let subscription = this.activatedRoute.paramMap.subscribe(params => {
            this.markerId = params.get('id');
            this.getComments();
        });
        this.subscriptions.push(subscription);

        subscription = this.userService.userName.subscribe((result: string) => this.userName = result);
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

        }

        this.waitForRequest = false;
    }

    public async removeComment(commentId: string): Promise<void> {
        const dialogResult = await this.dialogService
            .confirm('Delete comment', 'Do you want to delete this comment?')
            .toPromise();

        if (dialogResult === true) {
            await this.commentApiService.removeComment(commentId).toPromise();
            this.getComments();
        }
    }

    public async getComments(): Promise<void> {
        this.comments = await this.commentApiService.getCommentsForMarker(this.markerId).toPromise();
    }
}
