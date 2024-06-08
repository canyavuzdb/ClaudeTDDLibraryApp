// services/api.js
import axios from 'axios';

const apiClient = axios.create({
  baseURL: '/api', // Proxy ayarlarına göre API'nize erişim adresi
});

export const getAuthors = async () => {
  try {
    const response = await apiClient.get('/authors');
    return response.data;
  } catch (error) {
    console.error('Yazarlar alınırken bir hata oluştu:', error);
    throw error;
  }
};

export const getBooks = async () => {
  try {
    const response = await apiClient.get('/books');
    return response.data;
  } catch (error) {
    console.error('Kitaplar alınırken bir hata oluştu:', error);
    throw error;
  }
};