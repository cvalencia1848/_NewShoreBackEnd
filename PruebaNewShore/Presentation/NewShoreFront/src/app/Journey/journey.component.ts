import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { JourneyService } from "./Services/journey.service";
import { JourneyDTo } from './models/journeyDTo';
import { Journey } from './models/journey';
import { ValidatorService } from "./Services/validators.service";
import { Currency } from './models/currency';

@Component({
    selector: 'app-journey',
    styleUrls: ['journey.component.css'],
    templateUrl: 'journey.component.html'
})
export class JourneyComponent implements OnInit {
    
    constructor(private fb: FormBuilder, private httpService: JourneyService, 
        private validatorService: ValidatorService){}
    
    ngOnInit(){
        this.currencyResponse = new Currency();
    }
     
    journey: Journey;
    journeyResponse: Journey;
    journeyDTo = new JourneyDTo;
    responseData: boolean = false;
    currencyResponse: Currency;
    priceShow = 0;

    currency = [
        {value: 'EUR', viewValue: 'Euro'},
        {value: 'JPY', viewValue: 'Japanese yen'},
        {value: 'GBP', viewValue: 'Pound sterling'}
    ];

    public myForm: FormGroup = this.fb.group(
        {
            Origin: new FormControl('',[Validators.required, Validators.maxLength(3)]),
            Destination: new FormControl('',[Validators.required, Validators.maxLength(3)])
        },{
            validators: [
                this.validatorService.isFieldNotEquals('Origin', 'Destination')
            ]
        }
    );


    getRoute(){
        if(this.myForm.invalid)
        {
            this.myForm.markAllAsTouched();
            return;
        }
        this.responseData = false;
        this.journeyDTo = this.currentJourneyDTo;
        this.getJourney(this.journeyDTo);
    }

    get currentJourneyDTo(): JourneyDTo{
        const journeyDTo = this.myForm.value as JourneyDTo;
        return journeyDTo;
    }

    getJourney(dto: JourneyDTo){
        this.journey = new Journey();
        this.httpService.getFligths(dto).subscribe(
            (response) => {
                this.journey = response;
                this.responseData = true;
                this.calculatePrice();
            }
        );
    }

    changeClient(value:string){
        this.httpService.getCurrency(value).subscribe(
            response => {
                this.currencyResponse = response;
                this.calculatePrice();
            }
        );
    }

    calculatePrice(){
        this.priceShow = +this.currencyResponse.value * this.journey.price;
    }

    checkFieldsEquality(group: FormGroup) {
        const field1 = group.controls['Origin'].value;
        const field2 = group.controls['Destination'].value;
      
        return field1 !== field2 ? null : { fieldsNotDifferent: true };
      }
}

