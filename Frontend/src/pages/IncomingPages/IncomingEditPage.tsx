import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { incomingDocumentsService } from '../../services/incomingDocumentsService';
import { IncomingResourcesTable } from '../../components/IncomingResourcesTable';
import { IncomingResourceDTO } from '../../types/IncomingResourceDTO';
import { ResourceDTO } from '../../types/ResourceDTO';
import { UnitDTO } from '../../types/UnitDTO';
import { IncomingDocumentDTO } from '../../types/IncomingDocumentDTO';

const IncomingEditPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [number, setNumber] = useState('');
  const [date, setDate] = useState('');
  const [resourcesOptions, setResourcesOptions] = useState<ResourceDTO[]>([]);
  const [unitsOptions, setUnitsOptions] = useState<UnitDTO[]>([]);
  const [incomingResourcesDTO, setIncomingResourcesDTO] = useState<IncomingResourceDTO[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const navigate = useNavigate();

  useEffect(() => {
    if (!id) return;

    Promise.all([
      incomingDocumentsService.getFilters(),
      incomingDocumentsService.getById(id)
    ])
      .then(([filtersRes, docRes]) => {
        const filters = filtersRes.data;
        const doc: IncomingDocumentDTO = docRes.data;

        setNumber(doc.number);
        setDate(doc.date);
        setIncomingResourcesDTO(doc.incomingResourcesDTO || []);

        const resources: ResourceDTO[] = filters.resourceDTOs.map((r: any) => ({
          id: r.id,
          name: r.name,
          state: r.state ?? 1,
        }));

        const units: UnitDTO[] = filters.unitDTOs.map((u: any) => ({
          id: u.id,
          name: u.name,
          state: u.state ?? 1,
        }));

        setResourcesOptions(resources);
        setUnitsOptions(units);
      })
      .catch(() => setError('Ошибка загрузки данных документа'));
  }, [id]);

  const handleSave = async () => {
    if (!number.trim()) {
      setError('Введите номер документа');
      return;
    }
    if (!date) {
      setError('Выберите дату');
      return;
    }

    for (const item of incomingResourcesDTO) {
      if (!item.resourceDTO.id || !item.unitDTO.id || item.quantity <= 0) {
        setError('Пожалуйста, заполните все поля в таблице ресурсов и укажите количество больше 0');
        return;
      }
    }

    setLoading(true);
    setError(null);

    try {
      await incomingDocumentsService.update({
        id: id!,
        number,
        date,
        incomingResourcesDTO,
      });

      navigate('/incoming');
    } catch {
      setError('Ошибка при сохранении документа');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async () => {
    if (!window.confirm('Вы уверены, что хотите удалить этот документ?')) return;

    setLoading(true);
    try {
      // Используем метод remove вместо delete
      await incomingDocumentsService.remove(id!);
      navigate('/incoming');
    } catch {
      setError('Ошибка при удалении документа');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="add-resource-page">
      <div style={{ display: 'flex', gap: '10px', marginBottom: '20px' }}>
        <button onClick={handleSave} disabled={loading} className="btn save-btn">
          {loading ? 'Сохраняем...' : 'Сохранить'}
        </button>
        <button onClick={handleDelete} disabled={loading} className="btn delete-btn">
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
          disabled={loading}
          className="input"
        />
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

      <div className="form-group">
        <h3>Ресурсы</h3>
        <IncomingResourcesTable
          resourcesOptions={resourcesOptions}
          unitsOptions={unitsOptions}
          items={incomingResourcesDTO}
          setItems={setIncomingResourcesDTO}
        />
      </div>

      {error && <p className="error-message">{error}</p>}
    </div>
  );
};

export default IncomingEditPage;
