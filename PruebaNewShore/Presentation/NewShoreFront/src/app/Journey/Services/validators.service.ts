import { Injectable } from "@angular/core";
import { ValidationErrors, FormGroup } from '@angular/forms';

@Injectable({
    providedIn: 'root'
  })
export class ValidatorService{

    isFieldNotEquals(field1: string, field2: string){
        return (formGroup: FormGroup): ValidationErrors | null => {
            const fieldValued1 = (formGroup.get(field1)?.value).toUpperCase();
            const fieldValued2 = formGroup.get(field2)?.value.toUpperCase();
            
            if(fieldValued1 === fieldValued2){
                formGroup.get(field2)?.setErrors({ notEqual: true });
                return { noEqual: true }
            }

            formGroup.get(field2)?.setErrors(null);
            return null;
        }
    }
    
}