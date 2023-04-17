import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';

import { IServerResponse } from '../Interfaces/ServerResponse';

@Injectable({
  providedIn: 'root'
})
export class RequestService {
  constructor(private httpClient: HttpClient) { }

  get = (url: string): Promise<IServerResponse> => {
    return new Promise((resolve, reject) => {
      this.httpClient.get<IServerResponse>(url, { withCredentials: true })
        .subscribe({
          next: (response: IServerResponse) => resolve(response),
          error: (error: HttpErrorResponse) => reject(error)
        });
    });
  }

  post = (url: string, body: any): Promise<IServerResponse> => {
    return new Promise((resolve, reject) => {
      this.httpClient.post<IServerResponse>(url, body, { withCredentials: true })
        .subscribe({
          next: (response: IServerResponse) => resolve(response),
          error: (error: HttpErrorResponse) => reject(error)
        });
    });
  }

  put = (url: string, body: any): Promise<IServerResponse> => {
    return new Promise((resolve, reject) => {
      this.httpClient.put<IServerResponse>(url, body, { withCredentials: true })
        .subscribe({
          next: (response: IServerResponse) => resolve(response),
          error: (error: HttpErrorResponse) => reject(error)
        });
    });
  }

  getFile(url: string): Promise<Blob> {
    return new Promise((resolve, reject) => {
      this.httpClient.get(url, { withCredentials: true, responseType: 'blob' })
        .subscribe({
          next: (response: Blob) => resolve(response),
          error: (error: HttpErrorResponse) => reject(error)
        });
    });
  }

  putFile = (url: string, file: File): Promise<IServerResponse> => {
    const formData = new FormData();
    formData.append('file', file);

    return new Promise((resolve, reject) => {
      this.httpClient.put<IServerResponse>(url, formData, { withCredentials: true })
        .subscribe({
          next: (response: IServerResponse) => resolve(response),
          error: (error: HttpErrorResponse) => reject(error)
        });
    });
  }

  falseResponse = (response: IServerResponse): Promise<IServerResponse> => {
    return new Promise((resolve, reject) => {
      of(response).subscribe({
        next: (response: IServerResponse) => resolve(response),
        error: (error: HttpErrorResponse) => reject(error)
      });
    });
  }

  handleResponse = async (response: IServerResponse, reloadOnSuccess?: boolean, callback?: () => void) => {
    if (response.status === 'error') {
      await alert(response.message);
    }
    else if (response.redirect) {
      window.location.href = response.redirect!;
    }
    else if (reloadOnSuccess) {
      window.location.reload();
    }

    if (callback) { return callback(); }
  }
}
