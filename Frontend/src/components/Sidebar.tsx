import React from 'react';
import { Link } from 'react-router-dom';

const Sidebar: React.FC = () => {
  return (
    <aside className="sidebar">
      <h1 className="sidebar-main-title">Управление складом</h1>
      
      <nav className="sidebar-nav">
        <div className="sidebar-section">
          <h2 className="sidebar-section-title">Склад</h2>
          <ul className="sidebar-menu">
            <li className="sidebar-menu-item"><Link to="/balance">Баланс</Link></li>
            <li className="sidebar-menu-item"><Link to="/incoming">Поступления</Link></li>
            <li className="sidebar-menu-item"><Link to="/outgoing">Отгрузки</Link></li>
          </ul>
        </div>

        <div className="sidebar-section">
          <h2 className="sidebar-section-title">Справочники</h2>
          <ul className="sidebar-menu">
            <li className="sidebar-menu-item"><Link to="/clients">Клиенты</Link></li>
            <li className="sidebar-menu-item"><Link to="/units">Единицы измерения</Link></li>
            <li className="sidebar-menu-item"><Link to="/resources">Ресурсы</Link></li>
          </ul>
        </div>
      </nav>
    </aside>
  );
};

export default Sidebar;