<template>
  <div class="container">
    <h2>Buscar Livros</h2>
    <div class="book-search">
      <input v-model="query" @keyup.enter="fetchBooks" placeholder="Digite o nome do livro..." />
      <button @click="fetchBooks" class="search-btn" aria-label="Buscar">
        <MagnifyingGlassIcon class="icon" />
      </button>
    </div>

    <ul class="book-grid">
      <li v-for="book in books" :key="book.id">
        <router-link :to="`/books/${book.id}`" class="book-card">
          <img :src="book.image" alt="Capa" />

          <!-- Mostra o título cortado, mas o completo no hover -->
          <p class="book-title" :title="book.title">
            {{ truncateTitle(book.title) }}
          </p>

          <small>{{ book.author }}</small>
        </router-link>
      </li>
    </ul>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import Api from '../services/api'
import { MagnifyingGlassIcon } from '@radix-icons/vue'

const books = ref([])
const query = ref('')

function truncateTitle(title) {
  return title.length > 60 ? title.slice(0, 60) + '…' : title
}

async function fetchBooks() {
  if (!query.value.trim()) return
  const res = await Api.get(`/Books/search?q=${encodeURIComponent(query.value)}`)

  books.value = (res.data.items || []).map(b => ({
    id: b.id,
    title: b.volumeInfo.title,
    author: b.volumeInfo.authors?.[0] || 'Desconhecido',
    image: b.volumeInfo.imageLinks?.thumbnail || '/test.jpg',
  }))
}

fetchBooks()
</script>

<style scoped>
.container {
  padding: 0 40px;
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.book-search {
  display: flex;
  align-items: center;
  gap: 10px;
}

.book-search input {
  flex: 1;
  padding: 10px;
  border-radius: 8px;
  border: none;
  background-color: #2a2a2a;
  color: white;
  outline: none;
}

.search-btn {
  background-color: gold;
  border: none;
  border-radius: 8px;
  padding: 8px 12px;
  cursor: pointer;
  transition: background-color 0.2s;
  display: flex;
  align-items: center;
  justify-content: center;
}

.search-btn:hover {
  background-color: #e0b700;
}

.icon {
  width: 20px;
  height: 20px;
  color: black;
}

.book-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(180px, 1fr));
  gap: 20px;
  list-style: none;
}

.book-card {
  background-color: #1e1e1e;
  padding: 10px;
  border-radius: 10px;
  text-align: center;
  color: #fff;
  display: block;
}

.book-card img {
  width: 100%;
  border-radius: 4px;
}


.book-title {
  margin-top: 8px;
  font-weight: 600;
  font-size: 14px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
</style>
