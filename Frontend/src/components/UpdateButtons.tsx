import React from 'react';
import { DirectoriesStateEnum } from '../types/DirectoriesStateEnum';

type UpdateButtonsProps = {
  currentState: DirectoriesStateEnum;
  onSave: () => Promise<void> | void;
  onDelete: () => Promise<void> | void;
  onToggleState: () => Promise<void> | void;
  loading?: boolean;
};

const UpdateButtons: React.FC<UpdateButtonsProps> = ({
  currentState,
  onSave,
  onDelete,
  onToggleState,
  loading = false,
}) => {
  const toggleLabel =
    currentState === DirectoriesStateEnum.Used ? 'В работу' : 'В архив';

  return (
    <div className="toggle-buttons">
      <button onClick={onSave} disabled={loading} className="btn save-btn">
        Сохранить
      </button>

      <button onClick={onDelete} disabled={loading} className="btn delete-btn">
        Удалить
      </button>

      <button
        onClick={onToggleState}
        disabled={loading}
        className="btn toggle-btn"
        data-state={
          currentState === DirectoriesStateEnum.Used
            ? 'to-used'
            : 'to-archived'
        }
      >
        {toggleLabel}
      </button>
    </div>
  );
};

export default UpdateButtons;
