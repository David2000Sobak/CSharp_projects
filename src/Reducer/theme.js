import { CHANGE_THEME } from "../Actions/theme";

const initialState = {
  theme: 'light'
}

export const themeReducer = (state = initialState, action) => {
  switch (action.type) {
    case CHANGE_THEME: {
      return {
        ...state,
        theme: action.payload,
      };
    }
    default:
      return state;
  }
};
