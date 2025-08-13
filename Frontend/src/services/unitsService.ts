import axios from 'axios';
import { UnitDTO } from '../types/UnitDTO';
import { DirectoriesStateEnum } from '../types/DirectoriesStateEnum';

const API_BASE = 'https://localhost:8083/api/Units';

export const unitsService = {
  getAll: () => axios.get<UnitDTO[]>(API_BASE),

  getById: (id: string) => axios.get<UnitDTO>(`${API_BASE}/${id}`),

  create: (data: { id: string | null; name: string; state: number }) =>
  axios.post<UnitDTO>(API_BASE, data),

  update: (data: Partial<UnitDTO>) =>
  axios.put<UnitDTO>(API_BASE, data),

  remove: (id: string) => axios.delete(`${API_BASE}/${id}`),

  updateStatus: (id: string) =>
    axios.patch(`${API_BASE}/${id}/status`),

  getByState: (state: DirectoriesStateEnum) =>
    axios.get<UnitDTO[]>(`${API_BASE}/state/${state-1}`),
};
