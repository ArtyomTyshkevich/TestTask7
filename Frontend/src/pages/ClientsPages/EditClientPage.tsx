import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { ClientDTO } from '../../types/ClientDTO';
import { DirectoriesStateEnum } from '../../types/DirectoriesStateEnum';
import { clientsService } from '../../services/clientsService';
import UpdateButtons from '../../components/UpdateButtons';

const EditClientPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const [client, setClient] = useState<ClientDTO | null>(null);
  const [name, setName] = useState('');
  const [address, setAddress] = useState('');
  const [state, setState] = useState<DirectoriesStateEnum | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!id) return;

    setLoading(true);
    clientsService.getById(id)
      .then(res => {
        setClient(res.data);
        setName(res.data.name);
        setAddress(res.data.address);
        setState(res.data.state);
      })
      .catch(() => setError('Ошибка при загрузке данных'))
      .finally(() => setLoading(false));
  }, [id]);

  const handleSave = async () => {
    if (!client || state === null) return;
    if (!name.trim()) {
      setError('Введите название');
      return;
    }
    if (!address.trim()) {
      setError('Введите адрес');
      return;
    }

    setLoading(true);
    setError(null);
    try {
      await clientsService.update({ id: client.id, name, address, state });
      navigate(`/clients`);
    } catch {
      setError('Ошибка при сохранении');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async () => {
    if (!client) return;

    if (!window.confirm('Вы уверены, что хотите удалить этот элемент?')) return;

    setLoading(true);
    setError(null);
    try {
      await clientsService.remove(client.id!);
      navigate(`/clients`);
    } catch {
      setError('Ошибка при удалении');
    } finally {
      setLoading(false);
    }
  };

  const handleToggleState = async () => {
    if (!client || state === null) return;

    const newState =
      state === DirectoriesStateEnum.Used
        ? DirectoriesStateEnum.Archived
        : DirectoriesStateEnum.Used;

    setLoading(true);
    setError(null);
    try {
      await clientsService.updateStatus(client.id!);
      setClient({ ...client, state: newState });
      setState(newState);
      navigate(`/clients`);
    } catch {
      setError('Ошибка при изменении статуса');
    } finally {
      setLoading(false);
    }
  };

  if (loading && !client) {
    return <div>Загрузка...</div>;
  }

  if (!client) {
    return <div>Данные не найдены</div>;
  }

  return (
    <div className="edit-client-page">
      <h1>Клиенты</h1>
      <UpdateButtons
        currentState={state!}
        onSave={handleSave}
        onDelete={handleDelete}
        onToggleState={handleToggleState}
        loading={loading}
      />
      <div className="form-group">
        <label htmlFor="name">Название</label>
        <input
          id="name"
          type="text"
          value={name}
          onChange={e => setName(e.target.value)}
          disabled={loading}
          className="input"
        />
      </div>
      <div className="form-group">
        <label htmlFor="address">Адрес</label>
        <input
          id="address"
          type="text"
          value={address}
          onChange={e => setAddress(e.target.value)}
          disabled={loading}
          className="input"
        />
      </div>

      {error && <p className="error-message">{error}</p>}
    </div>
  );
};

export default EditClientPage;
