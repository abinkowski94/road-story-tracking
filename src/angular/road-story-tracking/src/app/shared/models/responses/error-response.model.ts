import { BaseResponse } from './base-response.model';
import { BackendException } from './../exceptions/backend-exception.model';

export class BackendErrorResponse extends BaseResponse {
    public exception: BackendException;
}
