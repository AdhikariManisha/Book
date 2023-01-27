import { ErrorHandler, Inject, Injectable, Injector } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class GlobalErrorHandlerService implements ErrorHandler {
  
constructor(@Inject(Injector) private readonly injector: Injector) {}

private get toastService() {
    return this.injector.get(ToastrService);
}

  handleError(error: any) {
    if (error.error) {
      this.toastService.error(error.error.message);
    }
  }
}