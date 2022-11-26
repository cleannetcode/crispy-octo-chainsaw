import React, { useState } from 'react';
import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { Layout } from 'antd';
import HeaderComponent from './components/HeaderComponent';
import 'antd/dist/antd.min.css';
import { AuthPage } from './pages/AuthPage/AuthPage';
import TestPage from './pages/TestPage/TestPage';
import { CoursePage } from './pages/CoursePage/CoursePage';
import { ExercisePage } from './pages/ExercisePage/ExercisePage';
import { EditCoursePage } from './pages/EditCoursePage/EditCoursePage';

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
              <AuthPage />
            </div>
          }
        />
        <Route
          path='test'
          element={
            <div>
              <TestPage />
            </div>
          }
        />
        <Route
          path='createcourse'
          element={
            <>
              <CoursePage />
            </>
          }
        />
        <Route
          path='createexercise'
          element={
            <>
              <ExercisePage />
            </>
          }
        />
        <Route
          path='course'
          element={
            <>
              <EditCoursePage />
            </>
          }
        />
      </Routes>
    </Router>
  );
}

export default App;
