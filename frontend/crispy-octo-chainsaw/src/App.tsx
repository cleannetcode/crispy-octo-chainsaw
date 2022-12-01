import React, { useState } from 'react';
import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { Layout } from 'antd';
import HeaderComponent from './components/HeaderComponent';
import 'antd/dist/antd.min.css';
import { AuthPage } from './pages/AuthPage/AuthPage';
import { CoursePage } from './pages/CoursePage/CoursePage';
import { ExercisePage } from './pages/ExercisePage/ExercisePage';
import { EditCoursePage } from './pages/EditCoursePage/EditCoursePage';
import { PageNames } from './PageName';
import { CourseAdminCatalogPage } from './pages/CourseAdminCatalog/CourseAdminCatalogPage';
import { AdminRegistraionPage } from './pages/AdminRegistrationPage/AdminRegistraionPage';

function App() {
  return (
    <Router>
      <Routes>
        <Route
          path={PageNames.Mane}
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
          path={PageNames.Login}
          element={
            <div>
              <AuthPage />
            </div>
          }
        />
        <Route
          path={PageNames.CourseAdminRegistraation}
          element={
            <div>
              <AdminRegistraionPage />
            </div>
          }
        />
        <Route
          path={PageNames.CreteCourse}
          element={
            <>
              <CoursePage />
            </>
          }
        />
        <Route path={`${PageNames.CreteCourse}:id`} element={<></>} />
        <Route
          path={PageNames.CreteExercise}
          element={
            <>
              <ExercisePage />
            </>
          }
        />
        <Route
          path={PageNames.EditeCourse}
          element={
            <>
              <EditCoursePage />
            </>
          }
        />
        <Route
          path={PageNames.CourseAdminCatalog}
          element={
            <>
              <CourseAdminCatalogPage />
            </>
          }
        />
      </Routes>
    </Router>
  );
}

export default App;
