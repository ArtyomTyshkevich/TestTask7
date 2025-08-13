import { ResourceDTO } from "./ResourceDTO";
import { UnitDTO } from "./UnitDTO";

export interface OutgoingResourceDTO {
  id: string | null;           
  resourceDTO: ResourceDTO;
  unitDTO: UnitDTO;
  quantity: number;
}
