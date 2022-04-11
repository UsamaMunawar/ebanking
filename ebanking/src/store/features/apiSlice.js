import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

export const ebankingApi = createApi({
  reducerPath: 'ebankingApi',
  baseQuery: fetchBaseQuery({
    baseUrl: 'https://localhost:44376/api',
    // prepareHeaders: (headers) => {
    //   const token = sessionStorage.getItem(
    //     process.env.REACT_APP_SESSION_TOKEN_KEY
    //   );
    //   if (token) {
    //     headers.set('authorization', `Bearer ${token}`);
    //   }
    //   return headers;
    // },
  }),
  endpoints: (builder) => ({
    loginUser: builder.mutation({
      query: () => ({
        url: `/Authenticate/Login`,
        method: `post`,
        body: JSON.stringify({
          username: 'string',
          accountNumber: 'string',
          pincode: 'string',
          password: 'string',
        }),
      }),
    }),
    getWorkers: builder.mutation({
      query: (args) => `workerapi?last_worker_id=${args}`,
    }),
    getWorkOrders: builder.mutation({
      query: () => `workorderapi`,
    }),
    getOrderTypes: builder.query({
      query: () => `ordertypeapi`,
    }),
    getOrderStages: builder.query({
      query: () => `orderstageapi`,
    }),
    getSavedFilters: builder.mutation({
      query: () => ({
        url: `filters`,
        method: `post`,
        body: JSON.stringify({ action_type: 'read' }),
      }),
    }),
    getTimeZones: builder.mutation({
      query: (args) => `timezones?googleId=${args}`,
    }),
    deleteSavedFilter: builder.mutation({
      query: (id) => ({
        url: `filters`,
        method: `post`,
        body: JSON.stringify({ action_type: 'delete', filter: `${id}` }),
      }),
    }),
    createNewFilter: builder.mutation({
      query: (args) => ({
        url: `filters`,
        method: `post`,
        body: JSON.stringify({ action_type: 'create', filter: { ...args } }),
      }),
    }),
    createNewEventApi: builder.mutation({
      query: (args) => ({
        url: `neweventapi`,
        method: 'post',
        body: JSON.stringify(args),
      }),
    }),
    loadMasterData: builder.mutation({
      query: () => `masterdata`,
    }),
    pushMasterData: builder.mutation({
      query: (args) => `pushmasterdata?googleId=${args}`,
    }),
    deleteEvent: builder.mutation({
      query: (args) => `deleteeventapi?dispatch_event_id=${args}`,
    }),
  }),
});

export const {
  useLoginUserMutation,
  useLoadMasterDataQuery,
  useGetEventsMutation,
  useGetWorkersMutation,
  useGetWorkOrdersMutation,
  useGetOrderTypesQuery,
  useGetOrderStagesQuery,
  useCreateNewEventApiMutation,
  useLoadMasterDataMutation,
  usePushMasterDataMutation,
  useDeleteEventMutation,
  useGetSavedFiltersMutation,
  useDeleteSavedFilterMutation,
  useCreateNewFilterMutation,
  useGetTimeZonesMutation,
} = ebankingApi;
