import { BaseResponse } from './base-response.model';

export class BackendSuccessResponse<T> extends BaseResponse {
    public result: T;
}
