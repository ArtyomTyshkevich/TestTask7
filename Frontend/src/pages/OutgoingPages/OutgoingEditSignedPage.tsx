import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { outgoingDocumentsService } from '../../services/outgoingDocumentsService';
import { OutgoingDocumentDTO } from '../../types/OutgoingDocumentDTO';
import { OutgoingResourceDTO } from '../../types/OutgoingResourceDTO';
import { ColumnDef } from '@tanstack/react-table';
import { DataTable } from '../../components/DataTable';

const OutgoingEditSignedPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const [document, setDocument] = useState<OutgoingDocumentDTO | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchDocument = async () => {
      if (!id) return;

      try {
        setLoading(true);
        const res = await outgoingDocumentsService.getById(id);
        const doc = res.data;

        if (doc.state !== 1) {
          navigate('/outgoing');
          return;
        }

        setDocument(doc);
      } catch {
        setError('Ошибка загрузки документа');
      } finally {
        setLoading(false);
      }
    };

    fetchDocument();
  }, [id, navigate]);

  const handleRevoke = async () => {
    if (!id) return;
    const confirmRevoke = window.confirm('Вы уверены, что хотите отозвать этот документ?');
    if (!confirmRevoke) return;

    setLoading(true);
    try {
      await outgoingDocumentsService.revoke(id);
      navigate('/outgoing');
    } catch {
      setError('Ошибка при отзыве документа');
    } finally {
      setLoading(false);
    }
  };

  const columns: ColumnDef<OutgoingResourceDTO>[] = [
    { header: 'Ресурс', accessorFn: row => row.resourceDTO.name },
    { header: 'Единица измерения', accessorFn: row => row.unitDTO.name },
    { header: 'Количество', accessorFn: row => row.quantity },
  ];

  if (loading) return <p>Загрузка...</p>;
  if (error) return <p className="error-message">{error}</p>;
  if (!document) return null;

return (
  <div className="view-resource-page">
    {/* Кнопка сверху, над номером */}
    <div style={{ marginBottom: 16 }}>
      <button
        onClick={handleRevoke}
        className="btn delete-btn"
        disabled={loading}
      >
        {loading ? 'Отзываем...' : 'Отозвать'}
      </button>
    </div>

    <div className="form-group">
      <label>Номер:</label>
      <span>{document.number}</span>
    </div>

    <div className="form-group">
      <label>Клиент:</label>
      <span>{document.clientDTO.name}</span>
    </div>

    <div className="form-group">
      <label>Дата:</label>
      <span>{document.date}</span>
    </div>

    <h3>Ресурсы</h3>
    <DataTable
      columns={columns}
      data={document.outgoingResourcesDTO}
      getRowLink={undefined}
    />
  </div>
);

};

export default OutgoingEditSignedPage;
