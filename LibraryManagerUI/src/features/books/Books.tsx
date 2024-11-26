import { useEffect } from "react";

import { useAppSelector, useAppDispatch } from "../../app/hooks";
import {
  selectBookCount,
  returnBook,
  setNumberOfAvailableBooks,
} from "./booksSlice";
import styles from "./Books.module.css";
import { useLazyGetAvailableBooksQuery, useLendBookMutation } from "./booksAPI";
import { QueryStatus } from "@reduxjs/toolkit/query";

export function Books() {
  const count = useAppSelector(selectBookCount);
  const dispatch = useAppDispatch();
  const [getBooksTrigger, { data }] = useLazyGetAvailableBooksQuery();
  const [lendBookMutation, { status: lendBookResponseStatus }] =
    useLendBookMutation();

  useEffect(() => {
    if (data) {
      let totalNumberOfAvailableBooks = 0;
      data.forEach((book) => {
        totalNumberOfAvailableBooks += book.availableCount;
      });
      dispatch(setNumberOfAvailableBooks(totalNumberOfAvailableBooks));
    }
  }, [data, dispatch]);

  useEffect(() => {
    if (lendBookResponseStatus === QueryStatus.fulfilled) {
      getBooksTrigger();
    }
  }, [lendBookResponseStatus, getBooksTrigger]);

  return (
    <div>
      <h1>Library Manager v0.1</h1>
      <div className={styles.row}>
        <span>Number of available books</span>
        <span className={styles.value}>{count}</span>
      </div>
      <div className={styles.row}>
        <button
          className={styles.button}
          aria-label="Fetch books"
          onClick={() => getBooksTrigger()}
        >
          Fetch available books from server
        </button>
      </div>
      <div>Available books:</div>
      {data &&
        data.map((book, i) => (
          <div key={i} style={{ borderStyle: "dashed" }}>
            <h3>{book.title}</h3>
            <p>by {book.author}</p>
            <p>isbn: {book.isbn}</p>
            <p>Tags: {book.tags.map((tag) => `${tag} `)}</p>
            <p>Available copies: {book.availableCount}</p>
            <div className={styles.row}>
              <button
                className={styles.button}
                aria-label="Decrement value"
                onClick={() => dispatch(returnBook())}
              >
                Return this book
              </button>
              <button
                className={styles.button}
                aria-label="Increment value"
                onClick={() => lendBookMutation(book.isbn)}
              >
                Lend this book
              </button>
            </div>
          </div>
        ))}
    </div>
  );
}
