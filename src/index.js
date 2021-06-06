import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './Components/App/App';
import reportWebVitals from './reportWebVitals';
import { Provider } from 'react-redux'
import { createStore } from 'redux'
import { rootReducer } from './Reducer/index';

const store = createStore(rootReducer)

ReactDOM.render(
  <React.StrictMode>
    <Provider store={store}>
	<App />
	</Provider>
  </React.StrictMode>,
  document.getElementById('root')
);

reportWebVitals();