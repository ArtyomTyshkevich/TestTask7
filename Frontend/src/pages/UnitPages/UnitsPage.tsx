import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { UnitDTO } from '../../types/UnitDTO';
import { DirectoriesStateEnum } from '../../types/DirectoriesStateEnum';
import { unitsService } from '../../services/unitsService';
import { DataTable } from '../../components/DataTable';
import { ColumnDef } from '@tanstack/react-table';
import ToggleButtons from '../../components/ToggleButtons';

function parseState(state?: string): DirectoriesStateEnum | undefined {
  if (state === undefined) return undefined;
  if (state === '0' || state.toLowerCase() === 'used') return DirectoriesStateEnum.Used;
  if (state === '1' || state.toLowerCase() === 'archived') return DirectoriesStateEnum.Archived;
  return undefined;
}

const UnitsPage: React.FC = () => {
  const { state } = useParams<{ state?: string }>();
  const currentState = parseState(state);
  const [units, setUnits] = useState<UnitDTO[]>([]);

  useEffect(() => {
    if (currentState) {
      unitsService.getByState(currentState).then(res => {
        setUnits(res.data);
      });
    } else {
      setUnits([]);
    }
     console.log(currentState);
  }, [currentState]);

  const columns: ColumnDef<UnitDTO>[] = [
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
      <h1>Единицы измерения</h1>

      <ToggleButtons
        currentState={currentState}
        navigateToUsed="/units/Used"
        navigateToArchived="/units/Archived"
        navigateToAdd="/units/add"
      />

      <DataTable
        columns={columns}
        data={units}
        getRowLink={unit => `/units/edit/${unit.id}`}
      />
    </div>
  );
};

export default UnitsPage;
