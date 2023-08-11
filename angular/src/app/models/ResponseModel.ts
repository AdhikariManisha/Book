export interface ResponseModal<T> {
    success: boolean;
    data: T;
    errors : ValidationResult[];
}

export interface ValidationResult{
   errorMessage : string;
   memberNames : string []; 
}