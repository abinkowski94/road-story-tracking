import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { environment } from '../../../environments/environment';
import { TokenInfo } from './../models/data/token-info.model';
import { BackendSuccessResponse } from './../models/responses/success-response.model';

@Injectable()
export abstract class BaseHttpService {

    private httpEndpoint: string;

    protected constructor(private client: HttpClient, controllerName: string, actionName: string) {
        this.httpEndpoint = `${environment.backendHotst}/api/${controllerName}/${actionName}`;
    }

    protected get<T>(httpParams?: HttpParams): Observable<T> {
        return this.extractResult(this.client.get<BackendSuccessResponse<T>>(this.httpEndpoint, {
            params: httpParams
        }));
    }

    protected post<T>(body: any, httpParams?: HttpParams): Observable<T> {
        return this.extractResult(this.client.post<BackendSuccessResponse<T>>(this.httpEndpoint, body, {
            params: httpParams
        }));
    }

    protected put<T>(body: any, httpParams?: HttpParams): Observable<T> {
        return this.extractResult(this.client.put<BackendSuccessResponse<T>>(this.httpEndpoint, body, {
            params: httpParams
        }));
    }

    protected delete<T>(httpParams?: HttpParams): Observable<T> {
        return this.extractResult(this.client.delete<BackendSuccessResponse<T>>(this.httpEndpoint, {
            params: httpParams
        }));
    }

    private extractResult<T>(result: Observable<BackendSuccessResponse<T>>): Observable<T> {
        return result.map((response: BackendSuccessResponse<T>) => {
            return response.result;
        });
    }
}
