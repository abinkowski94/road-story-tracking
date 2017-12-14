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

    private subscription: Subscription;
    public markerId: string;
    public userName: string;
    public isAuthenticated: boolean;
    public comments: MarkerComment[];
    public waitForRequest: boolean;
    public text: string;

    public constructor(private commentApiService: CommentApiService, private activatedRoute: ActivatedRoute,
        private snackBar: MatSnackBar, private userService: UserService, private dialogService: DialogService) { }

    public ngOnInit(): void {
        this.subscription = this.activatedRoute.paramMap.subscribe((params: ParamMap) => {
            this.markerId = params.get('id');
            this.getComments();
        });

        this.userService.userName.subscribe((result: string) => this.userName = result);
        this.userService.isAuthenticated.subscribe((result: boolean) => this.isAuthenticated = result);
    }

    public ngOnDestroy(): void {
        this.subscription.unsubscribe();
    }

    public addComment() {
        const newComment = new MarkerComment();
        newComment.markerId = this.markerId;
        newComment.text = this.text;

        this.waitForRequest = true;

        this.commentApiService.addComment(newComment).subscribe((comment: MarkerComment) => {
            this.text = '';
            this.getComments();
        }, error => this.waitForRequest = false,
            () => this.waitForRequest = false);
    }

    public updateComment(comment: MarkerComment) {
        this.commentApiService.addComment(comment).subscribe((resultComment: MarkerComment) => {
            this.getComments();
        });
    }

    public removeComment(commentId: string) {
        this.dialogService.confirm('Delete comment', 'Do you want to delete this comment?').subscribe(result => {
            if (result === true) {
                this.commentApiService.removeComment(commentId).subscribe((comment: MarkerComment) => {
                    this.getComments();
                });
            }
        });
    }

    public getComments(): void {
        this.commentApiService.getCommentsForMarker(this.markerId).subscribe((comments: MarkerComment[]) => {
            this.comments = comments;
        });
    }
}
