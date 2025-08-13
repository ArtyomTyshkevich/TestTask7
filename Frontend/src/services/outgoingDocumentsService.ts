import axios from 'axios';
import { OutgoingDocumentDTO } from '../types/OutgoingDocumentDTO';

const API_BASE = 'https://localhost:8083/api/OutgoingDocuments';

export const outgoingDocumentsService = {
  getAll: () => axios.get<OutgoingDocumentDTO[]>(API_BASE),

  getById: (id: string) => axios.get<OutgoingDocumentDTO>(`${API_BASE}/${id}`),

  getFiltered: (params: {
    numbers?: string[];
    resourceIds?: string[];
    unitIds?: string[];
    clientIds?: string[];
    startDate?: string;
    endDate?: string;
  }) => {
    const queryParams = new URLSearchParams();

    params.numbers?.forEach(n => queryParams.append('numbers', n));
    params.resourceIds?.forEach(id => queryParams.append('resourceIds', id));
    params.unitIds?.forEach(id => queryParams.append('unitIds', id));
    params.clientIds?.forEach(id => queryParams.append('clientIds', id));
    if (params.startDate) queryParams.append('startDate', params.startDate);
    if (params.endDate) queryParams.append('endDate', params.endDate);

    return axios.get<OutgoingDocumentDTO[]>(`${API_BASE}/filtered`, {
      params: queryParams,
    });
  },

  create: (data: OutgoingDocumentDTO) =>
    axios.post<OutgoingDocumentDTO>(API_BASE, data),

  update: (data: OutgoingDocumentDTO) =>
    axios.put<OutgoingDocumentDTO>(API_BASE, data),

  remove: (id: string) =>
    axios.delete(`${API_BASE}/${id}`),

  getFilters: () => axios.get(`${API_BASE}/filters`),

  sign: (id: string) =>
    axios.post(`${API_BASE}/${id}/sign`),

  revoke: (id: string) =>
    axios.post(`${API_BASE}/${id}/revoke`),

  createAndSign: (data: OutgoingDocumentDTO) =>
    axios.post<OutgoingDocumentDTO>(`${API_BASE}/create-and-sign`, data), 
  updateAndSign: (data: OutgoingDocumentDTO) =>
    axios.post<OutgoingDocumentDTO>(`${API_BASE}/update-and-sign`, data),
};
