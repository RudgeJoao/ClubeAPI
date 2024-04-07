import { Injectable } from '@angular/core';
import { Client } from '../models/client';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  constructor() { }

  public getClients() : Client[]{ //getClients() retorna um array de Client
    let client = new Client();
    client.id = 1;
    client.cpf = "53384548825";
    client.name = "João"
    client.phoneNumber = "(11) 941124344"

    return [client];
  }
}
