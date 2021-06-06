import TodoList from '../TodoList/TodoList'
import React from 'react';
import styles from './App.module.scss';
import classnames from 'classnames/bind'
import { BrowserRouter, Link } from "react-router-dom"
import { ThemesChange } from '../ThemesChange/ThemesChange';
import { connect } from "react-redux";

const cx = classnames.bind(styles)

const mapStateToProps = (state) => ({
  theme: state.theme.theme,
})

class AppComponent extends React.Component {

  render() {
    return (

      <BrowserRouter>
      <div className={cx("header")}>
        <Link to="/"><span className={cx("logo")}>Your favourite task manager</span></Link>
        <ThemesChange />
        <div className={cx("container", `container-theme-${this.props.theme}`)}>
            <TodoList />
        </div>
      </div>
      </BrowserRouter>
    )
  }
}
const App = connect(mapStateToProps)(AppComponent)

export default App;