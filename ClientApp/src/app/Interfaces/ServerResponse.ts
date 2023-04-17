interface IServerResponse<T = any> {
  status: 'ok' | 'error';
  body?: T;
  message?: string;
  redirect?: string;
}

export {
  IServerResponse
}