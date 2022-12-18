import React from 'react';
import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import 'antd/dist/antd.min.css';
import { AuthPage } from './pages/AuthPage/AuthPage';
import { CreateCoursePage } from './pages/CreateCoursePage/CreateCoursePage';
import { ExercisePage } from './pages/ExercisePage/ExercisePage';
import { EditCoursePage } from './pages/EditCoursePage/EditCoursePage';
import { PageRoots } from './PageRoots';
import { CourseAdminCatalogPage } from './pages/CourseAdminCatalog/CourseAdminCatalogPage';
import { AdminRegistraionPage } from './pages/AdminRegistrationPage/AdminRegistraionPage';
import { MainPage } from './pages/MainPage/MainPage';
import { UserCoursePage } from './pages/UserCoursePage/UserCoursePage';

function App() {
  return (
    <Router>
      <Routes>
        <Route path={PageRoots.Main} element={<MainPage />} />
        <Route
          path={PageRoots.Login}
          element={
            <>
              <AuthPage />
            </>
          }
        />
        <Route
          path={PageRoots.CourseAdminRegistraation}
          element={
            <>
              <AdminRegistraionPage />
            </>
          }
        />
        <Route
          path={PageRoots.CreteCourse}
          element={
            <>
              <CreateCoursePage />
            </>
          }
        />
        <Route
          path={`${PageRoots.CourseAdminCatalog}/${PageRoots.Course}/:id`}
          element={
            <>
              <EditCoursePage />
            </>
          }
        />
        <Route
          path={`${PageRoots.CourseAdminCatalog}/${PageRoots.Course}/:id/${PageRoots.CreteExercise}`}
          element={
            <>
              <ExercisePage />
            </>
          }
        />
        <Route
          path={PageRoots.CourseAdminCatalog}
          element={
            <>
              <CourseAdminCatalogPage />
            </>
          }
        />
        <Route
          path={`${PageRoots.Course}/:id`}
          element={
            <>
              <UserCoursePage />
            </>
          }
        />
      </Routes>
    </Router>
  );
}

export default App;
