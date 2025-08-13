import React from 'react';
import { BalanceDTO } from '../types/BalanceDTO';

type Props = {
  balances: BalanceDTO[];
  quantities: { [key: string]: number };
  setQuantities: React.Dispatch<React.SetStateAction<{ [key: string]: number }>>;
  disabled?: boolean;
};

export function OutgoingResourcesTable({ balances, quantities, setQuantities, disabled }: Props) {
  const handleQuantityChange = (balanceId: string, value: string) => {
    const num = Number(value);
    setQuantities(prev => ({ ...prev, [balanceId]: isNaN(num) ? 0 : num }));
  };

  return (
    <div className="data-table-container">
      <table className="data-table">
        <thead>
          <tr>
            <th>Ресурс</th>
            <th>Единица измерения</th>
            <th>Количество</th>
            <th>Доступно</th>
          </tr>
        </thead>
        <tbody>
          {balances.map(b => {
            const key = b.id; 
            return (
              <tr key={key}>
                <td>{b.resourceDTO.name}</td>
                <td>{b.unitDTO.name}</td>
                <td>
                  <input
                    className="input quantity-input"
                    type="number"
                    min="0"
                    value={quantities[key] ?? 0} 
                    onChange={e => handleQuantityChange(key, e.target.value)}
                    disabled={disabled}
                  />
                </td>
                <td>{b.quantity}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}
