import { DirectoriesStateEnum } from "./DirectoriesStateEnum";

export interface ResourceDTO {
  id: string | null;
  name: string;
  state: DirectoriesStateEnum;
}
