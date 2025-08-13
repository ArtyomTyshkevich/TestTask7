import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { clientsService } from '../../services/clientsService';
import { DirectoriesStateEnum } from '../../types/DirectoriesStateEnum';

const AddClientPage: React.FC = () => {
  const [name, setName] = useState('');
  const [address, setAddress] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleSave = async () => {
    if (!name.trim()) {
      setError('Введите имя');
      return;
    }
    if (!address.trim()) {
      setError('Введите адрес');
      return;
    }

    setLoading(true);
    setError(null);

    try {
      await clientsService.create({
        id: null,
        name,
        address,
        state: DirectoriesStateEnum.Used as unknown as number - 1,
      });

      navigate('/clients/Used');
    } catch (e) {
      setError('Ошибка при создании');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="add-client-page">
      <button onClick={handleSave} disabled={loading} className="btn save-btn">
        {loading ? 'Сохраняем...' : 'Сохранить'}
      </button>

      <div className="form-group">
        <label htmlFor="name">Имя</label>
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

export default AddClientPage;
