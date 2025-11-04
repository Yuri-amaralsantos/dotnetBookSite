<template>
  <div>
    <h2>Seus livros lidos</h2>
    <ul v-if="readBooks.length">
      <li v-for="r in readBooks" :key="r.id">
        <router-link :to="`/books/${r.googleBookId}`">
          <strong>{{ r.googleBookId }}</strong> — Nota: {{ r.rating }}
        </router-link>
        <p>{{ r.comment }}</p>
      </li>
    </ul>
    <p v-else>Nenhuma review ainda.</p>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import Api from '../services/api'
import useAuth from '../components/useAuth'

const readBooks = ref([])
const { token } = useAuth()
const headers = { Authorization: `Bearer ${token.value}` }

onMounted(async () => {
  const res = await Api.get('/ReadBooks', { headers })
  readBooks.value = res.data
})
</script>
