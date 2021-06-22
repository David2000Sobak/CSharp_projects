import React from 'react';
import classnames from 'classnames/bind'
import styles from './ThemesChange.module.scss';
import { connect } from "react-redux";
import { handleThemeChange } from "../../Actions/theme";

const cx = classnames.bind(styles)

const mapStateToProps = (state) => ({
  theme: state.theme.theme
})

const mapDispatchToProps = (dispatch) => ({
  dispatchOnThemeChange: (theme) => dispatch(handleThemeChange(theme))
})

const ThemesChangeComponent = ({dispatchOnThemeChange, theme}) => {
  const onThemeChange = (e) => {
    dispatchOnThemeChange(e.target.value)
  }  
  return(
        <div className={cx("radios")}>
          <div>
            <input
              type="radio"
              name="theme"
              id="light"
              value="light"
              checked={theme === "light"}
              onChange={onThemeChange}
            />
            <label htmlFor="light">Light side</label>
          </div>
          <div>
            <input
              type="radio"
              name="theme"
              id="dark"
              value="dark"
              checked={theme === "dark"}
              onChange={onThemeChange}
            />
            <label htmlFor="dark">Dark side</label>
          </div>
        </div>
    )
}

export const ThemesChange = connect(mapStateToProps, mapDispatchToProps)(ThemesChangeComponent)