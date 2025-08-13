import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { UnitDTO } from '../../types/UnitDTO';
import { DirectoriesStateEnum } from '../../types/DirectoriesStateEnum';
import { unitsService } from '../../services/unitsService';
import UpdateButtons from '../../components/UpdateButtons';

const EditUnitPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const [unit, setUnit] = useState<UnitDTO | null>(null);
  const [name, setName] = useState('');
  const [state, setState] = useState<DirectoriesStateEnum | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!id) return;

    setLoading(true);
    unitsService.getById(id)
      .then(res => {
        setUnit(res.data);
        setName(res.data.name);
        setState(res.data.state); // Сохраняем текущий статус
      })
      .catch(() => setError('Ошибка при загрузке данных'))
      .finally(() => setLoading(false));
  }, [id]);

  const handleSave = async () => {
    if (!unit || state === null) return;
    if (!name.trim()) {
      setError('Введите название');
      return;
    }

    setLoading(true);
    setError(null);
    try {
      await unitsService.update({ id: unit.id, name, state });
      navigate(`/units`);
    } catch {
      setError('Ошибка при сохранении');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async () => {
    if (!unit) return;

    if (!window.confirm('Вы уверены, что хотите удалить этот элемент?')) return;

    setLoading(true);
    setError(null);
    try {
      await unitsService.remove(unit.id!);
      navigate(`/units`);
    } catch {
      setError('Ошибка при удалении');
    } finally {
      setLoading(false);
    }
  };

  const handleToggleState = async () => {
    if (!unit || state === null) return;

    const newState =
      state === DirectoriesStateEnum.Used
        ? DirectoriesStateEnum.Archived
        : DirectoriesStateEnum.Used;

    setLoading(true);
    setError(null);
    try {
      await unitsService.updateStatus(unit.id!);
      setUnit({ ...unit, state: newState });
      setState(newState); 
      navigate(`/units`);
    } catch {
      setError('Ошибка при изменении статуса');
    } finally {
      setLoading(false);
    }
  };

  if (loading && !unit) {
    return <div>Загрузка...</div>;
  }

  if (!unit) {
    return <div>Данные не найдены</div>;
  }

  return (
    <div className="edit-unit-page">
      <h1>Единицы измерения</h1>
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

export default EditUnitPage;
