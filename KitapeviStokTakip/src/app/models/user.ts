import { Role } from "./role";

export class User {
    userId: number;
    email: string;
    username: string;
    password: string;
    authdata?: string;
    token?: string;
    role: Role;
}