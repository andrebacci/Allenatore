import { Component, Injectable, HostListener } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ResultData } from '../../models/resultData';
import { LoginService } from '../../services/login.service';
import { UserLogin } from '../../models/userLogin';
import { UtilityService } from '../../services/utility.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})

export class LoginComponent {

    loginEmail: string = "";
    loginPassword: string = "";

    sub: any;
    
    chekValidate: boolean = true;
    errorNamePass: boolean = false;
    sideBarVisibility: boolean = false;

    constructor(private loginService: LoginService, private router: Router, private route: ActivatedRoute, private utilityService: UtilityService,) {

    }

    login(email: string, password: string): void {
        let user = new UserLogin;

        user.password = password;
        user.mail = email;

        this.loginService.login(user).subscribe(res => {
            var resultData = res as ResultData;
            if (resultData.status) {
                var result = resultData.data as UserLogin

                localStorage.setItem('readOnly', (result.role == 1 ? false : true).toString());
                
                this.router.navigate(['']);
                //localStorage visibility readOnly or not
            } else {
                this.errorNamePass = true;
            }
        });
    }
}
