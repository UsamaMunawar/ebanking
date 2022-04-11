import { Switch } from 'antd';

import LoginViaUsername from './components/LoginViaUsername';
import LoginViaACNumber from './components/LoginViaACNumber';
import './login.css';
import { useState } from 'react';
const Index = () => {
  const [loginToggle, setloginToggle] = useState(false);
  return (
    <div className='login-container'>
      <div>
        <span>Login via</span>{' '}
        <Switch
          style={{ width: '130px' }}
          checkedChildren='Username'
          unCheckedChildren='Account Number'
          onChange={(val) => {
            setloginToggle(val);
          }}
          checked={loginToggle}
        />
      </div>
      <div
        style={{
          display: 'flex',
          justifyContent: 'space-evenly',
          width: '100vw',
          marginTop: '2rem',
        }}
      >
        {loginToggle ? <LoginViaACNumber /> : <LoginViaUsername />}
      </div>
    </div>
  );
};

export default Index;
