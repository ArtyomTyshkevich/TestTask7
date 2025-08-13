
import { useNavigate } from 'react-router-dom';
import { DirectoriesStateEnum } from '../types/DirectoriesStateEnum';

type StatusType = DirectoriesStateEnum;

type ToggleButtonsProps = {
  currentState: StatusType;
  navigateToUsed: string;
  navigateToArchived: string;
  navigateToAdd: string;
};

function ToggleButtons({
  currentState,
  navigateToUsed,
  navigateToArchived,
  navigateToAdd,
}: ToggleButtonsProps) {
  const navigate = useNavigate();

  const handleAdd = () => {
    navigate(navigateToAdd);
  };

  const handleToggle = () => {
    if (currentState === DirectoriesStateEnum.Used) {
      navigate(navigateToArchived);
    } else {
      navigate(navigateToUsed);
    }
  };

  return (
    <div className="toggle-buttons">
      {currentState === DirectoriesStateEnum.Used && (
        <button className="btn add-btn" onClick={handleAdd}>
          Добавить
        </button>
      )}
      <button
        className="btn toggle-btn"
        data-state={currentState === DirectoriesStateEnum.Used ? 'to-archived' : 'to-used'}
        onClick={handleToggle}
      >
        {currentState === DirectoriesStateEnum.Used ? 'К архиву' : 'К рабочим'}
      </button>
    </div>
  );
}

export default ToggleButtons;
