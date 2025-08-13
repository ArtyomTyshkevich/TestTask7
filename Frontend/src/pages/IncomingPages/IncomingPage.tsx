import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { IncomingDocumentDTO } from '../../types/IncomingDocumentDTO';
import { incomingDocumentsService } from '../../services/incomingDocumentsService';
import { IncomingDocumentsTable } from '../../components/IncomingDocumentsTable';
import { MultiSelect, Option } from '../../components/MultiSelect';
import { DateRangePicker } from '../../components/DateRangePicker';

export function IncomingPage() {
  const navigate = useNavigate(); // инициализируем

  const [data, setData] = useState<IncomingDocumentDTO[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const [resources, setResources] = useState<Option[]>([]);
  const [units, setUnits] = useState<Option[]>([]);
  const [numbers, setNumbers] = useState<Option[]>([]);

  const [selectedResources, setSelectedResources] = useState<string[]>([]);
  const [selectedUnits, setSelectedUnits] = useState<string[]>([]);
  const [selectedNumbers, setSelectedNumbers] = useState<string[]>([]);

  const [startDate, setStartDate] = useState<string>('');
  const [endDate, setEndDate] = useState<string>('');

  useEffect(() => {
    incomingDocumentsService.getFilters()
      .then(res => {
        const filters = res.data;
        setResources(filters.resourceDTOs.map((r: any) => ({ id: r.id!, name: r.name })));
        setUnits(filters.unitDTOs.map((u: any) => ({ id: u.id!, name: u.name })));
        setNumbers(filters.incomingDocumentDTOs.map((d: any) => ({ id: d.number, name: d.number })));
      })
      .catch(() => setError('Ошибка загрузки фильтров'));

    handleApply();
  }, []);

  const handleApply = () => {
    setLoading(true);
    incomingDocumentsService.getFiltered({
      numbers: selectedNumbers,
      resourceIds: selectedResources,
      unitIds: selectedUnits,
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
    navigate('/incoming/add');
  };

  return (
    <div>
      <h1>Поступившие документы</h1>

      <div className="filters-container" style={{ display: 'flex', gap: 12, flexWrap: 'wrap', marginBottom: 16 }}>
        <DateRangePicker
          startDate={startDate}
          endDate={endDate}
          onStartDateChange={setStartDate}
          onEndDateChange={setEndDate}
        />

        <MultiSelect
          label="Номера поступлений"
          options={numbers}
          selected={selectedNumbers}
          onChange={setSelectedNumbers}
        />
        <MultiSelect
          label="Ресурс"
          options={resources}
          selected={selectedResources}
          onChange={setSelectedResources}
        />
        <MultiSelect
          label="Единица измерения"
          options={units}
          selected={selectedUnits}
          onChange={setSelectedUnits}
        />
        </div>
        <div style={{ marginBottom: '20px' }}>
        <button className="btn toggle-btn" onClick={handleApply} style={{ alignSelf: 'flex-end', marginInline: '5px' } }>
            Применить
        </button>
        <button className="btn add-btn" onClick={handleAdd} style={{ alignSelf: 'flex-end' }}>
            Добавить
        </button>
        </div>

      {loading && <div>Загрузка...</div>}
      {error && <div className="error">{error}</div>}
      {!loading && !error && <IncomingDocumentsTable data={data} loading={loading} error={error} />}
    </div>
  );
}
