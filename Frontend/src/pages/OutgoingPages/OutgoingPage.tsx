// src/pages/OutgoingDocumentsPage.tsx
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { ColumnDef } from '@tanstack/react-table';
import { DataTable } from '../../components/DataTable';
import { MultiSelect, Option } from '../../components/MultiSelect';
import { DateRangePicker } from '../../components/DateRangePicker';
import { outgoingDocumentsService } from '../../services/outgoingDocumentsService';
import { OutgoingDocumentDTO } from '../../types/OutgoingDocumentDTO';
import { OutgoingResourceDTO } from '../../types/OutgoingResourceDTO';
import { OutgoingStateEnum } from '../../types/OutgoingStateEnum';
import { log } from 'console';

export function OutgoingPage() {
  const navigate = useNavigate();

  const [data, setData] = useState<OutgoingDocumentDTO[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const [resources, setResources] = useState<Option[]>([]);
  const [units, setUnits] = useState<Option[]>([]);
  const [numbers, setNumbers] = useState<Option[]>([]);
  const [clients, setClients] = useState<Option[]>([]);

  const [selectedResources, setSelectedResources] = useState<string[]>([]);
  const [selectedUnits, setSelectedUnits] = useState<string[]>([]);
  const [selectedNumbers, setSelectedNumbers] = useState<string[]>([]);
  const [selectedClients, setSelectedClients] = useState<string[]>([]);

  const [startDate, setStartDate] = useState<string>('');
  const [endDate, setEndDate] = useState<string>('');

  useEffect(() => {
    outgoingDocumentsService.getFilters()
      .then(res => {
        const filters = res.data;
        setResources(filters.resourceDTOs.map((r: any) => ({ id: r.id!, name: r.name })));
        setUnits(filters.unitDTOs.map((u: any) => ({ id: u.id!, name: u.name })));
        setNumbers(filters.outgoingDocumentDTOs.map((d: any) => ({ id: d.number, name: d.number })));
        setClients(filters.clientDTOs.map((c: any) => ({ id: c.id!, name: c.name })));
      })
      .catch(() => setError('Ошибка загрузки фильтров'));

    handleApply();
  }, []);

  const handleApply = () => {
    setLoading(true);
    outgoingDocumentsService.getFiltered({
      numbers: selectedNumbers,
      resourceIds: selectedResources,
      unitIds: selectedUnits,
      clientIds: selectedClients,
      startDate: startDate || undefined,
      endDate: endDate || undefined,
    })
      .then(res => {
        setData(res.data);
        setError(null);
      })
      .catch(() => setError('Ошибка при загрузке данных'))
      .finally(() => setLoading(false));
  };

  const handleAdd = () => {
    navigate('/outgoing/add');
  };

  const columns: ColumnDef<OutgoingDocumentDTO>[] = [
    { header: 'Номер', accessorKey: 'number' },
    {
      header: 'Дата',
      accessorKey: 'date',
      cell: info => new Date(info.getValue<string>()).toLocaleDateString(),
    },
    { header: 'Клиент', accessorFn: row => row.clientDTO?.name || '' },
    {
      header: 'Статус',
      accessorKey: 'state',
      cell: info => {
        const state = info.getValue<OutgoingStateEnum>();
        return state === OutgoingStateEnum.signed ? 'Подписан' : 'Не подписан';
      },
    },
    {
      header: 'Ресурс',
      accessorFn: row => row.outgoingResourcesDTO?.map((r: OutgoingResourceDTO) => r.resourceDTO.name).join(', ') || '',
    },
    {
      header: 'Единица измерения',
      accessorFn: row => row.outgoingResourcesDTO?.map((r: OutgoingResourceDTO) => r.unitDTO.name).join(', ') || '',
    },
    {
      header: 'Количество',
      accessorFn: row => row.outgoingResourcesDTO?.map((r: OutgoingResourceDTO) => r.quantity).join(', ') || '',
    },
  ];

  return (
    <div>
      <h1>Исходящие документы</h1>

      <div className="filters-container" style={{ display: 'flex', gap: 12, flexWrap: 'wrap', marginBottom: 16 }}>
        <DateRangePicker
          startDate={startDate}
          endDate={endDate}
          onStartDateChange={setStartDate}
          onEndDateChange={setEndDate}
        />

        <MultiSelect label="Номер документа" options={numbers} selected={selectedNumbers} onChange={setSelectedNumbers} />
        <MultiSelect label="Клиент" options={clients} selected={selectedClients} onChange={setSelectedClients} />
        <MultiSelect label="Ресурс" options={resources} selected={selectedResources} onChange={setSelectedResources} />
        <MultiSelect label="Единица измерения" options={units} selected={selectedUnits} onChange={setSelectedUnits} />
         </div>
        <div style={{ marginBottom: '20px' }}>
        <button className="btn toggle-btn" onClick={handleApply} style={{ alignSelf: 'flex-end',  marginInline: '5px' }}>
            Применить
        </button>
        <button className="btn add-btn" onClick={handleAdd} style={{ alignSelf: 'flex-end' }}>
            Добавить
        </button>
        </div>


      {loading && <div>Загрузка...</div>}
      {error && <div className="error">{error}</div>}
      {!loading && !error && (
        <DataTable
  columns={columns}
  data={data}
  getRowLink={(row) => {
     console.log('row.state:', row.state);
    if (row.state === 2) {
      return `/outgoing/edit/${row.id}`;
    } else if (row.state === 1) {
      return `/outgoing/view/${row.id}`;
    } else {
      return `/outgoing`;
    }
  }}
/>
      )}
    </div>
  );
}
