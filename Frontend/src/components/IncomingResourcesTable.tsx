import React from 'react';
import { IncomingResourceDTO } from '../types/IncomingResourceDTO';
import { ResourceDTO } from '../types/ResourceDTO';
import { UnitDTO } from '../types/UnitDTO';

type Props = {
  resourcesOptions: ResourceDTO[];
  unitsOptions: UnitDTO[];
  items: IncomingResourceDTO[];
  setItems: React.Dispatch<React.SetStateAction<IncomingResourceDTO[]>>;
};

export function IncomingResourcesTable({ resourcesOptions, unitsOptions, items, setItems }: Props) {
  const emptyResource: ResourceDTO = { id: '', name: '', state: 1 };
  const emptyUnit: UnitDTO = { id: '', name: '', state: 1 };

  const handleAdd = () => {
    setItems(prev => [
      ...prev,
      { id: crypto.randomUUID(), resourceDTO: emptyResource, unitDTO: emptyUnit, quantity: 0 },
    ]);
  };

  const handleRemove = (id: string) => {
    setItems(prev => prev.filter(item => item.id !== id));
  };

  const handleChange = (id: string, field: 'resource' | 'unit' | 'quantity', value: string | number) => {
    setItems(prev =>
      prev.map(item => {
        if (item.id !== id) return item;

        if (field === 'resource') {
          const selectedResource = resourcesOptions.find(r => r.name === value);
          return { ...item, resourceDTO: selectedResource ?? emptyResource };
        }
        if (field === 'unit') {
          const selectedUnit = unitsOptions.find(u => u.name === value);
          return { ...item, unitDTO: selectedUnit ?? emptyUnit };
        }
        if (field === 'quantity') {
          return { ...item, quantity: Number(value) };
        }

        return item;
      })
    );
  };

  return (
    <div className="data-table-container">
      <table className="data-table">
        <thead>
          <tr>
            <th className="table-add-col">
              <button onClick={handleAdd} className="btn add-btn" title="Добавить ресурс">+</button>
            </th>
            <th>Ресурс</th>
            <th>Единица измерения</th>
            <th>Количество</th>
          </tr>
        </thead>
        <tbody>
          {items.map(item => (
            <tr key={item.id}>
              <td className="table-remove-col">
                <button onClick={() => handleRemove(item.id)} className="btn delete-btn">&times;</button>
              </td>
              <td>
                <select
                  className="multi-select-input"
                  value={item.resourceDTO.name}
                  onChange={e => handleChange(item.id, 'resource', e.target.value)}
                >
                  <option value="">-- выберите ресурс --</option>
                  {resourcesOptions.map(r => (
                    <option key={r.id} value={r.name}>{r.name}</option>
                  ))}
                </select>
              </td>
              <td>
                <select
                  className="multi-select-input"
                  value={item.unitDTO.name}
                  onChange={e => handleChange(item.id, 'unit', e.target.value)}
                >
                  <option value="">-- выберите единицу --</option>
                  {unitsOptions.map(u => (
                    <option key={u.id} value={u.name}>{u.name}</option>
                  ))}
                </select>
              </td>
              <td>
                <input
                  className="input quantity-input"
                  type="number"
                  min={0}
                  step={1}
                  value={item.quantity}
                  onChange={e => handleChange(item.id, 'quantity', e.target.value)}
                />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
