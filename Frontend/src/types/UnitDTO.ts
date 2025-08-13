import { DirectoriesStateEnum } from "./DirectoriesStateEnum";
export interface UnitDTO {
  id: string | null;
  name: string;
  state: DirectoriesStateEnum;
}
