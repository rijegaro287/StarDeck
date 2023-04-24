import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';

// import { any } from '../Interfaces/ServerResponse';

@Injectable({
  providedIn: 'root'
})
export class RequestService {
  constructor(private httpClient: HttpClient) { }

  get = (url: string): Promise<any> => {
    return new Promise((resolve, reject) => {
      this.httpClient.get<any>(url, { withCredentials: true })
        .subscribe({
          next: (response: any) => resolve(response),
          error: (error: HttpErrorResponse) => reject(error)
        });
    });
  }

  post = (url: string, body: any): Promise<any> => {
    return new Promise((resolve, reject) => {
      this.httpClient.post<any>(url, body, { withCredentials: true })
        .subscribe({
          next: (response: any) => resolve(response),
          error: (error: HttpErrorResponse) => reject(error)
        });
    });
  }

  put = (url: string, body: any): Promise<any> => {
    return new Promise((resolve, reject) => {
      this.httpClient.put<any>(url, body, { withCredentials: true })
        .subscribe({
          next: (response: any) => resolve(response),
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

  putFile = (url: string, file: File): Promise<any> => {
    const formData = new FormData();
    formData.append('file', file);

    return new Promise((resolve, reject) => {
      this.httpClient.put<any>(url, formData, { withCredentials: true })
        .subscribe({
          next: (response: any) => resolve(response),
          error: (error: HttpErrorResponse) => reject(error)
        });
    });
  }

  falseResponse = (response: any): Promise<any> => {
    return new Promise((resolve, reject) => {
      of(response).subscribe({
        next: (response: any) => resolve(response),
        error: (error: HttpErrorResponse) => reject(error)
      });
    });
  }

  handleResponse = async (response: any, reloadOnSuccess?: boolean, callback?: () => void) => {
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
