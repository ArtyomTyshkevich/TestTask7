import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { clientsService } from '../../services/clientsService';
import { balanceService } from '../../services/balanceService';
import { outgoingDocumentsService } from '../../services/outgoingDocumentsService';
import { ClientDTO } from '../../types/ClientDTO';
import { BalanceDTO } from '../../types/BalanceDTO';
import { OutgoingResourcesTable } from '../../components/OutgoingResourcesTable';
import { OutgoingDocumentDTO } from '../../types/OutgoingDocumentDTO';
import { OutgoingResourceDTO } from '../../types/OutgoingResourceDTO';
import { OutgoingStateEnum } from '../../types/OutgoingStateEnum';

const OutgoingEditPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const [clientId, setClientId] = useState<string>('');
  const [number, setNumber] = useState<string>('');
  const [date, setDate] = useState<string>('');
  const [balances, setBalances] = useState<BalanceDTO[]>([]);
  const [quantities, setQuantities] = useState<{ [key: string]: number }>({});
  const [clients, setClients] = useState<ClientDTO[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [originalDocument, setOriginalDocument] = useState<OutgoingDocumentDTO | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      if (!id) {
        setError('Не указан ID документа');
        setLoading(false);
        return;
      }

      try {
        setLoading(true);
        const [clientsRes, balancesRes, docRes] = await Promise.all([
          clientsService.getByState(1),
          balanceService.getFiltered(),
          outgoingDocumentsService.getById(id)
        ]);

        setClients(clientsRes.data);
        setBalances(balancesRes.data);

        const doc: OutgoingDocumentDTO = docRes.data;
        setOriginalDocument(doc);
        setClientId(doc.clientDTO?.id ?? '');
        setNumber(doc.number ?? '');
        setDate(doc.date ? doc.date.slice(0, 10) : '');

        const initialQuantities: { [key: string]: number } = {};
        balancesRes.data.forEach(b => {
          initialQuantities[b.id] = 0;
        });
        doc.outgoingResourcesDTO.forEach(or => {
          const balIdFromDto: string | undefined = (or as any).balanceId;
          if (balIdFromDto && initialQuantities.hasOwnProperty(balIdFromDto)) {
            initialQuantities[balIdFromDto] = or.quantity;
            return;
          }

          const matching = balancesRes.data.find(b =>
            b.resourceDTO?.id === or.resourceDTO?.id &&
            b.unitDTO?.id === or.unitDTO?.id
          );
          if (matching) {
            initialQuantities[matching.id] = or.quantity;
          }
        });

        setQuantities(initialQuantities);
      } catch (e) {
        console.error(e);
        setError('Ошибка загрузки данных');
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [id]);

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

    const selectedResources = Object.entries(quantities)
      .map(([balanceId, qty]) => {
        const quantity = Number(qty);
        const balance = balances.find(b => b.id === balanceId);
        if (!balance || quantity <= 0) return null;

        const resObj = {
          id: null,
          balanceId,
          quantity,
          resourceDTO: balance.resourceDTO,
          unitDTO: balance.unitDTO
        };

        return resObj as unknown as OutgoingResourceDTO;
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
      id: id ?? null,
      number,
      clientDTO: client,
      date,
      state: originalDocument?.state ?? OutgoingStateEnum.unsigned,
      outgoingResourcesDTO: selectedResources,
    };
  };

  const handleUpdate = async () => {
    const data = prepareDocumentData();
    if (!data) return;
    try {
      setLoading(true);
      await outgoingDocumentsService.update(data);
      navigate('/outgoing');
    } catch (e) {
      console.error(e);
      setError('Ошибка при обновлении документа');
    } finally {
      setLoading(false);
    }
  };

  const handleUpdateAndSign = async () => {
    const data = prepareDocumentData();
    if (!data) return;
    try {
      setLoading(true);
      await outgoingDocumentsService.updateAndSign(data);
      navigate('/outgoing');
    } catch (e) {
      console.error(e);
      setError('Ошибка при обновлении и подписании документа');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async () => {
    if (!id) return;
    if (!window.confirm('Вы уверены, что хотите удалить документ?')) return;
    try {
      setLoading(true);
      await outgoingDocumentsService.remove(id);
      navigate('/outgoing');
    } catch (e) {
      console.error(e);
      setError('Ошибка при удалении документа');
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return <p>Загрузка...</p>;
  }

  return (
    <div className="edit-resource-page">
      <div style={{ marginBottom: 16 }}>
        <button
          onClick={handleUpdate}
          className="btn save-btn"
          disabled={loading}
          style={{ marginRight: 8 }}
        >
          Сохранить
        </button>

        {originalDocument?.state === OutgoingStateEnum.unsigned && (
          <button
            onClick={handleUpdateAndSign}
            className="btn save-btn"
            disabled={loading}
            style={{ marginRight: 8 }}
          >
            Сохранить и подписать
          </button>
        )}

        <button
          onClick={handleDelete}
          className="btn delete-btn"
          disabled={loading}
          style={{ backgroundColor: '#d9534f', color: '#fff' }}
        >
          Удалить
        </button>
      </div>

      <div className="form-group">
        <label htmlFor="number">Номер</label>
        <input
          id="number"
          type="text"
          value={number}
          onChange={e => setNumber(e.target.value)}
          disabled={loading || originalDocument?.state !== OutgoingStateEnum.unsigned}
          className="input"
        />
      </div>

      <div className="form-group">
        <label htmlFor="client">Клиент</label>
        <select
          id="client"
          value={clientId}
          onChange={e => setClientId(e.target.value)}
          disabled={loading || originalDocument?.state !== OutgoingStateEnum.unsigned}
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
          disabled={loading || originalDocument?.state !== OutgoingStateEnum.unsigned}
          className="input"
        />
      </div>

      <h3>Ресурсы</h3>
      <OutgoingResourcesTable
        balances={balances}
        quantities={quantities}
        setQuantities={setQuantities}
        disabled={loading || originalDocument?.state !== OutgoingStateEnum.unsigned}
      />

      {error && <p className="error-message">{error}</p>}
    </div>
  );
};

export default OutgoingEditPage;
