import React, { useState } from 'react';

export interface Option {
  id: string;
  name: string;
}

interface MultiSelectProps {
  label: string;
  options: Option[];
  selected: string[];
  onChange: (values: string[]) => void;
}

export const MultiSelect: React.FC<MultiSelectProps> = ({ label, options, selected, onChange }) => {
  const [isOpen, setIsOpen] = useState(false);

  const toggleOption = (id: string) => {
    if (selected.includes(id)) {
      onChange(selected.filter(s => s !== id));
    } else {
      onChange([...selected, id]);
    }
  };

  const removeOption = (id: string) => {
    onChange(selected.filter(s => s !== id));
  };

  return (
    <div className="multi-select">
      <label>{label}</label>
      <div className="multi-select-input" onClick={() => setIsOpen(!isOpen)}>
        {selected.length > 0 ? (
          <div className="selected-tags">
            {selected.map(id => {
              const option = options.find(o => o.id === id);
              return option ? (
                <span className="tag" key={id}>
                  {option.name}
                  <button
                    type="button"
                    className="remove-btn"
                    onClick={(e) => { e.stopPropagation(); removeOption(id); }}
                  >
                    ×
                  </button>
                </span>
              ) : null;
            })}
          </div>
        ) : (
          <span className="placeholder">Выберите</span>
        )}
        <span className="arrow">{isOpen ? '▲' : '▼'}</span>
      </div>
      {isOpen && (
        <div className="options-list">
          {options.map(o => (
            <div
              key={o.id}
              className={`option ${selected.includes(o.id) ? 'selected' : ''}`}
              onClick={() => toggleOption(o.id)}
            >
              {o.name}
            </div>
          ))}
        </div>
      )}
    </div>
  );
};
