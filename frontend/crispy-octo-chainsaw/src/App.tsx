import React, { useState } from 'react';
import logo from './logo.svg';
import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { Layout } from 'antd';
import HeaderComponent from './components/HeaderComponent';
import 'antd/dist/antd.min.css';
import AuthPage from './pages/AuthPage/AuthPage';
import TestPage from './pages/TestPage/TestPage';

function App() {
  return (
    <Router>
      <Routes>
        <Route
          path='/'
          element={
            <div className='App'>
              <header>
                <Layout>
                  <HeaderComponent />
                </Layout>
              </header>
            </div>
          }
        />
        <Route
          path='login'
          element={
            <div>
              {/* <Layout>
                <HeaderComponent />
              </Layout> */}
              <AuthPage />
            </div>
          }
        />
        <Route
          path='test'
          element={
            <div>
              {/* <Layout>
                <HeaderComponent />
              </Layout> */}
              <TestPage />
            </div>
          }
        />
      </Routes>
    </Router>
  );
}

export default App;
