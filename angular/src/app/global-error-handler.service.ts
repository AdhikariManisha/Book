import { ErrorHandler, Inject, Injectable, Injector } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ResponseModal, ValidationResult } from './models/ResponseModel';

@Injectable()
export class GlobalErrorHandlerService implements ErrorHandler {

  constructor(@Inject(Injector) private readonly injector: Injector) { }

  private get toastService() {
    return this.injector.get(ToastrService);
  }

  handleError(e: any) {
    let error = e?.error as ResponseModal<object>;
    for (let e1 of error.errors) {
      this.toastService.error(e1?.errorMessage);
    }
    // this.toastService.error(error?.error?.errors[0]?.errorMessage);
  }
}