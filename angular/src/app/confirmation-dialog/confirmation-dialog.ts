import { Component } from "@angular/core";
import { MatDialogRef } from "@angular/material/dialog";

@Component({
    selector: 'Confirm-dialog', 
    templateUrl: './confirmation-dialog.html'
})
export class ConfirmationDialog{
    public confirmMessage: string = '';
    constructor(public dialogRef: MatDialogRef<ConfirmationDialog>){

    }
}