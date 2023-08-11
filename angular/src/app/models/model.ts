export declare class AuditableEntityDto {
    createdDate? : string | Date;
    createdBy?: number;
    updatedDate?: string | Date;
    updatedBy?: number;
}

export declare class BaseEntityDto<TKey = number> extends AuditableEntityDto{
    id?: TKey;
}