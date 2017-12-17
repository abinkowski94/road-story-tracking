import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { environment } from 'environments/environment';
import { TokenInfo } from 'shared/models/data/token/token-info.model';
import { BackendSuccessResponse } from 'shared/models/responses/success-response.model';

@Injectable()
export abstract class BaseHttpService {

    private httpEndpoint: string;

    protected constructor(private client: HttpClient, private controllerName: string) {
        this.httpEndpoint = `${environment.backendHotst}api/${controllerName}/`;
    }

    protected get<T>(actionName: string, httpParams?: HttpParams): Observable<T> {
        return this.extractResult(this.client.get<BackendSuccessResponse<T>>(this.httpEndpoint + actionName, {
            params: httpParams
        }));
    }

    protected post<T>(actionName: string, body: any, httpParams?: HttpParams): Observable<T> {
        return this.extractResult(this.client.post<BackendSuccessResponse<T>>(this.httpEndpoint + actionName, body, {
            params: httpParams
        }));
    }

    protected put<T>(actionName: string, body: any, httpParams?: HttpParams): Observable<T> {

        return this.extractResult(this.client.put<BackendSuccessResponse<T>>(this.httpEndpoint + actionName, body, {
            params: httpParams
        }));
    }

    protected delete<T>(actionName: string, httpParams?: HttpParams): Observable<T> {
        return this.extractResult(this.client.delete<BackendSuccessResponse<T>>(this.httpEndpoint + actionName, {
            params: httpParams
        }));
    }

    private extractResult<T>(result: Observable<BackendSuccessResponse<T>>): Observable<T> {
        return result.map((response: BackendSuccessResponse<T>) => {
            return response.result;
        });
    }
}
