import { useNavigate } from 'react-router-dom';
import { IncomingDocumentDTO } from '../types/IncomingDocumentDTO';

type Props = {
  data: IncomingDocumentDTO[];
  loading: boolean;
  error: string | null;
};

export function IncomingDocumentsTable({ data, loading, error }: Props) {
  const navigate = useNavigate();

  if (loading) return <p>Загрузка...</p>;
  if (error) return <p style={{ color: 'red' }}>{error}</p>;
  if (!data.length) return <p>Данных нет</p>;

  const onRowClick = (docId: string) => {
    navigate(`/incomingDocuments/edit/${docId}`);
  };

  return (
    <div className="data-table-container">
      <table className="data-table">
        <thead>
          <tr>
            <th style={{ width: 120 }}>Номер</th>
            <th style={{ width: 100 }}>Дата</th>
            <th>Ресурс</th>
            <th>Ед. измерения</th>
            <th>Количество</th>
          </tr>
        </thead>
        <tbody>
          {data.map((doc) => {
            const rowSpan = doc.incomingResourcesDTO.length || 1;

            return doc.incomingResourcesDTO.length > 0 ? (
              doc.incomingResourcesDTO.map((res, i) => (
                <tr
                  key={res.id}
                  onClick={() => onRowClick(doc.id!)}
                  style={{ cursor: 'pointer' }}
                >
                  {i === 0 && (
                    <>
                      <td rowSpan={rowSpan} style={{ verticalAlign: 'middle' }}>
                        {doc.number}
                      </td>
                      <td rowSpan={rowSpan} style={{ verticalAlign: 'middle' }}>
                        {new Date(doc.date).toLocaleDateString()}
                      </td>
                    </>
                  )}
                  <td>{res.resourceDTO.name}</td>
                  <td>{res.unitDTO.name}</td>
                  <td>{res.quantity}</td>
                </tr>
              ))
            ) : (
              <tr
                key={doc.id}
                onClick={() => onRowClick(doc.id!)}
                style={{ cursor: 'pointer' }}
              >
                <td>{doc.number}</td>
                <td>{new Date(doc.date).toLocaleDateString()}</td>
                <td colSpan={3}>Нет ресурсов</td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}
