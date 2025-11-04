<template>
  <div>
    <h2>Buscar Livros</h2>
    <input v-model="query" @keyup.enter="fetchBooks" placeholder="Digite o nome do livro..." />
    <button @click="fetchBooks">Buscar</button>

    <ul class="book-grid">
      <li v-for="book in books" :key="book.id">
        <router-link :to="`/books/${book.id}`" class="book-card">
          <img :src="book.image" alt="Capa" />
          <p>{{ book.title }}</p>
          <small>{{ book.author }}</small>
        </router-link>
      </li>
    </ul>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import Api from '../services/api'

const books = ref([])
const query = ref('')

async function fetchBooks() {
  if (!query.value.trim()) return
  const res = await Api.get(`/Books/search?q=${encodeURIComponent(query.value)}`)

  books.value = (res.data.items || []).map(b => ({
    id: b.id,
    title: b.volumeInfo.title,
    author: b.volumeInfo.authors?.[0] || 'Desconhecido',
    image: b.volumeInfo.imageLinks?.thumbnail || '/test.jpg'
  }))

}

fetchBooks()
</script>

<style scoped>
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
</style>
