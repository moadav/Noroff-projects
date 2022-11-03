import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import Loading from './components/loading/Loading';
import { initialize } from "./keycloak";
import axios from 'axios';
import keycloak from './keycloak';
import { client } from './utils/client';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<Loading message="Connecting to Keycloak..." />)

initialize()
  .then(() => {
    root.render(
      <React.StrictMode>
          <App />
      </React.StrictMode>
    );
  }).then(() => {
    client.defaults.headers.common['Authorization'] = `Bearer ${keycloak?.token}`;
    client.defaults.headers.common['Content-Type'] = "application/json";
  })
  .catch((e) => {
    root.render(
      <React.StrictMode>
        <p>Could not Connect To Keycloak</p>
      </React.StrictMode>
    )
  })

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
