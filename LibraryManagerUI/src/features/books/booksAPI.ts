import { Book } from "./models/Book";
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

// Need to use the React-specific entry point to allow generating React hooks

// Define a service using a base URL and expected endpoints
export const bookApi = createApi({
  reducerPath: "bookApi",
  baseQuery: fetchBaseQuery({ baseUrl: "https://localhost:7285/" }),
  endpoints: (builder) => ({
    getAvailableBooks: builder.query<Book[], void>({
      query: () => `Books`,      
    }),
    lendBook: builder.mutation<void, string>({
      query: (params: string) => ({
        url: `Books/${params}/lend`,
        method: "POST",
      }),
    }),
  }),
});

// Export hooks for usage in function components, which are
// auto-generated based on the defined endpoints
export const { useGetAvailableBooksQuery, useLazyGetAvailableBooksQuery, useLendBookMutation } =
  bookApi;
