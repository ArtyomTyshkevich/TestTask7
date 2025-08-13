import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { ClientDTO } from '../../types/ClientDTO';
import { clientsService } from '../../services/clientsService';
import { DirectoriesStateEnum } from '../../types/DirectoriesStateEnum';
import { DataTable } from '../../components/DataTable';
import { ColumnDef } from '@tanstack/react-table';
import ToggleButtons from '../../components/ToggleButtons';

function parseState(state?: string): DirectoriesStateEnum | undefined {
  if (state === undefined) return undefined;
  if (state === '0' || state.toLowerCase() === 'used') return DirectoriesStateEnum.Used;
  if (state === '1' || state.toLowerCase() === 'archived') return DirectoriesStateEnum.Archived;
  return undefined;
}

const ClientsPage: React.FC = () => {
  const { state } = useParams<{ state?: string }>();
  const currentState = parseState(state);
  const [clients, setClients] = useState<ClientDTO[]>([]);

  useEffect(() => {
    if (currentState !== undefined) {
      clientsService.getByState(currentState).then(res => {
        setClients(res.data);
      });
    } else {
      setClients([]);
    }
  }, [currentState]);

  if (currentState === undefined) {
    return <div>Неверный параметр состояния</div>;
  }

  const columns: ColumnDef<ClientDTO>[] = [
    {
      header: 'Наименование',
      accessorKey: 'name',
    },
    {
      header: 'Адрес',
      accessorKey: 'address',
    },
  ];

  if (!currentState) {
    return <div>Неверный параметр состояния</div>;
  }

  return (
    <div>
      <h1>Клиенты</h1>

      <ToggleButtons
        currentState={currentState}
        navigateToUsed="/clients/Used"
        navigateToArchived="/clients/Archived"
        navigateToAdd="/clients/add"
      />

      <DataTable
        columns={columns}
        data={clients}
        getRowLink={client => `/clients/edit/${client.id}`}
      />
    </div>
  );
};

export default ClientsPage;
