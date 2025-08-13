import React from 'react';
import Sidebar from './Sidebar';

type Props = {
  children?: React.ReactNode;
};

const Layout: React.FC<Props> = ({ children }) => {
  return (
    <div style={{ display: 'flex' }}>
      <Sidebar />
      <main style={{ marginLeft: '15%', padding: '2%', flex: 1 }}>
        {children}
      </main>
    </div>
  );
};

export default Layout;
