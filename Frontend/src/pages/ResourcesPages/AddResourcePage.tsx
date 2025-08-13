import { useNavigate } from 'react-router-dom';
import React, { useState } from 'react';
import { resourcesService } from '../../services/resourcesService';
import { DirectoriesStateEnum } from '../../types/DirectoriesStateEnum';

const AddResourcePage: React.FC = () => {
  const [name, setName] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleSave = async () => {
    if (!name.trim()) {
      setError('Введите название');
      return;
    }

    setLoading(true);
    setError(null);

    try {
      await resourcesService.create({
        id: null,
        name,
        state: DirectoriesStateEnum.Used as unknown as number - 1,
      });

      navigate('/resources/Used');
    } catch (e) {
      setError('Ошибка при создании');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="add-resource-page">
      <button onClick={handleSave} disabled={loading} className="btn save-btn">
        {loading ? 'Сохраняем...' : 'Сохранить'}
      </button>

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

export default AddResourcePage;
