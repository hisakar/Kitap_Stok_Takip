import { User } from "./user";

export class Demand{
    demandId:number;
    user: User;
    bookName: string;
    writer: string;
    requestedDate: string;
}