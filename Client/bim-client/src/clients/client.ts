import { BimClient } from "./client.generated";

export class Client extends BimClient{
    constructor(){
        super('https://localhost:7207')
    }
}