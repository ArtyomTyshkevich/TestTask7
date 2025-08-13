import { ClientDTO } from "./ClientDTO";
import { OutgoingStateEnum } from "./OutgoingStateEnum";
import { OutgoingResourceDTO } from "./OutgoingResourceDTO";

export interface OutgoingDocumentDTO {
  id: string | null;                 
  number: string;
  clientDTO: ClientDTO;
  date: string;                     
  state: OutgoingStateEnum;
  outgoingResourcesDTO: OutgoingResourceDTO[];
}