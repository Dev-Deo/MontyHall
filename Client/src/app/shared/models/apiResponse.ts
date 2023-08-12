export interface IApiResponse<T> {
  code: String;
  isSuccess: Boolean;
  message: String;
  data: T;
}
