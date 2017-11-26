import { BaseResponse } from './base-response.model';

export class BackendErrorResponse extends BaseResponse {
    public exception: { Message: string };
}
