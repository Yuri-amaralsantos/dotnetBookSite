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



.search-btn {
  background-color: var(--color-primary);
  border: none;
  padding: 8px 12px;
  cursor: pointer;
  transition: background-color 0.2s;
  display: flex;
  align-items: center;
  justify-content: center;
}


.icon {
  width: 20px;
  height: 20px;
  color: var(--color-light-text);
}

.book-grid {
  display: grid;
  grid-template-columns: repeat(5, 1fr);
  justify-content: center;
  gap: 20px;
  list-style: none;
  padding: 0;
  margin: 0 auto;
  max-width: 1300px;
}

.book-card:hover {
  opacity: 80%;
}

.book-card img {
  width: 200px;
  height: 250px;
  object-fit: cover;
  border-radius: 4px;
  background-color: #ddd;
}

.book-title {
  color: #e0960c;
  margin-top: 8px;
  font-weight: 600;
  font-size: 14px;
  white-space: nowrap;
  overflow: hidden;
  text-decoration: none;
  text-overflow: ellipsis;
}
</style>
