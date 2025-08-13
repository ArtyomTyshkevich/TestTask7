import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { clientsService } from '../../services/clientsService';
import { balanceService } from '../../services/balanceService';
import { outgoingDocumentsService } from '../../services/outgoingDocumentsService';
import { ClientDTO } from '../../types/ClientDTO';
import { BalanceDTO } from '../../types/BalanceDTO';
import { OutgoingResourcesTable } from '../../components/OutgoingResourcesTable';
import { OutgoingDocumentDTO } from '../../types/OutgoingDocumentDTO';
import { OutgoingResourceDTO } from '../../types/OutgoingResourceDTO';
import { OutgoingStateEnum } from '../../types/OutgoingStateEnum';

const OutgoingAddPage: React.FC = () => {
  const navigate = useNavigate();

  const [clientId, setClientId] = useState<string>('');
  const [number, setNumber] = useState<string>('');
  const [date, setDate] = useState<string>(() => new Date().toISOString().slice(0, 10));
  const [balances, setBalances] = useState<BalanceDTO[]>([]);
  const [quantities, setQuantities] = useState<{ [key: string]: number }>({});
  const [clients, setClients] = useState<ClientDTO[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const clientsRes = await clientsService.getByState(1);
        setClients(clientsRes.data);

        const balancesRes = await balanceService.getFiltered();
        setBalances(balancesRes.data);

        const initialQuantities: { [key: string]: number } = {};
        balancesRes.data.forEach(b => { initialQuantities[b.id] = 0; });
        setQuantities(initialQuantities);
      } catch {
        setError('Ошибка загрузки данных');
      }
    };
    fetchData();
  }, []);

  const prepareDocumentData = (): OutgoingDocumentDTO | null => {
    setError(null);

    if (!clientId) {
      setError('Выберите клиента');
      return null;
    }
    if (!number.trim()) {
      setError('Введите номер');
      return null;
    }
    if (!date) {
      setError('Выберите дату');
      return null;
    }

    const selectedResources: OutgoingResourceDTO[] = Object.entries(quantities)
      .map(([balanceId, qty]) => {
        const quantity = Number(qty);
        const balance = balances.find(b => b.id === balanceId);
        if (!balance || quantity <= 0) return null;
        return {
          id: null,
          balanceId,
          quantity,
          resourceDTO: balance.resourceDTO,
          unitDTO: balance.unitDTO,
        } as OutgoingResourceDTO;
      })
      .filter((r): r is OutgoingResourceDTO => r !== null);

    if (selectedResources.length === 0) {
      setError('Укажите количество хотя бы для одного ресурса');
      return null;
    }

    const client = clients.find(c => c.id === clientId);
    if (!client) {
      setError('Клиент не найден');
      return null;
    }

    return {
      id: null,
      number,
      clientDTO: client,
      date,
      state: OutgoingStateEnum.unsigned,
      outgoingResourcesDTO: selectedResources,
    };
  };

  const handleSave = async () => {
    const data = prepareDocumentData();
    if (!data) return;

    setLoading(true);
    try {
      await outgoingDocumentsService.create(data);
      navigate('/outgoing');
    } catch {
      setError('Ошибка при создании документа');
    } finally {
      setLoading(false);
    }
  };

  const handleCreateAndSign = async () => {
    const data = prepareDocumentData();
    if (!data) return;

    setLoading(true);
    try {
      await outgoingDocumentsService.createAndSign(data);
      navigate('/outgoing');
    } catch {
      setError('Ошибка при создании и подписании документа');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="add-resource-page">
      <div style={{ marginBottom: 16 }}>
        <button
          onClick={handleSave}
          className="btn save-btn"
          disabled={loading}
          style={{ marginRight: 8 }}
        >
          {loading ? 'Сохраняем...' : 'Сохранить'}
        </button>

        <button
          onClick={handleCreateAndSign}
          className="btn save-btn"
          disabled={loading}
        >
          {loading ? 'Сохраняем и подписываем...' : 'Сохранить и подписать'}
        </button>
      </div>

      <div className="form-group">
        <label htmlFor="number">Номер</label>
        <input
          id="number"
          type="text"
          value={number}
          onChange={e => setNumber(e.target.value)}
          disabled={loading}
          className="input"
        />
      </div>

      <div className="form-group">
        <label htmlFor="client">Клиент</label>
        <select
          id="client"
          value={clientId}
          onChange={e => setClientId(e.target.value)}
          disabled={loading}
          className="input"
        >
          <option value="">Выберите клиента</option>
          {clients.map(c => (
            <option key={c.id} value={c.id ?? ''}>
              {c.name}
            </option>
          ))}
        </select>
      </div>

      <div className="form-group">
        <label htmlFor="date">Дата</label>
        <input
          id="date"
          type="date"
          value={date}
          onChange={e => setDate(e.target.value)}
          disabled={loading}
          className="input"
        />
      </div>

      <h3>Ресурсы</h3>
      <OutgoingResourcesTable
        balances={balances}
        quantities={quantities}
        setQuantities={setQuantities}
        disabled={loading}
      />

      {error && <p className="error-message">{error}</p>}
    </div>
  );
};

export default OutgoingAddPage;
