import { IRegisterViewModel } from '../_interfaces';

export class RegisterViewModel implements IRegisterViewModel {
  email: string;
  password: string;
  confirmPassword: string;
}
