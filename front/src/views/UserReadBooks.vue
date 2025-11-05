<template>
  <div class="user-reviews">
    <h2>Suas reviews</h2>

    <ul v-if="readBooks.length">
      <li v-for="r in readBooks" :key="r.id" class="review-item">
        <router-link :to="`/books/${r.googleBookId}`">
          <div class="book-info">
            <img v-if="r.book?.image" :src="r.book.image" alt="Capa do livro" class="thumb" />
            <div>
              <h3>{{ r.book?.title || 'Livro não encontrado' }}</h3>
              <p class="authors">{{ r.book?.authors?.join(', ') }}</p>
            </div>
          </div>

          <div class="stars">
            <StarFilledIcon v-for="n in r.rating" :key="n" class="icon filled small" />
            <StarIcon v-for="n in 5 - r.rating" :key="'empty-' + n" class="icon small" />
          </div>

          <p class="comment">{{ r.comment }}</p>
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
import { StarFilledIcon, StarIcon } from '@radix-icons/vue'

const readBooks = ref([])
const { token } = useAuth()
const headers = { Authorization: `Bearer ${token.value}` }

onMounted(async () => {
  try {
    const res = await Api.get('/ReadBooks/user', { headers })
    readBooks.value = res.data
  } catch (error) {
    console.error('Erro ao buscar suas reviews', error)
  }
})
</script>

<style scoped>
.user-reviews {
  padding: 20px;
}

.review-item {
  margin-bottom: 25px;
  padding-bottom: 15px;
}

.review-item a {
  text-decoration: none;
  color: inherit;
  display: block;
}

.book-info {
  display: flex;
  align-items: center;
  gap: 12px;
}

.thumb {
  width: 60px;
  height: 90px;
  object-fit: cover;
  border-radius: 6px;
}

.authors {
  font-size: 13px;
  color: white;
}

.stars {
  display: flex;
  gap: 3px;
  margin: 8px 0;
}

.icon.small {
  width: 20px;
  height: 20px;
  color: #ccc;
}

.icon.filled {
  color: gold;
}

.comment {
  font-size: 14px;
  color: white;
}
</style>
