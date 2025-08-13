import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { ResourceDTO } from '../../types/ResourceDTO';
import { DirectoriesStateEnum } from '../../types/DirectoriesStateEnum';
import { resourcesService } from '../../services/resourcesService';
import { DataTable } from '../../components/DataTable';
import { ColumnDef } from '@tanstack/react-table';
import ToggleButtons from '../../components/ToggleButtons';

function parseState(state?: string): DirectoriesStateEnum | undefined {
  if (state === undefined) return undefined;
  if (state === '0' || state.toLowerCase() === 'used') return DirectoriesStateEnum.Used;
  if (state === '1' || state.toLowerCase() === 'archived') return DirectoriesStateEnum.Archived;
  return undefined;
}

const ResourcesPage: React.FC = () => {
  const { state } = useParams<{ state?: string }>();
  const currentState = parseState(state);
  const [resources, setResources] = useState<ResourceDTO[]>([]);

  useEffect(() => {
    if (currentState) {
      resourcesService.getByState(currentState).then(res => {
        setResources(res.data);
      });
    } else {
      setResources([]);
    }
  }, [currentState]);

  const columns: ColumnDef<ResourceDTO>[] = [
    {
      header: 'Наименование',
      accessorKey: 'name',
    },
  ];

  if (!currentState) {
    return <div>Неверный параметр состояния</div>;
  }

  return (
    <div>
      <h1>Ресурсы</h1>

      <ToggleButtons
        currentState={currentState}
        navigateToUsed="/resources/Used"
        navigateToArchived="/resources/Archived"
        navigateToAdd="/resources/add"
      />

      <DataTable
        columns={columns}
        data={resources}
        getRowLink={resource => `/resources/edit/${resource.id}`}
      />
    </div>
  );
};

export default ResourcesPage;
