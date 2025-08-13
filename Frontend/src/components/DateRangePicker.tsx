import React from 'react';

interface DateRangePickerProps {
  startDate: string;
  endDate: string;
  onStartDateChange: (date: string) => void;
  onEndDateChange: (date: string) => void;
}

export const DateRangePicker: React.FC<DateRangePickerProps> = ({
  startDate,
  endDate,
  onStartDateChange,
  onEndDateChange,
}) => {
  return (
   <div className="date-range-picker">
  <div>
    <label htmlFor="startDate">Начальная дата</label>
    <input
      id="startDate"
      type="date"
      value={startDate}
      onChange={e => onStartDateChange(e.target.value)}
    />
  </div>

  <div>
    <label htmlFor="endDate">Конечная дата</label>
    <input
      id="endDate"
      type="date"
      value={endDate}
      onChange={e => onEndDateChange(e.target.value)}
    />
  </div>
</div>

  );
};
