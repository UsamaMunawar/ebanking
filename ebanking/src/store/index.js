import { configureStore } from '@reduxjs/toolkit';
import { ebankingApi } from './features/apiSlice';
import { setupListeners } from '@reduxjs/toolkit/dist/query';

export const store = configureStore({
  reducer: {
    [ebankingApi.reducerPath]: ebankingApi.reducer,
  },

  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(ebankingApi.middleware),
});

setupListeners(store.dispatch);
