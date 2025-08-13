import React, { useEffect, useState } from 'react';
import { BalanceDTO } from '../../types/BalanceDTO';
import { balanceService } from '../../services/balanceService';
import { DataTable } from '../../components/DataTable';
import { ColumnDef } from '@tanstack/react-table';
import { MultiSelect, Option } from '../../components/MultiSelect';

const BalancePage: React.FC = () => {
  const [data, setData] = useState<BalanceDTO[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const [resources, setResources] = useState<Option[]>([]);
  const [units, setUnits] = useState<Option[]>([]);

  const [selectedResources, setSelectedResources] = useState<string[]>([]);
  const [selectedUnits, setSelectedUnits] = useState<string[]>([]);

  useEffect(() => {
    balanceService.getFilters()
      .then(res => {
        const filters = res.data;
        setResources(filters.resourceDTOs.map((r: any) => ({ id: r.id!, name: r.name })));
        setUnits(filters.unitDTOs.map((u: any) => ({ id: u.id!, name: u.name })));
      })
      .catch(() => setError('Ошибка загрузки фильтров'));
    handleApply();
  }, []);

  const handleApply = () => {
    setLoading(true);
    balanceService.getFiltered(selectedResources, selectedUnits)
      .then(res => {
        setData(res.data);
        setError(null);
      })
      .catch(() => setError('Ошибка при загрузке данных'))
      .finally(() => setLoading(false));
  };

  const columns: ColumnDef<BalanceDTO>[] = [
    { header: 'Ресурс', accessorFn: row => row.resourceDTO.name, id: 'resource' },
    { header: 'Единица измерения', accessorFn: row => row.unitDTO.name, id: 'unit' },
    { header: 'Количество', accessorFn: row => row.quantity.toString(), id: 'quantity' },
  ];

  return (
    <div>
      <h1>Баланс ресурсов</h1>

      <div className="filters-container">
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
        <button className="btn toggle-btn" onClick={handleApply}>
          Применить
        </button>
      </div>

      {loading && <div>Загрузка...</div>}
      {error && <div className="error">{error}</div>}
      {!loading && !error && <DataTable columns={columns} data={data} />}
    </div>
  );
};

export default BalancePage;
