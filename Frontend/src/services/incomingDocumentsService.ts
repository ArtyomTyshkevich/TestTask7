import axios from 'axios';
import { IncomingDocumentDTO } from '../types/IncomingDocumentDTO';

const API_BASE = 'https://localhost:8083/api/IncomingDocuments';

export const incomingDocumentsService = {
  getAll: () => axios.get<IncomingDocumentDTO[]>(API_BASE),

  getById: (id: string) => axios.get<IncomingDocumentDTO>(`${API_BASE}/${id}`),

getFiltered: (params: {
  numbers?: string[];
  resourceIds?: string[];
  unitIds?: string[];
  startDate?: string;
  endDate?: string;
}) => {
  const queryParams = new URLSearchParams();

  params.numbers?.forEach(n => queryParams.append('numbers', n));
  params.resourceIds?.forEach(id => queryParams.append('resourceIds', id));
  params.unitIds?.forEach(id => queryParams.append('unitIds', id));
  if (params.startDate) queryParams.append('startDate', params.startDate);
  if (params.endDate) queryParams.append('endDate', params.endDate);

  return axios.get<IncomingDocumentDTO[]>(`${API_BASE}/filtered`, {
    params: queryParams,
  });
},


  create: (data: IncomingDocumentDTO) =>
    axios.post<IncomingDocumentDTO>(API_BASE, data),

  update: (data: IncomingDocumentDTO) =>
    axios.put<IncomingDocumentDTO>(API_BASE, data),

  remove: (id: string) =>
    axios.delete(`${API_BASE}/${id}`),

    getFilters: () => axios.get(`${API_BASE}/filters`),
};
