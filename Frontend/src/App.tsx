import React from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import Layout from './components/Layout';
import './styles/sidebar.css';
import './styles/datatable.css';
import './styles/buttons.css';
import './styles/form.css';
import './styles/multi-select.css'
import './styles/filters.css'
import './styles/date-range-picker.css'

import UnitsPage from './pages/UnitPages/UnitsPage';
import ClientsPage from './pages/ClientsPages/ClientsPage';
import ResourcesPage from './pages/ResourcesPages/ResourcesPage';
import AddUnitPage from './pages/UnitPages/AddUnitPage';
import AddResourcePage from './pages/ResourcesPages/AddResourcePage';
import AddClientPage from './pages/ClientsPages/AddClientPage';
import EditUnitPage from './pages/UnitPages/EditUnitPage';
import EditResourcePage from './pages/ResourcesPages/EditResourcePage';
import EditClientPage from './pages/ClientsPages/EditClientPage';
import BalancePage from './pages/BalancePages/BalancePage';
import { IncomingPage } from './pages/IncomingPages/IncomingPage';
import IncomingAddPage from './pages/IncomingPages/IncomingAddPage';
import { OutgoingPage } from './pages/OutgoingPages/OutgoingPage';
import OutgoingAddPage from './pages/OutgoingPages/OutgoingAddPage';
import IncomingEditPage from './pages/IncomingPages/IncomingEditPage';
import OutgoingEditNosignedPage from './pages/OutgoingPages/OutgoingEditPage';
import OutgoingEditSignedPage from './pages/OutgoingPages/OutgoingEditSignedPage';

const App: React.FC = () => {
  return (
    <BrowserRouter>
      <Layout>
        <Routes>
          <Route path="/" element={<Navigate to="/clients/Used" replace />} />
          <Route path="/units" element={<Navigate to="/units/Used" replace />} />
          <Route path="/units/:state" element={<UnitsPage />} />
          <Route path="/clients" element={<Navigate to="/clients/Used" replace />} />
          <Route path="/clients/:state" element={<ClientsPage />} />
          <Route path="/resources" element={<Navigate to="/resources/Used" replace />} />
          <Route path="/resources/:state" element={<ResourcesPage />} />
          <Route path="/units/add" element={<AddUnitPage />} />
          <Route path="/resources/add" element={<AddResourcePage />} />
          <Route path="/clients/add" element={<AddClientPage />} />
          <Route path="/units/edit/:id" element={<EditUnitPage />} />
          <Route path="/resources/edit/:id" element={<EditResourcePage />} /> 
          <Route path="/clients/edit/:id" element={<EditClientPage />} /> 
          <Route path="/balance" element={<BalancePage />} />
          <Route path="/incoming" element={<IncomingPage />} />
          <Route path="/outgoing" element={<OutgoingPage />} />
          <Route path="/incoming/add" element={<IncomingAddPage />} />
          <Route path="/outgoing/add" element={<OutgoingAddPage />} />
          <Route path="/incomingDocuments/edit/:id" element={<IncomingEditPage />} />
          <Route path="/outgoing/edit/:id" element={<OutgoingEditNosignedPage />} />
          <Route path="/outgoing/view/:id" element={<OutgoingEditSignedPage />} />
        </Routes>
      </Layout>
    </BrowserRouter>
  );
};

export default App;
