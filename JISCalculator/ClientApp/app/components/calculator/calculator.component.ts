import { Component, Inject } from '@angular/core';
import { Headers, Http, RequestOptionsArgs } from "@angular/http";


@Component({
    selector: 'calculator',
    templateUrl: './calculator.component.html',
    styleUrls: ['./calculator.component.css']
})
export class CalculatorComponent {
    public calculationString: string;
    public displayCalculation: string;
    private submissionUrl: string;
    private headers = new Headers();

    constructor(@Inject('BASE_URL') baseUrl: string, public http: Http) {
        this.submissionUrl = baseUrl + 'api/Calculator/CalculateExpression';
        this.calculationString = "";
        this.headers.append('Content-Type', 'application/json');
    }

    public handleInput(source: string, value: string) {
        if (value === "clear") {
            this.calculationString = "";
            return;
        }
        
        value = this.calculationString + value;
        this.calculationString = value;
    }

    public submit() {
        var t = this.calculationString;
        this.http.post(this.submissionUrl, {Expression: this.calculationString}, { headers: this.headers } as RequestOptionsArgs).subscribe(
            result => {
                this.calculationString = result.json().toString();
            },
            error => {
                // todo handle errors
                console.error(error)
            }
        );
    }

}
