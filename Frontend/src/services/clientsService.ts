import axios from 'axios';
import { ClientDTO } from '../types/ClientDTO';
import { DirectoriesStateEnum } from '../types/DirectoriesStateEnum';

const API_BASE = 'https://localhost:8083/api/Clients';

export const clientsService = {
  getAll: () => axios.get<ClientDTO[]>(API_BASE),

  getById: (id: string) => axios.get<ClientDTO>(`${API_BASE}/${id}`),

  create: (data: { id: string | null; name: string; address: string; state: number }) =>
  axios.post<ClientDTO>(API_BASE, data),

  update: (data: Partial<ClientDTO>) =>
  axios.put<ClientDTO>(API_BASE, data),

  remove: (id: string) => axios.delete(`${API_BASE}/${id}`),

  updateStatus: (id: string) =>
    axios.patch(`${API_BASE}/${id}/status`),

  getByState: (state: DirectoriesStateEnum) =>
    axios.get<ClientDTO[]>(`${API_BASE}/state/${state-1}`),
  
};
