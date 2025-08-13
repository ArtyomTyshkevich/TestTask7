import axios from 'axios';
import { BalanceDTO } from '../types/BalanceDTO';

const API_BASE = 'https://localhost:8083/api/Balances';

export const balanceService = {
  getFiltered: (resourcesId: string[] = [], unitsId: string[] = []) => {
    const params = new URLSearchParams();
    resourcesId.forEach(id => params.append('resourcesId', id));
    unitsId.forEach(id => params.append('unitsId', id));
    return axios.get<BalanceDTO[]>(`${API_BASE}/filtered`, { params });
  },

  getFilters: () => axios.get(`${API_BASE}/filters`),
};
