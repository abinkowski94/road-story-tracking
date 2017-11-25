export class CustomResponse<T> {
    public id: string;
    public result: T;
    public exception: { Message: string, ClassName: string };
}
