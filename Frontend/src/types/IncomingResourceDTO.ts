import { ResourceDTO } from './ResourceDTO';
import { UnitDTO } from './UnitDTO';

export interface IncomingResourceDTO {
  id: string;
  resourceDTO: ResourceDTO;
  unitDTO: UnitDTO;
  quantity: number;
}
