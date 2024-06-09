// components/BookList.jsx
import React, { useState, useEffect } from "react";
import { getBooks, getAuthors } from "../services/api";
import SearchBar from "./searchBar";

const BookList = () => {
  const [books, setBooks] = useState([]);
  const [authors, setAuthors] = useState([]);
  const [searchQuery, setSearchQuery] = useState("");

  useEffect(() => {
    const fetchData = async () => {
      try {
        const booksData = await getBooks();
        const authorsData = await getAuthors();
        setBooks(booksData);
        setAuthors(authorsData);
      } catch (error) {
        console.error("Veriler alınırken bir hata oluştu:", error);
      }
    };

    fetchData();
  }, []);

  const filteredBooks = books.filter(
    (book) =>
      book.title.toLowerCase().includes(searchQuery.toLowerCase()) ||
      authors
        .find((author) => author.id === book.authorId)
        ?.name.toLowerCase()
        .includes(searchQuery.toLowerCase())
  );

  return (
    <div>
      <h2>Kitap Listesi</h2>
      <SearchBar searchQuery={searchQuery} setSearchQuery={setSearchQuery} />
      <ul>
        {filteredBooks.map((book) => (
          <li key={book.id}>
            <h3>{book.title}</h3>
            <p>
              Yazar:{" "}
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
