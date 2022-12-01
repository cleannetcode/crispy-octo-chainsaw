import React from 'react';
import PageWrapper from '../../components/PageWrapper';
import { useAuthService } from '../../Services/AuthService/useAuthService';
import { AdminRegistrationForm } from './AdminRegistrationForm';

export function AdminRegistraionPage() {
  const authServices = useAuthService();

  return (
    <>
      <PageWrapper>
        <div className='registration-page'>
          <AdminRegistrationForm registraion={authServices.registrAdmin} />
        </div>
      </PageWrapper>
    </>
  );
}
