import { ResourceDTO } from './ResourceDTO';
import { UnitDTO } from './UnitDTO';

export interface BalanceDTO {
  id: string;      
  resourceDTO: ResourceDTO;
  unitDTO: UnitDTO;
  quantity: number;
}
