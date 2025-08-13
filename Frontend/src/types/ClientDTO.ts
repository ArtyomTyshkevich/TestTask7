import { DirectoriesStateEnum } from "./DirectoriesStateEnum";

export interface ClientDTO {
  id: string | null;
  name: string;
  address: string;
  state: DirectoriesStateEnum;
}
