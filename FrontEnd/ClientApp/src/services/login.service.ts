import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { UserLogin } from "../models/userLogin";


@Injectable()
export class LoginService {

    baseUrl: string = "https://localhost:44328/";

    constructor(private http: HttpClient) {

    }

    login(user: UserLogin) {
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        let options = {
            headers: headers
        };

        let body = JSON.stringify({
            mail: user.mail,
            password: user.password,
        });

        return this.http.post(this.baseUrl + 'api/User/GetByLogin', body, options);

    }

}

