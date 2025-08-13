import { IncomingResourceDTO } from './IncomingResourceDTO';

export interface IncomingDocumentDTO {
  id: string | null;
  number: string;
  date: string; 
  incomingResourcesDTO: IncomingResourceDTO[];
}
