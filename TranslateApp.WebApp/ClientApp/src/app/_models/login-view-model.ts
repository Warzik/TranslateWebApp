import { ILoginViewModel } from '../_interfaces';

export class LoginViewModel implements ILoginViewModel {
    id: number;
    username: string;
    password: string;
    firstName: string;
    lastName: string;
    token?: string;
}
