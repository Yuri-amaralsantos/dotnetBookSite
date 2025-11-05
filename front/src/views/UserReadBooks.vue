<template>
  <div>
    <h2>Seus livros lidos</h2>
    <ul v-if="readBooks.length">
      <li v-for="r in readBooks" :key="r.id">
        <div class="rating">
          <span v-for="n in 5" :key="n" :class="['circle', { selected: n <= r.userRating }]" @click="setRating(r, n)">
            {{ n }}
          </span>
        </div>

        <router-link :to="`/books/${r.googleBookId}`">
          <p>{{ r.comment }}</p>
        </router-link>
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
  readBooks.value = res.data.map(book => ({
    ...book,
    userRating: book.userRating || 0,
  }))
})


</script>

<style lang="css" scoped>
.circle {
  display: inline-block;
  width: 30px;
  height: 30px;
  line-height: 30px;
  text-align: center;
  border: 1px solid #ccc;
  border-radius: 50%;
  cursor: pointer;
  margin-right: 5px;
  transition: background-color 0.2s, border-color 0.2s;
}

.circle.selected {
  background-color: gold;
  color: white;
  border-color: gold;
}

.rating {
  margin-bottom: 8px;
}
</style>
