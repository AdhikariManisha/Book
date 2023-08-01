export interface UserLoginDto {
    userName: string;
    password: string;
}
export interface TokenDto{
    token : string;
    expiration : Date;
}