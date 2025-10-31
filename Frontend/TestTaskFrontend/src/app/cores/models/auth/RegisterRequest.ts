import { Roles } from '../../enums/Roles'; 

export interface RegisterRequest {
  nickname: string;
  email: string;
  password: string;
  passwordConfirm: string;
  role: Roles;
}
