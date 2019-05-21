export class Address {
  constructor(
    public id: number,
    public streetLine1: string,
    public streetLine2: string,
    public companyName:string,
    public city: string,
    public zipCode:string,
    public state: string,
    public active: boolean

  ) { }
}

