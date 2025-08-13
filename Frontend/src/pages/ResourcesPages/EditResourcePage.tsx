import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { ResourceDTO } from '../../types/ResourceDTO';
import { DirectoriesStateEnum } from '../../types/DirectoriesStateEnum';
import { resourcesService } from '../../services/resourcesService';
import UpdateButtons from '../../components/UpdateButtons';

const EditResourcePage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const [resource, setResource] = useState<ResourceDTO | null>(null);
  const [name, setName] = useState('');
  const [state, setState] = useState<DirectoriesStateEnum | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!id) return;

    setLoading(true);
    resourcesService.getById(id)
      .then(res => {
        setResource(res.data);
        setName(res.data.name);
        setState(res.data.state);
      })
      .catch(() => setError('Ошибка при загрузке данных'))
      .finally(() => setLoading(false));
  }, [id]);

  const handleSave = async () => {
    if (!resource || state === null) return;
    if (!name.trim()) {
      setError('Введите название');
      return;
    }

    setLoading(true);
    setError(null);
    try {
      await resourcesService.update({ id: resource.id, name, state });
      navigate(`/resources`);
    } catch {
      setError('Ошибка при сохранении');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async () => {
    if (!resource) return;

    if (!window.confirm('Вы уверены, что хотите удалить этот элемент?')) return;

    setLoading(true);
    setError(null);
    try {
      await resourcesService.remove(resource.id!);
      navigate(`/resources`);
    } catch {
      setError('Ошибка при удалении');
    } finally {
      setLoading(false);
    }
  };

  const handleToggleState = async () => {
    if (!resource || state === null) return;

    const newState =
      state === DirectoriesStateEnum.Used
        ? DirectoriesStateEnum.Archived
        : DirectoriesStateEnum.Used;

    setLoading(true);
    setError(null);
    try {
      await resourcesService.updateStatus(resource.id!);
      setResource({ ...resource, state: newState });
      setState(newState);
      navigate(`/resources`);
    } catch {
      setError('Ошибка при изменении статуса');
    } finally {
      setLoading(false);
    }
  };

  if (loading && !resource) {
    return <div>Загрузка...</div>;
  }

  if (!resource) {
    return <div>Данные не найдены</div>;
  }

  return (
    <div className="edit-resource-page">
      <h1>Ресурсы</h1>
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

      {error && <p className="error-message">{error}</p>}
    </div>
  );
};

export default EditResourcePage;
