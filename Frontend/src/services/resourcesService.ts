import axios from 'axios';
import { ResourceDTO } from '../types/ResourceDTO';
import { DirectoriesStateEnum } from '../types/DirectoriesStateEnum';

const API_BASE = 'https://localhost:8083/api/Resources';

export const resourcesService = {
  getAll: () => axios.get<ResourceDTO[]>(API_BASE),

  getById: (id: string) => axios.get<ResourceDTO>(`${API_BASE}/${id}`),

  create: (data: { id: string | null; name: string; state: number }) =>
    axios.post<ResourceDTO>(API_BASE, data),
  
  update: (data: Partial<ResourceDTO>) =>
  axios.put<ResourceDTO>(API_BASE, data),

  remove: (id: string) => axios.delete(`${API_BASE}/${id}`),

  updateStatus: (id: string) =>
    axios.patch(`${API_BASE}/${id}/status`),

  getByState: (state: DirectoriesStateEnum) =>
    axios.get<ResourceDTO[]>(`${API_BASE}/state/${state-1}`),
};
