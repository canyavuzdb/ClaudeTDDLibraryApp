// components/BookList.jsx
import React, { useState, useEffect } from 'react';
import { getBooks, getAuthors } from '../services/api';

const BookList = () => {
  const [books, setBooks] = useState([]);
  const [authors, setAuthors] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const booksData = await getBooks();
        const authorsData = await getAuthors();
        setBooks(booksData);
        setAuthors(authorsData);
      } catch (error) {
        console.error('Veriler alınırken bir hata oluştu:', error);
      }
    };

    fetchData();
  }, []);

  return (
    <div>
      <h2>Kitap Listesi</h2>
      <ul>
        {books.map((book) => (
          <li key={book.id}>
            <h3>{book.title}</h3>
            <p>
              Yazar:{' '}
              {authors.find((author) => author.id === book.authorId)?.name}
            </p>
            <p>Yayın Yılı: {book.publicationYear}</p>
            <p>Tür: {book.genre}</p>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default BookList;